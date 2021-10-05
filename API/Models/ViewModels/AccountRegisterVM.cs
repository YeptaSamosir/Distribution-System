﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class AccountRegisterVM
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Name tidak boleh kosong")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Nama harus mengandung 3-64 karakter")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Bukan sebuah karakter nama")]
        public string Name { get; set; }

        /*[Required(ErrorMessage = "Password tidak boleh kosong")]
        [MinLength(5, ErrorMessage = "Password minimal 5 karakter")]
        [RegularExpression("^(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=\\D*\\d)[^:&.~\\s]{5,20}$", ErrorMessage = "Harus mengandung angka, huruf besar dan kecil")]*/
        public string Password { get; set; }

        [Required(ErrorMessage = "Username tidak boleh kosong")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Username harus mengandung 3-64 karakter")]
        [RegularExpression("^[a-zA-Z0-9.\\-_$@*!]{3,30}$", ErrorMessage = "Username tidak dapat digunakan")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email tidak boleh kosong")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Email harus mengandung 3-64 karakter")]
        [EmailAddress(ErrorMessage = "Bukan sebuah email")]
        public string Email { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage ="Role tidak boleh kosong")]
        public string[] Roles { get; set; }
    }

}
