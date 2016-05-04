using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forwarder.Entity
{
    public class Order
    {
        public Order()
        {
            Cargos = new List<Cargo>();
            Routes = new List<Route>();
            Racxod = new List<Racxod>();
        }
          //  cargos = carg;
            //sendCargos = sC;
           // routes = r;
            //status = 0;
        //}
        [Key]
        public int Id { get; set; }
        public Client Client { get; set; }
        public ICollection<Cargo> Cargos { get; set; }
        public byte Status { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Finish { get; set; }
        public decimal Price { get; set; }
        public ICollection<Route> Routes { get; set; }
        public float Way { get; set; }
        public ICollection<Racxod> Racxod { get; set; }
        
        
        
       
    }
}
