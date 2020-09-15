using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindConsole.Entities;

namespace WestWindConsole.DAL
{
    // Our context class acts as a virtual database in our application; it's the connection point
    // between our application's entities and the database
    public class WestWindContext : DbContext
    {
        public WestWindContext() : base("name=WWdb") // WWdb is the name of the connection info
        {
            // by defaul, DbContext will use an initializer that will create the database
            // if it doesn't exist.
            // TODO: Demonstrate null database initializer PROGRAMMATICALLY
            Database.SetInitializer<WestWindContext>(null); // turn off database initialization
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        // TODO: Practice - Add entities and DbSet<> properties for the remaining tables
    }
}
