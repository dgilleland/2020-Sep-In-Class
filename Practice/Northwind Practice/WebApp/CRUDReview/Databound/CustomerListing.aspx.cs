using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.CRUDReview.Databound
{
    public partial class CustomerListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CustomersGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // String Interpolation
            string name = string.Empty;
            // 1) We can "find" a control in our row by the control's ID
            // 2) We are doing a "safe cast" of the Control type to a Label type
            Label cmpy = CustomersGridView.SelectedRow.FindControl("Company") as Label;
            if (cmpy != null)
                name = cmpy.Text;
            HiddenField count = CustomersGridView.SelectedRow.FindControl("OrderCount") as HiddenField;

            MessageLabel.Text = $"You have selected a customer with the id of {CustomersGridView.SelectedValue}. The customer name is <b>{name}</b>. They have purchased <b>{count?.Value}</b> orders from us.";
        }
    }
}