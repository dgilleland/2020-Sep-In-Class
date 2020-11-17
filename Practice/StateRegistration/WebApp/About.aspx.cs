using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Demo how to secure this page for Authenticated (logged in) users
            if (!Request.IsAuthenticated)
                Response.Redirect("~/Account/Login", true); // send them to the Login page
        }
    }
}