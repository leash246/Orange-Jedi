<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Costabi.aspx.vb" Inherits="leightoneash.Costabi" EnableEventValidation="false"%>
<%@ Register Src="~/Cards/Controls/CostabiHand.ascx" TagPrefix="uctrl" TagName="Hand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Cards.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
                <span class="CostabiPlayer" style="width:50px;"><asp:Label runat="server" Width="50px" Text="Hand" /></span>
                <span class="CostabiPlayer"><asp:TextBox ID="txtPlayer1" runat="server" Width="100px" /></span>
                <span class="CostabiPlayer"><asp:TextBox ID="txtPlayer2" runat="server" Width="100px" /></span>
                <span class="CostabiPlayer"><asp:TextBox ID="txtPlayer3" runat="server" Width="100px" /></span>
                <span class="CostabiPlayer"><asp:TextBox ID="txtPlayer4" runat="server" Width="100px" /></span>
                <span class="CostabiPlayer"><asp:TextBox ID="txtPlayer5" runat="server" Width="100px" /></span>

        <asp:Repeater id="repCostabi" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <tr>
                        <span runat="server" class="CostabiPlayer" style="width:40px;"><asp:Label ID="lblHand" runat="server" /></span>
                        <asp:HiddenField ID="hfDirection" runat="server" />
                        <span id="spnPlayer1" runat="server" class="CostabiPlayer"><uctrl:Hand runat="server" id="Hand1" /></span>
                        <span id="spnPlayer2" runat="server" class="CostabiPlayer"><uctrl:Hand runat="server" id="Hand2" /></span>
                        <span id="spnPlayer3" runat="server" class="CostabiPlayer"><uctrl:Hand runat="server" id="Hand3" /></span>
                        <span id="spnPlayer4" runat="server" class="CostabiPlayer"><uctrl:Hand runat="server" id="Hand4" /></span>
                        <span id="spnPlayer5" runat="server" class="CostabiPlayer"><uctrl:Hand runat="server" id="Hand5" /></span>
                        <span runat="server" class="CostabiPlayer"><asp:Button ID="btnHand" runat="server" Text="Save Hand" CommandName="SaveHand" /></span>
                    </tr>
                </div>
            </ItemTemplate>
        </asp:Repeater>
</div>
</asp:Content>