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

        protected void AlbumsDataSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            // Before the Insert is attempted by the ObjectDataSource control
            MessageLabel.Text += "BEFORE - An INSERT is being made by the ObjectDataSource control.";
        }

        protected void AlbumsDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            // After the Insert has been attempted by the ObjectDataSource control
            MessageLabel.Text += "<br/>After - An INSERT has been attempted by the ObjectDataSource control.";
            if (e.Exception != null)
            {
                MessageLabel.Text += $"<blockquote>ERROR: {e.Exception.Message}</blockquote>";
                e.ExceptionHandled = true; // Tells the ObjectDataSource control that I've handled the exception with my own code.
            }
        }
    }
}