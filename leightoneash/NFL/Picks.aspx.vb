Imports leightoneash.Schedule
Imports System.Data.SqlClient

Public Class Picks
    Inherits System.Web.UI.Page
    Dim gmGames As New Schedule.Games
    Dim nWeek As Integer
    Dim nOverUnder As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim dt As DataTable = LoadStandings()
        Dim nWeek = (From dr As DataRow In dt.Rows
                Select dr.Item("nWeek")).Max + 1
        hfWeek.Value = nWeek
        lblMessage.Text = ""
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
            Dim rbtHome As RadioButton = CType(oItem.FindControl("rbtHome"), RadioButton)
            Dim rbtAway As RadioButton = CType(oItem.FindControl("rbtAway"), RadioButton)
            lblAwayRecord.Text = g.AwayRecord
            lblAwayTeam.Text = g.AwayTeam
            lblSpread.Text = Math.Abs(Math.Round(CDec(g.Spread), 1))
            lblHomeTeam.Text = g.HomeTeam
            lblHomeRecord.Text = g.HomeRecord
            If g.OverUnder > 0 Then
                nOverUnder += g.OverUnder
            End If
            Dim strGameTime As String = ""
            Select Case g.Day
                Case "T", "THU"
                    strGameTime = "Thursday"
                    If Now.DayOfWeek <> DayOfWeek.Wednesday And Now.DayOfWeek <> DayOfWeek.Thursday And Now.DayOfWeek <> DayOfWeek.Tuesday Then
                        rbtHome.Enabled = False
                        rbtAway.Enabled = False
                    End If
                Case "S", "SUN"
                    strGameTime = "Sunday"
                    If Now.DayOfWeek = DayOfWeek.Monday Then
                        rbtHome.Enabled = False
                        rbtAway.Enabled = False
                    End If
                Case "M", "MON"
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
            If Now.DayOfWeek = DayOfWeek.Tuesday Then
                Session("Error") = "Picks are not allowed on Tuesdays. Please give Leighton time to input results and update the next week's spreads."
                lSaved = False
            Else
                If nWeek = 0 Then nWeek = hfWeek.Value
                Dim cmd As New SqlCommand("NFL.usp_Get_Picks")
                cmd.Parameters.AddWithValue("@nWeek", nWeek)
                cmd.Parameters.AddWithValue("@cUser", txtUser.Text)
                Dim dt As DataTable = FillDataTable(cmd)
                cmd.CommandText = "NFL.usp_Get_Standings"
                cmd.Parameters.Clear()
                Dim dtStandings As DataTable = FillDataTable(cmd)
                If dt.Rows.Count > 0 Then
                    Session("Error") = "Picks for this week have already been submitted. Please contact Leighton to make changes."
                    lSaved = False
                ElseIf dtStandings.Select("nWeek = " & nWeek.ToString).Count > 0 Then
                    Session("Error") = "Results have already been input for week " & nWeek.ToString & ". Your selections are too late."
                    lSaved = False
                Else
                    lSaved = Save(nWeek, repGames, txtUser.Text, txtOverUnder.Text)
                End If
            End If
        Else
            lSaved = False
        End If
        If lSaved = False Then
            If Session("Error") Is Nothing Then
                Session("Error") = "Something went wrong saving your picks. Please contact Leighton."
            End If
            lblMessage.Text = Session("Error")
            lblMessage.Style.Item("color") = "Red"
        Else
            lblMessage.Text = "Picks Saved!"
            lblMessage.Style.Item("color") = "Green"
        End If

    End Sub

End Class