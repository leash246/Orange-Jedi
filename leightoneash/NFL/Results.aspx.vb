Imports System.IO
Imports System.Xml

Public Class Results
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        If Not IsPostBack Then
            If i > 0 Then
                ddlWeeks.SelectedValue = i
                LoadResults(i)
            Else
                Session("Error") = "No Results Found"
                Response.Redirect("~/Error.aspx")
            End If
        End If
    End Sub

    Private Sub ddlWeeks_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlWeeks.SelectedIndexChanged
        If IsPostBack Then
            LoadResults(ddlWeeks.SelectedValue)
        End If
    End Sub
    Private Sub LoadResults(nWeek As Integer)
        Dim dtResults As New DataTable()
        dtResults.Columns.Add("User")
        dtResults.Columns.Add("Correct")
        dtResults.Columns.Add("Tiebreaker")
        Dim cPath As String = Server.MapPath(RelativePath("NFL", "XML/Picks/Week" & nWeek.ToString))
        Dim fiResults As New FileInfo(cPath & "/Results.xml")
        Dim xmlDocResults As New XmlDataDocument
        Dim xmlnodeResults As XmlNodeList
        Dim i As Integer
        Dim fsResults As FileStream = fiResults.OpenRead
        xmlDocResults.Load(fsResults)
        xmlnodeResults = xmlDocResults.GetElementsByTagName("Pick")
        Dim lsWinners As New List(Of String)
        For i = 0 To xmlnodeResults.Count - 1
            Dim strTeam As String = xmlnodeResults(i).InnerText
            If Not lsWinners.Contains(strTeam) Then
                lsWinners.Add(strTeam)
            End If
        Next
        xmlnodeResults = xmlDocResults.GetElementsByTagName("Tiebreaker")
        Dim nScore As Integer = xmlnodeResults(0).InnerText
        Dim di As DirectoryInfo = New DirectoryInfo(cPath)
        For Each fi As FileInfo In di.GetFiles()
            If fi.Name <> "Results.xml" Then
                Dim cUser As String
                Dim nCorrect As Integer = 0
                Dim xmldocUser As New XmlDataDocument()
                Dim xmlnodeUser As XmlNodeList
                Dim fs As FileStream = fi.OpenRead
                xmldocUser.Load(fs)
                xmlnodeUser = xmldocUser.GetElementsByTagName("User")
                cUser = xmlnodeUser(0).InnerText
                xmlnodeUser = xmldocUser.GetElementsByTagName("Pick")
                For i = 0 To xmlnodeUser.Count - 1
                    If lsWinners.Contains(xmlnodeUser(i).InnerText) Then
                        nCorrect += 1
                    End If
                Next
                xmlnodeUser = xmldocUser.GetElementsByTagName("Tiebreaker")
                Dim nTiebreaker As Integer = xmlnodeUser(0).InnerText
                fs.Close()
                Dim dr As DataRow = dtResults.NewRow
                dr.Item("User") = cUser
                dr.Item("Correct") = nCorrect
                dr.Item("Tiebreaker") = Math.Abs(nTiebreaker - nScore)
                dtResults.Rows.Add(dr)
            End If
        Next
        Dim dv As New DataView(dtResults)
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
            lblTiebreaker.text = dr.Item("Tiebreaker")
        End If
    End Sub
End Class