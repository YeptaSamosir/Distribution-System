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
    public class ScheduleInterviewRepository : GenericRepository<ScheduleInterview, string>
    {
        private readonly MyConfiguration myConfiguration;
        private readonly ScheduleInterview scheduleInterview;
        private readonly string request;
        private readonly HttpClient httpClient;
        public ScheduleInterviewRepository(IOptions<MyConfiguration> myConfiguration, string request = "ScheduleInterview/") : base( request, myConfiguration )
        {
            this.request = request;
        }
    }
}