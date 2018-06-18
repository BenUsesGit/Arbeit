Imports System.Timers, System.Threading
''' <summary>
''' Class for outsourcing timer-activity during waittime for a starting process of a 3rd party program to finish
''' </summary>
Public Class ProControl
    WithEvents t As New System.Timers.Timer
    Private pr As Process = Nothing
    Private count As Integer = 0

    Public Event ObjReady As EventHandler(Of EventArgs)
    Public Event ObjNotReady As EventHandler(Of EventArgs)
    Public Event Test As EventHandler(Of EventArgs)
    Dim waitEvent As ManualResetEvent

    ''' <summary>
    ''' Sets the chain in motion
    ''' </summary>
    ''' <param name="p"></param>
    ''' <remarks>Tries to do something with a process, that has already started, but is not fully created. 
    ''' This will start a timer. When this timer elapses, a new try to do something with the process follows</remarks>
    Public Sub StartFirst(p As Process)
        setProc(p)
        Try
            Debug.Print("Prozess ID " & pr.Id)
            Dim e As New EventArgs
            RaiseEvent ObjReady(Me, e)
        Catch ex As Exception
            waitEvent = New ManualResetEvent(False)
            StartProc()
            waitEvent.WaitOne()
            Dim e As New EventArgs
            RaiseEvent ObjReady(Me, e)
        End Try

    End Sub

    Public Sub setProc(p As Process)
        pr = p
    End Sub

    'starts the timer
    Private Sub StartProc()
        t.Interval = 3000
        t.Start()
        Dim e As New EventArgs
        TimerElapsed()
    End Sub


    ''' <remarks>As the timer elapses, if the given process responds to </remarks>
    Private Sub TimerElapsed() Handles t.Elapsed
        Try
            Dim testID As Integer = pr.Id
            Debug.Print("Prozess ID " & testID)
            t.Stop()
            count = 0
            waitEvent.Set()
        Catch ex As Exception
            count += 1
            Dim e As New EventArgs
            If count <= 20 Then
                RaiseEvent ObjNotReady(Me, e)
            Else
                Debug.Print("TimeOut")
            End If
        End Try
    End Sub

End Class
