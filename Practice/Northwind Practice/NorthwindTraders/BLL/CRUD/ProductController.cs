using NorthwindTraders.DataStore;
using NorthwindTraders.DataStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.BLL.CRUD
{
    public class ProductController
    {
        #region Reading from the database
        public List<Product> ListAllProducts()
        {
            // This "using" statement is different than the "using" at the top of this file.
            // This "using" statement is to ensure that the connection to the database is properly closed after we are done.
            // The variable context is a NorthwindContext object
            // The NorthwindContext class represents a "virtual" database
            using (var context = new NorthwindContext())
            {
                return context.Products.ToList();
            }
        }

        public Product LookupProduct(int productId)
        {
            using (var context = new NorthwindContext())
            {
                return context.Products.Find(productId);
            }
        }
        #endregion

        #region Writing to the database
        public int AddProduct(Product item)
        {
            using(var context = new NorthwindContext())
            {
                var newItem = context.Products.Add(item);
                context.SaveChanges();
                return newItem.ProductID;
            }
        }

        public void UpdateProduct(Product item)
        {
            using (var context = new NorthwindContext())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var context = new NorthwindContext())
            {
                // The .Find method will look up the specific Product based on the Primary Key value
                var existing = context.Products.Find(id);
                context.Products.Remove(existing);
                context.SaveChanges();
            }
        }

        // Here is an overloaded method that "chains" to the DeleteProduct(int) version
        public void DeleteProduct(Product item)
        {
            DeleteProduct(item.ProductID);
        }
        #endregion

        #region Supporting Methods
        public List<Category> ListAllCategories()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Categories.OrderBy(item => item.CategoryName).ToList();
                //                       \[scary] Lambda stuff [this term]/
            }
        }
        public List<Supplier> ListAllSuppliers()
        {
            using (var context = new NorthwindContext())
            {
                return context.Suppliers.ToList();
            }
        }
        #endregion
    }
}
