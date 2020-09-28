<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WebApp.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>
    <address>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>
    <h4>Employees</h4>

    <asp:GridView ID="EmployeeContacts" runat="server"
         AutoGenerateColumns="false"
         ItemType="ChinookTunes.ViewModels.EmployeeContactInfo">
        <Columns>
            <asp:TemplateField HeaderText="Employee">
                <ItemTemplate>
                    <b><%# Item.FirstName %></b> <%# Item.LastName %>
                    (<i><%# Item.JobTitle %></i>)
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Phone">
                <ItemTemplate><%# Item.Phone %></ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Fax">
                <ItemTemplate><%# Item.Fax %></ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Email">
                <ItemTemplate><%# Item.Email %></ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
