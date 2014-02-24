Public Class Pitch
    Inherits System.Web.UI.Page
    Dim dtGame As DataTable
    Dim strTeam1 As String = ""
    Dim strTeam2 As String = ""
    Dim nScore1 As Integer = 0
    Dim nScore2 As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("Game") = Nothing
            dtGame = New DataTable
            dtGame.Columns.Add("nHand")
            dtGame.Columns.Add("nScore1")
            dtGame.Columns.Add("nScore2")
            dtGame.AcceptChanges()
            Session("Game") = dtGame
        End If
        strTeam1 = txtTeam1.Text.Trim
        strTeam2 = txtTeam2.Text.Trim
    End Sub

    Private Sub btnAddScore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddScore.Click
        Dim nAddScore1 As Integer
        Dim nAddScore2 As Integer
        Integer.TryParse(txtScore1.Text, nAddScore1)
        Integer.TryParse(txtScore2.Text, nAddScore2)
        dtGame = Session("Game")
        Dim dr As DataRow = dtGame.NewRow
        dr.Item("nHand") = dtGame.Rows.Count + 1
        dr.Item("nScore1") = nAddScore1
        dr.Item("nScore2") = nAddScore2
        dtGame.Rows.Add(dr)

        txtTeam1.Text = strTeam1
        txtTeam2.Text = strTeam2
        dtGame = Session("Game")
        nScore1 = 0
        nScore2 = 0
        Dim dv As New DataView(dtGame)
        repHands.DataSource = dv

        repHands.DataBind()
    End Sub

    Private Sub repHands_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repHands.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If oItem.ItemType = ListItemType.Header Then
            'Dim lblTeam1 As Label = oItem.FindControl("lblTeam1")
            'Dim lblTeam2 As Label = oItem.FindControl("lblTeam2")
            'lblTeam1.Text = strTeam1
            'lblTeam2.Text = strTeam2
        ElseIf oItem.ItemType = ListItemType.Item Or oItem.ItemType = ListItemType.AlternatingItem Then
            Dim drHand As DataRowView = oItem.DataItem
            Dim lblHand As Label = oItem.FindControl("lblHand")
            Dim lblScore1 As Label = oItem.FindControl("lblScore1")
            Dim lblScore2 As Label = oItem.FindControl("lblScore2")

            lblHand.Text = drHand.Item("nHand")
            lblScore1.Text = drHand.Item("nScore1")
            lblScore2.Text = drHand.Item("nScore2")
            nScore1 += drHand.Item("nScore1")
            nScore2 += drHand.Item("nScore2")
        ElseIf oItem.ItemType = ListItemType.Footer And dtGame IsNot Nothing Then
            Dim lblHandTotal As Label = oItem.FindControl("lblHandTotal")
            Dim lblTeam1Total As Label = oItem.FindControl("lblTeam1Total")
            Dim lblTeam2Total As Label = oItem.FindControl("lblTeam2Total")
            lblHandTotal.Text = dtGame.Rows.Count
            lblTeam1Total.Text = nScore1.ToString
            lblTeam2Total.Text = nScore2.ToString
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtTeam1.Text = ""
        txtTeam2.Text = ""
        dtGame = Session("Game")
        dtGame.Rows.Clear()
        Session("Game") = dtGame
        Dim dv As New DataView(dtGame)
        repHands.DataSource = dv
        repHands.DataBind()
    End Sub
End Class