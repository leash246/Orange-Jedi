<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Picks.aspx.vb" Inherits="leightoneash.Picks" %>

    <%@ Register Src="~/NFL/Controls/Game.ascx" TagPrefix="uctrl" TagName="GameLine" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="lblUser" class="leftCol" runat="server" Text="User:" /><asp:TextBox ID="txtUser" runat="server" />
    <ul class="GameView" id="GameList">
        <asp:Repeater ID="repGames" runat="server">
        <HeaderTemplate>
            <span class="GameTime">Time</span>
            <span class="rbtAway" >Pick</span>
            <span class="RecordAway" >Record</span>
            <span class="AwayDog" >Away</span>
            <span class="lblSpread" >Spread</span>
            <span class="HomeDog" >Home</span>
            <span class="RecordHome" >Record</span>
            <span class="rbtHome">Pick</span>
        </HeaderTemplate>
            <ItemTemplate>
            <div class="game" id="game" runat="server">
                    <span class="GameTime"><asp:Label ID="lblGameTime" runat="server" /></span>
                    <span class="rbtAway"><asp:RadioButton ID="rbtAway" runat="server" GroupName="Pick" /></span>
                    <span class="RecordAway"><asp:Label ID="lblAwayRecord" runat="server" Text="0-0"></asp:Label></span>
                    <span class="Away" id="away" runat="server"><asp:Label ID="lblAwayTeam" runat="server" Text="Away Team"></asp:Label></span>
                    <span class="lblSpread"><asp:Label ID="lblSpread" runat="server" Text="Pick"></asp:Label></span>
                    <span class="Home" id="home" runat="server"><asp:Label ID="lblHomeTeam" runat="server" Text="Home Team"></asp:Label></span>
                    <span class="RecordHome"><asp:Label ID="lblHomeRecord" runat="server" Text="0-0"></asp:Label></span>
                    <span class="rbtAway"><asp:RadioButton ID="rbtHome" runat="server" GroupName="Pick" /></span>
             </div>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <br />
    <div class="Center">
        <asp:Label ID="lblTotalScore" runat="server" text="Total Score of Monday Night game"/>
        <br />
        <asp:TextBox ID="txtOverUnder" runat="server" />
        <br />
        <asp:Label ID="lblOverUnder" runat="server" text="(Over/Under #)"/>
        <br />
        <br />
            <asp:Button Text="Submit" ID="btnSubmit" runat="server" />
            </div>
</asp:Content>
