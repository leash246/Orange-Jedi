Public Class SkillLine
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub SetSkillValues(dr As DataRowView, ctrlAbilities As Abilities)
        With dr
            chkClassSkill.Checked = .Item("lClassSkill")
            lblSkillName.Text = .Item("cSkillName")
            If .Item("cSkillName") = "Craft" Or .Item("cSkillName") = "Knowledge" Or
                .Item("cSkillName") = "Perform" Or .Item("cSkillName") = "Profession" Then
                lblLeftParen.Visible = True
                txtSkillOption.Visible = True
                txtSkillOption.Text = .Item("cSkillOption")
                lblRightParen.Visible = True
            Else
                lblLeftParen.Visible = False
                txtSkillOption.Visible = False
                lblRightParen.Visible = False
            End If
            lblUntrained.Visible = .Item("lUntrained")
            lblAbility.Text = .Item("cKeyAbility")
            lblArmorCheck.Visible = .Item("lArmorCheck")
            Select Case .Item("cKeyAbility")
                Case "Str"
                    lblAbilityMod.Text = ctrlAbilities.StrMod
                Case "Dex"
                    lblAbilityMod.Text = ctrlAbilities.DexMod
                Case "Con"
                    lblAbilityMod.Text = ctrlAbilities.ConMod
                Case "Int"
                    lblAbilityMod.Text = ctrlAbilities.IntMod
                Case "Cha"
                    lblAbilityMod.Text = ctrlAbilities.ChaMod
                Case "Wis"
                    lblAbilityMod.Text = ctrlAbilities.WisMod
            End Select
            Dim nRanks As Integer = 0
            Integer.TryParse(.Item("nRanks"), nRanks)
            txtRanks.Text = IIf(nRanks > 0, nRanks, "")
            Dim nMisc As Integer = 0
            Integer.TryParse(.Item("nMiscMod"), nMisc)
            txtMisc.Text = IIf(nMisc > 0, nMisc, "")
            If .Item("lUntrained") = False And nRanks = 0 Then
                lblSkillMod.Text = ""
            Else
                lblSkillMod.Text = CInt(lblAbilityMod.Text) + nRanks + nMisc
            End If
        End With
    End Sub
End Class