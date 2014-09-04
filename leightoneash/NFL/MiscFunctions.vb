Imports System.Data.SqlClient

Module MiscFunctions
    Public Sub LoadWeek(nWeek As Integer, gmGames As Schedule.Games)
        Dim dtWeek As DataTable = GetWeeklyGames(nWeek)
        For Each dr As DataRow In dtWeek.Rows
            Dim gm As New Schedule.Game
            gm.Day = dr.Item("cDay")
            gm.Time = CInt(Left(dr.Item("dTime").ToString().Replace(":", ""), 4))
            gm.AwayTeam = dr.Item("cAway")
            gm.AwayRecord = TeamRecord(gm.AwayTeam)
            gm.HomeTeam = dr.Item("cHome")
            gm.HomeRecord = TeamRecord(gm.HomeTeam)
            gm.Spread = dr.Item("nSpread")
            If Not IsDBNull(dr.Item("nOverUnder")) Then
                Decimal.TryParse(dr.Item("nOverUnder"), gm.OverUnder)
            Else
                gm.OverUnder = 0
            End If
            gmGames.Add(gm)
        Next
    End Sub
    Private Function GetWeeklyGames(nWeek As Integer) As DataTable
        Dim cmd As New SqlCommand("NFL.usp_GetGames")
        cmd.Parameters.AddWithValue("@nWeek", nWeek)
        Return FillDataTable(cmd)
    End Function
    Public Sub SaveWeek(nWeek As Integer, gmGames As Schedule.Games)
        
    End Sub
    Public Function TeamRecord(cTeam As String) As String
        Dim cmd As New SqlCommand("NFL.usp_Get_Standings")
        cmd.Parameters.AddWithValue("@cTeam", cTeam)
        Dim dt As DataTable = FillDataTable(cmd)
        Dim cRecord As String
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            cRecord = String.Format("{0}-{1}{2}{3}", dr.Item("nWins"), dr.Item("nLosses"), IIf(dr.Item("nTies") > 0, "-", ""), IIf(dr.Item("nTies") > 0, dr.Item("nTies"), ""))
        End If
        Return cRecord
    End Function

    Public Function Save(nWeek As Integer, ByVal repGames As Repeater, cUser As String, Optional ByVal nOverUnder As Integer = 0) As Boolean
        Try
            Dim dtPicks As New DataTable
            dtPicks.Columns.Add("nWeek", Type.GetType("System.Int32"))
            dtPicks.Columns.Add("cUser", Type.GetType("System.String"))
            dtPicks.Columns.Add("cPick", Type.GetType("System.String"))
            For Each item As RepeaterItem In repGames.Items
                Dim dr As DataRow = dtPicks.NewRow()
                dr.Item("nWeek") = nWeek
                dr.Item("cUser") = cUser
                Dim rbtAway As RadioButton = item.FindControl("rbtAway")
                Dim rbtHome As RadioButton = item.FindControl("rbtHome")
                If Not rbtAway.Checked And Not rbtHome.Checked Then Continue For
                Dim lblTeam As Label
                If rbtAway.Checked Then
                    lblTeam = item.FindControl("lblAwayTeam")
                    dr.Item("cPick") = lblTeam.Text
                ElseIf rbtHome.Checked Then
                    lblTeam = item.FindControl("lblHomeTeam")
                    dr.Item("cPick") = lblTeam.Text
                End If
                dtPicks.Rows.Add(dr)
            Next
            Dim drTB As DataRow = dtPicks.NewRow()
            drTB.Item("nWeek") = nWeek
            drTB.Item("cUser") = cUser
            drTB.Item("cPick") = nOverUnder.ToString
            dtPicks.Rows.Add(drTB)

            Dim cnn As SqlConnection = Connection()
            Dim cmd As New SqlCommand("NFL.usp_Upsert_PaidPlayer", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@nWeek", nWeek)
            cmd.Parameters.AddWithValue("@cUser", cUser)
            If cUser <> "Results" Then
                Try
                    cmd.Connection.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                Finally
                    cmd.Connection.Close()
                End Try
            End If
            cmd.CommandText = "NFL.usp_BulkUpsert_Picks"
            Dim tvp As SqlParameter = cmd.Parameters.AddWithValue("@Picks", dtPicks)
            tvp.SqlDbType = SqlDbType.Structured
            tvp.TypeName = "NFL.Picks_Type"
            Try
                cmd.Connection.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                cmd.Connection.Close()
            End Try
            Return True
        Catch
            Return False
        End Try
    End Function
    Public Function LoadStandings() As DataTable
        Dim cmd As New SqlCommand("NFL.usp_Get_Standings")

        Return FillDataTable(cmd)
    End Function
    Public Function FillDataTable(cmd As SqlCommand) As DataTable
        If cmd.Connection Is Nothing Then
            cmd.Connection = Connection()
        End If
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
End Module
