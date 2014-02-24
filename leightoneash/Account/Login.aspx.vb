Imports System.Data.SqlClient
Imports System.Web.Security

Public Class Login
    Inherits BasePage
    Private objUser As User
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("LoginError") IsNot Nothing Then
            lblMsg.Text = Session("LoginError")
            Session.Remove("LoginError")
        End If
    End Sub
    Private Sub cmdLogin_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        If ValidateUser(txtUserName.Text, txtUserPass.Text, objUser) Then
            Session("User") = objUser
            RedirectFromLoginPage(Request.Path.ToString)
            'FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, _
            'True) 'chkPersistCookie.Checked)
        Else
            Response.Redirect("Login.aspx", True)
        End If
    End Sub
    Private Sub RedirectFromLoginPage(pstrURL As String)
        Dim cQueryString As String = Request.QueryString("RequestURL")
        Dim cPath As String = "~/Default.aspx"
        If cQueryString IsNot Nothing Then
            cPath = cQueryString.Replace("%2f", "/")
        End If
        Response.Redirect(cPath)
    End Sub
End Class