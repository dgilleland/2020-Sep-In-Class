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

        protected void LoadVotes_Click(object sender, EventArgs e)
        {
            var controller = new DominionController();
            var votes = controller.LoadTally();
            DemocratGridView.DataSource = votes.Democrat;
            DemocratGridView.DataBind();
            RepublicanGridView.DataSource = votes.Republican;
            RepublicanGridView.DataBind();
        }

        protected void Adjust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());
            switch(e.CommandName)
            {
                case "Cure":
                    CureVote(RepublicanGridView.Rows[index]);
                    break;
                case "Recount":
                    RecountVote(DemocratGridView.Rows[index]);
                    break;
            }
        }

        void CureVote(GridViewRow gridViewRow)
        {
            throw new NotImplementedException();
        }
        void RecountVote(GridViewRow gridViewRow)
        {
            throw new NotImplementedException();
        }
    }
}