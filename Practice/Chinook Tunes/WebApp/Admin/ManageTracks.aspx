<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageTracks.aspx.cs" Inherits="WebApp.Admin.ManageTracks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class ="row">
        <div class="col-md-12">
            <asp:ListView ID="SongsListView" runat="server"></asp:ListView>
            <asp:ObjectDataSource ID="SongDataSource" runat="server"
                OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllTracks"
                TypeName="ChinookTunes.BLL.TrackManagment"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
