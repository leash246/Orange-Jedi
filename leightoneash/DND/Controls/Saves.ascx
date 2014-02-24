<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Saves.ascx.vb" Inherits="leightoneash.Saves" %>
<table> 
    <thead>
        <td><asp:Label runat="server" Width="75px" Text="Saving Throws" /></td>
        <td><asp:Label runat="server" Width="40px" Text="TOTAL" /></td>
        <td style="border-width:0px;" />
        <td><asp:Label runat="server" Width="40px" Text="Base Save" /></td>
        <td style="border-width:0px;" />
        <td><asp:Label runat="server" Width="40px" Text="Ability Modifier" /></td>
        <td style="border-width:0px;" />
        <td><asp:Label runat="server" Width="40px" Text="Magic Modifier" /></td>
        <td style="border-width:0px;" />
        <td><asp:Label runat="server" Width="40px" Text="Magic Modifier" /></td>
        <td style="border-width:0px;" />
        <td><asp:Label runat="server" Width="40px" Text="Temporary Modifier" /></td>
    </thead>
    <tbody>
        <tr class="BlackOutline">
            <td><asp:Label runat="server" Width="75px" Text="Fortitude (CON)" /></td>
            <td><asp:Label ID="lblFortSave" runat="server" Width="40px" Text="TOTAL" /></td>
            <td style="border-width:0px;"> = </td>
            <td><asp:TextBox ID="txtFortBase" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Fortitude" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:Label ID="lblConSave" runat="server" Width="40px" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:TextBox ID="txtMagicModFort" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Fortitude" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:TextBox ID="txtMiscModFort" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Fortitude" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:TextBox ID="txtTempModFort" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Fortitude" /></td>
        </tr>
        <tr class="BlackOutline">
            <td><asp:Label runat="server" Width="75px" Text="Reflex (DEX)" /></td>
            <td><asp:Label ID="lblRefSave" runat="server" Width="40px" Text="TOTAL" /></td>
            <td style="border-width:0px;"> = </td>
            <td><asp:TextBox ID="txtRefBase" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Reflex" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:Label ID="lblDexSave" runat="server" Width="40px" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:TextBox ID="txtMagicModRef" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Reflex" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:TextBox ID="txtMiscModRef" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Reflex" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:TextBox ID="txtTempModRef" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Reflex" /></td>
        </tr>
        <tr class="BlackOutline">
            <td><asp:Label runat="server" Width="75px" Text="Will (WIS)" /></td>
            <td><asp:Label ID="lblWillSave" runat="server" Width="40px" Text="TOTAL" /></td>
            <td style="border-width:0px;"> = </td>
            <td><asp:TextBox ID="txtWillBase" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Will" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:Label ID="lblWisSave" runat="server" Width="40px" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:TextBox ID="txtMagicModWill" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Will" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:TextBox ID="txtMiscModWill" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Will" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:TextBox ID="txtTempModWill" runat="server" Width="40px" OnChange="SetSavingThrow(this);" Title="Will" /></td>
        </tr>
    </tbody>
</table>
<table>
    <tbody>
        <tr class="BlackOutline">
            <td><asp:Label runat="server" Width="150px" Text="Base Attack Bonus" /></td>
            <td><asp:TextBox ID="txtBaseAttack" runat="server" Width="40px" OnChange="SetGrapple('BaseAttack',this.value);__doPostback();" /></td>
            <td style="border-width:0px;width:15px;" />
            <td><asp:Label runat="server" Width="75px" Text="Spell Resistance" /></td>
            <td><asp:TextBox ID="txtSpellResist" runat="server" Width="40px" /></td>
        </tr>
    </tbody>
</table>
<table>
    <tbody>
        <tr class="BlackOutline">
            <td><asp:Label runat="server" Width="125px" Text="Grapple Modifier" /></td>
            <td><asp:Label ID="lblGrapple" runat="server" Width="40px" /></td>
            <td style="border-width:0px;"> = </td>
            <td><asp:Label ID="lblGrappleBaseAttack" runat="server" Width="40px" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:Label ID="lblGrappleStr" runat="server" Width="40px" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:Label ID="lblGrappleSize" runat="server" Width="40px" /></td>
            <td style="border-width:0px;"> + </td>
            <td><asp:TextBox ID="txtGrappleMisc" runat="server" Width="40px" OnChange="SetGrapple();" /></td>
        </tr>
    </tbody>
    <tfoot>
        <td><asp:Label runat="server" Width="125px" /></td>
        <td><asp:Label runat="server" Width="40px" Text="TOTAL" /></td>
        <td />
        <td><asp:Label runat="server" Width="40px" Text="Base Attack" /></td>
        <td />
        <td><asp:Label runat="server" Width="40px" Text="Strength Modifier" /></td>
        <td />
        <td><asp:Label runat="server" Width="40px" Text="Size Modifier" /></td>
        <td />
        <td><asp:Label runat="server" Width="40px" Text="Misc Modifier" /></td>
    </tfoot>
</table>