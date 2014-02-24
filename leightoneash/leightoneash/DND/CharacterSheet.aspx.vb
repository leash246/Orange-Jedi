Public Class CharacterSheet
    Inherits BasePage

    Private Sub CharacterSheet_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            ctrlAbilities.InitDefaults()
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
    Private Sub SetUpWeapons()
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

        Dim drShortbow As DataRow = dt.NewRow
        With drShortbow
            .Item("cAttack") = "Shortbow"
            .Item("cAttackBonus") = ctrlAbilities.DexMod + ctrlSaves.BaseAttackBonus
            .Item("cDamage") = "1d6"
            .Item("cCritical") = "x3"
            .Item("cRange") = "60"
            .Item("cType") = "Piercing"
            .Item("cAmmunition") = "Arrows"
            .Item("nAmmunition") = 20
        End With
        dt.Rows.Add(drShortbow)
        Dim drLongsword As DataRow = dt.NewRow
        With drLongsword
            .Item("cAttack") = "Longsword"
            .Item("cAttackBonus") = ctrlAbilities.StrMod + ctrlSaves.BaseAttackBonus
            .Item("cDamage") = "1d8+3"
            .Item("cCritical") = "19-20/x2"
            .Item("cRange") = "M"
            .Item("cType") = "Slashing"
            .Item("cAmmunition") = ""
            .Item("nAmmunition") = 0
        End With
        dt.Rows.Add(drLongsword)
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
        Dim drEmpty2 As DataRow = dt.NewRow
        With drEmpty2
            .Item("cAttack") = ""
            .Item("cAttackBonus") = ""
            .Item("cDamage") = ""
            .Item("cCritical") = ""
            .Item("cRange") = ""
            .Item("cType") = ""
            .Item("cAmmunition") = ""
            .Item("nAmmunition") = 0
        End With
        dt.Rows.Add(drEmpty2)
        Dim drEmpty3 As DataRow = dt.NewRow
        With drEmpty3
            .Item("cAttack") = ""
            .Item("cAttackBonus") = ""
            .Item("cDamage") = ""
            .Item("cCritical") = ""
            .Item("cRange") = ""
            .Item("cType") = ""
            .Item("cAmmunition") = ""
            .Item("nAmmunition") = 0
        End With
        dt.Rows.Add(drEmpty3)

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

    Private Sub SetUpSkills()
        Dim dt As New DataTable
        dt.Columns.Add("lClassSkill")
        dt.Columns.Add("cSkillName")
        dt.Columns.Add("cSkillOption")
        dt.Columns.Add("lUntrained")
        dt.Columns.Add("cKeyAbility")
        dt.Columns.Add("lArmorCheck")
        dt.Columns.Add("nRanks")
        dt.Columns.Add("nMiscMod")

        dt.Rows.Add(False, "Appraise", "", True, "Int", False, 0, 0)
        dt.Rows.Add(False, "Balance", "", True, "Dex", True, 0, 0)
        dt.Rows.Add(False, "Bluff", "", True, "Cha", False, 0, 0)
        dt.Rows.Add(True, "Climb", "", True, "Str", True, 0, 0)
        dt.Rows.Add(True, "Concentration", "", True, "Con", False, 0, 0)
        dt.Rows.Add(True, "Craft", "Bowmaking", True, "Int", False, 3, 0)
        dt.Rows.Add(True, "Craft", "Leatherworking", True, "Int", False, 3, 0)
        dt.Rows.Add(True, "Craft", "", True, "Int", False, 0, 0)
        dt.Rows.Add(False, "Decipher Script", "", False, "Int", False, 0, 0)
        dt.Rows.Add(False, "Diplomacy", "", True, "Cha", False, 0, 0)
        dt.Rows.Add(False, "Disable Device", "", False, "Int", False, 0, 0)
        dt.Rows.Add(False, "Disguise", "", True, "Cha", False, 0, 0)
        dt.Rows.Add(False, "Escape Artist", "", True, "Dex", True, 0, 0)
        dt.Rows.Add(False, "Forgery", "", True, "Int", False, 0, 0)
        dt.Rows.Add(False, "Gather Information", "", True, "Cha", False, 0, 0)
        dt.Rows.Add(True, "Handle Animal", "", False, "Cha", False, 3, 0)
        dt.Rows.Add(True, "Heal", "", True, "Wis", False, 0, 0)
        dt.Rows.Add(True, "Hide", "", True, "Dex", True, 0, 0)
        dt.Rows.Add(False, "Intimidate", "", True, "Cha", False, 0, 0)
        dt.Rows.Add(True, "Jump", "", True, "Str", True, 0, 0)
        dt.Rows.Add(True, "Knowledge", "Dungeoneering", False, "Int", False, 0, 0)
        dt.Rows.Add(True, "Knowledge", "Geography", False, "Int", False, 0, 0)
        dt.Rows.Add(True, "Knowledge", "Nature", False, "Int", False, 2, 0)
        dt.Rows.Add(False, "Knowledge", "", False, "Int", False, 0, 0)
        dt.Rows.Add(False, "Knowledge", "", False, "Int", False, 0, 0)
        dt.Rows.Add(True, "Listen", "", True, "Wis", False, 1, 2)
        dt.Rows.Add(True, "Move Silently", "", True, "Dex", True, 3, 0)
        dt.Rows.Add(False, "Open Lock", "", False, "Dex", False, 0, 0)
        dt.Rows.Add(False, "Perform", "", False, "Cha", False, 0, 0)
        dt.Rows.Add(False, "Perform", "", False, "Cha", False, 0, 0)
        dt.Rows.Add(False, "Perform", "", False, "Cha", False, 0, 0)
        dt.Rows.Add(True, "Profession", "", False, "Wis", False, 0, 0)
        dt.Rows.Add(True, "Profession", "", False, "Wis", False, 0, 0)
        dt.Rows.Add(True, "Ride", "", True, "Dex", False, 0, 0)
        dt.Rows.Add(True, "Search", "", True, "Int", False, 3, 2)
        dt.Rows.Add(False, "Sense Motive", "", True, "Wis", False, 0, 0)
        dt.Rows.Add(False, "Sleight of Hand", "", False, "Dex", True, 0, 0)
        dt.Rows.Add(False, "Spellcraft", "", False, "Int", False, 0, 0)
        dt.Rows.Add(True, "Spot", "", True, "Wis", False, 0, 2)
        dt.Rows.Add(True, "Survival", "", True, "Wis", False, 2, 0)
        dt.Rows.Add(True, "Swim", "", True, "Str", True, 0, 0)
        dt.Rows.Add(False, "Tumble", "", False, "Dex", True, 0, 0)
        dt.Rows.Add(False, "Use Magic Device", "", False, "Cha", False, 0, 0)
        dt.Rows.Add(True, "Use Rope", "", True, "Dex", False, 0, 0)

        repSkills.DataSource = dt
        repSkills.DataBind()
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