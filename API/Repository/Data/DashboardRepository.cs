using API.Config;
using API.Context;
using API.Models.ViewModels;
using API.Repository.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly MyContext myContext;
        private readonly MyConfiguration myConfiguration;
        public DashboardRepository(MyContext myContext, IOptions<MyConfiguration> myConfiguration)
        {
            this.myContext = myContext;
            this.myConfiguration = myConfiguration.Value;
        }

        public DashboardVM GetDataCount()
        {
            try
            {
                DashboardVM dashboardVM = new DashboardVM();
                //total candidate 
                int getCountCandidate = myContext.Candidates.Count();
                dashboardVM.TotalCandidate = getCountCandidate;

                //total company
                int getCountCompany = myContext.Companies.Count();
                dashboardVM.TotalCompany = getCountCompany;

                //total CandidateIdle
                int getCountCandidateIdle = myContext.Candidates.Where(x => x.Status == Models.status.Idle).Count();
                dashboardVM.TotalCandidateIdle = getCountCandidateIdle;

                //total CandidateOnboard
                int getCountCandidateOnboard = myContext.Candidates.Where(x => x.Status == Models.status.Onboard).Count();
                dashboardVM.TotalCandidateOnboard = getCountCandidateOnboard;


                //Total Interview Ongoing
                int getCountInterviewOngoing = myContext.ScheduleInterviews.Where(x => x.StatusId == "ITV-OG").Count();
                dashboardVM.TotalInterviewOngoing = getCountInterviewOngoing;

                //Total Interview Success
                int getCountInterviewSuccess = myContext.ScheduleInterviews.Where(x => x.StatusId == "ITV-AC" || x.StatusId == "ITV-DN").Count();
                dashboardVM.TotalInterviewSuccess = getCountInterviewSuccess;
            
                //Total Interview Cancel
                int getCountInterviewCancel = myContext.ScheduleInterviews.Where(x => x.StatusId == "ITV-CN").Count();
                dashboardVM.TotalInterviewCanceled = getCountInterviewCancel;

                //Total Data Onboard
                int getCountOnboard = myContext.Onboards.Count();
                dashboardVM.TotalOnboard = getCountOnboard;
                return dashboardVM;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal IEnumerable<FullCalender> FullCalender()
        {
            var scheduleInterviews = myContext.ScheduleInterviews.Where(x => x.StatusId == "ITV-OG").ToList();
            
            List<FullCalender> fullCalenders = new List<FullCalender>();

            for (int i = 0; i < scheduleInterviews.Count(); i++)
            {
                fullCalenders.Add(new FullCalender() {
                    Title = $"{scheduleInterviews[i].Company.Name} - {scheduleInterviews[i].JobTitle} - {scheduleInterviews[i].Candidate.Name}",
                    Start = scheduleInterviews[i].StartInterview,
                    End = scheduleInterviews[i].StartInterview.AddHours(2),
                    Data = scheduleInterviews[i]
                });
            }

            return fullCalenders;
        }
    }
}
