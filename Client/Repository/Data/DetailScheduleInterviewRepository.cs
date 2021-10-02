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
    public class DetailScheduleInterviewRepository : GenericRepository<DetailScheduleInterview, int>
    {
        private readonly MyConfiguration myConfiguration;
        private readonly DetailScheduleInterview detailScheduleInterview;
        private readonly string request;
        private readonly HttpClient httpClient;
        public DetailScheduleInterviewRepository(IOptions<MyConfiguration> myConfiguration, string request = "detailScheduleInterview/") : base(request, myConfiguration)
        {
            this.request = request;
        }
    }
}