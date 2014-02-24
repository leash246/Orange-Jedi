<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Standings.aspx.vb" Inherits="leightoneash.Standings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<span class="NFC">
<asp:Repeater ID="repNFCN" runat="server">
    <HeaderTemplate>
        <span class="StandingsHeaderTeam">Team</span>
        <span class="StandingsHeader">Wins</span>
        <span class="StandingsHeader">Losses</span>
        <span class="StandingsHeader">Ties</span>
    </HeaderTemplate>
    <ItemTemplate>
        <div>
            <span class="StandingsTeam"><asp:Label ID="lblTeam" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblWins" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblLosses" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblTies" runat="server" /></span>
        </div>
    </ItemTemplate>
</asp:Repeater>
<br />
<asp:Repeater ID="repNFCS" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <div>
            <span class="StandingsTeam"><asp:Label ID="lblTeam" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblWins" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblLosses" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblTies" runat="server" /></span>
        </div>
    </ItemTemplate>
</asp:Repeater>
<br />
<asp:Repeater ID="repNFCE" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <div>
            <span class="StandingsTeam"><asp:Label ID="lblTeam" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblWins" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblLosses" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblTies" runat="server" /></span>
        </div>
    </ItemTemplate>
</asp:Repeater>
<br />
<asp:Repeater ID="repNFCW" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <div>
            <span class="StandingsTeam"><asp:Label ID="lblTeam" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblWins" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblLosses" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblTies" runat="server" /></span>
        </div>
    </ItemTemplate>
</asp:Repeater>
<br />
</span>
<span class="AFC">
<asp:Repeater ID="repAFCN" runat="server">
    <HeaderTemplate>
        <span class="StandingsHeaderTeam">Team</span>
        <span class="StandingsHeader">Wins</span>
        <span class="StandingsHeader">Losses</span>
        <span class="StandingsHeader">Ties</span>
    </HeaderTemplate>
    <ItemTemplate>
        <div>
            <span class="StandingsTeam"><asp:Label ID="lblTeam" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblWins" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblLosses" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblTies" runat="server" /></span>
        </div>
    </ItemTemplate>
</asp:Repeater>
<br />
<asp:Repeater ID="repAFCS" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <div>
            <span class="StandingsTeam"><asp:Label ID="lblTeam" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblWins" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblLosses" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblTies" runat="server" /></span>
        </div>
    </ItemTemplate>
</asp:Repeater>
<br />
<asp:Repeater ID="repAFCE" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <div>
            <span class="StandingsTeam"><asp:Label ID="lblTeam" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblWins" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblLosses" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblTies" runat="server" /></span>
        </div>
    </ItemTemplate>
</asp:Repeater>
<br />
<asp:Repeater ID="repAFCW" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <div>
            <span class="StandingsTeam"><asp:Label ID="lblTeam" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblWins" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblLosses" runat="server" /></span>
            <span class="StandingsRecord"><asp:Label ID="lblTies" runat="server" /></span>
        </div>
    </ItemTemplate>
</asp:Repeater>
</span>
</asp:Content>
