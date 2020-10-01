<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageAlbums.aspx.cs" Inherits="WebApp.Admin.ManageAlbums" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1 class="page-header">Manage Albums</h1>
    </div>
    <div class="row">
        <div class="col-md-12">
            <p>Our ListView control is a data-bound control (like GridView), but we have a lot more flexibility in how the content in the Listview is displayed (or "rendered") in the browser.</p>
            <style>
                .albums {
                    display: grid;
                    grid-template-columns: repeat(4, 1fr);
                    grid-gap: 5px;
                }

                    .albums h2 {
                        grid-column: 1 / 5;
                        background-color: cadetblue;
                    }

                    .albums > div {
                        padding: 3px;
                        border: solid thin cadetblue;
                    }
            </style>
            <div>
                <uc1:MessageUserControl runat="server" id="MessageUserControl" />
            </div>
            <asp:ListView ID="AlbumsListView" runat="server"
                DataSourceID="AlbumsDataSource"
                DataKeyNames="ID"
                InsertItemPosition="FirstItem"
                OnItemCommand="AlbumsListView_ItemCommand"
                ItemType="ChinookTunes.ViewModels.AlbumInfo">
                <EditItemTemplate>
                    <div class="bg-info">
                        <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                        <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                        <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBox" />
                        <asp:DropDownList ID="ArtistDropDown" runat="server" AppendDataBoundItems="true"
                            DataSourceID="ArtistDataSource" DataTextField="DisplayText" DataValueField="IDValue"
                            SelectedValue="<%# BindItem.ArtistID %>">
                            <asp:ListItem Value="0">[Select an Artist]</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <div>
                        <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                        <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                        <asp:TextBox Text='<%# BindItem.Title %>' runat="server" ID="TitleTextBox" placeholder="Title" />
                        <asp:DropDownList ID="ArtistDropDown" runat="server" AppendDataBoundItems="true"
                            DataSourceID="ArtistDataSource" DataTextField="DisplayText" DataValueField="IDValue"
                            SelectedValue="<%# BindItem.ArtistID %>">
                            <asp:ListItem Value="0">[Select an Artist]</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </InsertItemTemplate>
                <ItemTemplate>
                    <div>
                        <asp:LinkButton ID="SelectButton" runat="server" CommandName="Select">
                        <b><%# Item.Title %></b></asp:LinkButton>
                        <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                        <blockquote>by <i><%# Item.ArtistName %></i></blockquote>
                    </div>
                </ItemTemplate>
                <LayoutTemplate>
                    <div class="albums" runat="server" id="itemPlaceholderContainer">
                        <h2>Current Albums</h2>
                        <div runat="server" id="itemPlaceholder"></div>
                    </div>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <div>
                        <b><%# Item.Title %></b>
                        <asp:Button runat="server" CommandName="Unselect" Text="De-Select" ID="DeselectButton" />
                        <blockquote>by <i><%# Item.ArtistName %></i></blockquote>
                        <div>
                            <asp:Repeater ID="SongRepeater" runat="server" DataSource="<%# Item.Songs %>" ItemType="System.String">
                                 <ItemTemplate><%# Item %></ItemTemplate>
                                <SeparatorTemplate>, </SeparatorTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </SelectedItemTemplate>
            </asp:ListView>

            <asp:ObjectDataSource ID="ArtistDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListArtists" TypeName="ChinookTunes.BLL.AlbumManagement"></asp:ObjectDataSource>

            <asp:ObjectDataSource ID="AlbumsDataSource" runat="server"
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="ListAlbums"
                TypeName="ChinookTunes.BLL.AlbumManagement"
                DataObjectTypeName="ChinookTunes.ViewModels.AlbumInfo"
                DeleteMethod="DeleteAlbum"
                InsertMethod="AddAlbum"
                UpdateMethod="UpdateAlbum"
                OnInserted="CheckForExceptions"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
