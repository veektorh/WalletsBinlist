using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistApi.Models
{
    public class BinCount
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string Bin { get; set; }
        public long Count { get; set; }
    }
}
