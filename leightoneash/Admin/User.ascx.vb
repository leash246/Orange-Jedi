Public Class uctrlUser
    Inherits System.Web.UI.UserControl
    Public Property Username As String
        Get
            Return lblUserName.Text
        End Get
        Set(value As String)
            lblUserName.Text = value
        End Set
    End Property
    Public Property Name As String
        Get
            Return lblName.Text
        End Get
        Set(value As String)
            lblName.Text = value
        End Set
    End Property
    Public Property Admin As Boolean
        Get
            Return chkAdmin.Checked
        End Get
        Set(value As Boolean)
            chkAdmin.Checked = value
        End Set
    End Property
    Public Property Dragons As Boolean
        Get
            Return chkDragons.Checked
        End Get
        Set(value As Boolean)
            chkDragons.Checked = value
        End Set
    End Property
    Public Property NFL As Boolean
        Get
            Return chkNFL.Checked
        End Get
        Set(value As Boolean)
            chkNFL.Checked = value
        End Set
    End Property
    Public Property Cards As Boolean
        Get
            Return chkCards.Checked
        End Get
        Set(value As Boolean)
            chkCards.Checked = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Dim nRole As Integer = 0
        If Admin Then nRole += Enums.enmRoles.Admin
        If Dragons Then nRole += Enums.enmRoles.Dragons
        If NFL Then nRole += Enums.enmRoles.NFL
        If Cards Then nRole += Enums.enmRoles.Cards
        Dim cnn As New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings.Get("OrangeJedi"))
        Dim cmd As New System.Data.SqlClient.SqlCommand("[Admin].usp_Upsert_User", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@cEmail", Username)
        cmd.Parameters.AddWithValue("@nRole", nRole)
        Try
            cnn.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            cnn.Close()
        End Try

    End Sub
End Class