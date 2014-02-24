<%@ Page Title="" Language="vb" AutoEventWireup="false" 
MasterPageFile="~/Site.Master" CodeBehind="PitchStatistics.aspx.vb" 
Inherits="leightoneash.PitchStatistics" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:DropDownList runat="server" ID="ddlPlayers" />
            <asp:DropDownList runat="server" ID="ddlSize" >
            <asp:ListItem Text="" value=""/>
            <asp:ListItem Text="4" Value="4" />
            <asp:ListItem Text="6" value="6" />
        </asp:DropDownList>
        <%--<asp:RadioButton id="rbtTeammates" runat="server" Text="Teammates" GroupName="Stats" checked="true"/>
        <asp:RadioButton id="rbtOpponents" runat="server" Text="Opponents" GroupName="Stats" checked="false"/>--%>
        <asp:Button runat="server" ID="btnStatistics" Text="GO" />
    </div>
    <div id="Teammates">
        <asp:Chart ID="chtTeammates4" runat="server" Width="500px">
        <ChartAreas>
            <asp:ChartArea name="ChartArea1">
                <AxisY Maximum="1" Interval="1" />
            </asp:ChartArea>
        </ChartAreas>
        </asp:Chart>
        <asp:Chart ID="chtTeammates6" runat="server" Width="500px">
        <ChartAreas>
            <asp:ChartArea name="ChartArea1">
                <AxisY Maximum="1" Interval="1" />
            </asp:ChartArea>
        </ChartAreas>
        </asp:Chart>
    </div>
    <div id="Div1">
        <asp:Chart ID="chtOpponents4" runat="server" Width="500px">
        <ChartAreas>
            <asp:ChartArea name="ChartArea1">
                <AxisY Maximum="1" Interval="1" />
            </asp:ChartArea>
        </ChartAreas>
        </asp:Chart>
        <asp:Chart ID="chtOpponents6" runat="server" Width="500px">
        <ChartAreas>
            <asp:ChartArea name="ChartArea1">
                <AxisY Maximum="1" Interval="1" />
            </asp:ChartArea>
        </ChartAreas>
        </asp:Chart>
    </div>
</asp:Content>
