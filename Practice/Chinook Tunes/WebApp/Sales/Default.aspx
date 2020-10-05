<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp.Sales.Default" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1 class="page-header">Sales</h1>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:TextBox ID="EmailFilter" runat="server" CssClass="form-control" placeholder="Email Domain">

            </asp:TextBox>
            <asp:LinkButton ID="Search" runat="server" CssClass="btn btn-primary">Filter</asp:LinkButton>
            <uc1:MessageUserControl runat="server" id="MessageUserControl" />
            <asp:GridView ID="CustomersGridView" runat="server"></asp:GridView>

            <asp:ObjectDataSource ID="CustomerDataSource" runat="server"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
