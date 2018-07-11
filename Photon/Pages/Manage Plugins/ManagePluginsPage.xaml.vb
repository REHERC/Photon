Imports System.IO
Imports Newtonsoft.Json

Public Class ManagePluginsPage
    Private MANAGER As ManagerSettings

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        ' Display fancy errors if needed
        If Not Directory.Exists(Settings.DATA.SpectrumPath) Then
            Globals.MainWindow.SetPage(New ErrorPage("OOPS !", "You can't use this feature because the spectrum path has not been set." & vbCrLf & "Please go to the settings to fix this.", "Manage plugins"))
        ElseIf Not Directory.Exists(Settings.DATA.SpectrumPath & "\Settings") Then
            Globals.MainWindow.SetPage(New ErrorPage("OOPS !", "Your Spectrum installation seems to be corrupt. Impossible to find the following folder:" & vbCrLf & Settings.DATA.SpectrumPath & "Settings", "Manage plugins"))
        ElseIf Not File.Exists(Settings.DATA.SpectrumPath & "\Settings\ManagerSettings.json") Then
            Globals.MainWindow.SetPage(New ErrorPage("OOPS !", "Your Spectrum installation seems to be corrupt. Impossible to find the following file:" & vbCrLf & Settings.DATA.SpectrumPath & "Settings\ManagerSettings.json", "Manage plugins"))
        End If

        ' Get Spectrum manager settings
        MANAGER = JsonConvert.DeserializeObject(Of ManagerSettings)(File.ReadAllText(Settings.DATA.SpectrumPath & "\Settings\ManagerSettings.json"))
        EnableSpectrum.IsChecked = MANAGER.Enabled

        Plugins.Children.Clear()
        For Each Folder As String In Directory.GetDirectories(Settings.DATA.SpectrumPath & "\Plugins")
            Dim MANIFEST_FILE As String = Folder & "\plugin.json"
            If File.Exists(MANIFEST_FILE) Then
                Dim MANIFEST As PluginManifest = JsonConvert.DeserializeObject(Of PluginManifest)(File.ReadAllText(MANIFEST_FILE))
                Dim BOX As New CheckBox
                BOX.Content = MANIFEST.FriendlyName
                BOX.IsChecked = MANIFEST.SkipLoad
                BOX.Foreground = New SolidColorBrush(Colors.Black)
                BOX.FontSize = EnableSpectrum.FontSize
                BOX.FontFamily = EnableSpectrum.FontFamily
                BOX.FontStyle = EnableSpectrum.FontStyle

                Dim HIERARCHY As New List(Of String)(Folder.Split("\"))
                Dim PLUGIN_FOLDER As String = HIERARCHY.Last()

                BOX.Tag = PLUGIN_FOLDER
                Plugins.Children.Add(BOX)

                AddHandler BOX.Checked, AddressOf BOX_CheckedChanged
                AddHandler BOX.Unchecked, AddressOf BOX_CheckedChanged
            End If
        Next

    End Sub

    Private Sub BOX_CheckedChanged(sender As Object, e As RoutedEventArgs)
        With CType(sender, CheckBox)
            MsgBox(CStr(.Tag))
        End With
    End Sub

    Private Sub EnableSpectrum_CheckedChanged(sender As Object, e As RoutedEventArgs) Handles EnableSpectrum.Unchecked, EnableSpectrum.Checked
        ' Set Spectrum manager settings
        MANAGER.Enabled = EnableSpectrum.IsChecked
        File.WriteAllText(Settings.DATA.SpectrumPath & "\Settings\ManagerSettings.json", JsonConvert.SerializeObject(MANAGER))
    End Sub
End Class
