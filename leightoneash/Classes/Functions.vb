Imports System.Data.SqlClient
Imports System.Net.Mail
Module Functions
    Public Function LoginUser(pstrEmail As String) As DataRow
        Dim cmd As New SqlCommand("[Admin].usp_Login", Connection())
        cmd.Parameters.AddWithValue("@cEmail", pstrEmail)
        cmd.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        Try
            cmd.Connection.Open()
            da.Fill(dt)
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
        If dt.Rows.Count = 1 Then
            Return dt.Rows(0)
        Else
            Return Nothing
        End If
    End Function
    Public Function ConnectionString() As String
        Dim cnnstr As String = ConfigurationSettings.AppSettings.Get("OrangeJedi")
        Return cnnstr
    End Function
    Public Function Connection() As SqlConnection
        Dim cnn As New SqlConnection(ConnectionString)
        Return cnn
    End Function
    Public Sub SendEmail(ByVal cSubject As String, ByVal cBody As String, ParamArray CCs As String())
        Dim mMail As New MailMessage("noreply@leightoneash.com", "leighton@leightoneash.com", cSubject, cBody)
        For Each cc As String In CCs
            mMail.CC.Add(cc)
        Next
        Dim objSMTP As New SmtpClient("relay-hosting.secureserver.net", 25)
        objSMTP.UseDefaultCredentials = False
        objSMTP.Credentials = New System.Net.NetworkCredential(mMail.From.ToString, "hooray1")
        objSMTP.DeliveryMethod = SmtpDeliveryMethod.Network
        objSMTP.Port = 25
        objSMTP.EnableSsl = False
        objSMTP.Send(mMail)

    End Sub
    Public Function ValidateUser(ByVal userName As String, ByVal passWord As String, ByRef objUser As User) As Boolean
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
            drUser = LoginUser(userName)

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
            'Session("LoginError") = "Username not found"
            Return False
        End If
        ' Compare lookupPassword and input passWord by using a case-sensitive comparison.
        Dim cHashPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(passWord, "MD5")
        If (String.Compare(lookupPassword, cHashPassword, False) = 0) Then
            objUser = New User(drUser)
            Return True
        Else
            'Session("LoginError") = "Incorrect Password"
            objUser = Nothing
            Return False
        End If

    End Function

End Module
