using NorthwindTraders.DataStore;
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
                using(var context = new NorthwindContext())
                {
                    var employees = context.Employees.ToList();
                    EmployeeInfo.DataSource = employees;
                    EmployeeInfo.DataBind();
                }
            }
        }
    }
}