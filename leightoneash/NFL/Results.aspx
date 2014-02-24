<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Results.aspx.vb" Inherits="leightoneash.Results" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
    <asp:DropDownList ID="ddlWeeks" runat="server" />
</div>
<ul class="Results" id="ResultsList">
    <asp:Repeater ID="repResults" runat="server">
        <HeaderTemplate>
            <span class="ResultsUser"><h2>User</h2></span>
            <span class="ResultsCorrect"><h2>Correct Picks</h2></span>
            <span class="ResultsTiebreaker"><h2>Tiebreaker difference</h2></span>
        </HeaderTemplate>
        <ItemTemplate>
            <div>
                <span class="ResultsUser"><asp:Label ID="lblUser" runat="server" Text="User" /></span>
                <span class="ResultsCorrect"><asp:Label ID="lblCorrect" runat="server" Text="Correct" /></span>
                <span class="ResultsTiebreaker"><asp:Label ID="lblTiebreaker" runat="server" Text="Tiebreaker" /></span>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</ul>
</asp:Content>
