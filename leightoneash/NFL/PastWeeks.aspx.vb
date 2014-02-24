Imports System.IO
Imports System.Xml

Public Class PastWeeks
    Inherits System.Web.UI.Page
    Dim lsResults As List(Of String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadWeeks()
        End If
    End Sub

    Private Function LoadWeeks() As Boolean
        Try
            ddlWeeks.Items.Clear()
            ddlWeeks.Items.Add("")
            Dim cPath As String = Server.MapPath(RelativePath("NFL", "XML/Picks"))
            If Not Directory.Exists(cPath) Then
                Session("Error") = "No Results Found"
                Response.Redirect("~/Error.aspx")
            End If
            Dim i As Integer = 1
            Do While i <= 17
                cPath = Server.MapPath(RelativePath("NFL", "XML/Picks"))
                cPath &= "/Week" & i.ToString
                If Not Directory.Exists(cPath) Then
                    Exit Do
                End If
                cPath &= "/Results.xml"
                If File.Exists(cPath) Then
                    ddlWeeks.Items.Add(i.ToString)
                    i += 1
                Else
                    Exit Do
                End If
            Loop
            i -= 1
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub LoadUsers(nWeek As Integer)
        ddlUser.Items.Clear()
        ddlUser.Items.Add("Results")
        Dim cPath As String = Server.MapPath(RelativePath("NFL", "XML/Week" & nWeek.ToString))
        Dim di As New DirectoryInfo(cPath)
        For Each fi As FileInfo In di.GetFiles("*.xml", SearchOption.TopDirectoryOnly)
            If fi.Name <> "Results.xml" Then
                Dim xmlDoc As New XmlDataDocument
                Dim xmlnode As XmlNodeList
                Dim fsResults As FileStream = fi.OpenRead
                xmlDoc.Load(fsResults)
                xmlnode = xmlDoc.GetElementsByTagName("User")
                ddlUser.Items.Add(xmlnode(0).InnerText)
            End If
        Next
        ddlUser.SelectedValue = "Results"

        btnUser_Click(btnUser, Nothing)

    End Sub

    Private Sub btnUser_Click(sender As Object, e As System.EventArgs) Handles btnUser.Click
        Dim cUser As String = ddlUser.SelectedValue
        Dim cPath As String = Server.MapPath(RelativePath("NFL", "XML/Week" & ddlWeeks.SelectedValue & "/" & cUser & ".xml"))
        Dim lsPicks As New List(Of String)
        Dim fi As New FileInfo(cPath)
        Dim xmlDoc As New XmlDataDocument
        Dim xmlNode As XmlNodeList
        Dim fs As FileStream = fi.OpenRead
        xmlDoc.Load(fs)
        xmlNode = xmlDoc.GetElementsByTagName("Pick")
        If cUser = "Results" Then lsResults = New List(Of String)
        For i As Integer = 0 To xmlNode.Count - 1
            lsPicks.Add(xmlNode(i).InnerText)
            If cUser = "Results" Then
                lsResults.Add(xmlNode(i).InnerText)
            Else
                lsResults = Session("lsResults")
            End If
        Next
        If cUser = "Results" Then Session("lsResults") = lsResults
        Dim nPicks As Integer = 0
        If lsPicks.Count > 0 Then
            For Each item As RepeaterItem In repGames.Items
                Dim lblHome As Label = item.FindControl("lblHomeTeam")
                Dim lblAway As Label = item.FindControl("lblAwayTeam")
                Dim spnPick As HtmlControl
                If lsPicks.Contains(lblHome.Text) Then
                    spnPick = CType(item.FindControl("home"), HtmlControl)
                    spnPick.Attributes.Item("class") = "HomeFavorite"
                    If cUser <> "Results" Then
                        If Not lsResults.Contains(lblHome.Text) Then
                            spnPick.Attributes.Item("class") = "HomeFavorite Strike"
                        Else
                            nPicks += 1
                        End If
                    End If
                    spnPick = CType(item.FindControl("away"), HtmlControl)
                    spnPick.Attributes.Item("class") = "AwayDog"
                ElseIf lsPicks.Contains(lblAway.Text) Then
                    spnPick = CType(item.FindControl("away"), HtmlControl)
                    spnPick.Attributes.Item("class") = "AwayFavorite"
                    If cUser <> "Results" Then
                        If Not lsResults.Contains(lblAway.Text) Then
                            spnPick.Attributes.Item("class") = "AwayFavorite Strike"
                        Else
                            nPicks += 1
                        End If
                    End If
                    spnPick = CType(item.FindControl("home"), HtmlControl)
                    spnPick.Attributes.Item("class") = "HomeDog"
                    If cUser <> "Results" Then
                        lsResults.Add(lblAway.Text)
                    End If
                End If
            Next
        End If
        If nPicks > 0 Then
            lblPicks.Text = "Correct Picks: " & nPicks.ToString
        Else
            lblPicks.Text = ""
        End If

    End Sub

    Private Sub btnWeeks_Click(sender As Object, e As System.EventArgs) Handles btnWeeks.Click
        If Not ddlWeeks.SelectedValue = "" Then
            lsResults = New List(Of String)
            Dim gmGames As New Schedule.Games
            LoadWeek(ddlWeeks.SelectedValue, gmGames)
            repGames.DataSource = gmGames
            repGames.DataBind()
            LoadUsers(ddlWeeks.SelectedValue)
        End If
    End Sub
    Private Sub ddlWeeks_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlWeeks.SelectedIndexChanged

    End Sub
    Private Sub repGames_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repGames.ItemDataBound
        Dim oItem As RepeaterItem = e.Item

        If (oItem.ItemType = ListItemType.Item) Or (oItem.ItemType = ListItemType.AlternatingItem) Then
            Dim g As Schedule.Game = CType(oItem.DataItem, Schedule.Game)
            Dim lblAwayTeam As Label = CType(oItem.FindControl("lblAwayTeam"), Label)
            Dim lblSpread As Label = CType(oItem.FindControl("lblSpread"), Label)
            Dim lblHomeTeam As Label = CType(oItem.FindControl("lblHomeTeam"), Label)
            lblAwayTeam.Text = g.AwayTeam
            lblSpread.Text = g.Spread
            lblHomeTeam.Text = g.HomeTeam

            'Dim spnAway As HtmlControl = CType(oItem.FindControl("away"), HtmlControl)
            'Dim spnHome As HtmlControl = CType(oItem.FindControl("home"), HtmlControl)
            'If g.Spread < 0 Then
            '    spnAway.Attributes.Item("class") &= "Dog"
            '    spnHome.Attributes.Item("class") &= "Favorite"
            'ElseIf g.Spread > 0 Then
            '    spnAway.Attributes.Item("class") &= "Favorite"
            '    spnHome.Attributes.Item("class") &= "Dog"
            'End If
        End If
    End Sub

End Class