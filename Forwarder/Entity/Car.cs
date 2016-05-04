using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forwarder.Entity
{
    public class Car
    {
       // public Car(decimal p, string n, Firm f, float c, float w) 
       // {
       //     price = p;
       //     number = n;
           // firm = f;
         //   capacity = c;
       //     weight = w;             
        //}
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public Firm Firm { get; set; }
        public ICollection<Driver> Drivers { get; set; }
        public double MaxCapacity { get; set; }
        public double MaxWeight { get; set; }
        public Boolean IsReady { get; set; }
        public decimal Price { get; set; }
    }

}
