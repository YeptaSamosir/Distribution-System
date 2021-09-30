using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Client.Base.Urls;

namespace Client.Repository.Data
{
    public class DetailScheduleInterviewRepository : GenericRepository<DetailScheduleInterview, int>
    {
        private readonly Address address;
        private readonly DetailScheduleInterview detailScheduleInterview;
        private readonly string request;
        private readonly HttpClient httpClient;
        public DetailScheduleInterviewRepository(Address address, string request = "detailScheduleInterview/") : base(address, request)
        {
            this.address = address;
            this.request = request;
        }
    }
}