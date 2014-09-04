Public Class Standings
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dt As DataTable = LoadStandings()
        Dim dv As New DataView(dt)
        dv.Sort = "nWins desc, nLosses asc"
        dv.RowFilter = "Conference = 'NFC' and Division = 'North'"
        repNFCN.DataSource = dv
        repNFCN.DataBind()
        dv.RowFilter = "Conference = 'NFC' and Division = 'South'"
        repNFCS.DataSource = dv
        repNFCS.DataBind()
        dv.RowFilter = "Conference = 'NFC' and Division = 'East'"
        repNFCE.DataSource = dv
        repNFCE.DataBind()
        dv.RowFilter = "Conference = 'NFC' and Division = 'West'"
        repNFCW.DataSource = dv
        repNFCW.DataBind()
        dv.RowFilter = "Conference = 'AFC' and Division = 'North'"
        repAFCN.DataSource = dv
        repAFCN.DataBind()
        dv.RowFilter = "Conference = 'AFC' and Division = 'South'"
        repAFCS.DataSource = dv
        repAFCS.DataBind()
        dv.RowFilter = "Conference = 'AFC' and Division = 'East'"
        repAFCE.DataSource = dv
        repAFCE.DataBind()
        dv.RowFilter = "Conference = 'AFC' and Division = 'West'"
        repAFCW.DataSource = dv
        repAFCW.DataBind()
    End Sub
    
    Private Sub repDiv_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repNFCN.ItemDataBound, repNFCS.ItemDataBound, repNFCE.ItemDataBound, repNFCW.ItemDataBound, repAFCN.ItemDataBound, repAFCS.ItemDataBound, repAFCE.ItemDataBound, repAFCW.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If oItem.ItemType = ListItemType.Item Or oItem.ItemType = ListItemType.AlternatingItem Then
            Dim lblTeam As Label = oItem.FindControl("lblTeam")
            Dim lblWins As Label = oItem.FindControl("lblWins")
            Dim lblLosses As Label = oItem.FindControl("lblLosses")
            Dim lblTies As Label = oItem.FindControl("lblTies")
            Dim dr As DataRowView = oItem.DataItem
            lblTeam.Text = dr("cTeam")
            lblWins.Text = dr("nWins")
            lblLosses.Text = dr("nLosses")
            lblTies.Text = dr("nTies")
        End If
    End Sub

End Class