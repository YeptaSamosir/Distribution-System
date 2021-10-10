using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class InterviewResponseVM
    {
        public string ScheduleInterviewId { get; set; }
        public int ScheduleDateConfirmId { get; set; }
        public string CandidateAccepted { get; set; }
        public string EmailCustomer { get; set; }
    }
}
