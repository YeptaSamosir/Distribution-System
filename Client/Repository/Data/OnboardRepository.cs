using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Config;
using API.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repository.Data
{
    public class OnboardRepository : GenericRepository<Onboard, int>
    {
        private readonly MyConfiguration myConfiguration;
        private readonly Onboard onboard;
        private readonly string request;
        private readonly HttpClient httpClient;
        public OnboardRepository(IOptions<MyConfiguration> myConfiguration, string request = "onboard/") : base(request, myConfiguration)
        {
            this.request = request;
            this.myConfiguration = myConfiguration.Value;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(this.myConfiguration.BaseUrlApis)
            };
        }

        internal string CreateOnBoard(Onboard onboard)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(onboard), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(request + "create", content).Result.Content.ReadAsStringAsync().Result;
            return response;
        }
    }
}