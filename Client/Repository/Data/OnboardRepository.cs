using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Client.Base.Urls;

namespace Client.Repository.Data
{
    public class OnboardRepository : GenericRepository<Onboard, int>
    {
        private readonly Address address;
        private readonly Onboard onboard;
        private readonly string request;
        private readonly HttpClient httpClient;
        public OnboardRepository(Address address, string request = "onboard/") : base(address, request)
        {
            this.address = address;
            this.request = request;
        }
    }
}