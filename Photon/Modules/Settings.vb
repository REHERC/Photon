Imports System.IO
Imports Newtonsoft.Json

Public Module Settings
    Public APP_PATH As String
    Public Property DATA As AppSettings
    Public Sub Save()
        File.WriteAllText(APP_PATH & "\Config.json", JsonConvert.SerializeObject(DATA))
    End Sub

    Public Sub Initialize()
        APP_PATH = My.Application.Info.DirectoryPath
        If Not File.Exists(APP_PATH & "\Config.json") Then
            File.WriteAllText(APP_PATH & "\Config.json", JsonConvert.SerializeObject(New AppSettings()))
        End If

        DATA = JsonConvert.DeserializeObject(Of AppSettings)(File.ReadAllText(APP_PATH & "\Config.json"))
    End Sub
End Module
