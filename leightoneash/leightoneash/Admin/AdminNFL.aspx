<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AdminNFL.aspx.vb" Inherits="leightoneash.AdminNFL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div><asp:Label ID="lblUser" class="leftCol" runat="server" Text="Password:" /><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" /></div>
    <div>
        <asp:Label id="lblAction" class="leftCol" runat="server" Text="Choose Action:" />
        <asp:Button ID="btnSpreads" runat="server" text="Set Spreads" />
        <asp:Button ID="btnResults" runat="server" text="Set Results" />
    </div>
    <ul class="GameView" id="GameList">
        <asp:Repeater ID="repGames" runat="server">
        <HeaderTemplate>
            <span class="GameTime">Time</span>
            <span class="rbtAway" >Pick</span>
            <span class="rbtAway" >Win</span>
            <span class="RecordAway" >Record</span>
            <span class="AwayDog" >Away</span>
            <span class="txtSpread" >Spread</span>
            <span class="HomeDog" >Home</span>
            <span class="RecordHome" >Record</span>
            <span class="rbtHome">Win</span>
            <span class="rbtHome">Pick</span>
        </HeaderTemplate>
            <ItemTemplate>
            <div class="game" id="game" runat="server">
                <span class="GameTime"><asp:Label ID="lblGameTime" runat="server" /></span>
                <span class="rbtAway"><asp:RadioButton ID="rbtAway" runat="server" GroupName="Pick" /></span>
                <span class="rbtAway"><asp:RadioButton ID="rbtAwayWin" runat="server" GroupName="Win" /></span>
                <span class="RecordAway"><asp:Label ID="lblAwayRecord" runat="server" /></span>
                <span class="Away" id="away" runat="server"><asp:Label ID="lblAwayTeam" runat="server" /></span>
                <span class="txtSpread"><asp:TextBox ID="txtSpread" runat="server" Width="25px" /></span>
                <span class="Home" id="home" runat="server"><asp:Label ID="lblHomeTeam" runat="server" /></span>
                <span class="RecordHome"><asp:Label ID="lblHomeRecord" runat="server" /></span>
                <span class="rbtHome"><asp:RadioButton ID="rbtHomeWin" runat="server" GroupName="Win" /></span>
                <span class="rbtAway"><asp:RadioButton ID="rbtHome" runat="server" GroupName="Pick" /></span>
            </div>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    
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
