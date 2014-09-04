Public Class PitchRankingsFull
    Inherits System.Web.UI.UserControl
    Dim Unassigned As List(Of CardsPlayer)
    Dim TeamOne As List(Of CardsPlayer)
    Dim TeamOneELO As Integer
    Dim TeamTwo As List(Of CardsPlayer)
    Dim TeamTwoELO As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Unassigned = New List(Of CardsPlayer)
            TeamOne = New List(Of CardsPlayer)
            TeamTwo = New List(Of CardsPlayer)
            FillUnassignedPlayers()
            Session("UnassignedPitch") = Unassigned
            Session("TeamOnePitch") = TeamOne
            Session("TeamTwoPitch") = TeamTwo
            SetListBox(lstUnassigned, Unassigned, "ListBoxDisplay", "ID")
            SetListBox(lstTeamOne, TeamOne, "ListBoxDisplay", "ID")
            SetListBox(lstTeamTwo, TeamTwo, "ListBoxDisplay", "ID")
        Else
            Unassigned = Session("UnassignedPitch")
            TeamOne = Session("TeamOnePitch")
            TeamTwo = Session("TeamTwoPitch")
            TeamOneELO = Session("TeamOneELO")
            TeamTwoELO = Session("TeamTwoELO")
        End If

        Functions.CreateGraph(chtPitchRankings)
    End Sub
    Private Sub FillUnassignedPlayers()
        Dim dt As DataTable = GetPitchRankings()
        Dim dv As New DataView(dt)
        dv.Sort = "nELO desc"
        For Each dr As DataRow In dv.ToTable.Rows
            With dr
                Dim objPlayer As New CardsPlayer(.Item("nPlayerID"), .Item("cPlayerName"), .Item("nELO"), .Item("nGames"), 30)
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
    Private Sub SetTeamELO()
        If TeamOne.Count > 0 Then
            Dim nTeamOneTotal As Integer = TeamOne.Sum(Function(objPlayer) objPlayer.ELO)
            TeamOneELO = nTeamOneTotal / TeamOne.Count
        Else
            TeamOneELO = 1500
        End If
        If TeamTwo.Count > 0 Then
            Dim nTeamTwoTotal As Integer = TeamTwo.Sum(Function(objPlayer) objPlayer.ELO)
            TeamTwoELO = nTeamTwoTotal / TeamTwo.Count
        Else
            TeamTwoELO = 1500
        End If
        Session("TeamOneELO") = TeamOneELO
        Session("TeamTwoELO") = TeamTwoELO
        lblTeamOneELO.Text = "Team Rating: " & TeamOneELO.ToString
        lblTeamTwoELO.Text = "Team Rating: " & TeamTwoELO.ToString
    End Sub
#Region " Add/Remove Events "
    Private Sub btnAddOne_Click(sender As Object, e As System.EventArgs) Handles btnAddOne.Click
        If lstUnassigned.GetSelectedIndices.Count > 0 Then
            For Each idx As Integer In lstUnassigned.GetSelectedIndices
                Dim nPlayerID As Integer = lstUnassigned.Items(idx).Value
                AddToTeamOne(nPlayerID)
            Next
            SetListBoxes()
        End If
    End Sub
    Private Sub btnAddTwo_Click(sender As Object, e As System.EventArgs) Handles btnAddTwo.Click
        If lstUnassigned.GetSelectedIndices.Count > 0 Then
            For Each idx As Integer In lstUnassigned.GetSelectedIndices
                Dim nPlayerID As Integer = lstUnassigned.Items(idx).Value
                AddToTeamTwo(nPlayerID)
            Next
            SetListBoxes()
        End If
    End Sub
    Private Sub btnRemoveOne_Click(sender As Object, e As System.EventArgs) Handles btnRemoveOne.Click
        If lstTeamOne.GetSelectedIndices.Count > 0 Then
            For Each idx As Integer In lstTeamOne.GetSelectedIndices
                Dim nPlayerID As Integer = lstTeamOne.Items(idx).Value
                UnassignFromTeam(nPlayerID)
                SetListBoxes()
            Next
        End If
    End Sub
    Private Sub btnRemoveTwo_Click(sender As Object, e As System.EventArgs) Handles btnRemoveTwo.Click
        If lstTeamTwo.GetSelectedIndices.Count > 0 Then
            For Each idx As Integer In lstTeamTwo.GetSelectedIndices
                Dim nPlayerID As Integer = lstTeamTwo.Items(idx).Value
                UnassignFromTeam(nPlayerID)
            Next
            SetListBoxes()
        End If
    End Sub

    Private Sub AddToTeamOne(nID As Integer)
        For Each objPlayer As CardsPlayer In Unassigned
            If objPlayer.ID = nID Then
                Unassigned.Remove(objPlayer)
                TeamOne.Add(objPlayer)
                btnRemoveOne.Enabled = True
                Exit For
            End If
        Next
        Session("TeamOnePitch") = TeamOne
        Session("UnassignedPitch") = Unassigned
    End Sub
    Private Sub AddToTeamTwo(nID As Integer)
        For Each objPlayer As CardsPlayer In Unassigned
            If objPlayer.ID = nID Then
                Unassigned.Remove(objPlayer)
                TeamTwo.Add(objPlayer)
                btnRemoveTwo.Enabled = True
                Exit For
            End If
        Next
        Session("TeamTwoPitch") = TeamTwo
        Session("UnassignedPitch") = Unassigned
    End Sub
    Private Sub UnassignFromTeam(nID As Integer)
        For Each objPlayer As CardsPlayer In TeamOne
            If objPlayer.ID = nID Then
                TeamOne.Remove(objPlayer)
                Unassigned.Add(objPlayer)
                If TeamOne.Count = 0 Then
                    btnRemoveOne.Enabled = False
                End If
                Exit For
            End If
        Next
        For Each objPlayer As CardsPlayer In TeamTwo
            If objPlayer.ID = nID Then
                TeamTwo.Remove(objPlayer)
                Unassigned.Add(objPlayer)
                If TeamTwo.Count = 0 Then
                    btnRemoveTwo.Enabled = False
                End If
                Exit For
            End If
        Next
        Session("TeamOnePitch") = TeamOne
        Session("TeamTwoPitch") = TeamTwo
        Session("UnassignedPitch") = Unassigned
    End Sub
    Private Sub SetListBoxes()
        SetListBox(lstUnassigned, Unassigned, "ListBoxDisplay", "ID")
        SetListBox(lstTeamOne, TeamOne, "ListBoxDisplay", "ID")
        SetListBox(lstTeamTwo, TeamTwo, "ListBoxDisplay", "ID")
        SetTeamELO()
    End Sub
#End Region
    Private Sub CalculateGameResults(ByVal nTeamOneResult As Enums.enmCardsResult, ByVal nTeamTwoResult As Enums.enmCardsResult)

        Dim objUser As User = Session("User")
        If objUser Is Nothing Then

        End If
        For Each objPlayer As CardsPlayer In TeamOne
            Dim nELOChange As Integer
            nELOChange = CalculatePitchELO(TeamOneELO, TeamTwoELO, nTeamOneResult, objPlayer.Games) - TeamOneELO
            objPlayer.ELO += nELOChange
            objPlayer.Games += 1
            If objUser.ValidRole(Enums.enmRoles.Cards) Then
                UpdatePitchRanking(objPlayer.ID, objPlayer.ELO, objPlayer.Games, nTeamOneResult)
            End If
        Next
        For Each objPlayer As CardsPlayer In TeamTwo
            Dim nELOChange As Integer
            nELOChange = CalculatePitchELO(TeamTwoELO, TeamOneELO, nTeamTwoResult, objPlayer.Games) - TeamTwoELO
            objPlayer.ELO += nELOChange
            objPlayer.Games += 1
            If objUser.ValidRole(Enums.enmRoles.Cards) Then
                UpdatePitchRanking(objPlayer.ID, objPlayer.ELO, objPlayer.Games, nTeamTwoResult)
            End If
        Next
        SetTeamELO()
        Dim nTeamOneID As Integer = TeamID(TeamOne)
        Dim nTeamTwoID As Integer = TeamID(TeamTwo)
        If objUser.ValidRole(Enums.enmRoles.Cards) Then
            SendGameUpdateEmail(IIf(nTeamOneResult = Enums.enmCardsResult.Win, 1, IIf(nTeamOneResult = Enums.enmCardsResult.Loss, 2, 0)))
            SavePitchGame(Now.Date, nTeamOneID, nTeamOneID, IIf(nTeamOneResult = Enums.enmCardsResult.Win, 1, IIf(nTeamOneResult = Enums.enmCardsResult.Loss, 2, 0)))
        End If
        SetListBox(lstUnassigned, Unassigned, "ListBoxDisplay", "ID")
        SetListBox(lstTeamOne, TeamOne, "ListBoxDisplay", "ID")
        SetListBox(lstTeamTwo, TeamTwo, "ListBoxDisplay", "ID")
    End Sub
    Private Sub btnTeamOneWin_Click(sender As Object, e As System.EventArgs) Handles btnTeamOneWin.Click
        CalculateGameResults(Enums.enmCardsResult.Win, Enums.enmCardsResult.Loss)
    End Sub
    Private Sub btnTeamTwoWin_Click(sender As Object, e As System.EventArgs) Handles btnTeamTwoWin.Click
        CalculateGameResults(Enums.enmCardsResult.Loss, Enums.enmCardsResult.Win)
    End Sub
    Private Sub btnDraw_Click(sender As Object, e As System.EventArgs) Handles btnDraw.Click
        CalculateGameResults(Enums.enmCardsResult.Draw, Enums.enmCardsResult.Draw)
    End Sub
    Private Sub btnReset_Click(sender As Object, e As System.EventArgs) Handles btnReset.Click
        Dim nPlayerID = (From objPlayer As CardsPlayer In TeamOne
                        Select objPlayer.ID).Union(From objPlayer2 As CardsPlayer In TeamTwo
                                                   Select objPlayer2.ID)
        If nPlayerID.Count > 0 Then
            For Each i As Integer In nPlayerID.ToList
                UnassignFromTeam(i)
            Next
        End If

        SetListBoxes()
    End Sub
    Private Sub SendGameUpdateEmail(nWinningTeam As Integer)
        Dim objUser As User = Session("User")
        Dim cSubject As String = ""
        cSubject &= objUser.FirstName & " " & objUser.LastName & " has updated rankings for Pitch"
        Dim cBody As String = ""
        Dim winningTeam As List(Of CardsPlayer)
        Dim losingTeam As List(Of CardsPlayer)
        If nWinningTeam = 1 Then
            winningTeam = TeamOne
            losingTeam = TeamTwo
        ElseIf nWinningTeam = 2 Then
            winningTeam = TeamTwo
            losingTeam = TeamOne
        End If
        Dim strPlayers As String = ""
        If nWinningTeam <> 0 Then
            strPlayers = winningTeam.Aggregate(strPlayers, Function(current, objPlayer) current & (objPlayer.Name & "/"))

            cBody &= strPlayers.Trim("/") & " defeated "

            strPlayers = losingTeam.Aggregate("", Function(current, objPlayer) current & (objPlayer.Name & "/"))
        Else
            strPlayers = TeamOne.Aggregate(strPlayers, Function(current, objPlayer) current & (objPlayer.Name & "/"))

            cBody &= strPlayers.Trim("/") & " tied "

            strPlayers = TeamTwo.Aggregate("", Function(current, objPlayer) current & (objPlayer.Name & "/"))

        End If
        cBody &= strPlayers.Trim("/") & vbCrLf & vbCrLf '"<br/><br/>"

        cBody &= "New ELOs:" & vbCrLf
        For Each objPlayer As CardsPlayer In TeamOne
            cBody &= objPlayer.Name & ": " & objPlayer.ELO & vbCrLf
            InsertPitchGraphData(objPlayer.ID, objPlayer.ELO)
        Next
        For Each objPlayer As CardsPlayer In TeamTwo
            cBody &= objPlayer.Name & ": " & objPlayer.ELO & vbCrLf '"<br/>"
            InsertPitchGraphData(objPlayer.ID, objPlayer.ELO)
        Next
        cBody &= vbCrLf
        'cBody &= "</b>"
        For Each objPlayer As CardsPlayer In Unassigned
            cBody &= objPlayer.Name & ": " & objPlayer.ELO & vbCrLf '"<br/>"
            InsertPitchGraphData(objPlayer.ID, objPlayer.ELO)
        Next
        UpdateStatistics(nWinningTeam)
        SendEmail(cSubject, cBody, "aschaal1263@gmail.com")

    End Sub
    Private Sub UpdateStatistics(nWinningTeam As Integer)
        For i As Integer = 0 To TeamOne.Count - 1
            For j As Integer = 0 To TeamOne.Count - 1
                If j = i Then Continue For
                UpdatePitchTeammateStatistics(TeamOne(i).ID, TeamOne.Count + TeamTwo.Count, TeamOne(j).ID, IIf(nWinningTeam = 1, True, False))
            Next
        Next
        For i As Integer = 0 To TeamTwo.Count - 1
            For j As Integer = 0 To TeamTwo.Count - 1
                If j = i Then Continue For
                UpdatePitchTeammateStatistics(TeamTwo(i).ID, TeamOne.Count + TeamTwo.Count, TeamTwo(j).ID, IIf(nWinningTeam = 2, True, False))
            Next
        Next
        For Each objPlayer In TeamOne
            For Each objPlayerTwo In TeamTwo
                UpdatePitchOpponentStatistics(objPlayer.ID, TeamOne.Count + TeamTwo.Count, objPlayerTwo.ID, IIf(nWinningTeam = 1, True, False))
            Next
        Next
        For Each objPlayer In TeamTwo
            For Each objPlayerTwo In TeamOne
                UpdatePitchOpponentStatistics(objPlayer.ID, TeamOne.Count + TeamTwo.Count, objPlayerTwo.ID, IIf(nWinningTeam = 2, True, False))
            Next
        Next
    End Sub

End Class