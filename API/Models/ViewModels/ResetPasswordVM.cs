using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class ResetPasswordVM
    {
        public string EmailAccount { get; set; }
        public string NewPassword { get; set; }
    }
}
