using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }
        public string NewPass { get; set; }
        public string ConfPass { get; set; }
        public int OTP { get; set; }
    }
}
