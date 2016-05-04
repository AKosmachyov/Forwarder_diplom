using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forwarder.Entity
{
    public class Route
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}
