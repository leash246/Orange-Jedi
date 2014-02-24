Imports System.Data.SqlClient
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
End Module
