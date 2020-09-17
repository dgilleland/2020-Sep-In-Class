using NorthwindTraders.BLL.CRUD;
using NorthwindTraders.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.CRUDReview
{
    public partial class ManageProducts : System.Web.UI.Page
    {
        // TODO: Create a CRUD page to edit/add/remove a product
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // Populate the Products DropDown
                var controller = new ProductController();
                var data = controller.ListAllProducts();
                CurrentProducts.DataSource = data;
                CurrentProducts.DataTextField = nameof(Product.ProductName);
                CurrentProducts.DataValueField = nameof(Product.ProductID);
                CurrentProducts.DataBind();
            }
        }
    }
}