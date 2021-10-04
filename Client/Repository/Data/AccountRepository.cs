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
    public class AccountRepository : GenericRepository<Account, int>
    {
        private readonly MyConfiguration myConfiguration;
        private readonly Account account;
        private readonly string request;
        private readonly HttpClient httpClient;

        public AccountRepository(IOptions<MyConfiguration> myConfiguration, string request = "account/") : base(request, myConfiguration)
        {
            this.request = request;
        }
    }
}