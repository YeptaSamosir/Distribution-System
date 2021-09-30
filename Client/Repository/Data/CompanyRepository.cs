using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Client.Base.Urls;

namespace Client.Repository.Data
{
    public class CompanyRepository : GenericRepository<Company, int>
    {
        private readonly Address address;
        private readonly Company company;
        private readonly string request;
        private readonly HttpClient httpClient;
        public CompanyRepository(Address address, string request = "company/") : base(address, request)
        {
            this.address = address;
            this.request = request;
        }
    }
}