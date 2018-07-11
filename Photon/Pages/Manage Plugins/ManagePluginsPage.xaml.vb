Imports System.IO

Public Class ManagePluginsPage
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        If Not Directory.Exists(Settings.SPECTRUM_PATH) Then
            Globals.MainWindow.SetPage(New ErrorPage("OOPS !", "You can't use this feature because the spectrum path has not been set." & vbCrLf & "Please go to the settings to fix this.", "Manage plugins"))
        End If
    End Sub
End Class
