<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pitch.aspx.vb" Inherits="leightoneash.Pitch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Cards.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <span class="PitchColRight"><asp:Label ID="Label2" runat="server" Text="" /></span>    
        <span class="PitchColRight"  ><asp:Label ID="Label5" runat="server" Text="Teams"/></span> 
        <span class="PitchColRight"><asp:TextBox runat="server" ID="txtTeam1" CssClass="PitchTeam" /></span>
        <span class="PitchCol" ><asp:TextBox runat="server" ID="txtTeam2" CssClass="PitchTeam" /></span>
    </div>
    <div>
        <span class="PitchColRight" ><asp:Label ID="Label4" runat="server"/></span>
        <span class="PitchColRight"><asp:Label ID="Label3" runat="server" Text="Hand" /></span>   
        <span class="PitchColRight" ><asp:TextBox runat="server" ID="txtScore1" CssClass="PitchPoints" /></span>
        <span class="PitchCol" ><asp:TextBox runat="server" ID="txtScore2" CssClass="PitchPoints" /></span>
        <asp:Button runat="server" ID="btnAddScore" Text="Add Score" />
    </div>

    <div>
        <asp:Repeater runat="server" ID="repHands">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <span class="PitchColRight"><asp:Label ID="Label1" runat="server" Text="" /></span>
                    <span class="PitchColRight"><asp:Label runat="server" ID="lblHand" /></span>
                    <span class="PitchColRight"><asp:Label runat="server" ID="lblScore1" /></span>
                    <span class="PitchCol" ><asp:Label runat="server" ID="lblScore2" /></span>
                </div>
            </ItemTemplate>
            <FooterTemplate>
            <br /><br />
                <span class="PitchColRight"><asp:Label runat="server" Text="Total:" /></span>
                <span class="PitchColRight"><asp:Label runat="server" ID="lblHandTotal" /></span>
                <span class="PitchColRight"><asp:Label runat="server" ID="lblTeam1Total" /></span>
                <span class="PitchCol"><asp:Label runat="server" ID="lblTeam2Total" /></span>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div>
        <asp:Button runat="server" ID="btnClear" Text="Clear Game" />
    </div>
</asp:Content>
