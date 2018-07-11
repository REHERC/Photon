Public Class PluginManifest
    Public Property FriendlyName As String
    Public Property Author As String
    Public Property AuthorContact As String
    Public Property CompatibleAPILevel As Integer
    Public Property IPCIdentifier As String
    Public Property ModuleFileName As String
    Public Property EntryClassName As String
    Public Property Priority As Integer
    Public Property Dependencies As List(Of String)
    Public Property SkipLoad As Boolean = False
End Class
