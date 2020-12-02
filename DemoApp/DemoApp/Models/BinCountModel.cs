using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Models
{
    public class BinCountModel
    {
        public bool success { get; set; }
        public long size { get; set; }
        public Dictionary<string, long> response { get; set; }
    }
}
