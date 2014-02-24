Imports leightoneash.Enums
Public Class SubMenu
    Inherits MenuControl

    Public Property Content As Control
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub New()
        lnk = New HtmlAnchor
        li = New HtmlGenericControl
        lblLabel = New Label
        phContent = New PlaceHolder
        mnuSub = New HtmlGenericControl
        txtPage = New HtmlInputHidden
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Content Is Nothing Then
            phContent.Controls.Add(Content)
        End If
    End Sub
    Public Sub SetPage(ByVal penmPage As enmPages)
        txtPage.Value = penmPage
    End Sub

    Public Overrides ReadOnly Property SitePage As enmPages
        Get
            Return txtPage.Value
        End Get
    End Property

    Public Overrides Property Href As String
        Get
            Return lnk.HRef
        End Get
        Set(ByVal value As String)
            lnk.HRef = value
        End Set
    End Property

    Public Property subMenu As HtmlGenericControl
        Get
            Return mnuSub
        End Get
        Set(ByVal value As HtmlGenericControl)
            mnuSub = value
        End Set
    End Property
    Public Overrides Property Text As String
        Get
            Return lblLabel.Text
        End Get
        Set(ByVal value As String)
            lblLabel.Text = value
        End Set
    End Property

    Public Event Click(ByVal sender As Object, ByVal e As System.EventArgs)
    Private Sub lnk_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk.ServerClick
        RaiseEvent Click(sender, e)
        If Me.Href.Trim <> "" Then
            Response.Redirect(Me.Href)
        End If
    End Sub
End Class