Public Class PitchRob
    Inherits BasePage
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
            dtGame.Columns.Add("nBid1")
            dtGame.Columns.Add("nBid2")
            dtGame.Columns.Add("cSuit")
            dtGame.AcceptChanges()
            Session("Game") = dtGame
        End If
        strTeam1 = txtTeam1.Text.Trim
        strTeam2 = txtTeam2.Text.Trim
    End Sub
    Private Sub btnAddScore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddScore.Click
        Dim nTook1 As Integer = 0
        Dim nTook2 As Integer = 0
        Dim nBid1 As Integer
        Dim nBid2 As Integer
        Dim cSuit As String = ""
        Integer.TryParse(txtScore1.Text, nTook1)
        Integer.TryParse(txtScore2.Text, nTook2)
        Integer.TryParse(txtBid1.Text, nBid1)
        Integer.TryParse(txtBid2.Text, nBid2)
        If nBid1 > 0 Then
            If nTook1 <= 10 Then
                nTook2 = 10 - nTook1
            End If
            If nBid1 > nTook1 Then
                nTook1 = nBid1 * -1
            End If
        End If
        If nBid2 > 0 Then
            If nTook2 <= 10 Then
                nTook1 = 10 - nTook2
            End If
            If nBid2 > nTook2 Then
                nTook2 = nBid2 * -1
            End If
        End If

        cSuit = ddlSuit.SelectedValue
        dtGame = Session("Game")
        Dim dr As DataRow = dtGame.NewRow
        dr.Item("nHand") = dtGame.Rows.Count + 1
        dr.Item("nScore1") = nTook1
        dr.Item("nScore2") = nTook2
        dr.Item("nBid1") = nBid1
        dr.Item("nBid2") = nBid2
        dr.Item("cSuit") = cSuit
        dtGame.Rows.Add(dr)

        txtTeam1.Text = strTeam1
        txtTeam2.Text = strTeam2
        dtGame = Session("Game")
        nScore1 = 0
        nScore2 = 0
        txtScore1.Text = ""
        txtScore2.Text = ""
        txtBid1.Text = ""
        txtBid2.Text = ""
        ddlSuit.SelectedValue = ""
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
            Dim lblBid1 As Label = oItem.FindControl("lblBid1")
            Dim lblBid2 As Label = oItem.FindControl("lblBid2")
            Dim lblSuit As Label = oItem.FindControl("lblSuit")
            Dim imgSuit As Image = oItem.FindControl("imgSuit")
            lblHand.Text = drHand.Item("nHand")
            lblScore1.Text = drHand.Item("nScore1")
            lblScore2.Text = drHand.Item("nScore2")
            If drHand.Item("nBid1") > 0 Then
                lblBid1.Text = drHand.Item("nBid1")
            ElseIf drHand.Item("nBid2") > 0 Then
                lblBid2.Text = drHand.Item("nBid2")
            End If
            If drHand.Item("cSuit") <> "" Then
                imgSuit.ImageUrl = drHand.Item("cSuit") & ".png"
                imgSuit.Style.Add("height", "25px")
            End If
            'lblSuit.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url('" & drHand.Item("cSuit") & ".png')")
            'lblSuit.Text = drHand.Item("cSuit")
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