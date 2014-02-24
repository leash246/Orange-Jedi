<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SubMenu.ascx.vb" Inherits="leightoneash.SubMenu" %>

<li class="pureCssMenui blueBackground" id="li" runat="server">
    <a class="pureCssMenui watching" href="" id="lnk" runat="server" >
        <asp:Label ID="lblLabel" runat="server" Text=""></asp:Label>
    </a>
    <asp:PlaceHolder runat="server" ID="phContent" />
    <ul class="pureCssMenum" id="mnuSub" runat="server"></ul>
</li>

<input type="hidden" runat="server" id="txtPage" />