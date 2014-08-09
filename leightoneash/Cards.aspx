<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Cards.aspx.vb" Inherits="leightoneash.CardsDefault" %>
    
<%@ Register Src="~/Cards/Controls/PitchRankingsFull.ascx" TagPrefix="uctrl" TagName="PitchRankings" %>
<%@ Register Src="~/Cards/Controls/PitchStatisticsFull.ascx" TagPrefix="uctrl" TagName="PitchStats" %>
<%@ Register Src="~/Cards/Controls/PitchScoresheet.ascx" TagPrefix="uctrl" TagName="PitchScoresheet" %>
<%@ Register Src="~/Cards/Controls/CostabiRankingsFull.ascx" TagPrefix="uctrl" TagName="CostabiRankings" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../Styles/Cards.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div id="PS">
    <asp:Label runat="server" Height="50px" />
    <h2>Pitch Statistics:</h2>
    <uctrl:PitchStats runat="server" id="uctrlPitchStats"></uctrl:PitchStats>
</div>    
<div id="Scoresheet">
    <asp:Label runat="server" Height="50px" />
    <h2>Pitch Scoresheet:</h2>
    <uctrl:PitchScoresheet runat="server" id="uctrlPitchScoresheet"></uctrl:PitchScoresheet>
</div>
<div id="PR">
    <asp:Label runat="server" Height="50px" />
    <h2>Pitch Recording:</h2>
    <uctrl:PitchRankings runat="server" id="uctrlPitchRankings"></uctrl:PitchRankings>
</div>
<div id="CR">
    <asp:Label runat="server" Height="50px" />
    <h2>Costabi Recording:</h2>
    <uctrl:CostabiRankings runat="server" id="uctrlCostabiRankings"></uctrl:CostabiRankings>
</div>
<div>
    <asp:Label runat="server" Height="1200px"></asp:Label>
</div>
</asp:Content>
