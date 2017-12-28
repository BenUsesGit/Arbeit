Imports System.Timers, System.Threading.Tasks

Public Class TimeControl
    Dim t As Timer

    Public Event Go()

    Public Sub New()

    End Sub

    Public Sub TimetoGo(z As Integer)
        t = New Timer(z)
        t.Start()
        AddHandler t.Elapsed, AddressOf Me.EndWait
    End Sub

    Private Sub EndWait(sender As Object, e As EventArgs)
        t.Stop()
        Debug.Print("Timer abgelaufen")
        RaiseEvent Go()
    End Sub

End Class
