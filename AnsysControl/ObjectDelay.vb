Imports System.Timers

Public Class ObjectDelay
    Dim count As Integer = 0
    'Dim t As Timer = New Timer(50)
    Dim prob As Process = Nothing

    WithEvents t As New Timer(500)

    Public Event CountDownReady As EventHandler(Of EventArgs)
    Public Event Newtry As EventHandler(Of DelayClass)

    Public Sub New()

    End Sub

    Public Sub SetP(p As Process)
        prob = p
    End Sub

    Public Sub StartTrying()
        'AddHandler t.Elapsed, AddressOf Versuch
        t.Start()
    End Sub

    'Public Sub StopTrying(sender As Object, e As EventArgs)
    '    t.Stop()
    '    Debug.Print("Versuch abgebrochen")
    'End Sub

    Public Sub StopTrying()
        t.Stop()
    End Sub

    Public Sub Versuch(sender As Object, e As System.Timers.ElapsedEventArgs)
        'AddHandler CountDownReady, AddressOf StopTrying
        ''count = count + 1
        'Dim args As New DelayClass(prob)
        ''Debug.Print("Noch " & args.NumTries & " Versuche übrig")
        'If args.NumTries = 0 Then
        '    OnCountDownReady(args)
        'Else
        '    OnNewTry(args)
        'End If
        'RemoveHandler CountDownReady, AddressOf StopTrying
        AddHandler Newtry, AddressOf StartTrying

    End Sub

    Protected Overridable Sub OnCountDownReady(e As EventArgs)
        RaiseEvent CountDownReady(Me, e)
    End Sub

    Protected Overridable Sub OnNewTry(e As EventArgs)
        RaiseEvent Newtry(Me, e)
    End Sub

End Class
