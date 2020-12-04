using GeorgiaVoterRegistration.BLL;
using GeorgiaVoterRegistration.ViewModels;
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
            // Add them to the DemocratGridView
            var singleBallot = GetSingleBallot(gridViewRow);
            var currentBallots = GetBallots(DemocratGridView);
            currentBallots.Add(singleBallot);
            DemocratGridView.DataSource = currentBallots;
            DemocratGridView.DataBind();

            // remove that ballot from the RepublicanGridView
            currentBallots = GetBallots(RepublicanGridView);
            var foundBallot = currentBallots.Single(x => x.VoterId == singleBallot.VoterId);
            if(currentBallots.Remove(foundBallot))
            {
                RepublicanGridView.DataSource = currentBallots;
                RepublicanGridView.DataBind();
            }

        }
        void RecountVote(GridViewRow gridViewRow)
        {
            // Check the vote, and assign to the correct GridView
            var singleBallot = GetSingleBallot(gridViewRow);
            List<Ballot> currentBallots;
            if(singleBallot.PresidentialTicket == Candidate.Democrat)
            {
                // Leave it in the democrat gridview
            }
            else
            {
                // Move it to the republican gridview
                currentBallots = GetBallots(RepublicanGridView);
                currentBallots.Add(singleBallot);
                RepublicanGridView.DataSource = currentBallots;
                RepublicanGridView.DataBind();
                // Remove it from the democrat gridview
                currentBallots = GetBallots(DemocratGridView);
                var foundBallot = currentBallots.Single(x => x.VoterId == singleBallot.VoterId);
                if (currentBallots.Remove(foundBallot))
                {
                    DemocratGridView.DataSource = currentBallots;
                    DemocratGridView.DataBind();
                }
            }
        }

        private Ballot GetSingleBallot(GridViewRow row)
        {
            // Id, VoterName, Vote
            var idLabel = row.FindControl("Id") as Label;
            var nameLabel = row.FindControl("VoterName") as Label;
            var voteLabel = row.FindControl("Vote") as Label;
            // I should test the results of FindControl and the safe-cast
            Ballot result = null;
            if(idLabel != null && nameLabel != null && voteLabel != null)
            {
                // get the vote
                result = new Ballot
                {
                    VoterId = int.Parse(idLabel.Text),
                    ObfuscatedName = nameLabel.Text,
                    PresidentialTicket = (Candidate)Enum.Parse(typeof(Candidate), voteLabel.Text)
                };
            }
            return result;
        }

        private List<Ballot> GetBallots(GridView grid)
        {
            var result = new List<Ballot>();
            foreach(GridViewRow row in grid.Rows)
            {
                var ballot = GetSingleBallot(row);
                result.Add(ballot);
            }

            return result;
        }
    }
}