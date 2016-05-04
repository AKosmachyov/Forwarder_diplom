using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forwarder.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public decimal komissia { get; set; }
        public ICollection<Order> orders { get; set; }
    }
}
