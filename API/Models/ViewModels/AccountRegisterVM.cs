using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class AccountRegisterVM
    {
        public int AccountId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string RoleId { get; set; }
    }
}
