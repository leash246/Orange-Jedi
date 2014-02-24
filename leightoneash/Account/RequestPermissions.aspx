<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RequestPermissions.aspx.vb" Inherits="leightoneash.RequestPermissions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Request Additional Permissions</h1>
        <h2>If you don't know what permissions to request, you probably won't get them.</h2>
    </div>
    <div>
        <h2>Enter request:</h2>
        <asp:TextBox ID="txtRequest" runat="server" Height="200px" Width="400px" TextMode=MultiLine />
        <asp:Button ID="btnRequest" runat="server" Text="Request" />
    </div>
</asp:Content>
