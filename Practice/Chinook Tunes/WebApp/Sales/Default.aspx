<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp.Sales.Default" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1 class="page-header">Sales</h1>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:TextBox ID="EmailFilter" runat="server"
                 CssClass="form-control" placeholder="Email Domain" />
            <asp:LinkButton ID="Search" runat="server" CssClass="btn btn-primary">Filter</asp:LinkButton>

            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

            <asp:GridView ID="CustomersGridView" runat="server"
                 AutoGenerateColumns="False" DataSourceID="CustomersDataSource"
                 AllowPaging="True">
                <Columns>
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"></asp:BoundField>
                    <asp:BoundField DataField="Surname" HeaderText="Last Name" SortExpression="Surname"></asp:BoundField>
                </Columns>
            </asp:GridView>

            <asp:ObjectDataSource ID="CustomersDataSource" runat="server"
                 OldValuesParameterFormatString="original_{0}"
                 SelectMethod="ListCustomersByEmailDomain"
                 TypeName="ChinookTunes.BLL.SalesController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="EmailFilter" PropertyName="Text" Name="emailDomain" Type="String"></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
