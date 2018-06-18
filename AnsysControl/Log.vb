Imports System.IO, System.Text.RegularExpressions, System.Diagnostics, System.Math

''' <summary>
''' Creates an instance of the <see cref="FileSystemWatcher"/>-class to monitor the textfile, which keeps track of the process.
''' </summary>
Public Class Log
    Dim fn As String ' directory to log file
    Dim name As String ' name of the log-file
    Dim actfile As Integer ' value of the current individual

    '' TODO write own eventArgs-class that passes a string as argument, then raise only one event and handle the switch case 
    'Public Event Rdy As EventHandler(Of EventArgs)
    'Public Event PrjLoad As EventHandler(Of EventArgs)
    'Public Event UpdateCAD As EventHandler(Of EventArgs)

    ''' <summary>
    ''' Is raised, if the textfile has been chenged and closed.
    ''' </summary>
    Public Event nxtStep As EventHandler(Of eString)

    ' FileSystemwatcher-object for monitoring textfile
    WithEvents fw As New FileSystemWatcher

    ''' <summary>
    ''' constructor
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Sets the name for the text file.
    ''' </summary>
    ''' <param name="i">Name of text file without the path</param>
    Public Sub SetActFile(i As Integer)
        actfile = i
    End Sub

    ''' <summary>
    ''' Sets the Path to the textfile.
    ''' </summary>
    ''' <param name="p">p is the path to the log file</param>
    ''' <param name="n">n is the name of the logfile</param>
    Public Sub setPath(p As String, n As String)
        fn = p
        name = n
        fw.Path = p
        fw.Filter = n
        Try
            fw.EnableRaisingEvents = True
        Catch ex As Exception
            Debug.Print("Da ist etwas schief gegangen")
        End Try
    End Sub

    ''' <summary>
    ''' Sets parameter for this object, when there is alreeady an existing text file with the same name.
    ''' </summary>
    ''' <param name="p">p is the path to the log file</param>
    ''' <param name="n">n is the name of the logfile</param>
    Public Sub setExist(p As String, n As String)
        fn = p
        name = n
        Try
            Write("New Session", 0, -1)
        Catch ex As Exception
            Debug.Print("Da ist etwas schief gegangen")
        End Try
    End Sub

    ''' <summary>
    ''' Writes something into the text file.
    ''' </summary>
    ''' <param name="s">text to the log file</param>
    ''' <param name="stpNum"> Stepnumber of the process</param>
    ''' <param name="indi">The entry needs to be allocated to an </param>
    Public Sub Write(s As String, stpNum As Integer, indi As Integer)
        Dim fs As FileStream
        Dim sw As StreamWriter

        If File.Exists(fn & name) Then

            fs = New FileStream(fn & name, FileMode.Append)
            sw = New StreamWriter(fs)

            Dim stamp As DateTime = DateTime.Now

            sw.Write(vbCrLf & stpNum & vbTab & CStr(stamp) & vbTab & indi & vbTab & s, True)

        Else
            Try
                fs = New FileStream(fn & name, FileMode.CreateNew)
                sw = New StreamWriter(fs)
                Dim stamp As DateTime = DateTime.Now

                sw.Write(vbCrLf & stpNum & vbTab & CStr(stamp) & vbTab & indi & vbTab & s, True)
            Catch ex As Exception
                sw.Close()
                fs.Close()
                Debug.Print("Da ist etwas in der Write_methode des logs schiefgegangen")
            End Try

        End If
        sw.Close()
        fs.Close()
    End Sub

    ''' <summary>
    ''' Reads out the last entry of the log file.
    ''' </summary>
    ''' <returns>An array of string</returns>
    ''' <remarks>The single entries of the returned array represent the strings in between the tabulator spaces</remarks>
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

    ''' <summary>
    ''' Handles the event of some instance changing the log-file, raises certain events, depending on the value of the last log-entry
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"><see cref="eString"/></param>
    ''' <remarks>As the textfile is closed the<see cref="FileSystemWatcher"/>-class registers thias and raises the <see cref="FileSystemWatcher.Changed"/>-event.
    ''' Caution! An normal application e.g. the "Notepad" might cause the FileSystemwatcher to fire multiple times and ruining the process handling. therefore the <see cref="JobManager"/>-class</remarks>
    Public Sub OnClose(source As Object, e As EventArgs) Handles fw.Changed
        Dim arr As String() = Read()
        Dim edata As eString = New eString(arr)
        RaiseEvent nxtStep(Me, edata)
    End Sub

    ''' <summary>
    ''' Sub currently under development
    ''' </summary>
    Public Sub MyFinalize()
        'Finalize()
    End Sub

    ''' <summary>
    ''' Sub currently under development
    ''' </summary>
    Protected Overrides Sub Finalize()
        fw.Dispose()
        'MyBase.Finalize()
    End Sub

End Class
