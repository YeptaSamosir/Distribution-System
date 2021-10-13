using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class DashboardVM
    {
        public int TotalCandidate { get; set; }
        public int TotalCompany { get; set; }
        public int TotalCandidateIdle { get; set; }
        public int TotalCandidateOnboard { get; set; }
        public int TotalInterviewOngoing { get; set; }
        public int TotalInterviewSuccess { get; set; }
        public int TotalInterviewCanceled { get; set; }
        public int TotalOnboard { get; set; }
    }

    public class FullCalender
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ScheduleInterview Data { get; set; }
    }
}
