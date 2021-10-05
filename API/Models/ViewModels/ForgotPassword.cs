using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Email tidak boleh kosong")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Email harus mengandung 3-64 karakter")]
        [EmailAddress(ErrorMessage = "Bukan sebuah email")]
        public string Email { get; set; }
    }
}
