<Serializable> Public Class Individuum

    Private Fitness As Double
    Private Genome As Spline()
    Private Name As Integer

    'constructor
    Public Sub New()

    End Sub

    'constructor full
    Public Sub New(ByVal f As Double, ByVal n As Integer, ByVal g As Spline())
        Fitness = f
        Name = n
        Genome = g
    End Sub

    Public Sub sFitness(ByVal f As Double)
        Fitness = f
    End Sub

    Public Function gFitness() As Double
        Return Fitness
    End Function

    Public Sub sGenome(ByVal g As Spline())

        ReDim Genome(g.Length - 1)
        Dim i As Integer

        For i = 0 To Genome.Length - 1
            Genome(i) = g(i)
        Next

    End Sub

    Public Function gGenome() As Spline()

        If Genome IsNot Nothing Then
            Return Genome
        Else
            Genome = {}
            Return Genome
        End If

    End Function

    Public Sub sName(ByVal d As Integer)
        Name = d
    End Sub

    Public Function gName() As Integer
        Return Name
    End Function

End Class
