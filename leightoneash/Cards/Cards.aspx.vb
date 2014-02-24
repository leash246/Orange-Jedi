Imports leightoneash.Schedule
Imports System.IO
Imports System.Xml
Public Class CardsDefault
    Inherits System.Web.UI.Page
    Dim gmGames As New Schedule.Games
    Dim nWeek As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Error") Is Nothing Then
            lblMessage.Text = "Please select a card game"
        Else
            lblMessage.Text = Session("Error").ToString
            Session("Error") = Nothing
        End If
    End Sub

End Class