using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Client.Base.Urls;

namespace Client.Repository.Data
{
    public class StatusRepository : GenericRepository<Status, string>
    {
        private readonly Address address;
        private readonly Status status;
        private readonly string request;
        private readonly HttpClient httpClient;
        public StatusRepository(Address address, string request = "status/") : base(address, request)
        {
            this.address = address;
            this.request = request;
        }
    }
}