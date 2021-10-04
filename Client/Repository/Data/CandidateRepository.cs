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
    public class CandidateRepository : GenericRepository<Candidate, int>
    {
        private readonly MyConfiguration myConfiguration;
        private readonly Candidate candidate;
        private readonly string request;
        private readonly HttpClient httpClient;

        public CandidateRepository(IOptions<MyConfiguration> myConfiguration, string request = "candidate/") : base(request, myConfiguration)
        {    
            this.request = request;
        }
    }
}