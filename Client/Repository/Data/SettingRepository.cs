using System.Net.Http;
using System.Threading.Tasks;
using Client.Config;
using API.Models;
using Microsoft.Extensions.Options;
using API.Models.ViewModels;
using Newtonsoft.Json;
using System.Text;
using System;

namespace Client.Repository.Data
{
    public class SettingRepository : GenericRepository<Account, int>
    {
        private readonly MyConfiguration myConfiguration;
        private readonly Account account;
        private readonly string request;
        private readonly HttpClient httpClient;

        public SettingRepository(IOptions<MyConfiguration> myConfiguration, string request = "account") : base(request, myConfiguration)
        {
            this.request = request;
            this.myConfiguration = myConfiguration.Value;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(this.myConfiguration.BaseUrlApis)
            };
        }
    }
}
