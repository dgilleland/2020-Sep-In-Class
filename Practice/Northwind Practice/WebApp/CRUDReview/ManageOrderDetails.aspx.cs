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
    public partial class ManageOrderDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
        }

        protected void ShowOrderDetails_Click(object sender, EventArgs e)
        {

        }

        protected void AddOrderDetail_Click(object sender, EventArgs e)
        {

        }

        protected void UpdateOrderDetail_Click(object sender, EventArgs e)
        {

        }

        protected void DeleteOrderDetail_Click(object sender, EventArgs e)
        {

        }

        protected void ClearForm_Click(object sender, EventArgs e)
        {

        }
    }
}