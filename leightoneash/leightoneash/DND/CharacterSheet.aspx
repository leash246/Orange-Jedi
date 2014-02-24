<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CharacterSheet.aspx.vb" Inherits="leightoneash.CharacterSheet" %>
<%@ Register Src="~/DND/Controls/Abilities.ascx" TagPrefix="uctrl" TagName="Abilities" %>
<%@ Register Src="~/DND/Controls/Defenses.ascx" TagPrefix="uctrl" TagName="Defenses" %>
<%@ Register Src="~/DND/Controls/Saves.ascx" TagPrefix="uctrl" TagName="Saves" %>
<%@ Register Src="~/DND/Controls/Weapon.ascx" TagPrefix="uctrl" TagName="Weapon" %>
<%@ Register Src="~/DND/Controls/SkillLine.ascx" TagPrefix="uctrl" TagName="SkillLine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/DnD.js"></script>
    <link href="../Styles/DnD.css" rel="Stylesheet" type="text/css" />
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
    <div style="display:inline-block;"><uctrl:Abilities id="ctrlAbilities" runat="server" />  </div>
    <div style="display:inline-block;"><uctrl:Defenses id="ctrlDefenses" runat="server" /></div>
</div>
<div>
    <div style="display:inline-block;">
        <div><uctrl:Saves id="ctrlSaves" runat="server" /></div>
        <div>
            <asp:Repeater ID="repWeapons" runat="server">
                <ItemTemplate>
                    <div><uctrl:Weapon id="ctrlWeapon" runat="server" /></div>
                    <div><br /></div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div style="display:inline-block;">
        <asp:Repeater ID="repSkills" runat="server">
            <HeaderTemplate>
                <table>
                    <thead>
                        <td><asp:Label runat="server" Text="Class skill?" Width="40px" /></td>
                        <td><asp:Label runat="server" Text="Skill Name" /></td>
                        <td><asp:Label runat="server" Text="Key Ability" Width="40px" /></td>
                        <td><asp:Label runat="server" Text="Skill Modifier" Width="40px" /></td>
                        <td />
                        <td><asp:Label runat="server" Text="Ability Modifier" Width="40px" /></td>
                        <td />
                        <td><asp:Label runat="server" Text="Ranks" Width="40px" /></td>
                        <td />
                        <td><asp:Label runat="server" Text="Misc Modifier" Width="40px" /></td>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <uctrl:SkillLine ID="ctrlSkillLine" runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>  
</div>
</asp:Content>
