using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
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

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
    }
}