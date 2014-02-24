Imports System.Xml.Serialization
Imports System.IO
<Serializable()> _
Public MustInherit Class MenuControl
    Inherits System.Web.UI.UserControl

    Public MustOverride ReadOnly Property SitePage As Enums.enmPages

    Public MustOverride Property HREF As String

    Public MustOverride Property Text As String
End Class
