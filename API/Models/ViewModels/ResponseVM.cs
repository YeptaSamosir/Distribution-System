using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class ResponseVM
    {
        public int Status { get; set; }
        public Object Result { get; set; }
        public string Message { get; set; }
    }
}