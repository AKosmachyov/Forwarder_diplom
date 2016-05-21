using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forwarder.Entity
{
    public class Cargo
    {
        [Key]
        public int Id { get; set; }
        public Byte Status { get; set; }
        public string Name { get; set; }        
        public Double Weight { get; set; }
        public Double Capacity { get; set; }
        public Car Car { get; set; }
        public DateTime? DataTake { get; set; }
        public DateTime? DataReturn { get; set; }
        public string StartRoute { get; set; }
        public string FinishRoute { get; set; }
        public Order Order { get; set; } 
    }
}
