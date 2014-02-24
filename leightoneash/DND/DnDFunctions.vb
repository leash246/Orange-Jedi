Module DnDFunctions
    Public Sub AddSkillRow(ByRef dt As DataTable, ByVal dtClassSkills As DataTable, ByVal dtSkillRanks As DataTable, ByVal cSkill As String, lUntrained As Boolean,
                           cKeyAbility As String, lArmorCheck As Boolean, nMiscMod As Integer)
        Dim filtered = From row In dtSkillRanks.AsEnumerable
                       Where row.Field(Of String)("cSkillName") Like CStr(cSkill + " (*)")
                       Select row
        If filtered.Count > 0 Then

            For Each drSkill In filtered.CopyToDataTable.Rows
                Dim cSkillOption As String = drSkill.Item("cSkillName").ToString.Replace(cSkill, "").Replace(" (", "").Replace(")", "")
                Dim drLine As DataRow = dt.NewRow
                With drLine
                    .Item("lClassSkill") = IsClassSkill(dtClassSkills, cSkill, cSkillOption)
                    .Item("cSkillName") = cSkill
                    .Item("cSkillOption") = cSkillOption
                    .Item("lUntrained") = lUntrained
                    .Item("cKeyAbility") = cKeyAbility
                    .Item("lArmorCheck") = lArmorCheck
                    .Item("nRanks") = SkillRanks(dtSkillRanks, cSkill, cSkillOption)
                    .Item("nMiscMod") = nMiscMod
                End With
                dt.Rows.Add(drLine)
            Next
        End If
        Dim dr As DataRow = dt.NewRow
        With dr
            .Item("lClassSkill") = IsClassSkill(dtClassSkills, cSkill)
            .Item("cSkillName") = cSkill
            .Item("cSkillOption") = ""
            .Item("lUntrained") = lUntrained
            .Item("cKeyAbility") = cKeyAbility
            .Item("lArmorCheck") = lArmorCheck
            .Item("nRanks") = SkillRanks(dtSkillRanks, cSkill)
            .Item("nMiscMod") = nMiscMod
        End With
        dt.Rows.Add(dr)

    End Sub
    Public Function IsClassSkill(dtClassSkills As DataTable, cSkill As String, Optional ByVal cSkillOption As String = "") As Boolean
        Dim strSearch As String = ""
        If cSkillOption = "" Or cSkill = "Craft" Then
            strSearch = cSkill
        Else
            strSearch = cSkill & " (" & cSkillOption & ")"
        End If
        Dim dr = From rows In dtClassSkills Where rows.Item("cSkillName") = strSearch.Trim
                 Select rows
        If dr.Count > 0 Then
            Return True
        End If
        Return False
    End Function
    Public Function SkillRanks(dtSkillRanks As DataTable, cSkill As String, Optional ByVal cSkillOption As String = "") As Integer
        Dim strSearch As String = ""
        If cSkillOption = "" Then
            strSearch = cSkill
        Else
            strSearch = cSkill & " (" & cSkillOption & ")"
        End If

        Dim dr = From rows In dtSkillRanks Where rows.Item("cSkillName") = strSearch.Trim
                 Select rows
        If dr.Count > 0 Then
            Return CInt(dr(0).Item("nSkillRanks"))
        End If
        Return 0
    End Function
End Module
