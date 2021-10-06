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

        [Required(ErrorMessage = "Name tidak boleh kosong")]
        [StringLength(64, MinimumLength = 3, ErrorMessage ="Nama harus mengandung 3-64 karakter")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Bukan sebuah karakter nama")]
        public string Name { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Password tidak boleh kosong")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username tidak boleh kosong")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Username harus mengandung 3-64 karakter")]
        [RegularExpression("^[a-zA-Z0-9.\\-_$@*!]{3,30}$", ErrorMessage = "Username tidak dapat digunakan")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email tidak boleh kosong")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Email harus mengandung 3-64 karakter")]
        [EmailAddress(ErrorMessage = "Bukan sebuah email")]
        public string Email { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
        //[JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
