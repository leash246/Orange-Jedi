<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PastWeeks.aspx.vb" Inherits="leightoneash.PastWeeks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:DropDownList ID="ddlWeeks" runat="server" /> <asp:Button ID="btnWeeks" runat="server" Text="Go" />
    </div>
    <div>
        <asp:DropDownList ID="ddlUser" runat="server" /> <asp:Button ID="btnUser" runat="server" Text="Go" />
    </div>
    <ul class="GameView" id="GameList">
        <asp:Repeater ID="repGames" runat="server">
            <HeaderTemplate>
                <span class="AwayDog" >Away</span>
                <span class="lblSpread" >Spread</span>
                <span class="HomeDog" >Home</span>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="game" id="game" runat="server">
                    <span class="Away" id="away" runat="server"><asp:Label ID="lblAwayTeam" runat="server" /></span>
                    <span class="lblSpread"><asp:label ID="lblSpread" runat="server" /></span>
                    <span class="Home" id="home" runat="server"><asp:Label ID="lblHomeTeam" runat="server" /></span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
        <div><asp:label ID="lblPicks" runat="server" /></div>

</asp:Content>
