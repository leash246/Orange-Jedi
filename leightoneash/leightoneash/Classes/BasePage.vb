Public Class BasePage
    Inherits Page
    Private Function aspNetFormElements() As String()
        Dim cString() As String = {"__EVENTTARGET", "__EVENTARGUMENT", "__VIEWSTATE", "__EVENTVALIDATION", "__VIEWSTATEENCRYPTED"}
        Return cString
    End Function
    Protected Overrides Sub Render(ByVal Writer As HtmlTextWriter)
        Dim stringWriter As New System.IO.StringWriter()
        Dim htmlWriter As New HtmlTextWriter(stringWriter)
        MyBase.Render(htmlWriter)
        Dim html As String = stringWriter.ToString
        Dim FormStart As Integer = html.IndexOf("<form")
        Dim EndForm As Integer = -1
        If FormStart >= 0 Then
            EndForm = html.IndexOf(">", FormStart)
        End If
        If EndForm >= 0 Then
            Dim viewStateBuilder As New StringBuilder
            For Each element As String In aspNetFormElements()
                Dim StartPoint As Integer = html.IndexOf("<input type=""hidden"" name=""" + element.ToString() + """")

                If StartPoint >= 0 Then
                    Dim EndPoint As Integer = html.IndexOf("/>", StartPoint)
                    If EndPoint > 0 Then
                        EndPoint += 2
                        Dim ViewStateInput As String = html.Substring(StartPoint, EndPoint - StartPoint)
                        html = html.Remove(StartPoint, EndPoint - StartPoint)
                        viewStateBuilder.Append(ViewStateInput).Append(vbCrLf)
                    End If
                End If
            Next
            If viewStateBuilder.Length > 0 Then
                viewStateBuilder.Insert(0, vbCrLf)
                html = html.Insert(EndForm + 1, viewStateBuilder.ToString)
            End If
        End If
        Writer.Write(html)
    End Sub
End Class
