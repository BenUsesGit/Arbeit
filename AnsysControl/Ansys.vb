Imports System.Timers, System.Text, System.IO, System.Threading

Public Class Ansys
    'Dim ansproc As Process ' process, the command window is running on
    Dim actfile As Integer ' individual, the ans-object is currently working on

    ' TODO it might be necessary to create lock-object that protects the act-file, value as long as it is not finished with evaluation to prevent changing from other intances or threads
    WithEvents pc As New ProControl, ansproc As New Process, lg As New Log, JMan As New JobManager(lg)

    Dim logfile As String

    Public Event EvalRdy As EventHandler(Of EventArgs)
    Public Event ObjRdy As EventHandler(Of EventArgs)

    ' initialization without any parameters
    Public Sub New()
        If lg Is Nothing Then
            lg = New Log
        End If
    End Sub

    Public Sub SetActFile(i As Integer)
        actfile = i
        lg.SetActFile(i)
    End Sub

    ' Sets the path for the logfile and creates the new Logfile
    Public Sub slogfile(s As String)
        logfile = s

        If Directory.Exists(logfile) Then
            'If Not File.Exists(logfile & "Anslog.txt") Then
            lg.SetActFile(actfile)
            lg.setNew(logfile, "AnsLog.txt")
            'Else
            '    lg.SetActFile(actfile)
            '    lg.setExist(logfile, "Anslog.txt")
            'End If
        End If
    End Sub

    Public Sub Open()
        ' check for existing running instances of Ansys
        ansproc = GetAnsys()

        If ansproc IsNot Nothing Then
            ' Do nothing if there is already an instance of Ansys running
        Else
            Process.Start("D:\Programme\Ansys\ANSYS Inc\v172\Framework\bin\Win64\RunWB2", "-B")
            ansproc = GetAnsys()
            PokeAns()
        End If
    End Sub

    ' Sub for checking whether the AnsysProcess-creation has finished
    Public Sub PokeAns()
        pc.StartFirst(ansproc)
    End Sub

    ' Sub listens whether ProControl-class raises the "Object is ready"-event, gives notice and starts the dummy-project
    Public Sub AnsReady(sender As Object, e As EventArgs) Handles pc.ObjReady
        Debug.Print("AnsysProzess erfolgreich zugewiesen")
        TtoA("actfile = " & CStr(actfile))
        AnsEnter()
        slogfile("D:\Skripte\VBA\Splines\Test\Ichbinhier\")
        'TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\OpenProject.py" & Chr(34) & ")")
        'AnsEnter()
        RaiseEvent ObjRdy(Me, e)
    End Sub

    ' Sub listens whether ProControl-class raises the "Object is not ready"-event, gives notice and restarts the timer
    Public Sub AnsNotReady(sender As Object, e As EventArgs) Handles pc.ObjNotReady
        Debug.Print("AnsysProzess noch nicht so weit")
        ansproc = GetAnsys()
        pc.setProc(ansproc)
    End Sub

    ' returns the first out of a list of running processes
    ' TODO overload this function to check for a process with a certain unique identification
    Public Function GetAnsys() As Process
        Dim ap As Process() = ExistAnsys()
        Dim p As Process

        If ap.Length = 0 Then
            p = Nothing
        Else
            p = ap(0)
        End If

        ansproc = p
        Return p
    End Function

    ' check if ansys exists and returns the running processes
    Public Function ExistAnsys() As Process()
        Dim p As Process ' einzelner Prozess
        Dim pp() As Process ' Liste der Prozesse
        Dim ap As Process() = {}
        Dim count As Integer = 0

        ' --- Liste der Prozesse holen
        pp = Process.GetProcesses

        ' --- Schleife über alle Prozesse
        For Each p In pp
            If p.ProcessName = "AnsysFW" Then
                ReDim ap(ap.Length)
                ap(ap.Length - 1) = p
                Debug.Print("Ansysprozess gefunden")
            End If
        Next
        Return ap
    End Function

    ' Custom function to send Messages to windows with known handle
    ' it needs the Window-handle, the specific window command in this case 258 means "KeyDown", then the type of the key that  was "pressed", a last not important parameter
    Private Declare Function PostMessageA Lib "user32" (ByVal hWnd As IntPtr, ByVal wMsg As UInt32, ByVal wParam As UInt32, ByVal lParam As UInt32) As Long

    ' Converts a given string into subsequently executed simulated keystrokes that are send to the Ansys window
    Public Sub TtoA(ByVal s As String)
        SyncLock Me
            ' the Ansys Window needs its messages in a certain format
            Dim enc As New ASCIIEncoding
            Dim chars As Byte() = enc.GetBytes(s)

            For Each c As Byte In chars
                ' charakter
                PostMessageA(ansproc.MainWindowHandle, 258, Convert.ToUInt32(c), 0)
                ' followed by an SPACE
                PostMessageA(ansproc.MainWindowHandle, 258, 64, 0)
                ' followed by BACKSPACE, this is important to avoid wrong display of messages
                PostMessageA(ansproc.MainWindowHandle, 258, 8, 0)
            Next
        End SyncLock
    End Sub

    ' same for an array of strings
    Public Sub TtoA(ByVal s As String())
        SyncLock Me
            For Each text As String In s
                If text IsNot Nothing Then
                    TtoA(text)
                    AnsEnter()
                End If
            Next
        End SyncLock
    End Sub

    ' closes the ansys process and the command window
    Public Sub ExitAnsys()
        ' Enter
        PostMessageA(ansproc.MainWindowHandle, 258, 13, 0)
        TtoA("exit")
        PostMessageA(ansproc.MainWindowHandle, 258, 13, 0)
        Me.Finalize()
    End Sub

    Public Function ReadScript(ByVal p As String) As String()
        Dim fs As New FileStream(p, FileMode.Open)
        Dim sr As New StreamReader(fs)
        Dim tt As String() = {}

        Do
            ReDim Preserve tt(tt.Length)
            tt(tt.Length - 1) = sr.ReadLine()
        Loop Until tt(tt.Length - 1) Is Nothing
        'thfa.CleanText(sr.ReadToEnd)
        sr.Close()
        fs.Close()
        Return tt
    End Function

    ' sends the "Enter"-command to the ansys window
    Public Sub AnsEnter()
        PostMessageA(ansproc.MainWindowHandle, 258, 13, 0)
    End Sub

    ' Sub handles always the next step of the work on an individual
    Public Sub Stepp(sender As Object, progress As eString) Handles JMan.nextJob
        SyncLock Me
            ' check if the previous action has been only taken but once for the individual, this prevents multiple consecutive clicks by the user on the same button
            ' TODO make the button unavailable until the work is finished
            ' If the last entry begins with an "*", it was a process specific command else it is a command to an specific individual

            Select Case progress.lstAction
                Case 0
                    TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\OpenProject.py" & Chr(34) & ")")
                    AnsEnter()
                    TtoA("RunScript(" & Chr(34) & "D:\Skripte\VBA\Splines\Test\Java.py" & Chr(34) & ")")
                    AnsEnter()
                Case 10
                    ' do nothing
                Case 20
                    RaiseEvent ObjRdy(Me, progress)
                Case 30
                    TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\LoadAndUpdateCAD.py" & Chr(34) & ")")
                    AnsEnter()
                Case 40
                    TtoA("s.readResults(listOfResults)")
                    AnsEnter()
                Case 45
                    ' do nothing
                Case 50
                    RaiseEvent EvalRdy(Me, progress)
                Case 60
                    ' do nothing
            End Select
        End SyncLock
    End Sub

    Public Sub Begin(i As Integer)
        SyncLock Me
            actfile = i
            TtoA("actfile = " & Str(actfile))
            AnsEnter()
            lg.SetActFile(actfile)
            lg.Write("Begin work on individual", 30, actfile)
        End SyncLock
    End Sub

    ' tests how often the last action on the individual already has been taken
    Public Function TestForDouble(sArr As String(), s As String)
        Dim count As Integer = 0
        For Each line As String In sArr
            If s = line Then
                count += 1
            End If
        Next
        Return count
    End Function

    Public Sub MyFinalize()
        Finalize()
    End Sub

    Protected Overrides Sub Finalize()
        If File.Exists("D:   \AnsysSimulationen\DevByTry\Das_System_files\.lock") Then
            My.Computer.FileSystem.DeleteFile("D:\AnsysSimulationen\DevByTry\Das_System_files\.lock")
        End If
        Debug.Print("Ansys-Object destroyed")
        If lg IsNot Nothing Then
            lg.Write("Session closed", 60, -1)
            lg.MyFinalize()
            lg = Nothing
        End If
    End Sub
End Class
