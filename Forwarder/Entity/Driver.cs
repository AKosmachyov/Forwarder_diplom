using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forwarder.Entity
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
    }
}
