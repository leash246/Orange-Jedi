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
        <p>This is a template for a simple marketing or informational website. It includes a large callout called a jumbotron and three supporting pieces of content. Use it as a starting point to create something more unique.</p>
        <p><a class="btn btn-primary btn-lg" role="button">Learn more &raquo;</a></p>
      </div>
    </div>

    <div class="container">
      <!-- Example row of columns -->
      <div class="row">
        <div class="col-md-4">
          <h2>Costabi Rankings</h2>
          <p>Go here to view/update rankings for Costabi. You must be logged in to a valid account to actually update rankings.</p>
          <p><a class="btn btn-default" href="/Cards/CostabiRankings.aspx" role="button">View details &raquo;</a></p>
        </div>
        <div class="col-md-4">
          <h2>Pitch Rankings</h2>
          <p>Go here to view/update rankings for Pitch. You must be logged in to a valid account to actually update rankings.</p>
          <p><a class="btn btn-default" href="/Cards/PitchRankings.aspx" role="button">View details &raquo;</a></p>
       </div>
        <div class="col-md-4">
          <h2>Pitch Statistics</h2>
          <p>Go here to view various statistics regarding teammates and opponents for pitch players.</p>
          <p><a class="btn btn-default" href="/Cards/PitchStatistics.aspx" role="button">View details &raquo;</a></p>
        </div>
      </div>

      <hr>

      <footer>
        <p>&copy; Leighton Eash 2014</p>
      </footer>
    </div> <!-- /container -->

</asp:Content>
