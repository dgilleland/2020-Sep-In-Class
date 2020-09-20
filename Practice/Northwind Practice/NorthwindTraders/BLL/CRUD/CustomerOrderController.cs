using NorthwindTraders.DataStore;
using NorthwindTraders.DataStore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
        public int AddOrder(Order custOrder)
        {
            using (var context = new NorthwindContext())
            {
                custOrder.LastModified = DateTime.Now;
                var result = context.Orders.Add(custOrder);
                context.SaveChanges();
                return result.OrderID;
            }
        }
        public DateTime UpdateOrder(Order custOrder)
        {
            using (var context = new NorthwindContext())
            {
                custOrder.LastModified = DateTime.Now;
                context.Entry(custOrder).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return custOrder.LastModified;
            }
        }
        public void DeleteOrder(int orderId)
        {
            using (var context = new NorthwindContext())
            {
                context.Orders.Remove(context.Orders.Find(orderId));
                context.SaveChanges();
            }
        }
        #endregion

        #region OrderDetail CRUD
        public OrderDetail FindOrderDetail(int orderId, int productId)
        {
            using (var context = new NorthwindContext())
            {
                var result = context.OrderDetails.Find(orderId, productId);
                return result;
            }
        }
        public List<Product> FindOrderItems(int orderId)
        {
            using (var context = new NorthwindContext())
            {
                var result = context.OrderDetails.Where(item => item.OrderID == orderId).Select(item => item.Product);
                return result.ToList();
            }
        }
        public void AddOrderDetail(OrderDetail item)
        {
            using (var context = new NorthwindContext())
            {
                var newItem = context.OrderDetails.Add(item);
                context.SaveChanges();
            }
        }
        public void UpdateOrderDetail(OrderDetail item)
        {
            using (var context = new NorthwindContext())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteOrderDetail(OrderDetail item)
        {
            using (var context = new NorthwindContext())
            {
                var existing = context.OrderDetails.Find(item.OrderID, item.ProductID);
                context.OrderDetails.Remove(existing);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
