Imports System.Web.Security
Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString.Count = 2 Then
            SignIn(Request.QueryString.Item(0), Request.QueryString.Item(1))
            Response.Redirect("~/Default.aspx")
        End If
        InitMenu()
    End Sub
    Private Sub cmdSignOut_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles mnuLogout.Click
        Session.Remove("User")
        Response.Redirect("~/Account/Login.aspx", True)
    End Sub
    Private Sub InitMenu()

        Dim objUser As User = Session("User")
        If objUser Is Nothing OrElse objUser.Email = "Anonymous" Then
            divUser.Visible = False
            divLogin.Visible = True
        Else
            divLogin.Visible = False
            divUser.Visible = True
            lblUser.Text = objUser.FirstName
        End If
        'mnuLogin.Visible = False
        'mnuLogout.Visible = True
        'If Debugger.IsAttached Then mnuBlog.Href = "http://www.leightoneash.com/OrangeJedi"
        'If objUser Is Nothing OrElse objUser.Email = "Anonymous" Then
        '    objUser = New User("Anonymous", "", "", "", 10)
        '    mnuLogin.Visible = True
        '    mnuLogout.Visible = False
        '    'mnuDND.Visible = False
        '    mnuNFL.Visible = False
        '    mnuHelp.Visible = False
        '    Session("User") = objUser
        'End If
        'If objUser IsNot Nothing Then
        '    If Not objUser.ValidRole(Enums.enmRoles.Admin) Then
        '        mnuAdmin.Visible = False
        '        mnuNFLAdmin.Visible = False
        '        If InStr(Request.Path.ToString, "/Admin/") > 0 Then
        '            Response.Redirect("~/NotAuthorized.aspx")
        '        End If
        '    End If
        '    If Not objUser.ValidRole(Enums.enmRoles.NFL) Then
        '        mnuNFL.Visible = False
        '        If InStr(Request.Path.ToString, "/NFL/") > 0 Then
        '            Response.Redirect("~/NotAuthorized.aspx")
        '        End If
        '    End If
        'ElseIf Request.Path.ToString = "/Account/Login.aspx" OrElse Request.Path.ToString = "/Account/Register.aspx" Then
        '    mnuAdmin.Visible = False
        '    mnuNFL.Visible = False
        '    mnuCards.Visible = False
        'ElseIf Request.Path.ToString <> "/Account/Login.aspx" Then
        '    Response.Redirect("/Account/Login.aspx?RequestURL=" & RequestPath(Request.Path.ToString))
        'End If
    End Sub
    Private Function RequestPath(pstrURL As String) As String
        Return pstrURL.Replace("/", "%2f")
    End Function

    Private Sub SignIn()
        SignIn(txtUserName.Value, txtPassword.Value)
    End Sub
    Private Sub SignIn(userName As String, passWord As String)
        Dim objUser As User
        If ValidateUser(userName, passWord, objUser) Then
            Session("User") = objUser
        End If
    End Sub

    Private Sub btnSignIn_ServerClick(sender As Object, e As System.EventArgs) Handles btnSignIn.ServerClick
        SignIn()
    End Sub

    Private Sub btnSignOut_ServerClick(sender As Object, e As System.EventArgs) Handles btnSignOut.ServerClick
        Session.Remove("User")
        Response.Redirect("~/Default.aspx")
    End Sub
End Class