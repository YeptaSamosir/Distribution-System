using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_candidates")]
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<ScheduleInterview> ScheduleInterviews { get; set; }
        public virtual ICollection<Onboard> Onboards { get; set; }
    }
}