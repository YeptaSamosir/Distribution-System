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
}
