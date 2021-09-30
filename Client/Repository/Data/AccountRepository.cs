using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Client.Base.Urls;

namespace Client.Repository.Data
{
    public class AccountRepository : GenericRepository<Account, int>
    {
        private readonly Address address;
        private readonly Account account;
        private readonly string request;
        private readonly HttpClient httpClient;

        public AccountRepository(Address address, string request = "account/") : base(address, request)
        {
            this.address = address;
            this.request = request;
        }
    }
}