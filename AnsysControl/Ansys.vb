Imports System.Timers, System.Text, System.IO, System.Threading
''' <summary>
''' Class for handling the 3rd party Programm Ansys
''' </summary>
Public Class Ansys
    'Dim ansproc As Process ' process, the command window is running on
    Private actfile As Integer ' individual, the ans-object is currently working on
    Private ID As Integer = Math.Ceiling(Rnd() * 1000) ' for test purposes

    ' TODO it might be necessary to create lock-object that protects the act-file, value as long as it is not finished with evaluation to prevent changing from other intances or threads
    WithEvents pc As New ProControl, ansproc As New Process, lg As New Log, JMan As New JobManager(lg)

    Dim logfile As String

    Public Event EvalRdy As EventHandler(Of EventArgs)
    Public Event ObjRdy As EventHandler(Of EventArgs)

    ''' <summary>
    ''' constructor. initialization without any parameters
    ''' </summary>
    Public Sub New()
        Randomize()
        If lg Is Nothing Then
            lg = New Log
        End If
    End Sub

    ''' <summary>
    ''' Gives the python-script the file-name to work on.  Sets the same parameter in the <see cref="Log"/>.
    ''' </summary>
    ''' <param name="i"></param>
    Public Sub SetActFile(i As Integer)
        actfile = i
        lg.SetActFile(i)
    End Sub

    ''' <summary>
    ''' Sets the path for the logfile and creates the new Logfile
    ''' </summary>
    ''' <param name="s">name as path to the location of the logfile</param>
    Public Sub sLogfile(s As String)
        logfile = s

        If Directory.Exists(logfile) Then
            lg.SetActFile(actfile)
            lg.setPath(logfile, "AnsLog.txt")
        End If
    End Sub


    ''' <summary>
    ''' Initiates a new session for the log file and starts the filewatcher
    ''' </summary>
    ''' <param name="s"> path to the logfile</param>
    Public Sub iniLog(s As String)
        logfile = s
        If Directory.Exists(logfile) Then
            lg.SetActFile(actfile)
            lg.setPath(logfile, "AnsLog.txt")
            lg.Write("New Session", 0, -1)
        End If
    End Sub

    ''' <summary>
    ''' Starts the 3rd party programm Ansys in the so called "batch"-mode
    ''' </summary>
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

    ''' <summary>
    ''' Checking whether the AnsysProcess-creation has finished.
    ''' </summary>
    ''' <remarks>Has to be used together with <seealso cref="AnsReady(Object, EventArgs)"/> and <seealso cref="AnsNotReady(Object, EventArgs)"/></remarks>
    Public Sub PokeAns()
        pc.StartFirst(ansproc)
    End Sub

    ''' <summary>
    ''' This method listens whether ProControl-class raises the <seealso cref="ProControl.ObjReady"/>-event, gives notice and starts the dummy-project
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AnsReady(sender As Object, e As EventArgs) Handles pc.ObjReady
        Debug.Print("AnsysProzess erfolgreich zugewiesen")
        TtoA("actfile = " & CStr(actfile))
        AnsEnter()
        iniLog("D:\Skripte\VBA\Splines\Test\Ichbinhier\")
    End Sub

    ''' <summary>
    ''' Listens whether ProControl-class raises the <seealso cref="ProControl.ObjNotReady"/>-event, gives notice and restarts the timer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AnsNotReady(sender As Object, e As EventArgs) Handles pc.ObjNotReady
        Debug.Print("AnsysProzess noch nicht so weit")
        ansproc = GetAnsys()
        pc.setProc(ansproc)
    End Sub

    ''' <returns>Returns the first out of a list of running ansys processes if more than one process is running.</returns>
    Public Function GetAnsys() As Process
        ' TODO overload this function to check for a process with a certain unique identification
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

    ''' <summary>
    ''' Checks if ansys exists.
    ''' </summary>
    ''' <returns>Returns the running processes</returns>
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

    ''' <summary>
    ''' Custom function to send Messages to windows with known handle
    ''' </summary>
    ''' <param name="hWnd">Window handle off command window</param>
    ''' <param name="wMsg">Usually the message for the window to "press a button".</param>
    ''' <param name="wParam">The Button type to be pressed</param>
    ''' <param name="lParam">Not important := 0</param>
    ''' <returns></returns> 
    ''' <remarks>It needs the Window-handle, the specific window command in this case 258 means "KeyDown", then the type of the key that  was "pressed", a last not important parameter</remarks> 
    Private Declare Function PostMessageA Lib "user32" (ByVal hWnd As IntPtr, ByVal wMsg As UInt32, ByVal wParam As UInt32, ByVal lParam As UInt32) As Long

    ''' <summary>
    ''' Converts a given string into subsequently executed simulated keystrokes that are send to the Ansys window.
    ''' </summary>
    ''' <param name="s">String to be send to window.</param>
    Public Sub TtoA(ByVal s As String)
        SyncLock Me
            ' the Ansys Window needs its messages in a certain format
            Dim enc As New ASCIIEncoding
            Dim chars As Byte() = enc.GetBytes(s)

            For Each c As Byte In chars
                ' charakter
                PostMessageA(ansproc.MainWindowHandle, 258, Convert.ToUInt32(c), 0)
                ' followed by a SPACE
                PostMessageA(ansproc.MainWindowHandle, 258, 64, 0)
                ' followed by BACKSPACE, this is important to avoid wrong display of messages
                PostMessageA(ansproc.MainWindowHandle, 258, 8, 0)
            Next
        End SyncLock
    End Sub

    ''' <overloads>
    ''' Calls the <seealso cref="TtoA(String)"/>-method for an array of strings.
    ''' </overloads>
    ''' <param name="s">Array of strings to be send to window</param>
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

    ''' <summary>
    ''' Exits the Ansys-window and ends the process.
    ''' </summary>
    Public Sub ExitAnsys()
        ' Enter
        PostMessageA(ansproc.MainWindowHandle, 258, 13, 0)
        TtoA("exit")
        PostMessageA(ansproc.MainWindowHandle, 258, 13, 0)
        Me.Finalize()
    End Sub

    ' sends the "Enter"-command to the ansys window
    Public Sub AnsEnter()
        PostMessageA(ansproc.MainWindowHandle, 258, 13, 0)
    End Sub

    ''' <summary>
    ''' Handles the next step in handling the FEM-simulation or reading out data.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="progress"></param>
    ''' <remarks>Actions performed by this method depend on the action that is passed by the event paramter</remarks>
    Private Sub Stepp(sender As Object, progress As eString) Handles JMan.nextJob
        SyncLock Me
            ' check if the previous action has been only taken but once for the individual, this prevents multiple consecutive clicks by the user on the same button
            ' TODO make the button unavailable until the work is finished
            ' If the last entry begins with an "*", it was a process specific command else it is a command to an specific individual

            Select Case progress.lstAction
                Case 0 'start of ansys software instance
                    TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\OpenProject.py" & Chr(34) & ")")
                    AnsEnter()
                    TtoA("RunScript(" & Chr(34) & "D:\Skripte\VBA\Splines\Test\Java.py" & Chr(34) & ")")
                    AnsEnter()
                Case 10
                    ' do nothing
                Case 20 'software instance ready
                    Dim es As New EventArgs
                    RaiseEvent ObjRdy(Me, es)
                Case 30 ' begin evaluating individual
                    TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\LoadAndUpdateCAD.py" & Chr(34) & ")")
                    AnsEnter()
                Case 40 ' read results
                    TtoA("s.readResults(listOfResults)")
                    AnsEnter()
                Case 45 ' evaluation ready
                    RaiseEvent EvalRdy(Me, progress)
                Case 50
                    ' do nothing
                Case 60
                    ' do nothing
            End Select
        End SyncLock
    End Sub

    ''' <summary>
    ''' Begins the evaluation process of an individual
    ''' </summary>
    ''' <param name="i"></param>
    Public Sub Begin(i As Integer)
        SyncLock Me
            actfile = i
            TtoA("actfile = " & Str(actfile))
            AnsEnter()
            lg.SetActFile(actfile)
            lg.Write("Begin work on individual", 30, actfile)
        End SyncLock
    End Sub

    Public Sub MyFinalize()
        'Finalize()
    End Sub

    'Protected Overrides Sub Finalize()
    '    If File.Exists("D:   \AnsysSimulationen\DevByTry\Das_System_files\.lock") Then
    '        My.Computer.FileSystem.DeleteFile("D:\AnsysSimulationen\DevByTry\Das_System_files\.lock")
    '    End If
    '    Debug.Print("Ansys-Object destroyed")
    '    If lg IsNot Nothing Then
    '        lg.Write("Session closed", 60, -1)
    '        lg.MyFinalize()
    '        lg = Nothing
    '    End If
    'End Sub
End Class
