using NorthwindTraders.DataStore;
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
            using(var context = new NorthwindContext())
            {
                return context.Products.ToList();
            }
        }
        #endregion

        #region Writing to the database
        public void AddProduct(Product item)
        {
            using(var context = new NorthwindContext())
            {
                context.Products.Add(item);
                context.SaveChanges();
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

        public void DeleteProduct(Product item)
        {
            using (var context = new NorthwindContext())
            {
                context.Products.Remove(context.Products.Find(item.ProductID));
                context.SaveChanges();
            }
        }
        #endregion
    }
}
