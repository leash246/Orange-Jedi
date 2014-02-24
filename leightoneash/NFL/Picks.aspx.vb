Imports leightoneash.Schedule
Imports System.IO
Imports System.Xml
Public Class Picks
    Inherits System.Web.UI.Page
    Dim gmGames As New Schedule.Games
    Dim nWeek As Integer
    Dim nOverUnder As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim xmldoc As New XmlDataDocument

        Dim fi As FileInfo = New FileInfo(Server.MapPath(RelativePath("NFL", "XML/Standings.xml")))
        Dim fs As FileStream = fi.OpenRead
        xmldoc.Load(fs)
        Dim cWeek As String = xmldoc.SelectSingleNode("Standings").Attributes("week").Value.ToString
        Integer.TryParse(cWeek, nWeek)
        If Not IsPostBack Then
            nOverUnder = 0
            LoadWeek(nWeek, gmGames)

            repGames.DataSource = gmGames
            repGames.DataBind()
            lblOverUnder.Text = "(Over/Under " & nOverUnder & ")"
        End If
    End Sub

    Private Sub repGames_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repGames.ItemDataBound
        Dim oItem As RepeaterItem = e.Item

        If (oItem.ItemType = ListItemType.Item) Or (oItem.ItemType = ListItemType.AlternatingItem) Then
            Dim g As Schedule.Game = CType(oItem.DataItem, Schedule.Game)
            Dim lblAwayRecord As Label = CType(oItem.FindControl("lblAwayRecord"), Label)
            Dim lblAwayTeam As Label = CType(oItem.FindControl("lblAwayTeam"), Label)
            Dim lblSpread As Label = CType(oItem.FindControl("lblSpread"), Label)
            Dim lblHomeTeam As Label = CType(oItem.FindControl("lblHomeTeam"), Label)
            Dim lblHomeRecord As Label = CType(oItem.FindControl("lblHomeRecord"), Label)
            Dim lblGameTime As Label = CType(oItem.FindControl("lblGameTime"), Label)
            lblAwayRecord.Text = g.AwayRecord
            lblAwayTeam.Text = g.AwayTeam
            lblSpread.Text = Math.Abs(Math.Round(CDec(g.Spread), 1))
            lblHomeTeam.Text = g.HomeTeam
            lblHomeRecord.Text = g.HomeRecord
            nOverUnder = Math.Max(g.OverUnder, nOverUnder)
            Dim strGameTime As String = ""
            Select Case g.Day
                Case "T"
                    strGameTime = "Thursday"
                Case "S"
                    strGameTime = "Sunday"
                Case "M"
                    strGameTime = "Monday"
            End Select
            lblGameTime.Text = String.Format("{0} {1}:{2}", strGameTime, If(Left(g.Time.ToString, 2) = 12, 12, Left(g.Time.ToString, 2) Mod 12), Right(g.Time.ToString, 2))

            Dim spnAway As HtmlControl = CType(oItem.FindControl("away"), HtmlControl)
            Dim spnHome As HtmlControl = CType(oItem.FindControl("home"), HtmlControl)
            If g.Spread < 0 Then
                spnAway.Attributes.Item("class") &= "Dog"
                spnHome.Attributes.Item("class") &= "Favorite"
            ElseIf g.Spread > 0 Then
                spnAway.Attributes.Item("class") &= "Favorite"
                spnHome.Attributes.Item("class") &= "Dog"
            End If
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Dim lSaved As Boolean
        If txtUser.Text <> "Results" And txtUser.Text <> "" Then
            Dim cPath As String = RelativePath("NFL", "XML/Picks/Week" & nWeek.ToString & "/")
            Dim cFilePath As String = Server.MapPath(cPath & txtUser.Text.Trim & ".xml")

            If File.Exists(cFilePath) Then
                Session("Error") = "Picks for this week have already been submitted. Please contact Leighton to make changes."
                lSaved = False
            ElseIf File.Exists(Server.MapPath(cPath & "Results.xml")) Then
                Session("Error") = "Results have already been input for week " & nWeek.ToString & ". Your selections are too late."
                lSaved = False
            Else
                VerifyServer(Server)
                lSaved = Save(nWeek, repGames, txtUser.Text, txtOverUnder.Text)
            End If
        Else
            lSaved = False
        End If
        If lSaved = False Then
            If Session("Error") Is Nothing Then
                Session("Error") = "Something went wrong saving your picks. Please contact Leighton."
            End If
            Response.Redirect("~/Default.aspx")
        End If
    End Sub

End Class