Imports System.Web.Security
Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        InitMenu()
    End Sub
    Private Sub cmdSignOut_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLogout.Click
        Session.Remove("User")
        Response.Redirect("~/Account/Login.aspx", True)
    End Sub
    Private Sub InitMenu()
        Dim objUser As User = Session("User")
        mnuLogin.Visible = False
        mnuLogout.Visible = True
        If objUser Is Nothing Then
            objUser = New User("Anonymous", "", "", "", 14)
            mnuLogin.Visible = True
            mnuLogout.Visible = False
            'mnuDND.Visible = False
            mnuNFL.Visible = False
            mnuHelp.Visible = False
        End If
        If objUser IsNot Nothing Then
            If Not objUser.ValidRole(Enums.enmRoles.Admin) Then
                mnuAdmin.Visible = False
                mnuNFLAdmin.Visible = False
                If InStr(Request.Path.ToString, "/Admin/") > 0 Then
                    Response.Redirect("~/NotAuthorized.aspx")
                End If
            End If
            If Not objUser.ValidRole(Enums.enmRoles.NFL) Then
                mnuNFL.Visible = False
                If InStr(Request.Path.ToString, "/NFL/") > 0 Then
                    Response.Redirect("~/NotAuthorized.aspx")
                End If
            End If
            If Not objUser.ValidRole(Enums.enmRoles.Cards) Then
                mnuCards.Visible = False
                If InStr(Request.Path.ToString, "/Cards/") > 0 Then
                    Response.Redirect("~/NotAuthorized.aspx")
                End If
            End If
        ElseIf Request.Path.ToString = "/Account/Login.aspx" OrElse Request.Path.ToString = "/Account/Register.aspx" Then
            mnuAdmin.Visible = False
            mnuNFL.Visible = False
            mnuCards.Visible = False
        ElseIf Request.Path.ToString <> "/Account/Login.aspx" Then
            Response.Redirect("/Account/Login.aspx?RequestURL=" & RequestPath(Request.Path.ToString))
        End If
    End Sub
    Private Function RequestPath(pstrURL As String) As String
        Return pstrURL.Replace("/", "%2f")
    End Function
End Class