Public Class LoadPage

    Public Sub New(Title As String, Message As String, Optional Header As String = "...")
        ' Cet appel est requis par le concepteur.
        InitializeComponent()
        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        Me.Title.Text = Title
        Me.Paragraph.Text = Message
        Me.Header.Text = Header
    End Sub
End Class

