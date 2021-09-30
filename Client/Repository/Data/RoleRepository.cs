using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Client.Base.Urls;

namespace Client.Repository.Data
{
    public class RoleRepository : GenericRepository<Role, string>
    {
        private readonly Address address;
        private readonly Role role;
        private readonly string request;
        private readonly HttpClient httpClient;
        public RoleRepository(Address address, string request = "role/") : base(address, request)
        {
            this.address = address;
            this.request = request;
        }
    }
}