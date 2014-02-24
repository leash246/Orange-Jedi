Public Class _Error
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Error") IsNot Nothing Then
            lblError.Text = Session("Error").ToString
        End If
    End Sub

End Class