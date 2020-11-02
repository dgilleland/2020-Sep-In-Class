<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyPlaylists.aspx.cs" Inherits="WebApp.MyPlaylists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Customer Playlists</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="Label1" runat="server" AssociatedControlID="CustomerSelection">Customer:</asp:Label>
            <asp:DropDownList ID="CustomerSelection" runat="server" 
                AutoPostBack="true" OnSelectedIndexChanged="CustomerSelection_SelectedIndexChanged"
                CssClass="form-control">
            </asp:DropDownList>
            <asp:RadioButtonList ID="PlaylistSelection" runat="server"
                AutoPostBack="true" OnSelectedIndexChanged="PlaylistSelection_SelectedIndexChanged"></asp:RadioButtonList>
        </div>
    </div>

</asp:Content>
