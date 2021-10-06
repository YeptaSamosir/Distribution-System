using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_candidates")]
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }

        [Required(ErrorMessage = "Nama tidak boleh kosong")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Nama harus mengandung 3-64 karakter")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Grade tidak boleh kosong")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "Grade harus mengandung minimal 1 karakter")]
        public string Grade { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<ScheduleInterview> ScheduleInterviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<Onboard> Onboards { get; set; }
    }
}