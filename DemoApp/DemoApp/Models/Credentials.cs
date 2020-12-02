using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Models
{
    public class Credentials
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
