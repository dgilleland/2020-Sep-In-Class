<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="WebApp.CRUDReview.ManageProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Manage Products</h1>
            <asp:DropDownList ID="CurrentProducts" runat="server"></asp:DropDownList>
        </div>
    </div>
</asp:Content>
