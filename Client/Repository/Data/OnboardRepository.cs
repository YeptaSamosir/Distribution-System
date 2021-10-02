using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Config;
using API.Models;
using Microsoft.Extensions.Options;

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
        }
    }
}