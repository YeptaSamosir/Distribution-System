using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The name field must contain 3-64 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "The name field not a character name")]
        public string Name { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "The grade field must contain 1-3 characters")]
        public string Grade { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The email field must contain 3-64 characters")]
        [EmailAddress]
        public string Email { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public virtual ICollection<ScheduleInterview> ScheduleInterviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<Onboard> Onboards { get; set; }
    }

    public enum status
    {
        //[EnumMember(Value = "Idle")]
        Idle,
        //[EnumMember(Value = "Waiting")]
        Waiting,
        //[EnumMember(Value = "Onboard")]
        Onboard
    }
}