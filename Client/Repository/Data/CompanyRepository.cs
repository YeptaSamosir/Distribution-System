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
    public class CompanyRepository : GenericRepository<Company, int>
    {
        private readonly MyConfiguration myConfiguration;
        private readonly Company company;
        private readonly string request;
        private readonly HttpClient httpClient;
        public CompanyRepository(IOptions<MyConfiguration> myConfiguration, string request = "company/") : base(request, myConfiguration)
        {
            this.request = request;
        }
    }
}