Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient

Public Class Results
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer = 1
        Dim dsPicks As New DataSet
        Do While i <= 17
            Dim dtPicks As DataTable
            Dim cmd As New SqlCommand("NFL.usp_Get_Picks")
            cmd.Parameters.AddWithValue("@nWeek", i)
            dtPicks = FillDataTable(cmd)
            If dtPicks.Rows.Count = 0 Then
                Exit Do
            Else
                dsPicks.Tables.Add(dtPicks)
            End If
            Dim dtResults As DataTable
            cmd.Parameters.AddWithValue("@cUser", "Results")
            dtResults = FillDataTable(cmd)
            If dtResults.Rows.Count > 0 Then
                    ddlWeeks.Items.Add(i.ToString)
                    i += 1
            Else
                Exit Do
            End If
        Loop
        i -= 1
        If Not IsPostBack Then
            If i > 0 Then
                ddlWeeks.SelectedValue = i
                LoadResults(i)
            Else
                lblMessage.Text = "No Results Found"
                lblMessage.Style.Item("color") = "Red"
            End If
        End If
    End Sub

    Private Sub ddlWeeks_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlWeeks.SelectedIndexChanged
        If IsPostBack Then
            LoadResults(ddlWeeks.SelectedValue)
        End If
    End Sub
    Private Sub LoadResults(nWeek As Integer)
        Dim dtResults As DataTable
        Dim dtPicks As DataTable
        Dim cmd As New SqlCommand("NFL.usp_Get_Picks")
        cmd.Parameters.AddWithValue("@nWeek", nWeek)
        dtPicks = FillDataTable(cmd)
        cmd.Parameters.AddWithValue("@cUser", "Results")
        dtResults = FillDataTable(cmd)
        Dim i As Integer
        Dim lsWinners As New List(Of String)
        Dim nScore As Integer = 0
        For Each drResult As DataRow In dtResults.Rows
            Dim strTeam As String = drResult.Item("cPick")
            Dim nTieBreaker As Integer
            Integer.TryParse(drResult("cPick"), nTieBreaker)
            If Not lsWinners.Contains(strTeam) AndAlso nTieBreaker = 0 Then
                lsWinners.Add(strTeam)
            ElseIf nTieBreaker <> 0 Then
                nScore = nTieBreaker
            End If
        Next
        Dim lsUser As New List(Of String)
        Dim u = From dr As DataRow In dtPicks.Rows
                Where dr.Item("cUser") <> "Results"
                Select dr.Item("cUser")

        If u.Count > 0 Then
            For Each o As Object In u
                If Not lsUser.Contains(o.ToString()) Then lsUser.Add(o.ToString())
            Next
        End If
        Dim dtResultsList As New DataTable
        dtResultsList.Columns.Add("User", Type.GetType("System.String"))
        dtResultsList.Columns.Add("Correct", Type.GetType("System.Int32"))
        dtResultsList.Columns.Add("Tiebreaker", Type.GetType("System.Decimal"))
        For Each cUser As String In lsUser
            Dim nCorrect As Integer = 0
            Dim nTiebreaker As Integer = 0
            For Each drPick As DataRow In dtPicks.Select("cUser = '" & cUser & "'")
                If lsWinners.Contains(drPick.Item("cPick")) Then
                    nCorrect += 1
                ElseIf nTiebreaker = 0 Then
                    Integer.TryParse(drPick.Item("cPick"), nTiebreaker)
                End If
            Next
            Dim dr As DataRow = dtResultsList.NewRow
            dr.Item("User") = cUser
            dr.Item("Correct") = nCorrect
            dr.Item("Tiebreaker") = Math.Abs(nTiebreaker - nScore)
            dtResultsList.Rows.Add(dr)

        Next
        Dim dv As New DataView(dtResultsList)
        dv.Sort = "Correct desc, Tiebreaker asc"
        repResults.DataSource = dv
        repResults.DataBind()
    End Sub

    Private Sub repResults_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repResults.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If (oItem.ItemType = ListItemType.Item) Or (oItem.ItemType = ListItemType.AlternatingItem) Then
            Dim lblUser As Label = oItem.FindControl("lblUser")
            Dim lblCorrect As Label = oItem.FindControl("lblCorrect")
            Dim lblTiebreaker As Label = oItem.FindControl("lblTiebreaker")
            Dim dr As DataRowView = DirectCast(oItem.DataItem, DataRowView)
            lblUser.Text = dr.Item("User")
            lblCorrect.Text = dr.Item("Correct")
            lblTiebreaker.Text = dr.Item("Tiebreaker")
        End If
    End Sub
End Class