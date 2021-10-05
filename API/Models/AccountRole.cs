using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_account_roles")]
    public class AccountRole
    {
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }

        [ForeignKey("Role")]
        public string RoleId { get; set; }
        //[JsonIgnore]
        public virtual Role Role { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}