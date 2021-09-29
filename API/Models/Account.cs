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
        [Key]
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
