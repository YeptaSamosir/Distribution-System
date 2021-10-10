using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class SceduleInterviewVM
    {
        [Required]
        public int CandidateId { get; set; }
        [Required]
        public string CandidateEmail { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public string ScheduleFollowBy { get; set; }
        [Required]
        public DateTime DateTimeOne { get; set; }
        [Required]
        public DateTime DateTimeTwo { get; set; }
        [Required]
        public DateTime DateTimeThree { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
