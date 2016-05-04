using Forwarder.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forwarder.DB
{
    public static class ForwarderDB
    {
        public static DBContext _db = new DBContext();       
    }
    public class DBContext : DbContext
    {
        
        public DBContext()
        {
            //Отчистка всех таблиц 
            //Database.SetInitializer<DBContext>(new DropCreateDatabaseAlways<DBContext>());

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Racxod> Racxods { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }
        
    }   
}
