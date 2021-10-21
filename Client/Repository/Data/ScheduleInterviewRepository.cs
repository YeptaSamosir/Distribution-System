using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Config;
using API.Models;
using Microsoft.Extensions.Options;
using API.Models.ViewModels;
using System.Text;
using Newtonsoft.Json;

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
            this.myConfiguration = myConfiguration.Value;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(this.myConfiguration.BaseUrlApis)
            };
        }

        public string CreateInterview(CreateInterviewVM sceduleInterviewVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(sceduleInterviewVM), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(request + "create", content).Result.Content.ReadAsStringAsync().Result;
            return response;
        }

        public string CreateDateOption(CreateDateOptionsVM createDateOptionsVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(createDateOptionsVM), Encoding.UTF8, "application/json");
            return httpClient.PostAsync(request + "create-date-option", content).Result.Content.ReadAsStringAsync().Result;
        }

        internal string ResponseConfirmationDate(InterviewResponseVM interviewResponseVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(interviewResponseVM), Encoding.UTF8, "application/json");
            var response = httpClient.PutAsync(request + "response-confirmation-date", content).Result.Content.ReadAsStringAsync().Result;
            return response;
        }

        internal string ConfirmationAcceptedCandidate(InterviewResponseVM interviewResponseVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(interviewResponseVM), Encoding.UTF8, "application/json");
            var response = httpClient.PutAsync(request + "confirmation-accepted-candidate", content).Result.Content.ReadAsStringAsync().Result;
            return response;
        }

        internal string Feedback(FeedbackVM feedbackVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(feedbackVM), Encoding.UTF8, "application/json");
            return httpClient.PostAsync(request + "feedback-customer", content).Result.Content.ReadAsStringAsync().Result;
        }
    }
}