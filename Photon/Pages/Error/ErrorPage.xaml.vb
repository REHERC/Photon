Public Class ErrorPage
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(Title As String, Message As String, Optional Header As String = "Error")
        InitializeComponent()
        Me.Title.Text = Title
        Me.Paragraph.Text = Message
        Me.Header.Text = Header
    End Sub
End Class
