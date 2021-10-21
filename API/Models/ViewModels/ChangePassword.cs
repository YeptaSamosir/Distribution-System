using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class ChangePassword
    {
        [Required]
        public string CurrentPassword { get; set; }
     
        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        [RegularExpression("^(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=\\D*\\d)[^:&.~\\s]{5,20}$", ErrorMessage = "The new password fIeld must contain numbers, uppercase and lowercase")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password doesn't match")]
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The email field must contain 3-64 characters")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
