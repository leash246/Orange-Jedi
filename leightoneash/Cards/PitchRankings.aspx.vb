Imports System.Web.UI.DataVisualization.Charting
Public Class PitchRankings
    Inherits System.Web.UI.Page
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
            Session("UnassignedPlayers") = Unassigned
            Session("TeamOne") = TeamOne
            Session("TeamTwo") = TeamTwo
            SetListBox(lstUnassigned, Unassigned, "ListBoxDisplay", "ID")
            SetListBox(lstTeamOne, TeamOne, "ListBoxDisplay", "ID")
            SetListBox(lstTeamTwo, TeamTwo, "ListBoxDisplay", "ID")
        Else
            Unassigned = Session("UnassignedPlayers")
            TeamOne = Session("TeamOne")
            TeamTwo = Session("TeamTwo")
            TeamOneELO = Session("TeamOneELO")
            TeamTwoELO = Session("TeamTwoELO")
        End If

        CreateGraph()
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
            Dim nTeamOneTotal As Integer = 0
            For Each objPlayer As CardsPlayer In TeamOne
                nTeamOneTotal += objPlayer.ELO
            Next
            TeamOneELO = nTeamOneTotal / TeamOne.Count
        Else
            TeamOneELO = 1500
        End If
        If TeamTwo.Count > 0 Then
            Dim nTeamTwoTotal As Integer = 0
            For Each objPlayer As CardsPlayer In TeamTwo
                nTeamTwoTotal += objPlayer.ELO
            Next
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
        End If
    End Sub
    Private Sub btnAddTwo_Click(sender As Object, e As System.EventArgs) Handles btnAddTwo.Click
        If lstUnassigned.GetSelectedIndices.Count > 0 Then
            For Each idx As Integer In lstUnassigned.GetSelectedIndices
                Dim nPlayerID As Integer = lstUnassigned.Items(idx).Value
                AddToTeamTwo(nPlayerID)
            Next
        End If
    End Sub
    Private Sub btnRemoveOne_Click(sender As Object, e As System.EventArgs) Handles btnRemoveOne.Click
        If lstTeamOne.GetSelectedIndices.Count > 0 Then
            For Each idx As Integer In lstTeamOne.GetSelectedIndices
                Dim nPlayerID As Integer = lstTeamOne.Items(idx).Value
                UnassignFromTeam(nPlayerID)
            Next
        End If
    End Sub
    Private Sub btnRemoveTwo_Click(sender As Object, e As System.EventArgs) Handles btnRemoveTwo.Click
        If lstTeamTwo.GetSelectedIndices.Count > 0 Then
            For Each idx As Integer In lstTeamTwo.GetSelectedIndices
                Dim nPlayerID As Integer = lstTeamTwo.Items(idx).Value
                UnassignFromTeam(nPlayerID)
            Next
        End If
    End Sub

    Private Sub AddToTeamOne(nID As Integer)
        For Each objPlayer As CardsPlayer In Unassigned
            If objPlayer.ID = nID Then
                Unassigned.Remove(objPlayer)
                TeamOne.Add(objPlayer)
                SetTeamELO()
                btnRemoveOne.Enabled = True
                Exit For
            End If
        Next
        Session("TeamOne") = TeamOne
        Session("UnassignedPlayers") = Unassigned
        SetListBox(lstUnassigned, Unassigned, "ListBoxDisplay", "ID")
        SetListBox(lstTeamOne, TeamOne, "ListBoxDisplay", "ID")
        SetListBox(lstTeamTwo, TeamTwo, "ListBoxDisplay", "ID")
    End Sub
    Private Sub AddToTeamTwo(nID As Integer)
        For Each objPlayer As CardsPlayer In Unassigned
            If objPlayer.ID = nID Then
                Unassigned.Remove(objPlayer)
                TeamTwo.Add(objPlayer)
                SetTeamELO()
                btnRemoveTwo.Enabled = True
                Exit For
            End If
        Next
        Session("TeamTwo") = TeamTwo
        Session("UnassignedPlayers") = Unassigned
        SetListBox(lstUnassigned, Unassigned, "ListBoxDisplay", "ID")
        SetListBox(lstTeamOne, TeamOne, "ListBoxDisplay", "ID")
        SetListBox(lstTeamTwo, TeamTwo, "ListBoxDisplay", "ID")
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
        SetTeamELO()
        Session("TeamOne") = TeamOne
        Session("TeamTwo") = TeamTwo
        Session("UnassignedPlayers") = Unassigned
        SetListBox(lstUnassigned, Unassigned, "ListBoxDisplay", "ID")
        SetListBox(lstTeamOne, TeamOne, "ListBoxDisplay", "ID")
        SetListBox(lstTeamTwo, TeamTwo, "ListBoxDisplay", "ID")
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
    Private Sub SendGameUpdateEmail(nWinningTeam As Integer)
        Dim objUser As User = Session("User")
        Dim cSubject As String = ""
        cSubject &= objUser.FirstName & " " & objUser.LastName & " has updated rankings for Pitch"
        Dim cBody As String = ""
        Dim WinningTeam As List(Of CardsPlayer)
        Dim LosingTeam As List(Of CardsPlayer)
        If nWinningTeam = 1 Then
            WinningTeam = TeamOne
            LosingTeam = TeamTwo
        ElseIf nWinningTeam = 2 Then
            WinningTeam = TeamTwo
            LosingTeam = TeamOne
        End If
        Dim strPlayers As String = ""
        If nWinningTeam <> 0 Then
            For Each objPlayer As CardsPlayer In WinningTeam
                strPlayers &= objPlayer.Name & "/"
            Next

            cBody &= strPlayers.Trim("/") & " defeated "

            strPlayers = ""
            For Each objPlayer As CardsPlayer In LosingTeam
                strPlayers &= objPlayer.Name & "/"
            Next
        Else
            For Each objPlayer As CardsPlayer In TeamOne
                strPlayers &= objPlayer.Name & "/"
            Next

            cBody &= strPlayers.Trim("/") & " tied "

            strPlayers = ""
            For Each objPlayer As CardsPlayer In TeamTwo
                strPlayers &= objPlayer.Name & "/"
            Next

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
    Private Sub CreateGraphSeries(ByVal cPlayerName As String, ByVal dtPoints As DataTable, ByVal chtPitchRankings As Chart)
        Dim dv = From dr As DataRow In dtPoints.Rows Where dr.Item("cPlayerName") = cPlayerName
                 Select dr
                 Order By dr.Item("nGame")
        If dv.Count > 0 Then
            Dim ser As New Series(cPlayerName)
            ser.ChartType = SeriesChartType.Line

            ser.BorderWidth = 3
            For Each dr As DataRow In dv
                ser.Points.AddXY(dr.Item("nGame"), dr.Item("nELO"))
            Next
            chtPitchRankings.Series.Add(ser)
        End If
    End Sub
    Private Sub CreateGraph()

        Dim dtData As DataTable = GetPitchGraphData()

        Dim ps = From drs As DataRow In dtData.Rows
                 Select drs.Item("cPlayerName") Distinct

        For Each p In ps
            CreateGraphSeries(CStr(p), dtData, chtPitchRankings)
        Next
        'Dim ps2 = From drs2 As DataRow In dt3024.Rows
        '         Select drs2.Item("cPlayerName") Distinct

        'For Each p2 In ps2
        '    CreateGraphSeries(CStr(p2), dt3024, chtPitchRankings3024)
        'Next
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