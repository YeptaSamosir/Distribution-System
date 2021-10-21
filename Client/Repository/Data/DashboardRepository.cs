using API.Models.ViewModels;
using Client.Config;
using Client.Repository.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly MyConfiguration myConfiguration;
        private readonly DashboardVM dashboardVM;
        private readonly string request;
        private readonly HttpClient httpClient;

        public DashboardRepository(IOptions<MyConfiguration> myConfiguration, string request = "dashboard/")
        {
            this.request = request;
            this.myConfiguration = myConfiguration.Value;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(this.myConfiguration.BaseUrlApis)
            };
        }

        public async Task<DashboardVM> GetDataCount()
        {
            DashboardVM entity = null;

            using (var response = await httpClient.GetAsync(request + "data-count"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<DashboardVM>(apiResponse);
            }
            return entity;
        }

        internal async Task<List<FullCalender>> GetDataForCalender()
        {
            List<FullCalender> entities = new List<FullCalender>();

            using (var response = await httpClient.GetAsync(request + "data-for-calender"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<FullCalender>>(apiResponse);
            }
            return entities;
        }
    }
}
