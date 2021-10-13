using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_onboards")]
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

        [Required]
        public string JobTitle { get; set; }

        [ForeignKey("Status")]
        public string StatusId { get; set; }
        public virtual Status Status { get; set; }

        [Required]
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}