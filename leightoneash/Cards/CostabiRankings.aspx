﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CostabiRankings.aspx.vb" Inherits="leightoneash.CostabiRankings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
    <table style="display:inline-block;">
        <tr>
            <td><asp:ListBox ID="lstUnassigned" runat="server" Width="200px" Height="300px" /></td>
            <td><asp:ListBox ID="lstPlayers" runat="server" Width="200px" Height="300px" /></td>
        </tr>
        <tr>
            <td><asp:Label runat="server" Width="170px" /><asp:Button ID="btnAdd" runat="server" Width="30px" text="&gt;&gt;" /></td>
            <td><asp:Button ID="btnRemove" runat="server" Width="60px" Text="Remove" Enabled="false" /><asp:Label runat="server" Width="140px" /></td>
        </tr>
        <tr>
            <td />
            <td style="text-align:center;"><asp:Button ID="btnUpdate" runat="server" Text="Update" Width="125px" /></td>
        </tr>
    </table>
    <ul style="float:right;margin-right:100px;display:inline-block;">
        <li>2400 and above: Senior Master</li>
        <li>2200–2399 plus 300 games above 2200: Original Life Master</li>
        <li>2200–2399: National Master</li>
        <li>2000–2199: Expert</li>
        <li>1800–1999: Class A</li>
        <li>1600–1799: Class B</li>
        <li>1400–1599: Class C</li>
        <li>1200–1399: Class D</li>
        <li>1000–1199: Class E</li>
        <li>800–999: Class F</li>
        <li>600–799: Class G</li>
        <li>400–599: Class H</li>
        <li>200–399: Class I</li>
        <li>100–199: Class J</li>
    <br />
    *Provisional ranking until 10 games played
    
    </ul>
    </div>
</asp:Content>