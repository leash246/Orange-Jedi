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
    Private Function ValidateUser(ByVal userName As String, ByVal passWord As String) As Boolean
        Dim lookupPassword As String

        lookupPassword = Nothing
        Dim drUser As DataRow
        ' Check for an invalid userName.
        ' userName  must not be set to nothing and must be between one and 50 characters.
        If ((userName Is Nothing)) Then
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.")
            Return False
        End If
        If ((userName.Length = 0) Or (userName.Length > 50)) Then
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.")
            Return False
        End If

        ' Check for invalid passWord.
        ' passWord must not be set to nothing and must be between one and 50 characters.
        If (passWord Is Nothing) Then
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of passWord failed.")
            Return False
        End If
        If ((passWord.Length = 0) Or (passWord.Length > 50)) Then
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of passWord failed.")
            Return False
        End If

        Try
            drUser = LoginUser(txtUserName.Text)
            
            If drUser IsNot Nothing Then
                lookupPassword = drUser.Item("cPWD")
            End If
        Catch ex As Exception
            ' Add error handling here for debugging.
            ' This error message should not be sent back to the caller.
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " & ex.Message)
        End Try

        ' If no password found, return false.
        If (lookupPassword Is Nothing) Then
            ' You could write failed login attempts here to the event log for additional security.
            Session("LoginError") = "Username not found"
            Return False
        End If
        ' Compare lookupPassword and input passWord by using a case-sensitive comparison.
        Dim cHashPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(passWord, "MD5")
        If (String.Compare(lookupPassword, cHashPassword, False) = 0) Then
            objUser = New User(drUser)
            Return True
        Else
            Session("LoginError") = "Incorrect Password"
            objUser = Nothing
            Return False
        End If

    End Function
    Private Sub cmdLogin_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        If ValidateUser(txtUserName.Text, txtUserPass.Text) Then
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