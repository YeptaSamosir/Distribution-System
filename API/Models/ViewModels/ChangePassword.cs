using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Password sekarang tidak boleh kosong")]
        public string CurrentPassword { get; set; }
     
        [Required(ErrorMessage = "Password baru tidak boleh kosong")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Password minimal 5 karakter")]
        [RegularExpression("^(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=\\D*\\d)[^:&.~\\s]{5,20}$", ErrorMessage = "Harus mengandung angka, huruf besar dan kecil")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password tidak cocok.")]
        [Required(ErrorMessage = "Password konfirmasi tidak boleh kosong")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email tidak boleh kosong")]
        [EmailAddress(ErrorMessage = "Bukan sebuah email")]
        public string Email { get; set; }
    }
}
