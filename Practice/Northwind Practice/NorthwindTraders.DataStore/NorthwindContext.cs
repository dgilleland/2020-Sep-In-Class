using NorthwindTraders.DataStore.Entities;
using System;
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
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        // TODO: Add remaining tables for each entity in this class library
        #endregion
    }
}
