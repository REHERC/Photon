Imports System.IO
Imports System.Windows.Threading

Public Class LogViewer
    Private Log As String
    Private Title As String
    Private Loader As LoadPage

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(Log As String, Title As String, Optional Initialize As Boolean = True)
        InitializeComponent()
        Me.Log = Log
        Me.Title = Title
        If Initialize Then
            Init()
        End If
    End Sub

    Public Sub Init()
        Loader = New LoadPage("LOADING, PLEASE WAIT...", "Loading log file:" & vbCrLf & Log, "...")
        Globals.MainWindow.SetPage(Loader)
        ProcessUITasks()
        InitTask()
        Globals.MainWindow.SetPage(Me)
    End Sub

    Private Sub InitTask()
        Me.Header.Text = Log & " | " & Title

        Dim DATA As String = File.ReadAllText(Log)

        Output.Children.Clear()

        Try
            Using Reader As New StreamReader(Log)
                Do While Reader.Peek >= 0
                    Dim LINE_TEXT As String = Reader.ReadLine()

                    While {vbCr, vbLf, vbCrLf, Environment.NewLine}.Contains(LINE_TEXT(0))
                        LINE_TEXT = LINE_TEXT.Substring(1, LINE_TEXT.Length - 1)
                    End While



                    Dim LINE_BLOCK As TextBlock = New TextBlock() With {
                        .Text = LINE_TEXT,
                        .TextAlignment = TextAlignment.Left,
                        .TextWrapping = TextWrapping.Wrap,
                        .FontSize = 16,
                        .FontWeight = FontWeights.Bold,
                        .FontFamily = New FontFamily("file:///C:/Windows/Fonts/consola.ttf"),
                        .Background = New SolidColorBrush(Colors.Black),
                        .Foreground = New SolidColorBrush(Colors.White)
                    }

                    If LINE_TEXT(1) = "i" Then
                        LINE_BLOCK.Background = New SolidColorBrush(Colors.Black)
                        LINE_BLOCK.Foreground = New SolidColorBrush(Colors.White)
                    End If
                    If LINE_TEXT(1) = "*" Then
                        LINE_BLOCK.Background = New SolidColorBrush(Colors.Goldenrod)
                        LINE_BLOCK.Foreground = New SolidColorBrush(Colors.White)
                    End If
                    If LINE_TEXT(1) = "!" Then
                        LINE_BLOCK.Background = New SolidColorBrush(Colors.Firebrick)
                        LINE_BLOCK.Foreground = New SolidColorBrush(Colors.White)
                    End If

                    Output.Children.Add(LINE_BLOCK)
                Loop
            End Using
        Catch SWOOSHYBOI As Exception
            Globals.MainWindow.SetPage(New ErrorPage("OOPS !", "An error occured while opening the file, it will be opened with the default program associated to it ...", "Error | View Log"))
        Finally

        End Try
    End Sub

    Private Sub Back_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles Back.MouseUp
        Globals.MainWindow.SetPage(New ViewLogsPage())
    End Sub

    Public Shared Sub ProcessUITasks()
        Dim frame As DispatcherFrame = New DispatcherFrame()
        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, New DispatcherOperationCallback(Function(ByVal parameter As Object)
                                                                                                                    frame.[Continue] = False
                                                                                                                    Return Nothing
                                                                                                                End Function), Nothing)
        Dispatcher.PushFrame(frame)
    End Sub
End Class
