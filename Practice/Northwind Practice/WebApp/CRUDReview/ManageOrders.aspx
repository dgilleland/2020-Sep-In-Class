<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageOrders.aspx.cs" Inherits="WebApp.CRUDReview.ManageOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1 class="page-header">Manage Orders</h1>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-inline">
                        <asp:Label ID="Label1" runat="server" CssClass="control-label"
                            Text="Orders" AssociatedControlID="CurrentOrders" />
                        &nbsp;
                        <asp:DropDownList ID="CurrentOrders" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:LinkButton ID="ShowOrderDetails" runat="server" CausesValidation="false"
                            CssClass="btn btn-primary" OnClick="ShowOrderDetails_Click">
                            Show Order Details <i class="glyphicon glyphicon-search"></i>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="col-sm-6 text-center">
                    <asp:LinkButton ID="AddOrder" runat="server"
                        CssClass="btn btn-default" OnClick="AddOrder_Click">Add Order</asp:LinkButton>
                    <asp:LinkButton ID="UpdateOrder" runat="server"
                        CssClass="btn btn-default" OnClick="UpdateOrder_Click">Update Order</asp:LinkButton>
                    <asp:LinkButton ID="DeleteOrder" runat="server"
                        CssClass="btn btn-default" OnClick="DeleteOrder_Click">Delete Order</asp:LinkButton>
                    <asp:LinkButton ID="ClearForm" runat="server" CausesValidation="false"
                        CssClass="btn btn-default" OnClick="ClearForm_Click">Clear Form</asp:LinkButton>
                </div>
            </div>
        </div>
        <hr />
        <div class="col-md-12">
            <br />
            <asp:Panel ID="MessagePanel" runat="server" Visible="false" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <asp:Label ID="MessageLabel" runat="server" />
            </asp:Panel>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-info"
                HeaderText="Please note the following problems with your form. Correct these before adding or updating an Order." />
        </div>
        <div class="col-md-6">
            <fieldset>
                <legend>Order Summary</legend>
                <asp:Label ID="Label3" runat="server" Text="Order ID" AssociatedControlID="OrderID" />
                <asp:TextBox ID="OrderID" runat="server" Enabled="false" />

                <asp:Label ID="Label4" runat="server" Text="Customer" AssociatedControlID="CustomerDropDown" />
                <asp:DropDownList ID="CustomerDropDown" runat="server"></asp:DropDownList>

                <asp:Label ID="Label5" runat="server" Text="Employee" AssociatedControlID="EmployeeDropDown" />
                <asp:DropDownList ID="EmployeeDropDown" runat="server"></asp:DropDownList>

                <asp:Label ID="Label6" runat="server" Text="Order Date" AssociatedControlID="OrderDate" />
                <asp:TextBox ID="OrderDate" runat="server" TextMode="Date" />

                <asp:Label ID="Label7" runat="server" Text="Required Date" AssociatedControlID="RequiredDate" />
                <asp:TextBox ID="RequiredDate" runat="server" TextMode="Date" />

                <asp:Label ID="Label8" runat="server" Text="Shipped Date" AssociatedControlID="ShippedDate" />
                <asp:TextBox ID="ShippedDate" runat="server" TextMode="Date" />

                <asp:Label ID="Label9" runat="server" Text="Ship Via" AssociatedControlID="ShipperDropDown" />
                <asp:DropDownList ID="ShipperDropDown" runat="server"></asp:DropDownList>

                <asp:Label ID="Label10" runat="server" Text="Freight" AssociatedControlID="Freight" />
                <asp:TextBox ID="Freight" runat="server" />

                <asp:Label ID="Label2" runat="server" Text="Last Modified" AssociatedControlID="LastModified" />
                <asp:TextBox ID="LastModified" runat="server" Enabled="false" TextMode="Date" />
            </fieldset>
        </div>
        <div class="col-md-6">
            <fieldset>
                <legend>Shipping Details</legend>
                <asp:Label ID="Label11" runat="server" Text="Ship To" AssociatedControlID="ShipName" />
                <asp:TextBox ID="ShipName" runat="server" />

                <asp:Label ID="Label12" runat="server" Text="Address" AssociatedControlID="Address" />
                <asp:TextBox ID="Address" runat="server" />

                <asp:Label ID="Label13" runat="server" Text="City" AssociatedControlID="City" />
                <asp:TextBox ID="City" runat="server" />

                <asp:Label ID="Label14" runat="server" Text="Region" AssociatedControlID="Region" />
                <asp:TextBox ID="Region" runat="server" />

                <asp:Label ID="Label15" runat="server" Text="Postal Code" AssociatedControlID="PostalCode" />
                <asp:TextBox ID="PostalCode" runat="server" />

                <asp:Label ID="Label16" runat="server" Text="Country" AssociatedControlID="Country" />
                <asp:TextBox ID="Country" runat="server" />
            </fieldset>
        </div>
    </div>
    <link href="../Content/bootwrap-freecode.css" rel="stylesheet" />
    <script src="../Scripts/bootwrap-freecode.js"></script>
    <style>
        select.form-control {
            width: auto;
        }
    </style>
</asp:Content>
