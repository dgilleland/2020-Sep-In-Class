using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Admin.Security;

namespace WebApp
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated) // Do we know who this user is? Authentication
                MessageLabel.Text = "You should log in to the web app.";
            else if (User.IsInRole(DefaultRoles.AdminRole)) // Does the user have access? Authorization
                MessageLabel.Text = "Welcome, oh great Web Administrator!";
            else if (User.IsInRole(DefaultRoles.DefaultRole))
                MessageLabel.Text = "Hello there!";
            else
                MessageLabel.Text = "Who are you?";
        }
    }
}