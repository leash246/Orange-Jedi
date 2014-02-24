<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="User.ascx.vb" Inherits="leightoneash.uctrlUser" %>

<table class="user">
    <tr>
        <td class="username"><asp:Label ID="lblUserName" runat="server" Text="" /> </td>
        <td class="name"><asp:Label ID="lblName" runat="server"  Text=""/></td>
        <td class="roles">
            <table>
                <tr>
                    <td><asp:CheckBox ID="chkAdmin" runat="server" Text="Admin" /></td>
                    <td><asp:CheckBox ID="chkDragons" runat="server" Text="Dragons" /></td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkNFL" runat="server" Text="NFL"/></td>
                    <td><asp:CheckBox ID="chkCards" runat="server" Text="Cards" /></td>
                </tr>
            </table>
            
        </td>
        <td class="save"><asp:Button ID="btnSave" runat="server" Text="Save" /></td>
    </tr>
</table>