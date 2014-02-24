Public Class Abilities
    Inherits System.Web.UI.UserControl
#Region " Ability Modifier Properties "
    Public ReadOnly Property StrMod As Integer
        Get
            Dim nReturn As Integer
            If lblStrTemp.Text <> "" Then
                Integer.TryParse(lblStrTemp.Text, nReturn)
            Else
                Integer.TryParse(lblStr.Text, nReturn)
            End If
            Return nReturn
        End Get
    End Property
    Public ReadOnly Property DexMod As Integer
        Get
            Dim nReturn As Integer
            If lblDexTemp.Text <> "" Then
                Integer.TryParse(lblDexTemp.Text, nReturn)
            Else
                Integer.TryParse(lblDex.Text, nReturn)
            End If
            Return nReturn
        End Get
    End Property
    Public ReadOnly Property ConMod As Integer
        Get
            Dim nReturn As Integer
            If lblConTemp.Text <> "" Then
                Integer.TryParse(lblConTemp.Text, nReturn)
            Else
                Integer.TryParse(lblCon.Text, nReturn)
            End If
            Return nReturn
        End Get
    End Property
    Public ReadOnly Property IntMod As Integer
        Get
            Dim nReturn As Integer
            If lblIntTemp.Text <> "" Then
                Integer.TryParse(lblIntTemp.Text, nReturn)
            Else
                Integer.TryParse(lblInt.Text, nReturn)
            End If
            Return nReturn
        End Get
    End Property
    Public ReadOnly Property WisMod As Integer
        Get
            Dim nReturn As Integer
            If lblWisTemp.Text <> "" Then
                Integer.TryParse(lblWisTemp.Text, nReturn)
            Else
                Integer.TryParse(lblWis.Text, nReturn)
            End If
            Return nReturn
        End Get
    End Property
    Public ReadOnly Property ChaMod As Integer
        Get
            Dim nReturn As Integer
            If lblChaTemp.Text <> "" Then
                Integer.TryParse(lblChaTemp.Text, nReturn)
            Else
                Integer.TryParse(lblCha.Text, nReturn)
            End If
            Return nReturn
        End Get
    End Property
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitDefaults()
        End If
        UpdateAbilityLabels()
    End Sub
    Public Sub InitDefaults()
        FillDropDowns()
        ddlStr.SelectedValue = 16
        ddlDex.SelectedValue = 15
        ddlCon.SelectedValue = 10
        ddlInt.SelectedValue = 9
        ddlWis.SelectedValue = 15
        ddlCha.SelectedValue = 8

    End Sub
    Public Sub FillDropDowns()
        Dim lstAbilities As New List(Of Integer)
        For i As Integer = 0 To 20
            lstAbilities.Add(i)
        Next
        ddlStr.DataSource = lstAbilities
        ddlStr.DataBind()
        ddlDex.DataSource = lstAbilities
        ddlDex.DataBind()
        ddlCon.DataSource = lstAbilities
        ddlCon.DataBind()
        ddlInt.DataSource = lstAbilities
        ddlInt.DataBind()
        ddlWis.DataSource = lstAbilities
        ddlWis.DataBind()
        ddlCha.DataSource = lstAbilities
        ddlCha.DataBind()
    End Sub

    Public Sub UpdateAbilityLabels()

        Dim nStr, nDex, nCon, nInt, nWis, nCha As Integer

        Integer.TryParse(ddlStr.SelectedValue, nStr)
        lblStr.Text = Math.Floor((nStr - 10) / 2)
        If txtStrTemp.Text <> "" Then
            lblStrTemp.Text = Math.Floor((nStr + CInt(txtStrTemp.Text) - 10) / 2)
        Else
            lblStrTemp.Text = ""
        End If
        Integer.TryParse(ddlDex.SelectedValue, nDex)
        lblDex.Text = Math.Floor((nDex - 10) / 2)
        If txtDexTemp.Text <> "" Then
            lblDexTemp.Text = Math.Floor((nDex + CInt(txtDexTemp.Text) - 10) / 2)
        Else
            lblDexTemp.Text = ""
        End If
        Integer.TryParse(ddlCon.SelectedValue, nCon)
        lblCon.Text = Math.Floor((nCon - 10) / 2)
        If txtConTemp.Text <> "" Then
            lblConTemp.Text = Math.Floor((nCon + CInt(txtConTemp.Text) - 10) / 2)
        Else
            lblConTemp.Text = ""
        End If
        Integer.TryParse(ddlInt.SelectedValue, nInt)
        lblInt.Text = Math.Floor((nInt - 10) / 2)
        If txtIntTemp.Text <> "" Then
            lblIntTemp.Text = Math.Floor((nInt + CInt(txtIntTemp.Text) - 10) / 2)
        Else
            lblIntTemp.Text = ""
        End If
        Integer.TryParse(ddlWis.SelectedValue, nWis)
        lblWis.Text = Math.Floor((nWis - 10) / 2)
        If txtWisTemp.Text <> "" Then
            lblWisTemp.Text = Math.Floor((nWis + CInt(txtWisTemp.Text) - 10) / 2)
        Else
            lblWisTemp.Text = ""
        End If
        Integer.TryParse(ddlCha.SelectedValue, nCha)
        lblCha.Text = Math.Floor((nCha - 10) / 2)
        If txtChaTemp.Text <> "" Then
            lblChaTemp.Text = Math.Floor((nCha + CInt(txtChaTemp.Text) - 10) / 2)
        Else
            lblChaTemp.Text = ""
        End If

    End Sub
End Class