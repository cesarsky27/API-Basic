using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class JWTokenVM
    {
        public HttpStatusCode status { get; set; }
        public string Messages { get; set; }

        public string idToken { get; set; }
    }
}
