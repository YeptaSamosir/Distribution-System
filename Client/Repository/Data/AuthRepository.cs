
using API.Models;
using API.Models.ViewModels;
using Client.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class AuthRepository : GenericRepository<Account, int>
    {
        private readonly MyConfiguration myConfiguration;
        private readonly Account account;
        private readonly string request;
        private readonly HttpClient httpClient;
        public AuthRepository(IOptions<MyConfiguration> myConfiguration, string request = "account/") : base(request, myConfiguration)
        {
            this.request = request;
            this.myConfiguration = myConfiguration.Value;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(this.myConfiguration.BaseUrlApis)
            };
        }

        public async Task<JWTokenVM> Auth(LoginVM loginVM)
        {
            JWTokenVM jwtTokenVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(request + "login", content);

            string apiResponse = response.Content.ReadAsStringAsync().Result;
            jwtTokenVM = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);
            return jwtTokenVM;

        }

        public string SendResetPassword(ForgotPassword forgotPassword)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(forgotPassword), Encoding.UTF8, "application/json");
      
            return httpClient.PostAsync(request + "forgotpassword", content).Result.Content.ReadAsStringAsync().Result;
        }

        internal string ResetPasswordAccount(ResetPasswordVM resetPasswordVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(resetPasswordVM), Encoding.UTF8, "application/json");

            return httpClient.PostAsync(request + "resetpasswordaccount", content).Result.Content.ReadAsStringAsync().Result;
        }
    } 
}
