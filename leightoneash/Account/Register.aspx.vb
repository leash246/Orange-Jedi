Imports System.Data.SqlClient
Imports System.Web.Security

Namespace Account
    Public Class Register
        Inherits BasePage
        Private cnn As SqlConnection = New SqlConnection("server=184.168.47.21;database=orangejedi;uid=WarDoctor;pwd=Fantastic09")
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Session("cErrorMessage") IsNot Nothing Then
                lblMsg.Text = Session("cErrorMessage").ToString
                Session.Remove("cErrorMessage")
            End If
        End Sub

        Private Sub cmdRegister_Click(sender As Object, e As System.EventArgs) Handles cmdRegister.Click
            Dim cMsg As String = ""
            Dim lSuccess As Boolean = True
            If txtEmail.Text <> txtEmail2.Text Then
                cMsg &= "Emails do not match<br>"
                lSuccess = False
            End If
            If txtUserPass.Text <> txtUserPass2.Text Then
                cMsg &= "Passwords do not match<br>"
                lSuccess = False
            End If
            If UserExists(txtEmail.Text) Then
                lSuccess = False
                cMsg &= "User Already Exists"

            End If
            If lSuccess Then
                Try
                    Dim cHashPass As String = FormsAuthentication.HashPasswordForStoringInConfigFile(txtUserPass.Text, "MD5")
                    Dim cmd As New SqlCommand("[Admin].usp_Upsert_User")
                    cmd.Connection = cnn
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@cEmail", txtEmail.Text)
                    cmd.Parameters.AddWithValue("@cFName", txtFName.Text)
                    cmd.Parameters.AddWithValue("@cMName", txtMName.Text)
                    cmd.Parameters.AddWithValue("@cLName", txtLName.Text)
                    cmd.Parameters.AddWithValue("@cPWD", cHashPass)
                    cnn.Open()
                    cmd.ExecuteScalar()
                    cMsg = "User Successfully Added"
                Catch ex As Exception
                    cMsg = ex.Message
                    lSuccess = False
                End Try
            End If
            If lSuccess Then
                Session("User") = New User(txtEmail.Text)
                Response.Redirect("~/Default.aspx")
            Else
                Session("cErrorMessage") = cMsg
                Response.Redirect("Register.aspx")
            End If
        End Sub
        Private Function UserExists(cEmail As String) As Boolean
            Dim lExists As Boolean = True
            Try
                Dim cmd As New SqlCommand("[Admin].usp_UserExists")
                cmd.Connection = cnn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@cEmail", cEmail.Trim)
                cnn.Open()
                lExists = cmd.ExecuteScalar
            Catch ex As Exception
                lExists = True
            Finally
                cnn.Close()
            End Try
            Return lExists
        End Function
    End Class
End Namespace