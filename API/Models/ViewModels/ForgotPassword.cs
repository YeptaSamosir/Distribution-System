using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class ForgotPassword
    {
        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The email field must contain 3-64 characters")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
