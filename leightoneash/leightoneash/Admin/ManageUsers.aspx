<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ManageUsers.aspx.vb" Inherits="leightoneash.ManageUsers" %>
<%@ Register Src="~/Admin/User.ascx" TagPrefix="uctrl" TagName="user" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="user">
        <asp:Repeater ID="repUsers" runat="server">
            <HeaderTemplate>
                <tr>
                    <td class="username">Username</td>
                    <td class="name">Name</td>
                    <td class="roles">Roles</td>
                    <td class="save" />
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <uctrl:user id="ctrlUser" runat="server" />
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
