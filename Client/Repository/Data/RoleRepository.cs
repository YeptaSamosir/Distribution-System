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
    public class RoleRepository : GenericRepository<Role, string>
    {
        private readonly MyConfiguration myConfiguration;
        private readonly Role role;
        private readonly string request;
        private readonly HttpClient httpClient;
        public RoleRepository(IOptions<MyConfiguration> myConfiguration, string request = "role/") : base(request, myConfiguration)
        {
            this.request = request;
        }
    }
}