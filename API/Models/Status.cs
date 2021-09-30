using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_statuses")]
    public class Status
    {
        [Key]
        [StringLength(16)]
        public string StatusId { get; set; }
        [StringLength(64)]
        public string Name { get; set; }

        [ForeignKey("TypeStatus")]
        public string TypeStatusId { get; set; }
        [JsonIgnore]
        public virtual TypeStatus TypeStatus { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<Onboard> Onboards { get; set; }
        [JsonIgnore]
        public virtual ICollection<ScheduleInterview> ScheduleInterviews { get; set; }

    }
}