using ChinookTunes.BLL;
using ChinookTunes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class MyPlaylists : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) // GET request
            {
                var controller = new CustomerController();
                var data = controller.ListAllCustomers();
                CustomerSelection.DataSource = data;
                CustomerSelection.DataTextField = nameof(SelectionItem.DisplayText); // = "DisplayText";
                CustomerSelection.DataValueField = nameof(SelectionItem.IDValue);
                CustomerSelection.DataBind(); // populate the drop-down
                CustomerSelection.Items.Insert(0, new ListItem("[select a customer]", "0"));
                //                     .Insert(IndexPosition, ListItem)
            }
        }

        protected void CustomerSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CustomerSelection.SelectedIndex > 0) // not have a customer if they choose the [select a customer]
            {
                var controller = new CustomerController();
                var counts = controller.GetPlaylistCounts(int.Parse(CustomerSelection.SelectedValue));
                // Apply these to my RadioButtonList
                PlaylistSelection.Items.Clear(); // empty out the radiobutton list
                PlaylistSelection.Items.Add(new ListItem($"All Songs ({counts.AllTracks})", "All"));
                PlaylistSelection.Items.Add(new ListItem($"By Album ({counts.Albums})", "Albums"));
                PlaylistSelection.Items.Add(new ListItem($"By Artist ({counts.Artists})", "Artists"));
                //                     .Add              \     DisplayText             /  \  Value/
            }
            else
                PlaylistSelection.Items.Clear(); // empty out the radiobutton list
        }
    }
}