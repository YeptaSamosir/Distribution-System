using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_schedule_interviews")]
    public class ScheduleInterview
    {
        public ScheduleInterview(string scheduleInterviewId, DateTime startInterview, int candidateId, int companyId, string customerName, string jobTitle, string location,string followingBy, string statusId, DateTime createdAt, DateTime updatedAt)
        {
            ScheduleInterviewId = scheduleInterviewId;
            StartInterview = startInterview;
            CandidateId = candidateId;
            CompanyId = companyId;
            CustomerName = customerName;
            JobTitle = jobTitle;
            Location = location;
            FollowingBy = followingBy;
            StatusId = statusId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Key]
        [Required]
        [StringLength(16, MinimumLength = 1)]
        public string ScheduleInterviewId { get; set; }

        [ForeignKey("Candidate")]
        [Required]
        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        [ForeignKey("Company")]
        [Required]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        [Required]
        [StringLength(64)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(64)]
        public string JobTitle { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime StartInterview { get; set; }

        [Required]
        public DateTime EndInterview { get; set; }

        public string FollowingBy { get; set; }

        public string FeedbackMessage { get; set; }

        [ForeignKey("Status")]
        [Required]
        public string StatusId { get; set; }
       /* [JsonIgnore]*/
        public virtual Status Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        /*[JsonIgnore]*/
        public virtual ICollection<DetailScheduleInterview> DetailScheduleInterviews { get; set; }

        /*[JsonIgnore]*/
        public virtual ICollection<ScheduleInterviewDateOption> ScheduleInterviewDateOptions { get; set; }
        
    }
}