Imports System.Timers, System.Text, System.IO
Public Class Ansys

    Dim ansproc As Process
    'WithEvents t As TimeControl

    ' initialization without any parameters
    Public Sub New()
        ansproc = GetAnsys()
        If ansproc IsNot Nothing Then
        Else
            ansproc = Process.Start("D:\Programme\Ansys\ANSYS Inc\v172\Framework\bin\Win64\RunWB2", "-B")
            't = New TimeControl
            't.TimetoGo(1000)
        End If
    End Sub

    ' initialization with a given process in case ansys was started by another module
    Public Sub New(p As Process)
        ansproc = p
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

    'Public Sub t_elapsed() Handles t.Go
    '    GetAnsys()
    'End Sub

    Private Declare Function PostMessageA Lib "user32" (ByVal hWnd As IntPtr, ByVal wMsg As UInt32, ByVal wParam As UInt32, ByVal lParam As UInt32) As Long

    Public Sub TtoA(ByVal s As String)
        Dim enc As New ASCIIEncoding
        Dim chars As Byte() = enc.GetBytes(s)

        For Each c As Byte In chars
            PostMessageA(ansproc.MainWindowHandle, 258, Convert.ToUInt32(c), 0)
            PostMessageA(ansproc.MainWindowHandle, 258, 64, 0)
            PostMessageA(ansproc.MainWindowHandle, 258, 8, 0)
        Next

    End Sub

    Public Sub TtoA(ByVal s As String())
        For Each text As String In s
            If text IsNot Nothing Then
                TtoA(text)
                AnsEnter()
            End If
        Next
    End Sub

    Public Sub ExitAnsys()
        PostMessageA(ansproc.MainWindowHandle, 258, 13, 0)
        TtoA("exit")
        PostMessageA(ansproc.MainWindowHandle, 258, 13, 0)
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

    Public Sub AnsEnter()
        PostMessageA(ansproc.MainWindowHandle, 258, 13, 0)
    End Sub

End Class
