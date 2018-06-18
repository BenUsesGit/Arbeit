''' <summary>
''' Pair consisting of a job , individual and if necessary a result
''' </summary>
Public Class Pair
    ' TODO writing getter & setter methods?
    Public Job As Integer
    Public indi As Integer
    Public res As Double() = {}

    ' TODO write getter & setter methods

    Public Sub New()

    End Sub

    Public Sub New(j As Integer, i As Integer)
        Job = j ' Job ID
        indi = i ' individual ID
    End Sub
End Class
