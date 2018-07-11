Imports Microsoft.Win32

Public Class SettingsPage
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        SpectrumPath.Text = Settings.DATA.SpectrumPath
    End Sub

    Private Sub SpectrumPath_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles SpectrumPath.MouseDoubleClick
        Dim OFD As New OpenFileDialog
        OFD.Filter = "Game's executable (Distance.exe)|Distance.exe"
        MsgBox("To set the spectrum folder, locate the Distance.exe file")
        If OFD.ShowDialog Then
            Dim PATH As String = ""
            Dim FOLDERS As String() = OFD.FileName.Split("\")

            For Index As Integer = 0 To FOLDERS.Length - 2
                PATH &= FOLDERS(Index) & "\"
            Next
            PATH &= "Distance_Data\Spectrum\"

            Settings.DATA.SpectrumPath = PATH
            Settings.Save()
            SpectrumPath.Text = PATH
        End If
    End Sub
End Class
