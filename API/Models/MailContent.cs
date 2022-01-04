using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MailContent
    {
        public string Email { get; set; }
        public DateTime TimeNow { get; set; }
        public int Token { get; set; }
        public string Body { get; set; }
    }
}
