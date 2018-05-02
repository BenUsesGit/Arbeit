Public Class Pair
    Public Job As Integer
    Public indi As Integer
    Public res As Integer() = {}

    ' TODO write getter & setter methods

    Public Sub New()

    End Sub

    Public Sub New(j As Integer, i As Integer)
        Job = j ' Job ID
        indi = i ' individual ID
    End Sub
End Class
