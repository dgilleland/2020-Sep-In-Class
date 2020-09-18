<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageOrderDetails.aspx.cs" Inherits="WebApp.CRUDReview.ManageOrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1 class="page-header">Manage Order Details</h1>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h3>Order Detail/Selection</h3>
            <div class="form-inline bg-light">
                <asp:Label ID="Label3" runat="server" Text="Order Detail ID" CssClass="font-weight-bold font-italic" AssociatedControlID="OrderDetailID" />
                &nbsp;
                <asp:TextBox ID="OrderDetailID" runat="server" Enabled="false" />
                &nbsp;|&nbsp;
                <asp:Label ID="Label4" runat="server" CssClass="col-form-label col-form-label-sm"
                    Text="Filter Orders" AssociatedControlID="CustomerSearch" />
                &nbsp;
                <asp:TextBox ID="CustomerSearch" runat="server" placeholder="Partial name - e.g.: anc" CssClass="form-control form-control-sm" />
                &nbsp;
                <asp:LinkButton ID="FilterCustomers" runat="server" CausesValidation="false"
                    CssClass="btn btn-secondary btn-sm" OnClick="FilterCustomers_Click">Search</asp:LinkButton>
                &nbsp;|&nbsp;
                <asp:Label ID="Label7" runat="server" CssClass="col-form-label col-form-label-sm"
                    Text="Customers" AssociatedControlID="CustomerFilterDropDown" />
                &nbsp;
                <asp:DropDownList ID="CustomerFilterDropDown" runat="server" CssClass="form-control form-control-sm">
                </asp:DropDownList>
                &nbsp;
                <asp:LinkButton ID="FilterOrders" runat="server" CausesValidation="false"
                    CssClass="btn btn-secondary btn-sm" OnClick="FilterOrders_Click">
                    Show Order Details 
                </asp:LinkButton>
            </div>
            <br />
            <div class="form-inline">
                <asp:Label ID="Label1" runat="server" CssClass="col-form-label font-weight-bold"
                    Text="Order" AssociatedControlID="CurrentOrders" />
                &nbsp;
                <asp:DropDownList ID="CurrentOrders" runat="server" CssClass="form-control">
                </asp:DropDownList>
                &nbsp;|&nbsp;
                <asp:Label ID="Label2" runat="server" CssClass="col-form-label font-weight-bold"
                    Text="Product" AssociatedControlID="ProductDropDown" />
                &nbsp;
                <asp:DropDownList ID="ProductDropDown" runat="server" CssClass="form-control">
                </asp:DropDownList>
                &nbsp;
                <asp:LinkButton ID="ShowOrderDetails" runat="server" CausesValidation="false"
                    CssClass="btn btn-info" OnClick="ShowOrderDetails_Click">
                    Lookup Order Details 
                </asp:LinkButton>
            </div>
        </div>
        <div class="col-md-12">
            <h3>Order Amounts</h3>
            <div class="form-inline">
                <asp:Label ID="Label14" runat="server" Text="Unit Price" CssClass="col-form-label font-weight-bold" AssociatedControlID="UnitPrice" />
                &nbsp;
                <asp:TextBox ID="UnitPrice" runat="server" />
                &nbsp;|&nbsp;
                <asp:Label ID="Label15" runat="server" Text="Quantity" CssClass="col-form-label font-weight-bold" AssociatedControlID="Quantity" />
                &nbsp;
                <asp:TextBox ID="Quantity" runat="server" />
                &nbsp;|&nbsp;
                <asp:Label ID="Label16" runat="server" Text="Discount" CssClass="col-form-label font-weight-bold" AssociatedControlID="Discount" />
                &nbsp;
                <asp:TextBox ID="Discount" runat="server" />
            </div>
            <hr />
        </div>
        <div class="col-sm-6 text-center">
            <asp:LinkButton ID="AddOrderDetail" runat="server"
                CssClass="btn btn-success" OnClick="AddOrderDetail_Click">Add OrderDetail</asp:LinkButton>
            <asp:LinkButton ID="UpdateOrderDetail" runat="server"
                CssClass="btn btn-primary" OnClick="UpdateOrderDetail_Click">Update OrderDetail</asp:LinkButton>
            <asp:LinkButton ID="DeleteOrderDetail" runat="server"
                CssClass="btn btn-danger" OnClick="DeleteOrderDetail_Click">Delete OrderDetail</asp:LinkButton>
            <asp:LinkButton ID="ClearForm" runat="server" CausesValidation="false"
                CssClass="btn btn-warning" OnClick="ClearForm_Click">Clear Form</asp:LinkButton>
        </div>
        <div class="col-md-6">
            <br />
            <asp:Panel ID="MessagePanel" runat="server" Visible="false" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <asp:Label ID="MessageLabel" runat="server" />
            </asp:Panel>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-info"
                HeaderText="Please note the following problems with your form. Correct these before adding or updating a Product." />
        </div>
    </div>
</asp:Content>
