using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Client.Base.Urls;

namespace Client.Repository.Data
{
    public class TypeStatusRepository : GenericRepository<TypeStatus, string>
    {
        private readonly Address address;
        private readonly TypeStatus typeStatus;
        private readonly string request;
        private readonly HttpClient httpClient;
        public TypeStatusRepository(Address address, string request) : base(address, request)
        {
            this.address = address;
            this.request = request;
        }
    }
}