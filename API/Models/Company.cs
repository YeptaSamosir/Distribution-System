using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_companies")]
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The Name Company field must contain 3-64 characters")]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<ScheduleInterview> ScheduleInterviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<Onboard> Onboards { get; set; }
    }
}