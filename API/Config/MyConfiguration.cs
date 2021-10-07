using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Config
{
    public class MyConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BaseUrlClient { get; set; }
    }
}
