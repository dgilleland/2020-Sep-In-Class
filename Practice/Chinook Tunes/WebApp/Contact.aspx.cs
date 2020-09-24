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
                // BUG: Bad Code - don't access DAL directly!!!!
                using (var context = new ChinookContext())
                {
                    var data = context.Employees.ToList();
                    EmployeeContacts.DataSource = data;
                    EmployeeContacts.DataBind();
                }
            }
        }
    }
}