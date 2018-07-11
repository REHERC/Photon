Class MainWindow
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Globals.MainWindow = Me
        Settings.Initialize()
        If Menu.Items.Count > 0 Then
            Menu.SelectedIndex = 0
        End If
    End Sub

    Private Sub Menu_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles Menu.SelectionChanged
        Select Case Menu.SelectedIndex
            Case 0
                SetPage(New HomePage)
            Case 1
                SetPage(New SettingsPage)
            Case 3
                SetPage(New ManagePluginsPage)
            Case 4
                SetPage(New ViewLogsPage)
        End Select


    End Sub

    Public Sub SetPage(Page As UserControl)
        PageContent.Children.Clear()
        PageContent.Children.Add(Page)
    End Sub
End Class
