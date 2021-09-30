using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_schedule_interviews")]
    public class ScheduleInterview
    {
        [Key]
        [StringLength(16)]
        public string ScheduleInterviewId { get; set; }

        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [StringLength(64)]
        public string CustomerName { get; set; }
        [StringLength(64)]
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public DateTime StartInterview { get; set; }
        public DateTime EndInterview { get; set; }


        [ForeignKey("Status")]
        public string StatusId { get; set; }
        [JsonIgnore]
        public virtual Status Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<DetailScheduleInterview> DetailScheduleInterviews { get; set; }

    }
}