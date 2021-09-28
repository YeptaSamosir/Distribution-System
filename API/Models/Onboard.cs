using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_onboards")]
    public class Onboard
    {
        [Key]
        public int OnboardId { get; set; }

        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}