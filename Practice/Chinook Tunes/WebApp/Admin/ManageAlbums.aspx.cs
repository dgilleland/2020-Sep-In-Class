using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Admin
{
    public partial class ManageAlbums : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CheckForExceptions(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void AlbumsListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if(e.CommandName == "Unselect")
            {
                AlbumsListView.SelectedIndex = -1; // Effectively "de-selecting"
                e.Handled = true; // Tell the ListView we dealt with the command ourselves
            }
        }
    }
}