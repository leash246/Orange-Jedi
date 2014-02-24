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
            Dim mMail As New MailMessage("noreply@leightoneash.com", "leighton@leightoneash.com", "Request Permissions", cText.Trim)
            Dim objSMTP As New SmtpClient("relay-hosting.secureserver.net", 25)
            objSMTP.UseDefaultCredentials = False
            objSMTP.Credentials = New System.Net.NetworkCredential(mMail.From.ToString, "hooray1")
            objSMTP.DeliveryMethod = SmtpDeliveryMethod.Network
            objSMTP.Port = 25
            objSMTP.EnableSsl = False
            objSMTP.Send(mMail)
            Response.Redirect("~/Default.aspx")
        End If


    End Sub
End Class