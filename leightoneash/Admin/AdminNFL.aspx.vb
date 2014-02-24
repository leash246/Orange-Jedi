Imports System.Xml
Imports System.IO
Public Class AdminNFL
    Inherits System.Web.UI.Page
    Dim nWeek As Integer
    Dim gmGames As New Schedule.Games
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim xmldoc As New XmlDataDocument
        Dim fi As FileInfo = New FileInfo(Server.MapPath(RelativePath("NFL", "XML/Standings.xml")))
        Dim fs As FileStream = fi.OpenRead
        xmldoc.Load(fs)
        Dim cWeek As String = xmldoc.SelectSingleNode("Standings").Attributes("week").Value.ToString
        Integer.TryParse(cWeek, nWeek)
        If Not IsPostBack() Then
            LoadWeek(nWeek, gmGames)
            repGames.DataSource = gmGames
            repGames.DataBind()
            ChangeAction("")
        End If
    End Sub
    Private Sub ChangeAction(cAction As String)
        If cAction = "Spreads" Then
            Session("Action") = "Spreads"
            For Each ctrl As Control In repGames.Controls
                Dim rbtAway As RadioButton = CType(ctrl.FindControl("rbtAway"), RadioButton)
                If rbtAway IsNot Nothing Then
                    rbtAway.Enabled = False
                End If
                Dim rbtHome As RadioButton = CType(ctrl.FindControl("rbtHome"), RadioButton)
                If rbtHome IsNot Nothing Then
                    rbtHome.Enabled = False
                End If
                Dim txtSpread As TextBox = CType(ctrl.FindControl("txtSpread"), TextBox)
                If txtSpread IsNot Nothing Then
                    txtSpread.Enabled = True
                End If
            Next
        ElseIf cAction = "Results" Then
            Session("Action") = "Results"
            For Each ctrl As Control In repGames.Controls
                Dim rbtAway As RadioButton = CType(ctrl.FindControl("rbtAway"), RadioButton)
                If rbtAway IsNot Nothing Then
                    rbtAway.Enabled = True
                End If
                Dim rbtAwayWin As RadioButton = CType(ctrl.FindControl("rbtAwayWin"), RadioButton)
                If rbtAwayWin IsNot Nothing Then
                    rbtAwayWin.Enabled = True
                End If
                Dim rbtHome As RadioButton = CType(ctrl.FindControl("rbtHome"), RadioButton)
                If rbtHome IsNot Nothing Then
                    rbtHome.Enabled = True
                End If
                Dim rbtHomeWin As RadioButton = CType(ctrl.FindControl("rbtHomeWin"), RadioButton)
                If rbtHomeWin IsNot Nothing Then
                    rbtHomeWin.Enabled = True
                End If
                Dim txtSpread As TextBox = CType(ctrl.FindControl("txtSpread"), TextBox)
                If txtSpread IsNot Nothing Then
                    txtSpread.Enabled = False
                End If
            Next
        Else
            Session("Action") = Nothing
            For Each ctrl As Control In repGames.Controls
                Dim rbtAway As RadioButton = CType(ctrl.FindControl("rbtAway"), RadioButton)
                If rbtAway IsNot Nothing Then
                    rbtAway.Enabled = False
                End If
                Dim rbtAwayWin As RadioButton = CType(ctrl.FindControl("rbtAwayWin"), RadioButton)
                If rbtAwayWin IsNot Nothing Then
                    rbtAwayWin.Enabled = False
                End If
                Dim rbtHome As RadioButton = CType(ctrl.FindControl("rbtHome"), RadioButton)
                If rbtHome IsNot Nothing Then
                    rbtHome.Enabled = False
                End If
                Dim rbtHomeWin As RadioButton = CType(ctrl.FindControl("rbtHomeWin"), RadioButton)
                If rbtHomeWin IsNot Nothing Then
                    rbtHomeWin.Enabled = False
                End If
                Dim txtSpread As TextBox = CType(ctrl.FindControl("txtSpread"), TextBox)
                If txtSpread IsNot Nothing Then
                    txtSpread.Enabled = False
                End If
            Next
        End If
    End Sub
    Private Sub repGames_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repGames.ItemDataBound
        Dim oItem As RepeaterItem = e.Item

        If (oItem.ItemType = ListItemType.Item) Or (oItem.ItemType = ListItemType.AlternatingItem) Then
            Dim g As Schedule.Game = CType(oItem.DataItem, Schedule.Game)
            Dim lblAwayRecord As Label = CType(oItem.FindControl("lblAwayRecord"), Label)
            Dim lblAwayTeam As Label = CType(oItem.FindControl("lblAwayTeam"), Label)
            Dim txtSpread As TextBox = CType(oItem.FindControl("txtSpread"), TextBox)
            Dim lblHomeTeam As Label = CType(oItem.FindControl("lblHomeTeam"), Label)
            Dim lblHomeRecord As Label = CType(oItem.FindControl("lblHomeRecord"), Label)
            Dim lblGameTime As Label = CType(oItem.FindControl("lblGameTime"), Label)
            lblAwayRecord.Text = g.AwayRecord
            lblAwayTeam.Text = g.AwayTeam
            txtSpread.Text = g.Spread
            lblHomeTeam.Text = g.HomeTeam
            lblHomeRecord.Text = g.HomeRecord
            Dim strGameTime As String = ""
            Select Case g.Day
                Case "T"
                    strGameTime = "Thursday"
                Case "S"
                    strGameTime = "Sunday"
                Case "M"
                    strGameTime = "Monday"
            End Select
            lblGameTime.Text = String.Format("{0} {1}:{2}", strGameTime, Left(g.Time.ToString, 2), Right(g.Time.ToString, 2))

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
    Private Sub SaveRecords()
        Dim cPath As String = Server.MapPath(RelativePath("NFL", "XML/Standings.xml"))
        Dim dt As DataTable = LoadStandings(cPath)
        UpdateStandings(dt)
        dt.TableName = (CInt(dt.TableName) + 1).ToString
        SaveStandings(dt, cPath)
        SaveSpreads(True)
    End Sub
    Private Sub SaveStandings(dt As DataTable, cPath As String)

        If File.Exists(cPath) Then
            File.Delete(cPath)
        End If
        Dim w As New XmlTextWriter(cPath, System.Text.Encoding.UTF8)
        w.WriteStartDocument(True)
        w.Formatting = Formatting.Indented
        w.Indentation = 2
        w.WriteStartElement("Standings")
        WriteAttribute(w, "week", dt.TableName)
        For Each dr As DataRow In dt.Rows
            w.WriteStartElement("Team")
            WriteAttribute(w, "name", dr.Item("Team"))
            WriteElement(w, "Wins", dr.Item("Wins"))
            WriteElement(w, "Losses", dr.Item("Losses"))
            WriteElement(w, "Ties", dr.Item("Ties"))
            WriteElement(w, "Division", dr.Item("Conference") & Left(dr.Item("Division"), 1))
            w.WriteEndElement()
        Next
        w.WriteEndElement()
        w.Close()
    End Sub
    Private Sub UpdateStandings(ByRef dtStandings As DataTable)
        For Each item As RepeaterItem In repGames.Items
            Dim rbtAwayWin As RadioButton = item.FindControl("rbtAwayWin")
            Dim rbtHomeWin As RadioButton = item.FindControl("rbtHomeWin")
            Dim lblAwayTeam As Label = item.FindControl("lblAwayTeam")
            Dim lblHomeTeam As Label = item.FindControl("lblHomeTeam")
            Dim cAway As String = lblAwayTeam.Text
            Dim cHome As String = lblHomeTeam.Text
            If rbtAwayWin.Checked Then
                Dim drHome As DataRow = dtStandings.Select("Team = '" & cHome & "'")(0)
                drHome.Item("Losses") = CInt(drHome.Item("Losses")) + 1
                Dim drAway As DataRow = dtStandings.Select("Team = '" & cAway & "'")(0)
                drAway.Item("Wins") = CInt(drAway.Item("Wins")) + 1
            ElseIf rbtHomeWin.Checked Then
                Dim drHome As DataRow = dtStandings.Select("Team = '" & cHome & "'")(0)
                drHome.Item("Wins") = CInt(drHome.Item("Wins")) + 1
                Dim drAway As DataRow = dtStandings.Select("Team = '" & cAway & "'")(0)
                drAway.Item("Losses") = CInt(drAway.Item("Losses")) + 1
            Else
                Dim drHome As DataRow = dtStandings.Select("Team = '" & cHome & "'")(0)
                drHome.Item("Ties") = CInt(drHome.Item("Ties")) + 1
                Dim drAway As DataRow = dtStandings.Select("Team = '" & cAway & "'")(0)
                drAway.Item("Ties") = CInt(drAway.Item("Ties")) + 1
            End If
        Next
    End Sub

    Private Sub SaveSpreads(Optional ByVal lRecords As Boolean = False)
        Dim gmGames As New Schedule.Games
        For Each oItem As RepeaterItem In repGames.Items
            Dim g As New Schedule.Game
            Dim lblAwayRecord As Label = CType(oItem.FindControl("lblAwayRecord"), Label)
            Dim lblAwayTeam As Label = CType(oItem.FindControl("lblAwayTeam"), Label)
            Dim txtSpread As TextBox = CType(oItem.FindControl("txtSpread"), TextBox)
            Dim lblHomeTeam As Label = CType(oItem.FindControl("lblHomeTeam"), Label)
            Dim lblHomeRecord As Label = CType(oItem.FindControl("lblHomeRecord"), Label)
            Dim lblGameTime As Label = CType(oItem.FindControl("lblGameTime"), Label)
            Dim rbtAway As RadioButton = CType(oItem.FindControl("rbtAway"), RadioButton)
            Dim rbtHome As RadioButton = CType(oItem.FindControl("rbtHome"), RadioButton)
            g.AwayRecord = lblAwayRecord.Text
            g.AwayTeam = lblAwayTeam.Text
            g.Spread = txtSpread.Text
            If g.Spread.Contains("+") = False And g.Spread > 0 Then
                g.Spread = "+" & g.Spread
            End If
            g.HomeTeam = lblHomeTeam.Text
            g.HomeRecord = lblHomeRecord.Text
            If lRecords Then
                If rbtAway.Checked Then
                    g.Result = "Away"
                ElseIf rbtHome.Checked Then
                    g.Result = "Home"
                Else
                    g.Result = "Push"
                End If
            Else
                g.Result = ""
            End If
            If lblGameTime.Text.Contains("Thursday") Then
                g.Day = "T"
            ElseIf lblGameTime.Text.Contains("Sunday") Then
                g.Day = "S"
            ElseIf lblGameTime.Text.Contains("Monday") Then
                g.Day = "M"
                g.OverUnder = txtOverUnder.Text
            End If
            Integer.TryParse(Right(lblGameTime.Text, 5).Replace(":", ""), g.Time)
            Dim spnAway As HtmlControl = CType(oItem.FindControl("away"), HtmlControl)
            Dim spnHome As HtmlControl = CType(oItem.FindControl("home"), HtmlControl)
            gmGames.Add(g)
        Next
        SaveWeek(nWeek, gmGames)
    End Sub
    Private Sub btnSpreads_Click(sender As Object, e As System.EventArgs) Handles btnSpreads.Click
        ChangeAction("Spreads")
        lblOverUnder.Text = "Over/Under"
    End Sub

    Private Sub btnResults_Click(sender As Object, e As System.EventArgs) Handles btnResults.Click
        ChangeAction("Results")
        lblOverUnder.Text = "Total Score"
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        If txtPassword.Text = "Sméagol" Then
            If Session("Action") = "Spreads" Then
                SaveSpreads()
                Response.Redirect("~/Default.aspx")
            ElseIf Session("Action") = "Results" Then
                SaveRecords()
                Save(nWeek, repGames, "Results", txtOverUnder.Text)
                Response.Redirect("~/Default.aspx")
            Else

            End If
            'Save stuff
        End If
    End Sub
End Class
