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
    public class StatusRepository : GenericRepository<Status, string>
    {
        private readonly MyConfiguration myConfiguration;
        private readonly Status status;
        private readonly string request;
        private readonly HttpClient httpClient;
        public StatusRepository(IOptions<MyConfiguration> myConfiguration, string request = "status/") : base(request, myConfiguration)
        {
            this.request = request;
        }
    }
}