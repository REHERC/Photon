Imports Notifications.Wpf

Class MainWindow
    Public Sub New()
        ' Cet appel est requis par le concepteur.
        InitializeComponent()
        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        Globals.MainWindow = Me
        Globals.Toaster = New NotificationManager()
        Settings.Initialize()

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
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
            Case 6

        End Select

    End Sub

    Public Sub SetPage(Page As UserControl)
        PageContent.Children.Clear()
        PageContent.Children.Add(Page)
    End Sub

    Private Sub MetroWindow_Unloaded(sender As Object, e As RoutedEventArgs)
        System.Windows.Application.Current.Shutdown()
    End Sub
End Class
