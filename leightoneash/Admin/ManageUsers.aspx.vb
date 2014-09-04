Imports System.Data.SqlClient

Namespace Admin
    Public Class ManageUsers
        Inherits System.Web.UI.Page
        Dim cnn As New SqlConnection(ConfigurationSettings.AppSettings.Get("OrangeJedi"))
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                LoadActiveUsers()
            End If
        End Sub
        Private Sub LoadActiveUsers()
            Dim cmd As New SqlCommand("Admin.usp_Get_ActiveUsers", cnn)
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(cmd)
            Try
                cnn.Open()
                da.Fill(dt)
            Catch ex As Exception

            Finally
                cnn.Close()
            End Try
            repUsers.DataSource = dt
            repUsers.DataBind()
        End Sub

        Private Sub repUsers_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repUsers.ItemDataBound
            Dim oItem As RepeaterItem = e.Item
            If oItem.ItemType = ListItemType.Item Or oItem.ItemType = ListItemType.AlternatingItem Then
                Dim dr As DataRowView = oItem.DataItem
                Dim objUser As uctrlUser = oItem.FindControl("ctrlUser")
                objUser.Username = dr.Item("cEmail")
                objUser.Name = dr.Item("cName")
                objUser.Admin = (dr.Item("nRole") And Enums.enmRoles.Admin) > 0
                objUser.Dragons = (dr.Item("nRole") And Enums.enmRoles.Dragons) > 0
                objUser.NFL = (dr.Item("nRole") And Enums.enmRoles.NFL) > 0
                objUser.Cards = (dr.Item("nRole") And Enums.enmRoles.Cards) > 0

            End If
        End Sub
    End Class
End Namespace