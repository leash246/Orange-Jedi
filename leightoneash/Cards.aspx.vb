Public Class CardsDefault
    Inherits System.Web.UI.Page

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Dim cPath As String = Request.Path
        Else
            Dim cPath As String = Request.Path
        End If
    End Sub
End Class