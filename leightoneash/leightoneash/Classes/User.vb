Public Class User
    Private cEmail As String
    Private cFirstName As String
    Private cMiddleName As String
    Private cLastName As String
    Private nRole As Integer
    Public ReadOnly Property Email As String
        Get
            Return cEmail
        End Get
    End Property
    Public ReadOnly Property FirstName As String
        Get
            Return cFirstName
        End Get
    End Property
    Public ReadOnly Property MiddleName As String
        Get
            Return cMiddleName
        End Get
    End Property
    Public ReadOnly Property LastName As String
        Get
            Return cLastName
        End Get
    End Property
    Public ReadOnly Property Role As Integer
        Get
            Return nRole
        End Get
    End Property
    Public Function ValidRole(enmRole As Enums.enmRoles) As Boolean
        If (nRole And enmRole) > 0 Then
            Return True
        End If
        Return False
    End Function
    Public Sub New(pstrEmail As String, pstrFName As String, pstrMName As String, pstrLName As String, pintRole As Integer)
        Me.cEmail = pstrEmail
        Me.cFirstName = pstrFName
        Me.cMiddleName = pstrMName
        Me.cLastName = pstrLName
        Me.nRole = pintRole
    End Sub
    Public Sub New(drUser As DataRow)
        Me.cEmail = drUser.Item("cEmail")
        Me.cFirstName = drUser.Item("cFName")
        Me.cMiddleName = drUser.Item("cMName")
        Me.cLastName = drUser.Item("cLName")
        Me.nRole = drUser.Item("nRole")
    End Sub
    Public Sub New(pstrEmail As String)
        Me.New(LoginUser(pstrEmail))
    End Sub

End Class
