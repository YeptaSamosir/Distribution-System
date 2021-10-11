using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class CreateDateOptionsVM
    {
        [Required]
        public string ScheduleInterviewId { get; set; }

        [Required]
        public DateTime? DateTimeOne { get; set; }
        [Required]
        public DateTime? DateTimeTwo { get; set; }
        [Required]
        public DateTime? DateTimeThree { get; set; }
        [Required]
        public string ScheduleFollowBy { get;  set; }
    }
}
