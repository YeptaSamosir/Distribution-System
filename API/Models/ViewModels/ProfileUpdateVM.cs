using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class ProfileUpdateVM
    {
        public int AccountId { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The name field must contain 3-44 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Not a character name")]
        public string Name { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The username field must contain 3-64 characters")]
        [RegularExpression("^[a-zA-Z0-9.\\-_$@*!]{3,30}$", ErrorMessage = "The username field cannot be used")]
        public string Username { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The email field must contain 3-64 characters")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
