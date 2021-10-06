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

        [Required(ErrorMessage = "Nama Perusahaan tidak boleh kosong")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "Nama harus mengandung 1-64 karakter")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<ScheduleInterview> ScheduleInterviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<Onboard> Onboards { get; set; }
    }
}