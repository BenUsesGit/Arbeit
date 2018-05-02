Public Class DelayClass
    Inherits EventArgs

    Public NumTries As Integer
    Public prob As Process

    Public Sub New(p As Process)
        'NumTries = i
        prob = p
    End Sub
End Class
