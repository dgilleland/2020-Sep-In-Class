using NorthwindTraders.BLL.CRUD;
using NorthwindTraders.DataStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.CRUDReview
{
    public partial class ManageOrderDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateProductDropDown();
                CurrentOrders.Items.Clear();
                CurrentOrders.Items.Insert(0, "[use filter to shortlist orders]");
            }
        }
        private void PopulateProductDropDown()
        {
            // Get the data from the BLL
            var controller = new ProductController();
            var data = controller.ListAllProducts();

            // Use the data in the DropDownList control
            ProductDropDown.DataSource = data;  // supplies all the data to the control
            ProductDropDown.DataTextField = nameof(Product.ProductName); // identify which property will be used to display text
            ProductDropDown.DataValueField = nameof(Product.ProductID);// identify which property will be associated with the value of the <option> element
            ProductDropDown.DataBind();

            // Insert an item at the top to be our "placeholder" for the <select> tag
            ProductDropDown.Items.Insert(0, "[select a product]");
        }

        protected void FilterCustomers_Click(object sender, EventArgs e)
        {
            string partialName = CustomerSearch.Text;
            var controller = new CustomerOrderController();
            List<Customer> customers = controller.FindCustomers(partialName);
            CustomerFilterDropDown.DataSource = customers;
            CustomerFilterDropDown.DataTextField = nameof(Customer.CompanyName);
            CustomerFilterDropDown.DataValueField = nameof(Customer.CustomerID);
            CustomerFilterDropDown.DataBind();
        }

        protected void FilterOrders_Click(object sender, EventArgs e)
        {
            var controller = new CustomerOrderController();
            var orders = controller.FindOrders(CustomerFilterDropDown.SelectedValue);
            CurrentOrders.DataSource = orders;
            CurrentOrders.DataTextField = nameof(Order.OrderInfo);
            CurrentOrders.DataValueField = nameof(Order.OrderID);
            CurrentOrders.DataBind();
            CurrentOrders.Items.Insert(0, "[select an order]");
            ProductFilterDropDown.Items.Clear();
        }

        protected void ShowOrderDetails_Click(object sender, EventArgs e)
        {
            if (CurrentOrders.SelectedIndex > 0 && ProductFilterDropDown.SelectedIndex > 0)
            {
                int orderId = int.Parse(CurrentOrders.SelectedValue);
                int productId = int.Parse(ProductFilterDropDown.SelectedValue);
                var controller = new CustomerOrderController();
                var found = controller.FindOrderDetail(orderId, productId);
                if (found == null)
                {
                    ShowMessage("That product does not exist on the selected order", AlertStyle.info);
                }
                else
                {
                    ShowMessage("Order Detail Found", AlertStyle.success);
                    OrderDetailID.Text = $"Oid:{found.OrderID}-Pid:{found.ProductID}";
                    UnitPrice.Text = found.UnitPrice.ToString();
                    Quantity.Text = found.Quantity.ToString();
                    Discount.Text = found.Discount.ToString();
                    ProductDropDown.SelectedValue = found.ProductID.ToString();
                }
            }
            else
            {
                ShowMessage("Select an order and an item before trying to lookup order details", AlertStyle.info);
            }
        }

        protected void AddOrderDetail_Click(object sender, EventArgs e)
        {
            var item = ParseOrderDetail("Add");
            if (item != null)
            {
                var controller = new CustomerOrderController();
                try
                {
                    controller.AddOrderDetail(item);
                    ShowMessage("Order Item added", AlertStyle.success);
                    CurrentOrders_SelectedIndexChanged(sender, e);
                }
                catch (Exception ex)
                {
                    ShowFullExceptionMessage(ex);
                }
            }
        }

        private OrderDetail ParseOrderDetail(string action)
        {
            OrderDetail item = null;
            if (CurrentOrders.SelectedIndex > 0 && ProductDropDown.SelectedIndex > 0)
            {
                item = new OrderDetail();

                item.OrderID = int.Parse(CurrentOrders.SelectedValue);
                item.ProductID = int.Parse(ProductDropDown.SelectedValue);
                decimal price; short qty; float discount;
                if (decimal.TryParse(UnitPrice.Text, out price) && short.TryParse(Quantity.Text, out qty) && float.TryParse(Discount.Text, out discount))
                {
                    item.UnitPrice = price;
                    item.Quantity = qty;
                    item.Discount = discount;
                }
                else
                {
                    ShowMessage($"Unable to {action}. Values are required for Unit Price, Quantity, and Discount", AlertStyle.info);
                    item = null;
                }
            }
            else
            {
                ShowMessage($"Select an order and a product before trying to {action} an Order Detail", AlertStyle.info);
            }

            return item;
        }
        protected void UpdateOrderDetail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(OrderDetailID.Text))
            {
                ShowMessage("Update can only be done on an item that is on the order", AlertStyle.info);
            }
            else
            {
                var item = ParseOrderDetail("Update");
                if (item != null)
                {
                    var controller = new CustomerOrderController();
                    try
                    {
                        controller.UpdateOrderDetail(item);
                        ShowMessage("Order Item updated", AlertStyle.success);
                    }
                    catch (Exception ex)
                    {
                        ShowFullExceptionMessage(ex);
                    }
                }
            }
        }

        protected void DeleteOrderDetail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(OrderDetailID.Text))
            {
                ShowMessage("Delete can only be done on an item that is on the order", AlertStyle.info);
            }
            else
            {
                var item = ParseOrderDetail("Delete");
                if (item != null)
                {
                    var controller = new CustomerOrderController();
                    try
                    {
                        controller.DeleteOrderDetail(item);
                        ShowMessage("Order Item removed", AlertStyle.success);
                        CurrentOrders_SelectedIndexChanged(sender, e);
                    }
                    catch (Exception ex)
                    {
                        ShowFullExceptionMessage(ex);
                    }
                }
            }
        }

        protected void ClearForm_Click(object sender, EventArgs e)
        {
            ShowMessage("This feature is coming in v1.2", AlertStyle.info);
        }
        // Enumeration values based off of Bootstrap styles for alerts.
        public enum AlertStyle { success, info, warning, danger }

        private void ShowMessage(string message, AlertStyle alertStyle)
        {
            MessageLabel.Text = message;
            MessagePanel.CssClass = $"alert alert-{alertStyle} alert-dismissible";
            MessagePanel.Visible = true;
        }

        private void ShowFullExceptionMessage(Exception ex)
        {
            string message = $"ERROR: {ex.Message}";
            // get the inner exception....
            Exception inner = ex;
            // this next statement drills down on the details of the exception
            while (inner.InnerException != null)
                inner = inner.InnerException;
            if (inner != ex)
                message += $"<blockquote>{inner.Message}</blockquote>";
            ShowMessage(message, AlertStyle.danger);
        }

        protected void CurrentOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentOrders.SelectedIndex > 0)
            {
                var controller = new CustomerOrderController();
                var data = controller.FindOrderItems(int.Parse(CurrentOrders.SelectedValue));
                ProductFilterDropDown.DataSource = data;
                ProductFilterDropDown.DataTextField = nameof(Product.ProductName);
                ProductFilterDropDown.DataValueField = nameof(Product.ProductID);
                ProductFilterDropDown.DataBind();
                ProductFilterDropDown.Items.Insert(0, "[select existing item]");
            }
            else
            {
                ProductFilterDropDown.Items.Clear();
            }
        }
    }
}