Imports System.IO

Public Class ViewLogsPage
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        If Not Directory.Exists(Settings.DATA.SpectrumPath) Then
            Globals.MainWindow.SetPage(New ErrorPage("OOPS !", "You can't use this feature because the spectrum path has not been set." & vbCrLf & "Please go to the settings to fix this.", "View logs"))
        ElseIf Not Directory.Exists(Settings.DATA.SpectrumPath & "\Logs") Then
            Globals.MainWindow.SetPage(New ErrorPage("OOPS !", "Your Spectrum installation seems to be corrupt. Impossible to find the following folder:" & vbCrLf & Settings.DATA.SpectrumPath & "Logs", "View logs"))
        Else
            Scan()
        End If
    End Sub

    Public Sub Scan()
        Dim LOG_FILES As New List(Of String)
        LOG_FILES.Clear()
        For Each FILE As String In Directory.GetFiles(Settings.DATA.SpectrumPath & "Logs")
            LOG_FILES.Add(FILE)
        Next


        For Each PLUGIN_DIR As String In Directory.GetDirectories(Settings.DATA.SpectrumPath & "Plugins")
            If Directory.Exists(PLUGIN_DIR & "Logs") Then
                For Each FILE As String In Directory.GetFiles(PLUGIN_DIR & "Logs")
                    LOG_FILES.Add(FILE)
                Next
            End If
        Next

        Logs.Children.Clear()
        For Each LOG As String In LOG_FILES
            Dim LOG_BUTTON As New Button With {.Content = LOG}
            LOG_BUTTON.FontSize = 24
            AddHandler LOG_BUTTON.Click, AddressOf OpenLog
            Logs.Children.Add(LOG_BUTTON)
        Next
    End Sub

    Public Sub OpenLog(sender As Object, e As RoutedEventArgs)
        With CType(sender, Button)
            Globals.MainWindow.SetPage(New LogViewer(CStr(.Content), "View Logs"))
        End With
    End Sub
End Class
