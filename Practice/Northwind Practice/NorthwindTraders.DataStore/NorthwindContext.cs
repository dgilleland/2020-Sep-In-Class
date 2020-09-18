using NorthwindTraders.DataStore.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.DataStore
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext() : base("name=NW2018")
        { }

        #region "Virtual" Tables
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }
        public virtual DbSet<Version> Versions { get; set; }
        public virtual DbSet<VersionDDLEventLog> VersionDDLEventLogs { get; set; }
        #endregion
    }
}
