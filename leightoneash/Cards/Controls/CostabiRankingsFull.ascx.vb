Public Class CostabiRankingsFull
    Inherits System.Web.UI.UserControl

    Dim Unassigned As List(Of CardsPlayer)
    Dim Players As List(Of CardsPlayer)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Unassigned = New List(Of CardsPlayer)
            Players = New List(Of CardsPlayer)
            FillUnassignedPlayers()
            Session("UnassignedCostabi") = Unassigned
            Session("Players") = Players
            SetListBox(lstUnassigned, Unassigned, "ListBoxDisplay", "ID")
            SetListBox(lstPlayers, Players, "ListBoxDisplay", "ID")
        Else
            Unassigned = Session("UnassignedCostabi")
            Players = Session("Players")
        End If


    End Sub
    Private Sub FillUnassignedPlayers()
        Dim dt As DataTable = GetCostabiRankings()
        Dim dv As New DataView(dt)
        dv.Sort = "nELO desc"
        For Each dr As DataRow In dv.ToTable.Rows
            With dr
                Dim objPlayer As New CardsPlayer(.Item("nPlayerID"), .Item("cPlayerName"), .Item("nELO"), .Item("nGames"), 10)
                Unassigned.Add(objPlayer)
            End With
        Next
    End Sub

    Private Sub SetListBox(lst As ListBox, ds As Object, cTextField As String, cValueField As String)
        With lst
            .DataSource = ds
            .DataTextField = cTextField
            .DataValueField = cValueField
            .DataBind()
        End With
    End Sub
#Region " Add/Remove Events "
    Private Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        If lstUnassigned.GetSelectedIndices.Count > 0 Then
            For Each idx As Integer In lstUnassigned.GetSelectedIndices
                Dim nPlayerID As Integer = lstUnassigned.Items(idx).Value
                AddToPlayers(nPlayerID)
            Next
        End If
    End Sub
    Private Sub btnRemove_Click(sender As Object, e As System.EventArgs) Handles btnRemove.Click
        If lstPlayers.GetSelectedIndices.Count > 0 Then
            For Each idx As Integer In lstPlayers.GetSelectedIndices
                Dim nPlayerID As Integer = lstPlayers.Items(idx).Value
                UnassignFromPlayers(nPlayerID)
            Next
        End If
    End Sub

    Private Sub AddToPlayers(nID As Integer)
        For Each objPlayer As CardsPlayer In Unassigned
            If objPlayer.ID = nID Then
                Unassigned.Remove(objPlayer)
                Players.Add(objPlayer)
                btnRemove.Enabled = True
                Exit For
            End If
        Next
        Session("Players") = Players
        Session("UnassignedCostabi") = Unassigned
        SetListBox(lstUnassigned, Unassigned, "ListBoxDisplay", "ID")
        SetListBox(lstPlayers, Players, "ListBoxDisplay", "ID")
    End Sub
    Private Sub UnassignFromPlayers(nID As Integer)
        For Each objPlayer As CardsPlayer In Players
            If objPlayer.ID = nID Then
                Players.Remove(objPlayer)
                Unassigned.Add(objPlayer)
                If Players.Count = 0 Then
                    btnRemove.Enabled = False
                End If
                Exit For
            End If
        Next
        Session("Players") = Players
        Session("UnassignedCostabi") = Unassigned
        SetListBox(lstUnassigned, Unassigned, "ListBoxDisplay", "ID")
        SetListBox(lstPlayers, Players, "ListBoxDisplay", "ID")
    End Sub
#End Region
    Private Sub CalculateGameResults()

        Dim objUser As User = Session("User")
        If objUser Is Nothing Then

        End If
        Dim nPlayerELO() As Integer = {0, 0, 0, 0, 0}
        Dim nPlayerID() As Integer = {0, 0, 0, 0, 0}
        For i As Integer = 0 To Players.Count - 1
            Dim objPlayer As CardsPlayer = Players(i)
            Dim nNewELO As Integer = objPlayer.ELO
            nPlayerELO(i) = objPlayer.ELO
            nPlayerID(i) = objPlayer.ID
            Dim nELOChange As Integer = 0
            For j As Integer = 0 To Players.Count - 1
                Dim objOpponent As CardsPlayer = Players(j)
                If nPlayerELO(j) = 0 Then nPlayerELO(j) = objOpponent.ELO
                If i <> j Then
                    Dim nThisChange As Integer = CalculateCostabiELO(nPlayerELO(i), nPlayerELO(j), 4 - i, 4 - j, objPlayer.Games) - nPlayerELO(i)
                    nELOChange += nThisChange
                End If
            Next
            objPlayer.ELO = nPlayerELO(i) + nELOChange
            objPlayer.Games += 1
            If objUser.ValidRole(Enums.enmRoles.Admin) Or objUser.ValidRole(Enums.enmRoles.Cards) Then
                UpdateCostabiRanking(objPlayer.ID, objPlayer.ELO, objPlayer.Games, 4 - i)
            End If
        Next
        If objUser.ValidRole(Enums.enmRoles.Admin) Then
            SendGameUpdateEmail()
            SaveCostabiGame(Now.Date, nPlayerID)
        End If
        SetListBox(lstUnassigned, Unassigned, "ListBoxDisplay", "ID")
        SetListBox(lstPlayers, Players, "ListBoxDisplay", "ID")
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click
        CalculateGameResults()
    End Sub
    Private Sub SendGameUpdateEmail()
        Dim objUser As User = Session("User")
        Dim cSubject As String = ""
        cSubject &= objUser.FirstName & " " & objUser.LastName & " has updated rankings for Costabi"
        Dim cBody As String = ""
        Dim strPlayers As String = ""
        Dim i As Integer = 1
        For Each objPlayer As CardsPlayer In Players
            Select Case i
                Case 1
                    cBody &= "1st: "
                Case 2
                    cBody &= "2nd: "
                Case 3
                    cBody &= "3rd: "
                Case 4, 5
                    cBody &= i & "th: "
            End Select
            cBody &= objPlayer.Name & vbCrLf
            i += 1
        Next

        cBody &= "New ELOs:" & vbCrLf
        For Each objPlayer As CardsPlayer In Players
            cBody &= objPlayer.Name & ": " & objPlayer.ELO & vbCrLf
        Next
        cBody &= vbCrLf

        For Each objPlayer As CardsPlayer In Unassigned
            cBody &= objPlayer.Name & ": " & objPlayer.ELO & vbCrLf '"<br/>"
        Next
        SendEmail(cSubject, cBody, "aschaal1263@gmail.com")

    End Sub

End Class