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
        [StringLength(64)]
        public string Name { get; set; }
        public string Password { get; set; }
        [StringLength(64)]
        public string Username { get; set; }
        [StringLength(64)]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
