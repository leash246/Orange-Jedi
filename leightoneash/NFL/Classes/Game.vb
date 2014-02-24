Namespace Schedule
    Public Class Schedule
        Public gmGames As Games
    End Class
    Public Class Game
        Public HomeTeam As String
        Public AwayTeam As String
        Public HomeRecord As String
        Public AwayRecord As String
        Public Spread As String
        Public Day As String
        Public Time As Integer
        Public Result As String
        Public OverUnder As Decimal
        Public Sub New()
            Me.HomeTeam = ""
            Me.AwayTeam = ""
            Me.HomeRecord = ""
            Me.AwayRecord = ""
            Me.Spread = ""
            Me.Result = ""
            Me.OverUnder = 0
        End Sub
        Public Sub New(cHome As String, cAway As String, cHomeRec As String, cAwayRec As String, cSpread As String, nOverUnder As Decimal)
            Me.HomeTeam = cHome
            Me.AwayTeam = cAway
            Me.HomeRecord = cHomeRec
            Me.AwayRecord = cAwayRec
            Me.Spread = cSpread
            Me.Result = ""
            Me.OverUnder = nOverUnder
        End Sub
    End Class
    Public Class Games
        Inherits System.Collections.ObjectModel.Collection(Of Game)
        Public Sub New()

        End Sub
    End Class

End Namespace
