<%@ Page Title="" Language="vb" AutoEventWireup="false" 
MasterPageFile="~/Site.Master" CodeBehind="PitchRankings.aspx.vb" 
Inherits="leightoneash.PitchRankings" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
    <asp:UpdatePanel runat="server" ID="upBoxes" >
        <ContentTemplate>
            <table style="display:inline-block;">
                <tr>
                    <td><asp:ListBox ID="lstTeamOne" runat="server" Width="200px" Height="350px" /></td>
                    <td><asp:ListBox ID="lstUnassigned" runat="server" Width="200px" Height="350px" /></td>
                    <td><asp:ListBox ID="lstTeamTwo" runat="server" Width="200px" Height="350px" /></td>
                </tr>
                <tr>
                    <td><asp:Label runat="server" Width="125px" /><asp:Button ID="btnRemoveOne" runat="server" Width="75px" Text="Remove" Enabled="false" /></td>
                    <td><asp:Button ID="btnAddOne" runat="server" Width="30px" Text="&lt;&lt;" /><asp:Label runat="server" Width="140px" /><asp:Button ID="btnAddTwo" runat="server" Width="30px" text="&gt;&gt;" /></td>
                    <td><asp:Button ID="btnRemoveTwo" runat="server" Width="75px" Text="Remove" Enabled="false" /><asp:Label runat="server" Width="125px" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblTeamOneELO" runat="server" width="150px" /></td>
                    <td />
                    <td><asp:Label ID="lblTeamTwoELO" runat="server" width="150px" /></td>
                </tr>
                <tr>
                    <td style="text-align:center;"><asp:Button ID="btnTeamOneWin" runat="server" Text="Win" Width="125px" /></td>
                    <td style="text-align:center;"><asp:Button ID="btnDraw" runat="server" Text="Draw" Width="125px" /></td>
                    <td style="text-align:center;"><asp:Button ID="btnTeamTwoWin" runat="server" Text="Win" Width="125px" /></td>
                </tr>
            </table>
    <ul style="float:right;margin-right:50px;display:inline-block;">
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
    *Provisional ranking until 30 games played
    </ul>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div>
        <asp:Chart ID="chtPitchRankings" runat="server" Width="1200px" Height="500px" ToolTip="Pitch Players" >
            <Legends>
                <asp:Legend />
            </Legends>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisY Minimum="1200" Maximum="1800" Title="ELO">
                        <MajorGrid LineColor="LightGray" />
                    </AxisY>
                    <AxisX Minimum="0" Title="Game" >
                        <MajorGrid LineColor="LightGray" Interval="10" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
    
</asp:Content>
