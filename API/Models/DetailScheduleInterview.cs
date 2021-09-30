using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_detail_schedule_interviews")]
    public class DetailScheduleInterview
    {
        [Key]
        [StringLength(16)]
        public int DetailScheduleInterviewId { get; set; }

        [ForeignKey("ScheduleInterview")]
        public string ScheduleInterviewId { get; set; }
        [JsonIgnore]
        public virtual ScheduleInterview ScheduleInterview { get; set; }
        [StringLength(64)]
        public string EmailCandidate { get; set; }
        [StringLength(64)]
        public string EmailCustomer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public enum typeLocation { Online, Offline }
        public typeLocation TypeLocation { get; set; }
    }
}