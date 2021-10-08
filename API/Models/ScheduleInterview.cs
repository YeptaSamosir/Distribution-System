using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_schedule_interviews")]
    public class ScheduleInterview
    {
        [Key]
        [Required(ErrorMessage = "ID tidak boleh kosong")]
        [StringLength(16, MinimumLength = 1, ErrorMessage = "Role ID harus mengandung 3-64 karakter")]
        public string ScheduleInterviewId { get; set; }

        [ForeignKey("Candidate")]
        [Required(ErrorMessage = "Candidate Id tidak boleh kosong")]
        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        [ForeignKey("Company")]
        [Required(ErrorMessage = "Company ID tidak boleh kosong")]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        [Required(ErrorMessage = "Customer Name tidak boleh kosong")]
        [StringLength(64)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Job Title tidak boleh kosong")]
        [StringLength(64)]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Location tidak boleh kosong")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Start Date tidak boleh kosong")]
        public DateTime StartInterview { get; set; }

        [Required(ErrorMessage = "End Date tidak boleh kosong")]
        public DateTime EndInterview { get; set; }


        [ForeignKey("Status")]
        [Required(ErrorMessage = "Status ID tidak boleh kosong")]
        public string StatusId { get; set; }
        [JsonIgnore]
        public virtual Status Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<DetailScheduleInterview> DetailScheduleInterviews { get; set; }

    }
}