Public Class Weapon
    Inherits System.Web.UI.UserControl
#Region " Weapon Properties "
    Dim nStartingAmmo As Integer = 0
    Public Property Attack As String
        Get
            Return txtAttack.Text.Trim
        End Get
        Set(value As String)
            txtAttack.Text = value
        End Set
    End Property
    Public Property AttackBonus As String
        Get
            Return txtAttackBonus.Text
        End Get
        Set(value As String)
            txtAttackBonus.Text = value
        End Set
    End Property
    Public Property Damage As String
        Get
            Return txtDamage.Text
        End Get
        Set(value As String)
            txtDamage.Text = value
        End Set
    End Property
    Public Property Critical As String
        Get
            Return txtCritical.Text
        End Get
        Set(value As String)
            txtCritical.Text = value
        End Set
    End Property
    Public Property Range As String
        Get
            Return txtRange.Text
        End Get
        Set(value As String)
            txtRange.Text = value
        End Set
    End Property
    Public Property Type As String
        Get
            Return txtType.Text
        End Get
        Set(value As String)
            txtType.Text = value
        End Set
    End Property
    Public Property Notes As String
        Get
            Return txtNotes.Text
        End Get
        Set(value As String)
            txtNotes.Text = value
        End Set
    End Property
    Public Property AmmoType As String
        Get
            Return txtAmmunitionType.Text
        End Get
        Set(value As String)
            txtAmmunitionType.Text = value
        End Set
    End Property
    Public Property AmmoAvailable As Integer
        Get
            Return (From ri As RepeaterItem In rptAmmunitionChecks.Items _
                    Where ri.ItemType = ListItemType.Item _
                    Or ri.ItemType = ListItemType.AlternatingItem _
                    Select ri.FindControl("chkAmmunition")).Count(Function(chkAmmo) DirectCast(chkAmmo, CheckBox).Checked)
        End Get
        Set(value As Integer)
            nStartingAmmo = value
        End Set
    End Property
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
        End If

        InitDefaults()
    End Sub

    Private Sub InitDefaults()
        Dim dt As New DataTable
        dt.Columns.Add("nBoxNumber")
        dt.Columns.Add("lChecked")

        For i As Integer = 1 To 25
            Dim dr As DataRow = dt.NewRow
            dr.Item("nBoxNumber") = i
            If nStartingAmmo > 0 Then
                dr.Item("lChecked") = (i <= nStartingAmmo)
            Else
                dr.Item("lChecked") = False
            End If
            dt.Rows.Add(dr)
        Next
        rptAmmunitionChecks.DataSource = dt
        rptAmmunitionChecks.DataBind()
    End Sub
    Private Sub rptAmmunitionChecks_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptAmmunitionChecks.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If oItem.ItemType = ListItemType.Item Or oItem.ItemType = ListItemType.AlternatingItem Then
            Dim chkAmmo As CheckBox = oItem.FindControl("chkAmmunition")
            Dim dr As DataRowView = oItem.DataItem
            chkAmmo.Checked = dr.Item("lChecked")
            If dr.Item("nBoxNumber") Mod 5 = 0 Then
                chkAmmo.Style.Add("margin-right", "5px")
            End If
            AddHandler chkAmmo.CheckedChanged, AddressOf chkAmmunition_CheckedChanged
        End If
    End Sub
    Private Sub chkAmmunition_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim chkAmmo As CheckBox = DirectCast(sender, CheckBox)
        If chkAmmo.Checked Then
            nStartingAmmo += 1
        Else
            nStartingAmmo -= 1
        End If
    End Sub
End Class