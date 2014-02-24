Public Class CostabiHand
    Inherits System.Web.UI.UserControl
    Public Property Score As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(lblScore.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            lblScore.Text = value.ToString
        End Set
    End Property
    Public Property Bid As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(ddlBid.SelectedValue, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            ddlBid.SelectedValue = value
        End Set
    End Property
    Public Property Took As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(ddlTook.SelectedValue, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            ddlTook.SelectedValue = value
        End Set
    End Property
    Public Sub SetDDLValues(nHand As Integer)
        Dim dtBid As New DataTable
        dtBid.Columns.Add("nTricks")
        Dim dtTook As New DataTable
        dtTook.Columns.Add("nTricks")
        dtBid.Rows.Add("")
        dtTook.Rows.Add("")
        For i As Integer = 0 To nHand
            Dim dr As DataRow = dtBid.NewRow
            dr.Item("nTricks") = i
            dtBid.Rows.Add(dr)
            Dim dr2 As DataRow = dtTook.NewRow
            dr2.Item("nTricks") = i
            dtTook.Rows.Add(dr2)
        Next
        If nHand = 1 Then
            Dim dr As DataRow = dtBid.NewRow
            dr.Item("nTricks") = 2
            dtBid.Rows.Add(dr)
        End If
        With ddlBid
            .Items.Clear()
            .ClearSelection()
            .DataSource = dtBid
            .DataTextField = "nTricks"
            .DataValueField = "nTricks"
            .DataBind()
        End With
        With ddlTook
            .Items.Clear()
            .ClearSelection()
            .DataSource = dtTook
            .DataTextField = "nTricks"
            .DataValueField = "nTricks"
            .DataBind()
        End With
    End Sub
    Public Sub SetDDLEnabled(cStatus As String)
        If cStatus = "Past" Then
            ddlBid.Enabled = False
            ddlTook.Enabled = False
        ElseIf cStatus = "Current" Then
            ddlBid.Enabled = True
            ddlTook.Enabled = True
            lblScore.Text = ""
        ElseIf cStatus = "Future" Then
            ddlBid.SelectedIndex = -1
            ddlTook.SelectedIndex = -1
            ddlBid.Enabled = False
            ddlTook.Enabled = False
            lblScore.Text = ""
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class