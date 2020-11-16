<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Security Demo</h1>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="VoterGridView" runat="server" ItemType="GeorgiaVoterRegistration.Entities.Voter" AutoGenerateColumns="False" DataSourceID="VoterDataSource">
                <EmptyDataTemplate>
                    No Voters to Show
                    <asp:LinkButton ID="MailIn" runat="server" OnClick="MailIn_Click">Register</asp:LinkButton>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="VoterId" HeaderText="VoterId" SortExpression="VoterId"></asp:BoundField>
                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName"></asp:BoundField>
                    <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName"></asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"></asp:BoundField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <%--What will be rendered when this row is in Edit Mode--%>
                            <%--The row that is in Edit Mode can be seen by the GridView's .EditRow property--%>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%--What will be rendered for each item that is being displayed--%>
                            <image src="<%# Item.Avatar %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DateOfBirth" HeaderText="DateOfBirth" SortExpression="DateOfBirth"></asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="VoterDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllVoters" TypeName="GeorgiaVoterRegistration.BLL.DominionController"></asp:ObjectDataSource>
        </div>
    </div>

</asp:Content>
