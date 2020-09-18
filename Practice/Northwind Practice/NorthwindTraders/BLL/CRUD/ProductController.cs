using NorthwindTraders.DataStore;
using NorthwindTraders.DataStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.BLL.CRUD
{
    public class CustomerOrderController
    {
        #region Customers, Shippers
        public List<Employee> ListAllEmployees()
        {
            using (var context = new NorthwindContext())
            {
                return context.Employees.ToList();
            }
        }
        public List<Customer> ListCustomers()
        {
            using (var context = new NorthwindContext())
            {
                return context.Customers.ToList();
            }
        }
        public List<Customer> FindCustomers(string partialName)
        {
            using (var context = new NorthwindContext())
            {
                var results = from company in context.Customers
                              where company.CompanyName.Contains(partialName)
                              select company;
                return results.ToList();
            }
        }
        public List<Shipper> ListShippers()
        {
            using (var context = new NorthwindContext())
            {
                return context.Shippers.ToList();
            }
        }
        #endregion

        #region Order CRUD
        public object FindOrders(string customerId)
        {
            using (var context = new NorthwindContext())
            {
                var results = from order in context.Orders
                              where order.CustomerID.Equals(customerId)
                              select order;
                return results.ToList();
            }
        }
        public Order FindOrder(int orderId)
        {
            using (var context = new NorthwindContext())
            {
                var result = context.Orders.Find(orderId);
                return result;
            }
        }
        #endregion

        #region OrderDetail CRUD
        #endregion
    }
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
