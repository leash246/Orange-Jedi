Imports System.Data.SqlClient
Module CardsFunctions
    Partial Public Class CardsPlayer
        Dim nPlayerID As Integer
        Dim cPlayerName As String
        Dim nELO As Integer
        Dim nGames As Integer
        Dim nProvisional As Integer
        Public ReadOnly Property ListBoxDisplay As String
            Get
                Return cPlayerName & " (" & nELO & ")" & IIf(nGames < nProvisional, "*", "")
            End Get
        End Property
        Public ReadOnly Property ID As Integer
            Get
                Return nPlayerID
            End Get
        End Property
        Public ReadOnly Property Name As String
            Get
                Return cPlayerName
            End Get
        End Property
        Public Property ELO As Integer
            Get
                Return nELO
            End Get
            Set(value As Integer)
                nELO = value
            End Set
        End Property
        Public Property Games As Integer
            Get
                Return nGames
            End Get
            Set(value As Integer)
                nGames = value
            End Set
        End Property
        Public Sub New()
            Me.nPlayerID = -1
            Me.cPlayerName = ""
            Me.nELO = 1500
            Me.nGames = 0
        End Sub
        Public Sub New(nPlayerID As Integer, cPlayerName As String, nELO As Integer, nGames As Integer, nProvisional As Integer)
            Me.nPlayerID = nPlayerID
            Me.cPlayerName = cPlayerName
            Me.nELO = nELO
            Me.nGames = nGames
            Me.nProvisional = nProvisional
        End Sub

    End Class

    Public Function GetCostabiRankings() As DataTable
        Dim cmd As New SqlCommand("Cards.usp_Get_CostabiRankings", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        Try
            cmd.Connection.Open()
            da.Fill(dt)
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
        If dt.Rows.Count > 0 Then
            Return dt
        End If
        Return Nothing
    End Function
    Public Function GetPitchRankings() As DataTable
        Dim cmd As New SqlCommand("Cards.usp_Get_PitchRankings", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        Try
            cmd.Connection.Open()
            da.Fill(dt)
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
        If dt.Rows.Count > 0 Then
            Return dt
        End If
        Return Nothing
    End Function
    Public Sub UpdateCostabiRanking(ByVal nPlayerID As Integer, ByVal nELO As Integer, ByVal nGames As Integer, ByVal nResult As Enums.enmCostabiResult)
        Dim cmd As New SqlCommand("Cards.usp_Upsert_CostabiRankings", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@nPlayerID", nPlayerID)
        cmd.Parameters.AddWithValue("@nELO", nELO)
        cmd.Parameters.AddWithValue("@nGames", nGames)
        'cmd.Parameters.AddWithValue("@nResult", nResult)

        Try
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
    End Sub
    Public Sub UpdatePitchRanking(ByVal nPlayerID As Integer, ByVal nELO As Integer, ByVal nGames As Integer, ByVal nResult As Enums.enmCardsResult)
        Dim cmd As New SqlCommand("Cards.usp_Upsert_PitchRankings", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@nPlayerID", nPlayerID)
        cmd.Parameters.AddWithValue("@nELO", nELO)
        cmd.Parameters.AddWithValue("@nGames", nGames)
        cmd.Parameters.AddWithValue("@nResult", nResult)

        Try
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
    End Sub
    Public Function CreateNewCostabiPlayer(ByVal cPlayerName As String) As Integer
        Dim cmd As New SqlCommand("Cards.usp_Upsert_CostabiRankings", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@cPlayerName", cPlayerName)
        Dim nPlayerID As Integer = 0
        Try
            cmd.Connection.Open()
            nPlayerID = cmd.ExecuteScalar()
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
        Return nPlayerID
    End Function
    Public Function CreateNewPitchPlayer(ByVal cPlayerName As String) As Integer
        Dim cmd As New SqlCommand("Cards.usp_Upsert_PitchRankings", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@cPlayerName", cPlayerName)
        Dim nPlayerID As Integer = 0
        Try
            cmd.Connection.Open()
            nPlayerID = cmd.ExecuteScalar()
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
        Return nPlayerID
    End Function
    Public Function CalculateCostabiELO(ByVal nPlayer As Integer, ByVal nOpponent As Integer, eResult As Enums.enmCostabiResult, eOppResult As Enums.enmCostabiResult, nPlayerGames As Integer) As Integer
        Dim nResult As Integer = 0
        If eResult > eOppResult Then
            nResult = 1
        End If
        Dim nExpected As Decimal = ResultExpected(nPlayer, nOpponent)
        Dim K As Integer = 4
        If nPlayer >= 2100 Then
            K = 3
        End If
        If nPlayer >= 2400 Then
            K = 2
        End If
        If nPlayerGames < 10 Then
            K = K * 2
        End If
        Dim nNewRating As Integer = nPlayer
        nNewRating = Math.Round(nPlayer + (K * (nResult - nExpected)), 0, MidpointRounding.AwayFromZero)
        Return nNewRating
    End Function
    Public Function CalculatePitchELO(ByVal nPlayer As Integer, ByVal nOpponent As Integer, eResult As Enums.enmCardsResult, nPlayerGames As Integer) As Integer
        Dim nResult As Decimal = eResult / 2
        Dim nExpected As Decimal = ResultExpected(nPlayer, nOpponent)
        Dim K As Integer = 20
        If nPlayer >= 2100 Then
            K = 15
        End If
        If nPlayer >= 2400 Then
            K = 10
        End If
        If nPlayerGames < 30 Then
            K = 30
        End If
        Dim nNewRating As Integer = nPlayer
        nNewRating = Math.Round(nPlayer + (K * (nResult - nExpected)), 0, MidpointRounding.AwayFromZero)
        Return nNewRating
    End Function
    Private Function ResultExpected(ByVal nPlayer As Integer, ByVal nOpponent As Integer) As Decimal
        Dim nExpected As Decimal
        nExpected = 1 / (1 + 10 ^ ((nOpponent - nPlayer) / 400))
        Return nExpected
    End Function

    Public Function TeamID(lstTeam As List(Of CardsPlayer)) As Integer
        Dim cmd As New SqlCommand("Cards.usp_Get_PitchTeamID", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        Dim nTeamID As Integer = 0
        If lstTeam.Count >= 1 Then
            cmd.Parameters.AddWithValue("@nPlayer1", lstTeam(0))
        End If
        If lstTeam.Count >= 2 Then
            cmd.Parameters.AddWithValue("@nPlayer2", lstTeam(1))
        End If
        If lstTeam.Count = 3 Then
            cmd.Parameters.AddWithValue("@nPlayer3", lstTeam(2))
        End If
        Try
            cmd.Connection.Open()
            nTeamID = cmd.ExecuteScalar
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
        Return nTeamID
    End Function

    Public Sub SaveCostabiGame(dGameDate As Date, ParamArray nOrder As Integer())
        Dim cmd As New SqlCommand("Cards.usp_Insert_CostabiGame", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@dGameDate", dGameDate)
        cmd.Parameters.AddWithValue("@nFirst", nOrder(0))
        cmd.Parameters.AddWithValue("@nSecond", nOrder(1))
        cmd.Parameters.AddWithValue("@nThird", nOrder(2))
        cmd.Parameters.AddWithValue("@nFourth", nOrder(3))
        cmd.Parameters.AddWithValue("@nFirst", nOrder(4))
        Try
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try

    End Sub
    Public Sub SavePitchGame(dGameDate As Date, nTeamOne As Integer, nTeamTwo As Integer, nWinningTeam As Integer)
        Dim cmd As New SqlCommand("Cards.usp_Insert_PitchGame", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@dGameDate", dGameDate)
        cmd.Parameters.AddWithValue("@nTeamOne", nTeamOne)
        cmd.Parameters.AddWithValue("@nTeamTwo", nTeamTwo)
        cmd.Parameters.AddWithValue("@nWinningTeam", nWinningTeam)
        Try
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try

    End Sub
    Public Function GetPitchGraphData() As DataTable
        Dim cmd As New SqlCommand("Cards.usp_Get_PitchRankingsGraphData", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        Try
            cmd.Connection.Open()
            da.Fill(dt)
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
        Return dt
    End Function
    Public Sub InsertPitchGraphData(nPlayerID As Integer, nELO As Integer, Optional nBuilding As Integer = 0, Optional nGame As Integer = -1)
        Dim cmd As New SqlCommand("Cards.usp_Insert_PitchRankingsGraphData", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        With cmd.Parameters
            .AddWithValue("@nPlayerID", nPlayerID)
            .AddWithValue("@nELO", nELO)
            If nGame > -1 Then .AddWithValue("@nGame", nGame)
        End With
        Try
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
    End Sub
    Public Sub UpdatePitchTeammateStatistics(nPlayerID As Integer, nGameSize As Integer, nTeammate As Integer, lWin As Boolean)
        Dim cmd As New SqlCommand("Cards.usp_Update_PitchTeammateStatistics", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        With cmd.Parameters
            .AddWithValue("@nPlayerID", nPlayerID)
            .AddWithValue("@nGameSize", nGameSize)
            .AddWithValue("@nTeammate", nTeammate)
            .AddWithValue("@lWin", lWin)
        End With
        Try
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
    End Sub
    Public Sub UpdatePitchOpponentStatistics(nPlayerID As Integer, nGameSize As Integer, nOpponent As Integer, lWin As Boolean)
        Dim cmd As New SqlCommand("Cards.usp_Update_PitchOpponentStatistics", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        With cmd.Parameters
            .AddWithValue("@nPlayerID", nPlayerID)
            .AddWithValue("@nGameSize", nGameSize)
            .AddWithValue("@nOpponent", nOpponent)
            .AddWithValue("@lWin", lWin)
        End With
        Try
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
    End Sub
    Public Function GetPitchTeammateStatistics(Optional ByVal nPlayerID As Integer = 0, Optional ByVal nGameSize As Integer = 0) As DataTable
        Dim cmd As New SqlCommand("Cards.usp_Get_PitchTeammateStatistics", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        If nPlayerID <> 0 Then
            cmd.Parameters.AddWithValue("@nPlayerID", nPlayerID)
        End If
        If nGameSize <> 0 Then
            cmd.Parameters.AddWithValue("@nGameSize", nGameSize)
        End If
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        Try
            cmd.Connection.Open()
            da.Fill(dt)
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
        Return dt
    End Function
    Public Function GetPitchOpponentStatistics(Optional ByVal nPlayerID As Integer = 0, Optional ByVal nGameSize As Integer = 0) As DataTable
        Dim cmd As New SqlCommand("Cards.usp_Get_PitchOpponentStatistics", Connection)
        cmd.CommandType = CommandType.StoredProcedure
        If nPlayerID <> 0 Then
            cmd.Parameters.AddWithValue("@nPlayerID", nPlayerID)
        End If
        If nGameSize <> 0 Then
            cmd.Parameters.AddWithValue("@nGameSize", nGameSize)
        End If
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        Try
            cmd.Connection.Open()
            da.Fill(dt)
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
        Return dt
    End Function
End Module
