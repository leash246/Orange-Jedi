Imports System.Data.SqlClient
Namespace Account
    Public Class ChangePassword
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Session("LoginError") IsNot Nothing Then
                FailureText.Text = Session("LoginError").ToString
                Session.Remove("LoginError")
            End If
        End Sub

        Private Sub ChangeUserPassword_ChangingPassword(sender As Object, e As System.EventArgs) Handles ChangePasswordPushButton.Click
            Dim cmd As New SqlCommand("[Admin].usp_Upsert_User", New SqlConnection(ConfigurationSettings.AppSettings.Item("OrangeJedi")))
            Dim objUser As User = Session("User")
            Dim drUser As DataRow = LoginUser(objUser.Email)
            Dim lookupPassword As String = ""
            If drUser IsNot Nothing Then
                lookupPassword = drUser.Item("cPWD")
            End If
            Dim cOldPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(CurrentPassword.Text, "MD5")
            Dim cNewPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(NewPassword.Text, "MD5")
            If (String.Compare(lookupPassword, cOldPassword, False) = 0) Then
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@cEmail", objUser.Email)
                cmd.Parameters.AddWithValue("@cPWD", cNewPassword)
                Try
                    cmd.Connection.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                Finally
                    cmd.Connection.Close()
                End Try
                Response.Redirect("~/Account/ChangePasswordSuccess.aspx")
            Else
                Session("LoginError") = "Password incorrect. Please check your old password."
                Response.Redirect("ChangePassword.aspx")
            End If

        End Sub


        Private Sub CancelPushButton_Click(sender As Object, e As System.EventArgs) Handles CancelPushButton.Click
            Response.Redirect("~/Default.aspx")
        End Sub
    End Class
End Namespace