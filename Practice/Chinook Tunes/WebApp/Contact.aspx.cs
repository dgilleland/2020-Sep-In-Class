using ChinookTunes;
using ChinookTunes.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // Rule of thumb: Do NOT bypass the BLL by accessing the DAL/Entities directly.
                var controller = new ContactController();
                var data = controller.ListCurrentEmployees();
                EmployeeContacts.DataSource = data;
                EmployeeContacts.DataBind();
            }
        }
    }
}