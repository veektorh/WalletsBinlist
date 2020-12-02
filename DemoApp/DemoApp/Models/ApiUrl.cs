using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Models
{
    public class ApiUrl
    {
        public string Binlist { get; set; }
        public string IdentityServer { get; set; }
    }

    public class IdentityConfigOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public string Scope { get; set; }
    }
}
