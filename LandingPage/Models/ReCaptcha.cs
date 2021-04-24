using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models
{
    public class ReCaptcha
    {
        public readonly string SiteKey = "6LdZWbcaAAAAAIDhQZV0uPSkqo1ZPVDMCjVclMf1";
        public readonly string SecretKey = "6LdZWbcaAAAAAC9VzLb5MlExYPCEjlI6Pl-g9ugH";
        public string Response { get; set; }
    }
}
