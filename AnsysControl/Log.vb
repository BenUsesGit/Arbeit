Imports System.IO, System.Text.RegularExpressions, System.Diagnostics, System.Math

Public Class Log
    Dim fn As String ' directory to log file
    Dim name As String ' name of the log-file
    Dim actfile As Integer ' value of the current individual

    ' TODO write own eventArgs-class that passes a string as argument, then raise only one event and handle the switch case 
    Public Event Rdy As EventHandler(Of EventArgs)
    Public Event PrjLoad As EventHandler(Of EventArgs)
    Public Event UpdateCAD As EventHandler(Of EventArgs)

    Public Event nxtStep As EventHandler(Of eString)

    WithEvents fw As New FileSystemWatcher

    Public Sub New()

    End Sub

    Public Sub SetActFile(i As Integer)
        actfile = i
    End Sub
    ' p is the path to the log file, n is the name of the logfile
    Public Sub setNew(p As String, n As String)
        fn = p
        name = n
        fw.Path = p
        fw.Filter = n
        Try
            fw.EnableRaisingEvents = True
            Write("New Session", 0, -1)
        Catch ex As Exception
            Debug.Print("Da ist etwas schief gegangen")
        End Try
    End Sub

    Public Sub setExist(p As String, n As String)
        fn = p
        name = n
        Try
            Write("New Session", 0, -1)
        Catch ex As Exception
            Debug.Print("Da ist etwas schief gegangen")
        End Try
    End Sub

    ' writes text to the log file, cmdType is for future different types of commands e.g. enidividual specific
    Public Sub Write(s As String, stpNum As Integer, indi As Integer)
        Dim fs As FileStream
        Dim sw As StreamWriter

        If File.Exists(fn & name) Then

            fs = New FileStream(fn & name, FileMode.Append)
            sw = New StreamWriter(fs)

            Dim stamp As DateTime = DateTime.Now

            sw.Write(vbCrLf & stpNum & vbTab & CStr(stamp) & vbTab & indi & vbTab & s, True)

        Else
            fs = New FileStream(fn & name, FileMode.CreateNew)
            sw = New StreamWriter(fs)
            Dim stamp As DateTime = DateTime.Now

            sw.Write(vbCrLf & stpNum & vbTab & CStr(stamp) & vbTab & indi & vbTab & s, True)

        End If
        sw.Close()
        fs.Close()
    End Sub

    Public Function Read() As String()
        SyncLock Me
            Try
                Dim lstcmd As String = File.ReadLines(fn & name).Last()
                Dim lstcmdArr As String() = Regex.Split(lstcmd, "\t")

                Return lstcmdArr

            Catch ex As Exception
                Debug.Print("Kann nicht aus der Datei lesen")
                Return {"Nothing"}
            End Try
        End SyncLock
    End Function

    ' handles the event of some instance changing the log-file, raises certain events, depending on the value of the last log-entry
    Public Sub OnClose(source As Object, e As EventArgs) Handles fw.Changed
        Dim arr As String() = Read()
        Dim edata As eString = New eString(arr)
        RaiseEvent nxtStep(Me, edata)
    End Sub

    Public Sub MyFinalize()
        Finalize()
    End Sub

    Protected Overrides Sub Finalize()
        fw.Dispose()
        MyBase.Finalize()
    End Sub

End Class
