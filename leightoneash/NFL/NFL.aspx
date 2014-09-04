<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="NFL.aspx.vb" Inherits="leightoneash.NFLDefault" %>

    <%@ Register Src="~/NFL/Controls/Game.ascx" TagPrefix="uctrl" TagName="GameLine" %>
    
    <link href="/Styles/NFL.css" rel="stylesheet" />
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <asp:Label ID="lblMessage" runat="server" />
    </div>
</asp:Content>
