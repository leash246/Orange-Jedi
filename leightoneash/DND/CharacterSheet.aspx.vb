Imports System.Data.SqlClient

Public Class CharacterSheet
    Inherits BasePage

    Private Sub CharacterSheet_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            ctrlAbilities.InitDefaults(1)
        End If

        ctrlAbilities.UpdateAbilityLabels()
        Dim nDexMod As Integer = ctrlAbilities.DexMod
        ctrlDefenses.DexMod = nDexMod
        ctrlSaves.DexMod = nDexMod
        ctrlSaves.ConMod = ctrlAbilities.ConMod
        ctrlSaves.WisMod = ctrlAbilities.WisMod
        ctrlSaves.StrMod = ctrlAbilities.StrMod
        ctrlDefenses.UpdateTotalArmor()
        ctrlDefenses.UpdateTotalHP()
        ctrlDefenses.UpdateInitiativeModifier()
        SetUpWeapons()
        SetUpSkills()
    End Sub
    Private Sub SetUpWeapons(Optional ByVal nCharID As Integer = 1)
        Dim dt As New DataTable
        dt.Columns.Add("cAttack")
        dt.Columns.Add("cAttackBonus")
        dt.Columns.Add("cDamage")
        dt.Columns.Add("cCritical")
        dt.Columns.Add("cRange")
        dt.Columns.Add("cType")
        dt.Columns.Add("cNotes")
        dt.Columns.Add("cAmmunition")
        dt.Columns.Add("nAmmunition")

        Dim cmd As New SqlCommand("DandD.usp_Get_Attacks", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@nCharID", 1)

        Dim dtImport As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        Try
            cmd.Connection.Open()
            da.Fill(dtImport)

            Dim i As Integer = 0
            For i = 0 To dtImport.Rows.Count - 1
                Dim dr As DataRow = dtImport.Rows(i)
                Dim drNewRow As DataRow = dt.NewRow
                With drNewRow
                    .Item("cAttack") = dr.Item("cAttackName")
                    Select Case dr.Item("cAttackModifier")
                        Case "Dex"
                            .Item("cAttackBonus") = ctrlAbilities.DexMod + ctrlSaves.BaseAttackBonus
                        Case "Str"
                            .Item("cAttackBonus") = ctrlAbilities.StrMod + ctrlSaves.BaseAttackBonus
                        Case Else
                            .Item("cAttackBonus") = ctrlSaves.BaseAttackBonus
                    End Select
                    .Item("cDamage") = dr.Item("cDamage")
                    .Item("cCritical") = dr.Item("cCritRange")
                    .Item("cRange") = dr.Item("cRange")
                    .Item("cType") = dr.Item("cDamageType")
                    If IsDBNull(dr.Item("cAmmoType")) Then
                        .Item("cAmmunition") = ""
                    Else
                        .Item("cAmmunition") = dr.Item("cAmmoType")
                    End If
                    If IsDBNull(dr.Item("nAmmoQuantity")) Then
                        .Item("nAmmunition") = 0
                    Else
                        .Item("nAmmunition") = dr.Item("nAmmoQuantity")
                    End If
                End With
                dt.Rows.Add(drNewRow)
            Next
            While i < 5
                Dim drEmpty As DataRow = dt.NewRow
                With drEmpty
                    .Item("cAttack") = ""
                    .Item("cAttackBonus") = ""
                    .Item("cDamage") = ""
                    .Item("cCritical") = ""
                    .Item("cRange") = ""
                    .Item("cType") = ""
                    .Item("cAmmunition") = ""
                    .Item("nAmmunition") = 0
                End With
                dt.Rows.Add(drEmpty)
                i += 1
            End While

        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try

        repWeapons.DataSource = dt
        repWeapons.DataBind()

    End Sub
    Private Sub repWeapons_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repWeapons.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If oItem.ItemType = ListItemType.Item Or oItem.ItemType = ListItemType.AlternatingItem Then
            Dim ctrlWeapon As Weapon = oItem.FindControl("ctrlWeapon")
            Dim dr As DataRowView = oItem.DataItem
            With ctrlWeapon
                .Attack = dr.Item("cAttack")
                .AttackBonus = dr.Item("cAttackBonus")
                .Damage = dr.Item("cDamage")
                .Critical = dr.Item("cCritical")
                .Range = dr.Item("cRange")
                .Type = dr.Item("cType")
                .AmmoType = dr.Item("cAmmunition")
                .AmmoAvailable = CInt(dr.Item("nAmmunition"))
            End With
        End If
    End Sub

    Private Sub SetUpSkills(Optional ByVal nCharID As Integer = 1)
        Dim cnn As SqlConnection = Connection()
        Dim cmdClassSkills As New SqlCommand("DandD.usp_Get_ClassSkills", cnn)
        cmdClassSkills.CommandType = CommandType.StoredProcedure
        cmdClassSkills.Parameters.AddWithValue("@nCharID", nCharID)
        Dim cmdSkillRanks As New SqlCommand("DandD.usp_Get_SkillRanks", cnn)
        cmdSkillRanks.CommandType = CommandType.StoredProcedure
        cmdSkillRanks.Parameters.AddWithValue("@nCharID", nCharID)
        Dim dtClassSkills As New DataTable
        Dim daClassSkills As New SqlDataAdapter(cmdClassSkills)
        Dim dtSkillRanks As New DataTable
        Dim daSkillRanks As New SqlDataAdapter(cmdSkillRanks)

        Try
            cnn.Open()
            daClassSkills.Fill(dtClassSkills)
            daSkillRanks.Fill(dtSkillRanks)
        Catch ex As Exception
        Finally
            cnn.Close()
        End Try
        If dtClassSkills.Rows.Count > 0 AndAlso dtSkillRanks.Rows.Count > 0 Then
            Dim dt As New DataTable
            dt.Columns.Add("lClassSkill")
            dt.Columns.Add("cSkillName")
            dt.Columns.Add("cSkillOption")
            dt.Columns.Add("lUntrained")
            dt.Columns.Add("cKeyAbility")
            dt.Columns.Add("lArmorCheck")
            dt.Columns.Add("nRanks")
            dt.Columns.Add("nMiscMod")
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Appraise", True, "Int", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Balance", True, "Dex", True, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Bluff", True, "Cha", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Climb", True, "Str", True, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Concentration", True, "Con", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Craft", True, "Int", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Decipher Script", False, "Int", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Diplomacy", True, "Cha", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Disable Device", False, "Int", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Disguise", True, "Cha", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Escape Artist", True, "Dex", True, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Forgery", True, "Int", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Gather Information", True, "Cha", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Handle Animal", False, "Cha", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Heal", True, "Wis", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Hide", True, "Dex", True, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Intimidate", True, "Cha", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Jump", True, "Str", True, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Knowledge", False, "Int", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Listen", True, "Wis", False, 2)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Move Silently", True, "Dex", True, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Open Lock", False, "Dex", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Perform", False, "Cha", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Profession", False, "Wis", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Ride", True, "Dex", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Search", True, "Int", False, 2)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Sense Motive", True, "Wis", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Sleight of Hand", False, "Dex", True, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Spellcraft", False, "Int", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Spot", True, "Wis", False, 2)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Survival", True, "Wis", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Swim", True, "Str", True, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Tumble", False, "Dex", True, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Use Magic Device", False, "Cha", False, 0)
            AddSkillRow(dt, dtClassSkills, dtSkillRanks, "Use Rope", True, "Dex", False, 0)

            repSkills.DataSource = dt
            repSkills.DataBind()
        End If
    End Sub

    Private Sub repSkills_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repSkills.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If oItem.ItemType = ListItemType.Item Or oItem.ItemType = ListItemType.AlternatingItem Then
            Dim ctrlSkillLine As SkillLine = oItem.FindControl("ctrlSkillLine")
            Dim dr As DataRowView = oItem.DataItem
            ctrlSkillLine.SetSkillValues(dr, ctrlAbilities)
        End If
    End Sub
End Class