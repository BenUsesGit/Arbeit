Imports System.Timers, System.Threading
Public Class ProControl
    WithEvents t As New System.Timers.Timer
    Dim pr As Process = Nothing
    'WithEvents oD As New ObjectDelay
    Dim count As Integer = 0

    Public Event ObjReady As EventHandler(Of EventArgs)
    Public Event ObjNotReady As EventHandler(Of EventArgs)
    Public Event Test As EventHandler(Of EventArgs)
    Dim waitEvent As ManualResetEvent

    Public Sub StartFirst(p As Process)
        't.Interval = 2000
        't.Start()
        ''Debug.Print("TimerStart")
        setProc(p)
        Try
            Debug.Print("Prozess ID " & pr.Id)
            Dim e As New EventArgs
            RaiseEvent ObjReady(Me, e)
        Catch ex As Exception
            'Dim thr As Thread
            'thr = New Thread(AddressOf StartProc)
            'thr.IsBackground = True
            'thr.Start()
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

    Public Sub StartProc()
        'Try
        '    Debug.Print("Tick")
        '    Debug.Print("Prozess" & pr.Id)
        '    OnObjReady(e)
        'Catch ex As Exception
        '    OnObjNotReady(e)
        'End Try
        t.Interval = 1000
        t.Start()
        Dim e As New EventArgs
        TimerElapsed()
    End Sub



    Public Sub TimerElapsed() Handles t.Elapsed
        Try
            Dim testID As Integer = pr.Id
            Debug.Print("Prozess ID " & testID)
            t.Stop()
            count = 0
            waitEvent.Set()
        Catch ex As Exception
            count += 1
            Dim e As New EventArgs
            RaiseEvent ObjNotReady(Me, e)
            If count >= 20 Then
                Debug.Print("TimeOut")
            End If
        End Try
    End Sub

    'Protected Overridable Sub OnObjReady(e As EventArgs)
    '    t.Stop()
    '    'Debug.Print("Timer Stop")
    'End Sub

    'Protected Overridable Sub OnObjNotReady(e As EventArgs)
    '    t.Stop()
    '    'Debug.Print("Timer Stop")
    '    RaiseEvent ObjNotReady(Me, e)
    'End Sub

End Class
