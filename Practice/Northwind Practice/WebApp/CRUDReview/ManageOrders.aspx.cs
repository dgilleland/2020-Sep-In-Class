using NorthwindTraders.BLL.CRUD;
using NorthwindTraders.DataStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.CRUDReview
{
    public static class DateTimeExtensions
    {
        public static string ToHtmlInputDate(this DateTime date) => date.ToString("yyyy-MM-dd");
        public static string ToHtmlInputDate(this DateTime? date) => date.HasValue ? date.Value.ToHtmlInputDate() : string.Empty;
        public static DateTime? FromHtmlInputDate(this TextBox control) => control.Text.IsDateTime() ? DateTime.Parse(control.Text) : new DateTime?();
        public static bool IsDateTime(this string text)
        {
            DateTime temp;
            return DateTime.TryParse(text, out temp);
        }
    }
    public partial class ManageOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurrentOrders.Items.Clear();
                CurrentOrders.Items.Insert(0, "[use filter to shortlist orders]");

                var controller = new CustomerOrderController();

                ShipperDropDown.DataSource = controller.ListShippers();
                ShipperDropDown.DataTextField = nameof(Shipper.CompanyName);
                ShipperDropDown.DataValueField = nameof(Shipper.ShipperID);
                ShipperDropDown.DataBind();
                ShipperDropDown.Items.Insert(0, new ListItem("[Select a shipper]", "0"));

                CustomerDropDown.DataSource = controller.ListCustomers();
                CustomerDropDown.DataTextField = nameof(Customer.CompanyName);
                CustomerDropDown.DataValueField = nameof(Customer.CustomerID);
                CustomerDropDown.DataBind();
                CustomerDropDown.Items.Insert(0, new ListItem("[Select a customer]", string.Empty));

                EmployeeDropDown.DataSource = controller.ListAllEmployees();
                EmployeeDropDown.DataTextField = nameof(Employee.FullName);
                EmployeeDropDown.DataValueField = nameof(Employee.EmployeeID);
                EmployeeDropDown.DataBind();
                EmployeeDropDown.Items.Insert(0, new ListItem("[Select an employee]", "0"));
            }
        }

        #region Filter/Load Order
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
        }

        protected void ShowOrderDetails_Click(object sender, EventArgs e)
        {
            var controller = new CustomerOrderController();
            var order = controller.FindOrder(int.Parse(CurrentOrders.SelectedValue));
            OrderID.Text = order.OrderID.ToString();
            if (order.OrderDate.HasValue)
                OrderDate.Text = order.OrderDate.Value.ToString("yyyy-MM-dd");
            else
                OrderDate.Text = string.Empty;
            if (order.RequiredDate.HasValue)
                RequiredDate.Text = order.RequiredDate.Value.ToString("yyyy-MM-dd");
            else
                RequiredDate.Text = string.Empty;
            if (order.ShippedDate.HasValue)
                ShippedDate.Text = order.ShippedDate.Value.ToString("yyyy-MM-dd");
            else
                ShippedDate.Text = string.Empty;
            ShipperDropDown.SelectedValue = order.ShipVia.ToString();
            Freight.Text = order.Freight.ToString();
            LastModified.Text = order.LastModified.ToString("yyyy-MM-dd");

            ShipName.Text = order.ShipName;
            Address.Text = order.ShipAddress;
            City.Text = order.ShipCity;
            Region.Text = order.ShipRegion;
            PostalCode.Text = order.ShipPostalCode;
            Country.Text = order.ShipCountry;

            CustomerDropDown.SelectedValue = order.CustomerID.ToString();
            EmployeeDropDown.SelectedValue = order.EmployeeID.ToString();
        }
        #endregion

        #region CRUD
        private Order GetOrderFromForm(int orderId = 0)
        {
            Order custOrder = new Order();
            custOrder.OrderID = orderId;
            custOrder.CustomerID = CustomerDropDown.SelectedValue;
            custOrder.EmployeeID = int.Parse(EmployeeDropDown.SelectedValue);
            custOrder.OrderDate = OrderDate.FromHtmlInputDate();
            custOrder.RequiredDate = RequiredDate.FromHtmlInputDate();
            custOrder.ShippedDate = ShippedDate.FromHtmlInputDate();
            custOrder.ShipVia = int.Parse(ShipperDropDown.SelectedValue);
            decimal money;
            custOrder.Freight = decimal.TryParse(Freight.Text, out money) ? money : new decimal?();
            custOrder.ShipName = ShipName.Text;
            custOrder.ShipAddress = Address.Text;
            custOrder.ShipCity = City.Text;
            custOrder.ShipRegion = Region.Text;
            custOrder.ShipPostalCode = PostalCode.Text;
            custOrder.ShipCountry = Country.Text;
            return custOrder;
        }
        protected void AddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                var custOrder = GetOrderFromForm();
                var controller = new CustomerOrderController();
                int orderId = controller.AddOrder(custOrder);
                ShowMessage("New order added for customer.", AlertStyle.success);
                OrderID.Text = orderId.ToString();
            }
            catch(Exception ex)
            {
                ShowFullExceptionMessage(ex);
            }
        }

        protected void UpdateOrder_Click(object sender, EventArgs e)
        {
            int temp;
            if (int.TryParse(OrderID.Text, out temp))
            {
                try
                {
                    var custOrder = GetOrderFromForm(temp);
                    var controller = new CustomerOrderController();
                    DateTime lastModified = controller.UpdateOrder(custOrder);
                    ShowMessage("Customer order Updated.", AlertStyle.success);
                    LastModified.Text = lastModified.ToHtmlInputDate();
                }
                catch (Exception ex)
                {
                    ShowFullExceptionMessage(ex);
                }
            }
            else
            {
                ShowMessage("You can only update an order that already exists", AlertStyle.info);
            }
        }

        protected void DeleteOrder_Click(object sender, EventArgs e)
        {
            int temp;
            if (int.TryParse(OrderID.Text, out temp))
            {
                try
                {
                    var controller = new CustomerOrderController();
                    controller.DeleteOrder(temp);
                    ShowMessage("Customer order removed.", AlertStyle.success);
                    OrderID.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    ShowFullExceptionMessage(ex);
                }
            }
            else
            {
                ShowMessage("You can only remove an order that already exists", AlertStyle.info);
            }
        }

        protected void ClearForm_Click(object sender, EventArgs e)
        {
            ShowMessage("This feature is coming in v1.2", AlertStyle.info);
        }
        #endregion

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
    }
}