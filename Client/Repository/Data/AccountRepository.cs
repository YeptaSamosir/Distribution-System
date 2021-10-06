using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Config;
using API.Models;
using Microsoft.Extensions.Options;
using API.Models.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repository.Data
{
    public class AccountRepository : GenericRepository<Account, int>
    {
        private readonly MyConfiguration myConfiguration;
        private readonly Account account;
        private readonly string request;
        private readonly HttpClient httpClient;

        public AccountRepository(IOptions<MyConfiguration> myConfiguration, string request = "account/") : base(request, myConfiguration)
        {
            this.request = request;
            this.myConfiguration = myConfiguration.Value;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(this.myConfiguration.BaseUrlApis)
            };
        }

        public string InsertRegister(AccountRegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(request + "register", content).Result.Content.ReadAsStringAsync().Result;
            return response;
        }

        internal string UpdateAccount(AccountUpdateWithRole accountUpdateWithRole)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(accountUpdateWithRole), Encoding.UTF8, "application/json");
            var response = httpClient.PutAsync(request + "register/update", content).Result.Content.ReadAsStringAsync().Result;
            return response;
        }

        internal string ChangePassword(ChangePassword changePassword)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(changePassword), Encoding.UTF8, "application/json");
            var response = httpClient.PutAsync(request + "changepassword", content).Result.Content.ReadAsStringAsync().Result;
            return response;
        }
    }
}