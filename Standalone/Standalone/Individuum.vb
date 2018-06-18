''' <summary>
''' Basically an array of <see cref="Spline"/>s and a little additional information.
''' </summary>
''' <remarks>Builds the core for things like mutation, selection and so on.</remarks>
<Serializable> Public Class Individuum

    Private Fitness As Double ' fitness value
    Private Evaluated As Boolean ' falg to see whether this individual has been evaluated by ansys yet
    Public Corporal As Boolean ' flag to see wether this individual has a corporal form yet, a phenotype if you will
    Private Genome As Spline()
    Private Name As Integer ' unique ID

    'constructor
    Public Sub New()

    End Sub

    'constructor full
    Public Sub New(ByVal f As Double, ByVal n As Integer, ByVal g As Spline())
        Fitness = f
        Name = n
        Genome = g
        Evaluated = False
        Corporal = False
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

    Public Sub sEvaluated(b As Boolean)
        Evaluated = b
    End Sub

    Public Function gEvaluated()
        Return Evaluated
    End Function

End Class
