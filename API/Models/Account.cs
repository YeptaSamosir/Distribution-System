using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_Account")]
    public class Account
    {
        [Key]
        public int Account_id { get; set; }
        public string Account_Name { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
