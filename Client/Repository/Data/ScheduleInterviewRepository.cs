using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Client.Base.Urls;

namespace Client.Repository.Data
{
    public class ScheduleInterviewRepository : GenericRepository<ScheduleInterview, string>
    {
        private readonly Address address;
        private readonly ScheduleInterview scheduleInterview;
        private readonly string request;
        private readonly HttpClient httpClient;
        public ScheduleInterviewRepository(Address address, string request = "ScheduleInterview/") : base(address, request)
        {
            this.address = address;
            this.request = request;
        }
    }
}