using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_statuses")]
    public class Status
    {
        [Key]
        public string StatusId { get; set; }
        public string Name { get; set; }

        [ForeignKey("TypeStatus")]
        public string TypeStatusId { get; set; }
        public virtual TypeStatus TypeStatus { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Onboard> Onboards { get; set; }
        public virtual ICollection<ScheduleInterview> ScheduleInterviews { get; set; }

    }
}