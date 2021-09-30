using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Client.Base.Urls;

namespace Client.Repository.Data
{
    public class CandidateRepository : GenericRepository<Candidate, int>
    {
        private readonly Address address;
        private readonly Candidate candidate;
        private readonly string request;
        private readonly HttpClient httpClient;

        public CandidateRepository(Address address, string request = "candidate/") : base(address, request)
        {
            this.address = address;
            this.request = request;
        }
    }
}