using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_roles")]
    public class Role
    {
        [Key]
        [Required(ErrorMessage = "Role ID tidak boleh kosong")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Role ID harus mengandung 3-64 karakter")]
        public string RoleId { get; set; }

        [Required(ErrorMessage = "Nama role tidak boleh kosong")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Role harus mengandung 3-64 karakter")]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}