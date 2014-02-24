Public Class Costabi
    Inherits BasePage
    Dim dtGame As DataTable
    Dim repItem As RepeaterItem
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            dtGame = SetUpDatatable()
            FillUnplayedHands(dtGame)
            repCostabi.DataSource = dtGame
            repCostabi.DataBind()
        Else
            dtGame = Session("CostabiGame")
            ClearUnplayedHands(dtGame)
            If dtGame.Select("cStatus = 'Current'").Count > 0 Then
                Dim dr As DataRow = dtGame.Select("cStatus = 'Current'")(0)
                Dim drNew As DataRow = dtGame.NewRow
                drNew.ItemArray = dr.ItemArray
                drNew.Item("cStatus") = "Next"
                If dr.Item("nHand") = 1 Then
                    drNew.Item("nHand") = 2
                    drNew.Item("cDirection") = "Up"
                    dtGame.Rows.Add(drNew)
                ElseIf dr.Item("cDirection") = "Down" Then
                    drNew.Item("nHand") = dr.Item("nHand") - 1
                    dtGame.Rows.Add(drNew)
                ElseIf dr.Item("cDirection") = "Up" And dr.Item("nHand") <> 10 Then
                    drNew.Item("nHand") = dr.Item("nHand") + 1
                    dtGame.Rows.Add(drNew)
                End If
                FillUnplayedHands(dtGame)
            End If
        End If
        Session("CostabiGame") = dtGame
    End Sub

    Private Sub repCostabi_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles repCostabi.ItemCommand
        If e.CommandName = "SaveHand" Then
            Dim oItem As RepeaterItem = Nothing
            For Each ri As RepeaterItem In repCostabi.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    Dim lblHand As Label = ri.FindControl("lblHand")
                    Dim hDirection As HiddenField = ri.FindControl("hfDirection")
                    Dim nHand As Integer = Split(e.CommandArgument, ",")(0)
                    Dim cDirection As String = Split(e.CommandArgument, ",")(1)
                    If lblHand.Text = nHand AndAlso hDirection.Value = cDirection Then
                        oItem = ri
                        Exit For
                    End If
                End If
            Next
            If oItem IsNot Nothing Then
                SaveHand(oItem)
            End If
            repCostabi.DataSource = dtGame
            repCostabi.DataBind()
        End If
    End Sub

    Private Sub repCostabi_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repCostabi.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If oItem.ItemType = ListItemType.Item Or oItem.ItemType = ListItemType.AlternatingItem Then
            Dim dr As DataRowView = oItem.DataItem
            Dim nHand As Integer = dr.Item("nHand")
            Dim lblHand As Label = oItem.FindControl("lblHand")
            lblHand.Text = nHand
            Dim hfDirection As HiddenField = oItem.FindControl("hfDirection")
            hfDirection.Value = dr.Item("cDirection")
            Dim cStatus As String = dr.Item("cStatus")
            For i As Integer = 1 To 5
                Dim ch As CostabiHand = oItem.FindControl("Hand" & i)
                If ch IsNot Nothing Then
                    ch.SetDDLValues(nHand)
                    Integer.TryParse(dr.Item("nScore" & i), ch.Score)
                    Integer.TryParse(dr.Item("nBid" & i), ch.Bid)
                    Integer.TryParse(dr.Item("nTook" & i), ch.Took)
                    If Dealer(i, nHand, hfDirection.Value) Then
                        Dim spnPlayer As HtmlControl = CType(oItem.FindControl("spnPlayer" & i.ToString), HtmlControl)
                        spnPlayer.Attributes.Item("class").Replace("Player", "Dealer")
                    End If
                    ch.SetDDLEnabled(cStatus)
                End If

            Next
            Dim btnHand As Button = oItem.FindControl("btnHand")
            btnHand.CommandArgument = String.Concat(nHand, ",", hfDirection.Value)
            If cStatus = "Current" Then
                btnHand.CommandName = "SaveHand"
            Else
                btnHand.Visible = False
            End If
        End If
    End Sub
    Private Function SetUpDatatable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("nHand")
        dt.Columns.Add("cDirection")
        dt.Columns.Add("cStatus")
        For i As Integer = 1 To 5
            dt.Columns.Add("nScore" & i)
            dt.Columns.Add("nBid" & i)
            dt.Columns.Add("nTook" & i)
        Next
        Dim dr As DataRow = dt.NewRow()
        For Each dc As DataColumn In dt.Columns
            If dc.ColumnName.StartsWith("n") Then dr.Item(dc) = 0
            If dc.ColumnName.StartsWith("c") Then dr.Item(dc) = ""
        Next
        dr.Item("nHand") = 10
        dr.Item("cDirection") = "Down"
        dr.Item("cStatus") = "Current"
        dt.Rows.Add(dr)
        dt.AcceptChanges()
        Return dt
    End Function
    Public Sub FillUnplayedHands(ByRef dt As DataTable)
        For i As Integer = 10 To 1 Step -1
            Dim q = From dr As DataRow In dt.Rows Where dr.Item("nHand") = i And dr.Item("cDirection") = "Down"
                    Select dr
            If q.Count > 0 Then
                Continue For
            Else
                Dim dr As DataRow = dt.NewRow
                For Each dc As DataColumn In dt.Columns
                    dr.Item(dc) = ""
                Next
                dr.Item("nHand") = i
                dr.Item("cDirection") = "Down"
                dr.Item("cStatus") = "Future"
                dt.Rows.Add(dr)
            End If
        Next
        For j As Integer = 2 To 10
            Dim q = From dr As DataRow In dt.Rows Where dr.Item("nHand") = j And dr.Item("cDirection") = "Up"
                Select dr
            If q.Count > 0 Then
                Continue For
            Else
                Dim dr As DataRow = dt.NewRow
                For Each dc As DataColumn In dt.Columns
                    dr.Item(dc) = ""
                Next
                dr.Item("nHand") = j
                dr.Item("cDirection") = "Up"
                dr.Item("cStatus") = "Future"
                dt.Rows.Add(dr)
            End If
        Next

    End Sub
    Private Sub ClearUnplayedHands(ByRef dt As DataTable)

        For i As Integer = dt.Rows.Count - 1 To 0 Step -1
            If dt.Rows(i).Item("cStatus") = "Future" Then
                dt.Rows.RemoveAt(i)
            End If
        Next
        dt.AcceptChanges()

    End Sub
    Public Sub SaveHand(oItem As RepeaterItem)
        Dim lblHand As Label = oItem.FindControl("lblHand")
        Dim hfDirection As HiddenField = oItem.FindControl("hfDirection")
        Dim q = From dr As DataRow In dtGame.Rows Where dr.Item("cStatus") = "Current"
                Select dr
        Dim q2 = From dr As DataRow In dtGame.Rows Where dr.Item("cStatus") = "Next"
                Select dr
        Dim drCurrent As DataRow
        Dim drNext As DataRow
        If q.Count > 0 Then
            drCurrent = q.Single
        Else
            Exit Sub
        End If
        If q2.Count > 0 Then
            drNext = q2.Single
        ElseIf drCurrent.Item("nHand") = 10 And drCurrent.Item("cDirection") = "Up" Then
            drNext = Nothing
        Else
            Exit Sub
        End If
        For i As Integer = 1 To 5
            Dim ch As CostabiHand = oItem.FindControl("Hand" & i)
            With ch
                drCurrent.Item("nBid" & i) = .Bid
                drCurrent.Item("nTook" & i) = .Took
                drCurrent.Item("nScore" & i) = CInt(drCurrent.Item("nScore" & i)) + CalculateProgressiveScore(.Bid, .Took)
                If drNext IsNot Nothing Then
                    drNext.Item("nScore" & i) = drCurrent.Item("nScore" & i)
                End If
            End With
        Next
        drCurrent.Item("cStatus") = "Past"
        If drNext IsNot Nothing Then drNext.Item("cStatus") = "Current"
        dtGame.AcceptChanges()
        Session("CostabiGame") = dtGame
    End Sub

    Private Sub btnHand_Click(sender As Object, e As System.EventArgs)
        SaveHand(repItem)
    End Sub
    Private Function CalculateProgressiveScore(nBid As Integer, nTook As Integer) As Integer
        If nTook > nBid Then
            Return 0
        ElseIf nTook < nBid Then
            Return nTook
        ElseIf nTook = nBid Then
            Return Math.Min(10 + (nBid * nTook), 69)
        End If
        Return 0
    End Function
    Private Function Dealer(nPlayer As Integer, nHand As Integer, cDirection As String) As Boolean
        Dim lDealer As Boolean = False
        Select Case nPlayer
            Case 1
                If cDirection = "Down" AndAlso (nHand = 10 Or nHand = 5) Then
                    lDealer = True
                ElseIf cDirection = "Up" AndAlso (nHand = 2 Or nHand = 7) Then
                    lDealer = True
                End If
            Case 2
                If cDirection = "Down" AndAlso (nHand = 9 Or nHand = 4) Then
                    lDealer = True
                ElseIf cDirection = "Up" AndAlso (nHand = 3 Or nHand = 8) Then
                    lDealer = True
                End If
            Case 3
                If cDirection = "Down" AndAlso (nHand = 8 Or nHand = 3) Then
                    lDealer = True
                ElseIf cDirection = "Up" AndAlso (nHand = 4 Or nHand = 9) Then
                    lDealer = True
                End If
            Case 4
                If cDirection = "Down" AndAlso (nHand = 7 Or nHand = 2) Then
                    lDealer = True
                ElseIf cDirection = "Up" AndAlso (nHand = 5 Or nHand = 10) Then
                    lDealer = True
                End If
            Case 5
                If cDirection = "Down" AndAlso (nHand = 6 Or nHand = 1) Then
                    lDealer = True
                ElseIf cDirection = "Up" AndAlso nHand = 6 Then
                    lDealer = True
                End If
        End Select
        Return lDealer
    End Function
End Class