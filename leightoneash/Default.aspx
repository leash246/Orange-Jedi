<%@ Page Title="" Language="vb" AutoEventWireup="false" 
MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.vb" 
Inherits="leightoneash._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!-- Main jumbotron for a primary marketing message or call to action -->
    <div class="jumbotron">
      <div class="container">
        <h1>Hello, world!</h1>
        <p>The main function of this website is to track card-playing statistics. Log in and record stats, if you dare!</p>
        <%--<p><a class="btn btn-primary btn-lg" role="button">Learn more &raquo;</a></p>--%>
      </div>
    </div>

    <div class="container">
      <!-- Example row of columns -->
      <div class="row">
        <div class="col-md-4">
          <h2>Costabi Rankings</h2>
          <p>Go here to view/update rankings for Costabi. You must be logged in to a valid account to actually update rankings.</p>
          <p><a class="btn btn-default" href="/Cards/CostabiRankings.aspx" role="button">Take me there! &raquo;</a></p>
        </div>
        <div class="col-md-4">
          <h2>Pitch Rankings</h2>
          <p>Go here to view/update rankings for Pitch. You must be logged in to a valid account to actually update rankings.</p>
          <p><a class="btn btn-default" href="/Cards/PitchRankings.aspx" role="button">Take me there! &raquo;</a></p>
       </div>
        <div class="col-md-4">
          <h2>Pitch Statistics</h2>
          <p>Go here to view various statistics regarding teammates and opponents for pitch players.</p>
          <p><a class="btn btn-default" href="/Cards/PitchStatistics.aspx" role="button">Take me there! &raquo;</a></p>
        </div>
      </div>

      <hr>

    </div> <!-- /container -->

</asp:Content>
