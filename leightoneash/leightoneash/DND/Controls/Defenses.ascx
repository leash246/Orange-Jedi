<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Defenses.ascx.vb" Inherits="leightoneash.Defenses" %>
<table>
    <tbody>
        <thead>
            <td><asp:Label runat="server" Width="75px" Text="" /></td>
            <td><asp:Label runat="server" Width="55px" Text="Total" /></td>
            <td><asp:Label runat="server" Width="100px" Text="Wounds" /></td>
            <td></td>
            <td><asp:Label runat="server" Width="45px" Text="Current HP" /></td>
        </thead>
        <tr class="BlackOutline">
            <td><asp:Label runat="server" Width="75px" Text="Hit Points" /></td>
            <td><asp:DropDownList id="ddlMaxHP" runat="server" Width="50px" OnChange="ChangedHP(this);" /></td>
            <td><asp:TextBox ID="txtWounds" runat="server" Width="100px" OnChange="ChangedHP(this);" /></td>
            <td style="border:0px solid black;"> = </td>
            <td><asp:Label ID="lblCurrentHP" runat="server" /></td>
        </tr>
    </tbody>
</table>
<table>
    <tbody>
        <tr class="BlackOutline">
            <td><asp:Label runat="server" Width="75px" Text="Armor Class" /></td>
            <td style="text-align:center;"><b><asp:Label ID="lblAC" runat="server" Width="45px" /></b></td>
            <td style="border:0px solid black;"> = 10 + </td>
            <td><asp:TextBox ID="txtArmorBonus" runat="server" Width="40px" /></td>
            <td style="border:0px solid black;"> + </td>
            <td><asp:TextBox ID="txtShieldBonus" runat="server" Width="40px" /></td>
            <td style="border:0px solid black;"> + </td>
            <td><asp:Label ID="lblDexMod" runat="server" Width="40px" /></td>
            <td style="border:0px solid black;"> + </td>
            <td><asp:TextBox ID="txtSizeMod" runat="server" Width="40px" OnChange="SetGrapple('Size',this.value);" /></td>
            <td style="border:0px solid black;"> + </td>
            <td><asp:TextBox ID="txtNaturalArmor" runat="server" Width="40px" /></td>
            <td style="border:0px solid black;"> + </td>
            <td><asp:TextBox ID="txtDeflectionMod" runat="server" Width="40px" /></td>
            <td style="border:0px solid black;"> + </td>
            <td><asp:TextBox ID="txtMiscMod" runat="server" Width="40px" /></td>
        </tr>
    </tbody>
    <tfoot>
        <td><asp:Label runat="server" Width="75px" Text="" /></td>
        <td><asp:Label runat="server" Width="45px" Text="TOTAL" /></td>
        <td style="border:0px solid black;"></td>
        <td><asp:Label runat="server" Width="40px" Text="Armor Bonus" /></td>
        <td style="border:0px solid black;"></td>
        <td><asp:Label runat="server" Width="40px" Text="Shield Bonus" /></td>
        <td style="border:0px solid black;"></td>
        <td><asp:Label runat="server" Width="40px" Text="Dex Modifier" /></td>
        <td style="border:0px solid black;"></td>
        <td><asp:Label runat="server" Width="40px" Text="Size Modifer" /></td>
        <td style="border:0px solid black;"></td>
        <td><asp:Label runat="server" Width="40px" Text="Natural Armor" /></td>
        <td style="border:0px solid black;"></td>
        <td><asp:Label runat="server" Width="40px" Text="Deflection Modifier" /></td>
        <td style="border:0px solid black;"></td>
        <td><asp:Label runat="server" Width="40px" Text="Misc Modifier" /></td>
    </tfoot>
</table>
<table>
    <tbody>
        <tr class="BlackOutline">
            <td><asp:Label runat="server" Width="75px" Text="Touch AC" /></td>
            <td><b><asp:Label ID="lblTouch" runat="server" Width="40px" /></b></td>
            <td><asp:Label runat="server" Width="75px" Text="Flat-Footed AC" /></td>
            <td><b><asp:Label ID="lblFlat" runat="server" Width="40px" /></b></td>
            <td style="border:0px solid black;"><asp:Label runat="server" Width="50px" /></td>
            <td><asp:Label ID="Label1" runat="server" Width="100px" Text="Initiative Modifier" /></td>
            <td><b><asp:Label ID="lblInitMod" runat="server" Width="40px" /></b></td>
            <td style="border:0px solid black;">=</td>
            <td><asp:Label ID="lblDexInit" runat="server" Width="40px" /></td>
            <td style="border:0px solid black;">+</td>
            <td><asp:TextBox ID="txtMiscInit" runat="server" Width="40px" OnChange="SetInitiativeLabel(this);" /></td>
        </tr>
    </tbody>
    <tfoot>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        <td><asp:Label ID="Label2" runat="server" Width="100px" Text="" /></td>
        <td><asp:Label ID="Label3" runat="server" Width="40px" Text="TOTAL" /></td>
        <td style="border:0px solid black;"></td>
        <td><asp:Label ID="Label4" runat="server" Width="40px" Text="Dex Modifier" /></td>
        <td style="border:0px solid black;"></td>
        <td><asp:Label ID="Label5" runat="server" Width="40px" Text="Misc Modifier" /></td>
    </tfoot>
</table>