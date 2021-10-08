using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_accounts")]
    public class Account
    {
        public Account()
        {
        }

        public Account(string name, string password, string username, string email, bool isActive, DateTime createdAt, DateTime updatedAt)
        {

            Name = name;
            Password = password;
            Username = username;
            Email = email;
            IsActive = isActive;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Key]
        public int AccountId { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The name field must contain 3-64 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "The name field not a character name")]
        public string Name { get; set; }

        [JsonIgnore]
        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The username field must contain 3-64 characters")]
        [RegularExpression("^[a-zA-Z0-9.\\-_$@*!]{3,30}$", ErrorMessage = "The username field cannot be used")]
        public string Username { get; set; }

        [Required]
        [StringLength(64)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [JsonIgnore]
        public int AttemptCount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
        //[JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
