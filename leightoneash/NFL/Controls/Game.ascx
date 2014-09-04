<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Game.ascx.vb" Inherits="leightoneash.Game" %>

<div class="rowGame">
    <span><asp:RadioButton ID="rbtAway" runat="server" /></span>
    <span class="RecordAway"><asp:Label ID="lblAwayRecord" runat="server" Text="0-0"></asp:Label></span>
    <span class="Team"><asp:Label ID="lblAwayTeam" runat="server" Text="Away Team"></asp:Label></span>
    <span class="Spread"><asp:Label ID="lblSpread" runat="server" Text="Pick"></asp:Label></span>
    <span class="Team"><asp:Label ID="lblHomeTeam" runat="server" Text="Home Team"></asp:Label></span>
    <span class="RecordHome"><asp:Label ID="lblHomeRecord" runat="server" Text="0-0"></asp:Label></span>
    <span><asp:RadioButton ID="rbtHome" runat="server" /></span>
</div>
