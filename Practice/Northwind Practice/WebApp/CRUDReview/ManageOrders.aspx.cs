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
                ShipperDropDown.Items.Insert(0, new ListItem("[Select a shipper]", string.Empty));

                CustomerDropDown.DataSource = controller.ListCustomers();
                CustomerDropDown.DataTextField = nameof(Customer.CompanyName);
                CustomerDropDown.DataValueField = nameof(Customer.CustomerID);
                CustomerDropDown.DataBind();
                CustomerDropDown.Items.Insert(0, new ListItem("[Select a customer]", string.Empty));

                EmployeeDropDown.DataSource = controller.ListAllEmployees();
                EmployeeDropDown.DataTextField = nameof(Employee.FullName);
                EmployeeDropDown.DataValueField = nameof(Employee.EmployeeID);
                EmployeeDropDown.DataBind();
                EmployeeDropDown.Items.Insert(0, new ListItem("[Select an employee]", string.Empty));
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
        protected void AddOrder_Click(object sender, EventArgs e)
        {

        }

        protected void UpdateOrder_Click(object sender, EventArgs e)
        {

        }

        protected void DeleteOrder_Click(object sender, EventArgs e)
        {

        }

        protected void ClearForm_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}