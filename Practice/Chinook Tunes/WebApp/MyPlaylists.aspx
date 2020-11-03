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
                AutoPostBack="true" OnSelectedIndexChanged="PlaylistSelection_SelectedIndexChanged">
            </asp:RadioButtonList>
            <hr />
            <asp:Panel ID="AllTracksPanel" runat="server" Visible="false">
                <asp:GridView ID="AllTracks" runat="server"
                    AutoGenerateColumns="false" CssClass="table"
                    EmptyDataText="No songs were found">
                    <Columns>
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:BoundField HeaderText="Time" DataField="RunningTime" />
                        <asp:BoundField HeaderText="Artist" DataField="Artist" />
                        <asp:BoundField HeaderText="Album" DataField="Album" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <asp:Panel ID="ByAlbumPanel" runat="server" Visible="false">
                <asp:Repeater ID="ByAlbum" runat="server"
                    ItemType="ChinookTunes.ViewModels.AlbumTracks">
                    <ItemTemplate>
                        <h3><%# Item.Album %> <small><%# Item.Artist %></small></h3>
                        <%--My nested GridView should pull its data from the Repeater's Item property--%>
                        <asp:GridView ID="AlbumTracks" runat="server" DataSource="<%# Item.Tracks %>"
                            CssClass="table" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:BoundField HeaderText="Time" DataField="RunningTime" />
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel ID="ByArtistPanel" runat="server" Visible="false">
                <asp:Repeater ID="ByArtist" runat="server"
                    ItemType="ChinookTunes.ViewModels.ArtistTracks">
                    <ItemTemplate>
                        <h3><%# Item.Artist %></h3>
                        <asp:GridView ID="ArtistTracks" runat="server" DataSource="<%# Item.Tracks %>"
                            CssClass="table" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:BoundField HeaderText="Time" DataField="RunningTime" />
                                <asp:BoundField HeaderText="Album" DataField="Album" />
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
        </div>
    </div>

</asp:Content>
