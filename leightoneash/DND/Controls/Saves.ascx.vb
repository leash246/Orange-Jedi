Public Class Saves
    Inherits System.Web.UI.UserControl
#Region " Ability Modifier Properties "
    Public Property StrMod As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(lblGrappleStr.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            lblGrappleStr.Text = value.ToString
        End Set
    End Property
    Public Property DexMod As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(lblDexSave.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            lblDexSave.Text = value.ToString
        End Set
    End Property
    Public Property ConMod As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(lblConSave.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            lblConSave.Text = value.ToString
        End Set
    End Property
    Public Property WisMod As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(lblWisSave.Text, nReturn)
            Return nReturn
        End Get
        Set(value As Integer)
            lblWisSave.Text = value.ToString
        End Set
    End Property
#End Region
#Region " Saving Throw Properties "
    Public ReadOnly Property FortitudeSave As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(lblFortSave.Text, nReturn)
            Return nReturn
        End Get
    End Property
    Public ReadOnly Property ReflexSave As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(lblRefSave.Text, nReturn)
            Return nReturn
        End Get
    End Property
    Public ReadOnly Property WillSave As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(lblWillSave.Text, nReturn)
            Return nReturn
        End Get
    End Property
#End Region
#Region " Other Properties"
    Public ReadOnly Property BaseAttackBonus As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(txtBaseAttack.Text, nReturn)
            Return nReturn
        End Get
    End Property
    Public ReadOnly Property SpellResist As Integer
        Get
            Dim nReturn As Integer
            Integer.TryParse(txtSpellResist.Text, nReturn)
            Return nReturn
        End Get
    End Property

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitDefaults()
        End If
        UpdateSavingThrows()
        UpdateGrapple()
    End Sub
    Public Sub InitDefaults()
        txtFortBase.Text = "2"
        txtRefBase.Text = "2"
        txtWillBase.Text = "0"
        txtBaseAttack.Text = "1"
        lblGrappleBaseAttack.Text = BaseAttackBonus
        txtSpellResist.Text = "0"
    End Sub

    Private Sub UpdateSavingThrows()

        Dim nBase, nAbility, nMagic, nMisc, nTemp As Integer
        Integer.TryParse(txtFortBase.Text, nBase)
        nAbility = ConMod
        Integer.TryParse(txtMagicModFort.Text, nMagic)
        Integer.TryParse(txtMiscModFort.Text, nMisc)
        Integer.TryParse(txtTempModFort.Text, nTemp)
        lblFortSave.Text = (nBase + nAbility + nMagic + nMisc + nTemp).ToString

        Integer.TryParse(txtRefBase.Text, nBase)
        nAbility = DexMod
        Integer.TryParse(txtMagicModRef.Text, nMagic)
        Integer.TryParse(txtMiscModRef.Text, nMisc)
        Integer.TryParse(txtTempModRef.Text, nTemp)
        lblRefSave.Text = (nBase + nAbility + nMagic + nMisc + nTemp).ToString

        Integer.TryParse(txtWillBase.Text, nBase)
        nAbility = WisMod
        Integer.TryParse(txtMagicModWill.Text, nMagic)
        Integer.TryParse(txtMiscModWill.Text, nMisc)
        Integer.TryParse(txtTempModWill.Text, nTemp)
        lblWillSave.Text = (nBase + nAbility + nMagic + nMisc + nTemp).ToString
    End Sub
    Private Sub UpdateGrapple()
        Dim nBaseAttack, nStr, nSize, nMisc As Integer
        Integer.TryParse(lblGrappleBaseAttack.Text, nBaseAttack)
        nStr = StrMod
        Integer.TryParse(lblGrappleSize.Text, nSize)
        Integer.TryParse(txtGrappleMisc.Text, nMisc)
        lblGrapple.Text = (nBaseAttack + nStr + nSize + nMisc).ToString
    End Sub
End Class