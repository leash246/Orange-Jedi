Imports System.Web.HttpServerUtility
Imports System.Xml
Imports System.IO
Module MiscFunctions
    Public hsu As HttpServerUtility
    Public Sub VerifyServer(server As HttpServerUtility)
        hsu = server
    End Sub
    Public Function RelativePath(cApp As String, cPath As String) As String
        Dim cReturn As String = "~/"
        cReturn &= cApp & "/"
        cReturn &= cPath
        Return cReturn.Replace("//", "/")
    End Function
    Public Sub LoadWeek(nWeek As Integer, gmGames As Schedule.Games)
        Dim cPathWeek As String = hsu.MapPath(RelativePath("NFL", "XML/Week" & nWeek.ToString & ".xml"))
        Dim fiWeek As FileInfo = New FileInfo(cPathWeek)
        Dim xmldocWeek As New XmlDataDocument()
        Dim xmlnodeWeek As XmlNodeList
        Dim i As Integer
        Dim fs As FileStream = fiWeek.OpenRead
        xmldocWeek.Load(fs)
        xmlnodeWeek = xmldocWeek.GetElementsByTagName("game")
        For i = 0 To xmlnodeWeek.Count - 1
            Dim gm As New Schedule.Game
            gm.Day = xmlnodeWeek(i).Attributes("day").Value.ToString
            gm.Time = xmlnodeWeek(i).ChildNodes.Item(0).InnerText.Trim
            gm.AwayTeam = xmlnodeWeek(i).ChildNodes.Item(1).InnerText.Trim
            gm.AwayRecord = TeamRecord(gm.AwayTeam)
            gm.HomeTeam = xmlnodeWeek(i).ChildNodes.Item(2).InnerText.Trim
            gm.HomeRecord = TeamRecord(gm.HomeTeam)
            gm.Spread = xmlnodeWeek(i).ChildNodes.Item(3).InnerText.Trim
            If xmlnodeWeek(i).ChildNodes.Count = 5 Then
                Decimal.TryParse(xmlnodeWeek(i).ChildNodes.Item(4).InnerText.Trim, gm.OverUnder)
            End If
            gmGames.Add(gm)
        Next
        fs.Close()
    End Sub
    Public Sub SaveWeek(nWeek As Integer, gmGames As Schedule.Games)
        Dim cPathWeek As String = hsu.MapPath(RelativePath("NFL", "XML/Week" & nWeek.ToString & ".xml"))
        If File.Exists(cPathWeek) Then
            File.Delete(cPathWeek)
        End If
        Dim writer As New XmlTextWriter(cPathWeek, System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        writer.Formatting = Formatting.Indented
        writer.Indentation = 2
        writer.WriteStartElement("week")
        WriteAttribute(writer, "number", nWeek.ToString)
        For Each Game As Schedule.Game In gmGames
            writer.WriteStartElement("game")
            WriteAttribute(writer, "day", Game.Day)
            WriteElement(writer, "time", Game.Time)
            WriteElement(writer, "Away", Game.AwayTeam)
            WriteElement(writer, "Home", Game.HomeTeam)
            WriteElement(writer, "spread", Game.Spread)
            If Game.OverUnder > 0 Then
                WriteElement(writer, "OverUnder", Game.OverUnder)
            End If
            writer.WriteEndElement()
        Next
        writer.WriteEndElement()
        writer.Close()
    End Sub
    Public Sub WriteElement(ByVal writer As XmlTextWriter, ByVal cElement As String, ByVal cValue As String)
        writer.WriteStartElement(cElement)
        writer.WriteString(cValue)
        writer.WriteEndElement()
    End Sub
    Public Sub WriteAttribute(ByVal writer As XmlTextWriter, ByVal cAttribute As String, ByVal cValue As String)
        writer.WriteStartAttribute(cAttribute)
        writer.WriteString(cValue)
        writer.WriteEndAttribute()
    End Sub
    Public Function TeamRecord(cTeam As String) As String
        Dim cPathStandings As String = hsu.MapPath(RelativePath("NFL", "XML/Standings.xml"))
        Dim fi As FileInfo = New FileInfo(cPathStandings)
        Dim xmldoc As New XmlDataDocument()
        Dim xmlnode As XmlNodeList
        Dim i As Integer
        Dim fs As FileStream = fi.OpenRead
        Dim cRecord As String = ""
        xmldoc.Load(fs)
        xmlnode = xmldoc.GetElementsByTagName("Team")
        For i = 0 To xmlnode.Count - 1
            If xmlnode(i).Attributes("name").Value.ToString = cTeam Then
                cRecord = xmlnode(i).ChildNodes.Item(0).InnerText.Trim
                cRecord &= "-"
                cRecord &= xmlnode(i).ChildNodes.Item(1).InnerText.Trim
                If xmlnode(i).ChildNodes.Item(2).InnerText.Trim <> "0" Then
                    cRecord &= "-" & xmlnode(i).ChildNodes.Item(2).InnerText.Trim
                End If
                Exit For
            End If
        Next
        Return cRecord
    End Function

    Public Function Save(nWeek As Integer, ByVal repGames As Repeater, cUser As String, Optional ByVal nOverUnder As Integer = 0) As Boolean
        Try
            Dim hsu As HttpServerUtility
            Dim cPath As String = hsu.MapPath(RelativePath("NFL", "Picks"))
            If Not Directory.Exists(cPath) Then
                Directory.CreateDirectory(cPath)
            End If
            cPath &= "/Week" & nWeek.ToString
            If Not Directory.Exists(cPath) Then
                Directory.CreateDirectory(cPath)
            End If
            cPath &= "/" & cUser & ".xml"
            If File.Exists(cPath) Then
                File.Delete(cPath)
            End If
            Dim writer As New XmlTextWriter(cPath, System.Text.Encoding.UTF8)
            writer.WriteStartDocument(True)
            writer.Formatting = Formatting.Indented
            writer.Indentation = 2
            writer.WriteStartElement("Selections")
            writer.WriteStartAttribute("Week")
            writer.WriteString(nWeek.ToString)
            writer.WriteEndAttribute()
            writer.WriteStartElement("User")
            writer.WriteString(cUser)
            writer.WriteEndElement()
            writer.WriteStartElement("Picks")
            Dim i As Integer = 1
            For Each item As RepeaterItem In repGames.Items
                Dim rbtAway As RadioButton = item.FindControl("rbtAway")
                Dim rbtHome As RadioButton = item.FindControl("rbtHome")
                Dim lblTeam As Label
                Dim cTeam As String = ""
                If rbtAway.Checked Then
                    lblTeam = item.FindControl("lblAwayTeam")
                    cTeam = lblTeam.Text
                ElseIf rbtHome.Checked Then
                    lblTeam = item.FindControl("lblHomeTeam")
                    cTeam = lblTeam.Text
                End If
                writer.WriteStartElement("Pick")
                writer.WriteStartAttribute("Game")
                writer.WriteString(i.ToString)
                writer.WriteEndAttribute()
                writer.WriteString(cTeam)
                writer.WriteEndElement()
                i += 1
            Next
            writer.WriteEndElement()
            writer.WriteStartElement("Tiebreaker")
            writer.WriteString(nOverUnder.ToString)
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.Close()
            Return True
        Catch
            Return False
        End Try
    End Function
    Public Function LoadStandings(ByVal cPath As String) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Conference")
        dt.Columns.Add("Division")
        dt.Columns.Add("Team")
        dt.Columns.Add("Wins")
        dt.Columns.Add("Losses")
        dt.Columns.Add("Ties")
        Dim fi As New FileInfo(cPath)
        Dim xmlDoc As New XmlDataDocument
        Dim xmlNode As XmlNodeList
        Dim i As Integer
        Dim fs As FileStream = fi.OpenRead
        xmlDoc.Load(fs)
        xmlNode = xmlDoc.GetElementsByTagName("Standings")
        dt.TableName = xmlNode(0).Attributes(0).InnerText
        xmlNode = xmlDoc.GetElementsByTagName("Team")
        For i = 0 To xmlNode.Count - 1
            Dim dr As DataRow = dt.NewRow
            dr.Item("Team") = xmlNode(i).Attributes(0).InnerText
            dr.Item("Wins") = xmlNode(i).ChildNodes(0).InnerText
            dr.Item("Losses") = xmlNode(i).ChildNodes(1).InnerText
            dr.Item("Ties") = xmlNode(i).ChildNodes(2).InnerText
            dr.Item("Conference") = Left(xmlNode(i).ChildNodes(3).InnerText, 3)
            Select Case Right(xmlNode(i).ChildNodes(3).InnerText, 1)
                Case "N"
                    dr.Item("Division") = "North"
                Case "S"
                    dr.Item("Division") = "South"
                Case "E"
                    dr.Item("Division") = "East"
                Case "W"
                    dr.Item("Division") = "West"
            End Select
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function
End Module
