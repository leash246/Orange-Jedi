Imports System.Net.Mail
Public Class RequestPermissions
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnRequest_Click(sender As Object, e As System.EventArgs) Handles btnRequest.Click

        Dim objUser As User = Session("User")
        If objUser Is Nothing Then
            Response.Redirect("~/Account/Login.aspx?RequestURL=%2fAccount%2fRequestPermission.aspx")
        Else
            Dim cText As String = "User " & objUser.FirstName & " " & objUser.LastName & " (" & objUser.Email & ") requests additional permissions:"
            cText &= vbCrLf & vbCrLf & txtRequest.Text
            SendEmail("Request Permissions", cText)
            Response.Redirect("~/Default.aspx")
        End If


    End Sub
End Class