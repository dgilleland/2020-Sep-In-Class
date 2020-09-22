<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerListing.aspx.cs" Inherits="WebApp.CRUDReview.Databound.CustomerListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1 class="page-header">Customer Listing</h1>
    </div>
    <div class="row">
        <div class="col-md-3">
            <h2>Search Customers</h2>
            <asp:Label ID="Label1" runat="server" AssociatedControlID="PartialName">Partial Company Name</asp:Label>
            <asp:TextBox ID="PartialName" runat="server" CssClass="form-control" />
            <asp:LinkButton ID="Search" runat="server" CssClass="btn btn-primary">Filter Results</asp:LinkButton>
            <hr />
            <asp:Label ID="MessageLabel" runat="server" />
        </div>
        <div class="col-md-9">
            <asp:GridView ID="CustomersGridView" runat="server" AutoGenerateColumns="False"
                 DataSourceID="CustomersDataSource"
                 CssClass="table table-hover"
                 DataKeyNames="CustomerID"
                 ItemType="NorthwindTraders.DataStore.Entities.Customer"
                 OnSelectedIndexChanged="CustomersGridView_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-info" HeaderText="Show Info"></asp:CommandField>

                    <asp:TemplateField HeaderText="Company">
                        <ItemTemplate>
                            <asp:Label ID="Company" runat="server" CssClass="lead font-weight-bold" Text="<%# Item.CompanyName %>" />
                            <br />
                            FAX: <%# Item.Fax %>
                            <asp:HiddenField ID="OrderCount" runat="server" Value="<%# Item.Orders.Count %>" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Contact Info">
                        <ItemTemplate>
                            <asp:Label ID="ContactName" runat="server" 
                                Text='<%# Item.ContactName %>'
                                CssClass="font-weight-bold"/>
                            (<asp:Label ID="ContactTitle" runat="server" Text='<%# Item.ContactTitle %>' />)
                            <br />
                            <asp:Label ID="PhoneNumber" runat="server" Text='<%# Item.Phone %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>
                            <%# Item.Address %>
                            <br />
                            <u><%# Item.City %></u>
                            <%--I'm using a ternary expression to optionally add the comma--%>
                            <%# string.IsNullOrEmpty(Item.Region) ? string.Empty : ", " + Item.Region %>
                            <br />
                            <%# Item.Country %> &nbsp;&nbsp; <%# Item.PostalCode %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="CustomersDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListCustomers" TypeName="NorthwindTraders.BLL.CRUD.CustomerOrderController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="PartialName" PropertyName="Text" Name="partialName" Type="String"></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
