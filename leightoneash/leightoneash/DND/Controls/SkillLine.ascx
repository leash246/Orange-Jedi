<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SkillLine.ascx.vb" Inherits="leightoneash.SkillLine" %>


<tr>
    <td><asp:CheckBox ID="chkClassSkill" runat="server" /></td>
    <td>
        <asp:Label ID="lblSkillName" runat="server" /><asp:Label ID="lblUntrained" runat="server" text=" &#x25A0;" Visible="false" />
        <asp:Label ID="lblLeftParen" runat="server" Text="(" Visible="false"/>
        <asp:TextBox ID="txtSkillOption" runat="server" Visible="false" />
        <asp:Label ID="lblRightParen" runat="server" Text=")" Visible="false" />
    </td>
    <td><asp:Label ID="lblAbility" runat="server" /><asp:Label ID="lblArmorCheck" runat="server" Visible="false" Text="*" /></td>
    <td style="border:1px solid black;text-align:center;"><asp:Label ID="lblSkillMod" runat="server" /></td>
    <td>=</td>
    <td style="text-align:center;"><asp:Label ID="lblAbilityMod" runat="server" /></td>
    <td>+</td>
    <td><asp:TextBox ID="txtRanks" runat="server" Width="40px" /></td>
    <td>+</td>
    <td><asp:TextBox ID="txtMisc" runat="server" Width="40px" /></td>
</tr>