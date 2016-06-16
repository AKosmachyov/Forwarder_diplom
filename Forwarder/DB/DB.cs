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
        public DBContext(): base ("ForwarderContext")
        {
            Database.SetInitializer<DBContext>(new DBInitializer());
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
    public class DBInitializer : CreateDatabaseIfNotExists<DBContext>
    {
        protected override void Seed(DBContext context)
        {
            var user = new User();
            user.Login = "admin";
            user.Password = Core.generatePasswordHash("admin");
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
