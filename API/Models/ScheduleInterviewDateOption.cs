using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_schedule_interview_date_options")]
    public class ScheduleInterviewDateOption
    {
        public ScheduleInterviewDateOption(string scheduleInterviewId, DateTime dateInterview, DateTime createdAt, DateTime updatedAt)
        {
            ScheduleInterviewId = scheduleInterviewId;
            DateInterview = dateInterview;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("ScheduleInterview")]
        public string ScheduleInterviewId { get; set; }

        [JsonIgnore]
        public virtual ScheduleInterview ScheduleInterview { get; set; }

        public DateTime DateInterview { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
