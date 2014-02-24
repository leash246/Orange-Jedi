<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Abilities.ascx.vb" Inherits="leightoneash.Abilities" %>
<table>
    <thead>
        <td><asp:Label runat="server" Width="75px" Text="Ability Name" /></td>
        <td><asp:Label runat="server" Width="50px" Text="Ability Score" /></td>
        <td><asp:Label runat="server" Width="50px" Text="Ability Modifier" /></td>
        <td><asp:Label runat="server" Width="50px" text="Temp Score" /></td>
        <td><asp:Label runat="server" Width="50px" Text="Temp Modifer" /></td>
    </thead>
    <tbody>
        <tr class="BlackOutline">
            <td style="text-align:left;">Strength</td>
            <td><asp:DropDownList ID="ddlStr" runat="server" Width="50px" OnChange="SetAbilityLabel(this);" Title="Str"/></td>
            <td><asp:Label ID="lblStr" runat="server" /></td>
            <td><asp:TextBox ID="txtStrTemp" runat="server" Width="45px" OnChange="SetTempAbilityLabel(this);" Title="Str" /></td>
            <td><asp:Label ID="lblStrTemp" runat="server" /></td>
        </tr>
        <tr class="BlackOutline">
            <td style="text-align:left;">Dexterity</td>
            <td><asp:DropDownList ID="ddlDex" runat="server" Width="50px" OnChange="SetAbilityLabel(this);" Title="Dex" /></td>
            <td><asp:Label ID="lblDex" runat="server" /></td>
            <td><asp:TextBox ID="txtDexTemp" runat="server" Width="45px" OnChange="SetTempAbilityLabel(this);" Title="Dex" /></td>
            <td><asp:Label ID="lblDexTemp" runat="server" /></td>
        </tr>
        <tr class="BlackOutline">
            <td style="text-align:left;">Constitution</td>
            <td><asp:DropDownList ID="ddlCon" runat="server" Width="50px" OnChange="SetAbilityLabel(this);" Title="Con" /></td>
            <td><asp:Label ID="lblCon" runat="server" /></td>
            <td><asp:TextBox ID="txtConTemp" runat="server" Width="45px" OnChange="SetTempAbilityLabel(this);" Title="Con" /></td>
            <td><asp:Label ID="lblConTemp" runat="server" /></td>
        </tr>
        <tr class="BlackOutline">
            <td style="text-align:left;">Intelligence</td>
            <td><asp:DropDownList ID="ddlInt" runat="server" Width="50px" OnChange="SetAbilityLabel(this);" Title="Int" /></td>
            <td><asp:Label ID="lblInt" runat="server" /></td>
            <td><asp:TextBox ID="txtIntTemp" runat="server" Width="45px" OnChange="SetTempAbilityLabel(this);" Title="Int" /></td>
            <td><asp:Label ID="lblIntTemp" runat="server" /></td>
        </tr>
        <tr class="BlackOutline">
            <td style="text-align:left;">Wisdom</td>
            <td><asp:DropDownList ID="ddlWis" runat="server" Width="50px" OnChange="SetAbilityLabel(this);" Title="Wis" /></td>
            <td><asp:Label ID="lblWis" runat="server" /></td>
            <td><asp:TextBox ID="txtWisTemp" runat="server" Width="45px" OnChange="SetTempAbilityLabel(this);" Title="Wis" /></td>
            <td><asp:Label ID="lblWisTemp" runat="server" /></td>
        </tr>
        <tr class="BlackOutline">
            <td style="text-align:left;">Charisma</td>
            <td><asp:DropDownList ID="ddlCha" runat="server" Width="50px" OnChange="SetAbilityLabel(this);" Title="Cha" /></td>
            <td><asp:Label ID="lblCha" runat="server" /></td>
            <td><asp:TextBox ID="txtChaTemp" runat="server" Width="45px" OnChange="SetTempAbilityLabel(this);" Title="Cha" /></td>
            <td><asp:Label ID="lblChaTemp" runat="server" /></td>
        </tr>
    </tbody>
</table>