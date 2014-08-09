Imports System.Web.UI.DataVisualization.Charting
Public Class PitchStatisticsFull
    Inherits System.Web.UI.UserControl
    Dim dtT4, dtT6, dtO4, dtO6 As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dtPlayers As DataTable = GetPitchRankings()

            For Each dr As DataRow In dtPlayers.Rows
                ddlPlayers.Items.Add(New ListItem(dr.Item("cPlayerName"), dr.Item("nPlayerID")))
            Next

        End If
        upGraphs.Visible = False
    End Sub
    Private Sub CreateGraphSeries(cht As Chart, cStatus As String, cPlayerName As String, nGameSize As Integer)
        cht.Titles.Add(cPlayerName & "'s " & cStatus & "s - " & nGameSize & " player")
        Dim ser As New Series
        ser.ChartType = SeriesChartType.Radar
        'ser.Name = dtData.Rows(0).Item("cTeammateName")
        ser.YValueMembers = "nPct"
        ser.XValueMember = "cPlayerTitle"
        cht.Series.Add(ser)

    End Sub

    Private Sub btnStatistics_Click(sender As Object, e As System.EventArgs) Handles btnStatistics.Click
        Dim nPlayer As Integer, nGameSize As Integer = 0
        nPlayer = CInt(ddlPlayers.SelectedValue)
        If ddlSize.SelectedValue <> "" Then
            nGameSize = CInt(ddlSize.SelectedValue)
        End If
        Dim cStatus As String = ""
        If nGameSize <> 6 Then
            dtT4 = GetPitchTeammateStatistics(nPlayer, 4)
            dtO4 = GetPitchOpponentStatistics(nPlayer, 4)
            If dtT4.Rows.Count > 0 Then
                chtTeammates4.DataSource = dtT4
                CreateGraphSeries(chtTeammates4, "Teammate", dtT4.Rows(0).Item("cPlayerName"), 4)
                chtTeammates4.DataBind()
            End If
            If dtO4.Rows.Count > 0 Then
                chtOpponents4.DataSource = dtO4
                CreateGraphSeries(chtOpponents4, "Opponent", dtO4.Rows(0).Item("cPlayerName"), 4)
                chtOpponents4.DataBind()
            End If
        Else
            chtOpponents4.Visible = False
        End If
        If nGameSize <> 4 Then
            dtT6 = GetPitchTeammateStatistics(nPlayer, 6)
            dtO6 = GetPitchOpponentStatistics(nPlayer, 6)
            If dtT6.Rows.Count > 0 Then
                chtTeammates6.DataSource = dtT6
                CreateGraphSeries(chtTeammates6, "Teammate", dtT6.Rows(0).Item("cPlayerName"), 6)
                chtTeammates6.DataBind()
            End If
            If dtO6.Rows.Count > 0 Then
                chtOpponents6.DataSource = dtO6
                CreateGraphSeries(chtOpponents6, "Opponent", dtO6.Rows(0).Item("cPlayerName"), 6)
                chtOpponents6.DataBind()
            End If
        Else
            chtOpponents6.Visible = False
        End If
        Dim dtPlayers As DataTable = GetPitchRankings()
        Dim cPlayerName As String = dtPlayers.Select("nPlayerID = " & nPlayer)(0).Item("cPlayerName")
        Functions.CreateGraph(chtPitchRankings, cPlayerName)
        upGraphs.Visible = True
    End Sub
End Class