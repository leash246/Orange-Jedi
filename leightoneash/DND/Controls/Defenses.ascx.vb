Imports System.Data.SqlClient

Public Class Defenses
    Inherits System.Web.UI.UserControl
#Region "AC Mods"
    Public Property ArmorBonus As Integer
        Get
            Dim nReturn As Integer = 0
            Integer.TryParse(txtArmorBonus.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            txtArmorBonus.Text = value.ToString
        End Set
    End Property
    Public Property ShieldBonus As Integer
        Get
            Dim nReturn As Integer = 0
            Integer.TryParse(txtShieldBonus.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            txtShieldBonus.Text = value.ToString
        End Set
    End Property
    Public Property DexMod As Integer
        Get
            Dim nReturn As Integer = 0
            Integer.TryParse(lblDexMod.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            lblDexMod.Text = value.ToString
            lblDexInit.Text = value.ToString
        End Set
    End Property
    Public Property SizeMod As Integer
        Get
            Dim nReturn As Integer = 0
            Integer.TryParse(txtSizeMod.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            txtSizeMod.Text = value.ToString
        End Set
    End Property
    Public Property NaturalArmor As Integer
        Get
            Dim nReturn As Integer = 0
            Integer.TryParse(txtNaturalArmor.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            txtNaturalArmor.Text = value.ToString
        End Set
    End Property
    Public Property DeflectionMod As Integer
        Get
            Dim nReturn As Integer = 0
            Integer.TryParse(txtDeflectionMod.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            txtDeflectionMod.Text = value.ToString
        End Set
    End Property
    Public Property MiscMod As Integer
        Get
            Dim nReturn As Integer = 0
            Integer.TryParse(txtMiscMod.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            txtMiscMod.Text = value.ToString
        End Set
    End Property
    Public ReadOnly Property TouchArmor As Integer
        Get
            Return 10 + DexMod + SizeMod + DeflectionMod + MiscMod
        End Get
    End Property
    Public ReadOnly Property FlatFooted As Integer
        Get
            Return 10 + ArmorBonus + ShieldBonus + SizeMod + NaturalArmor + DeflectionMod + MiscMod
        End Get
    End Property
    Public ReadOnly Property InitiativeMod As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(lblInitMod.Text, nReturn)
            Return nReturn
        End Get
    End Property
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            FillDropDowns()
            InitDefaults()
        End If
        UpdateTotalHP()
        UpdateTotalArmor()
        UpdateInitiativeModifier()
    End Sub
    Public Sub InitDefaults(Optional ByVal nCharID As Integer = 1)
        Dim cmd As New SqlCommand("DandD.usp_Get_CharacterInfo", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@nCharId", nCharID)
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        Try
            cmd.Connection.Open()
            da.Fill(dt)
            If dt.Rows.Count = 1 Then
                ddlMaxHP.SelectedValue = dt.Rows(0).Item("nMaxHP")
                txtArmorBonus.Text = 2 'This is hardcoded, need to create armor stuff still
            End If
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
    End Sub
    Private Sub FillDropDowns()
        Dim lstHP As New List(Of Integer)
        For i As Integer = 0 To 100
            lstHP.Add(i)
        Next
        ddlMaxHP.DataSource = lstHP
        ddlMaxHP.DataBind()
    End Sub
    Public Sub UpdateTotalArmor()
        Dim nArmorBonus, nShieldBonus, nDexMod, nSizeMod, nNaturalArmor, nDefMod, nMiscMod As Integer
        Integer.TryParse(txtArmorBonus.Text, nArmorBonus)
        Integer.TryParse(txtShieldBonus.Text, nShieldBonus)
        Integer.TryParse(DexMod, nDexMod)
        Integer.TryParse(txtSizeMod.Text, nSizeMod)
        Integer.TryParse(txtNaturalArmor.Text, nNaturalArmor)
        Integer.TryParse(txtDeflectionMod.Text, nDefMod)
        Integer.TryParse(txtMiscMod.Text, nMiscMod)

        lblAC.Text = CStr(10 + nArmorBonus + nShieldBonus + nDexMod + nSizeMod + nNaturalArmor + nDefMod + nMiscMod)
        lblTouch.Text = TouchArmor.ToString
        lblFlat.Text = FlatFooted.ToString
    End Sub

    Public Sub UpdateTotalHP()
        Dim strWounds As String = txtWounds.Text.Replace("-", ",-").Replace("+", ",+").Replace(",,", ",")
        strWounds = strWounds.TrimStart(",")
        Dim indWounds() As String = strWounds.Split(",")
        Dim nWounds As Integer = 0
        For Each intWound As String In indWounds
            Dim nWound As Integer = 0
            Integer.TryParse(intWound, nWound)
            nWounds += nWound
        Next
        Dim nMaxHP As Integer
        Integer.TryParse(ddlMaxHP.SelectedValue, nMaxHP)
        Dim nCurrentHP As Integer = nMaxHP + nWounds
        lblCurrentHP.Text = nCurrentHP.ToString
    End Sub
    Public Sub UpdateInitiativeModifier()
        Dim nMiscMod As Integer
        Integer.TryParse(txtMiscInit.Text, nMiscMod)
        lblInitMod.Text = (CInt(lblDexInit.Text) + nMiscMod).ToString
    End Sub
End Class