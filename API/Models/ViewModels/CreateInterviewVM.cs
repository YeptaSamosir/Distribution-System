using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class CreateInterviewVM
    {
        [Range(1, int.MaxValue, ErrorMessage = "The Candidate field is required.")]
        public int CandidateId { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The candidate email field must contain 3-64 characters")]
        [EmailAddress]
        public string CandidateEmail { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The customer name field must contain 3-44 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Not a character name")]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The customer email field must contain 3-64 characters")]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The company name field must contain 3-44 characters")]
        public string CompanyName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The job title field must contain 3-44 characters")]
        public string JobTitle { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Type Location field is required.")]
        public int Type { get; set; }

        [Required]
        public string ScheduleFollowBy { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
