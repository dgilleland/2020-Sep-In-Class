using GeorgiaVoterRegistration.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void MailIn_Click(object sender, EventArgs e)
        {
            var controller = new DominionController();
            controller.GenerateData();
        }
    }
}