using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Client.Base.Urls;
using Client.Repository.Interface;
using Newtonsoft.Json;

namespace Client.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
    where TEntity : class
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;

        public GenericRepository(Address address, string request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<TEntity>> GetAll()
        {
            List<TEntity> entities = new List<TEntity>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TEntity>>(apiResponse);
            }
            return entities;
        }

        public async Task<TEntity> Get(TKey key)
        {
            TEntity entity = null;

            using (var response = await httpClient.GetAsync(request + key))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
            return entity;
        }

        public string Post(TEntity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            return httpClient.PostAsync(request, content).Result.Content.ReadAsStringAsync().Result;
        }

        public string Put(TKey key, TEntity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            return httpClient.PutAsync(request, content).Result.Content.ReadAsStringAsync().Result;
        }

        public HttpStatusCode Delete(TKey key)
        {
            var result = httpClient.DeleteAsync(request + key).Result;
            return result.StatusCode;
        }
    }
}