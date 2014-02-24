<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Weapon.ascx.vb" Inherits="leightoneash.Weapon" %>

<table style="margin-bottom:0px;">
    <thead>
        <td class="h2"><asp:Label runat="server" Width="150px" Text="Attack" /></td>
        <td><asp:Label runat="server" Width="100px" Text="Attack Bonus" /></td>
        <td><asp:Label runat="server" Width="75px" Text="Damage" /></td>
        <td><asp:Label runat="server" Width="75px" Text="Critical" /></td>
    </thead>
    <tbody>
        <tr class="BlackOutline">
            <td><asp:TextBox ID="txtAttack" runat="server" Width="150px" /></td>
            <td><asp:TextBox ID="txtAttackBonus" runat="server" Width="100px" /></td>
            <td><asp:TextBox ID="txtDamage" runat="server" Width="75px" /></td>
            <td><asp:TextBox ID="txtCritical" runat="server" Width="75px" /></td>
        </tr>
    </tbody>
</table>
<table style="margin-top:0px;">
    <thead>
        <td><asp:Label runat="server" Width="50px" Text="Range" /></td>
        <td><asp:Label runat="server" Width="75px" Text="Type" /></td>
        <td><asp:Label runat="server" Width="275px" Text="Notes" /></td>
    </thead>
    <tbody>
        <tr class="BlackOutline">
            <td><asp:TextBox ID="txtRange" runat="server" Width="50px" /></td>
            <td><asp:TextBox ID="txtType" runat="server" Width="75px" /></td>
            <td><asp:TextBox ID="txtNotes" runat="server" Width="275px" /></td>
        </tr>
    </tbody>
</table>
<tr>
    <td><asp:Label runat="server" Width="75px" Text="Ammunition:" /></td>
    <td><asp:TextBox ID="txtAmmunitionType" runat="server" Width="100px" /></td>
    <asp:Repeater ID="rptAmmunitionChecks" runat="server">
        <ItemTemplate>
            <asp:CheckBox ID="chkAmmunition" Width="10px" runat="server" />
        </ItemTemplate>
    </asp:Repeater>
</tr>