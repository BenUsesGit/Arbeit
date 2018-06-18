Imports System.Math
''' <summary>
''' Holds different methods for mutationg the genome of an individual
''' ''' </summary>
Public Class Mutation
    'Dim ans As AnsysControl.Ansys

    'Dim fit As Fitness

    ' WithEvents f As New Fitness(ans)

    Public Sub New()
        'ans = a
        ' fit = f
    End Sub

    ''' <summary>
    ''' Applies a very simple random generator on all control points of splines of given individual
    ''' </summary>
    ''' <param name="indi"></param>
    ''' <returns></returns>
    Public Function SimpleMut(ByVal indi As Individuum)
        Dim s As Spline() = indi.gGenome

        '' DEBUG------------------------------------------------------------------------------------
        'Dim text As String
        'text = indi.gName & vbTab & indi.gFitness & vbTab & vbTab & "=>" & vbTab
        '' -----------------------------------------------------------------------------------------
        For Each member As Spline In s
            Dim pts As Point() = member.gPoints()

            For Each element As Point In member.gPoints

                element.sX(Gauss(element.gX))
                element.sY(Gauss(element.gY))
                element.sZ(Gauss(element.gZ))
            Next

        Next

        indi.sGenome(s)
        'indi.sFitness(fit.EvalFitness(indi))
        indi.sEvaluated(False)
        'text = text & indi.gFitness
        '' DEBUG------------------------------------------------------------------------------------
        'Debug.Print(text & vbCrLf)
        '' -----------------------------------------------------------------------------------------
        Return indi
    End Function

    ''' <overloads>calls <see cref="SimpleMut(Individuum)"/> for each item in an array of <see cref="Individuum"/>s</overloads>
    Public Function SimpleMut(ByVal arr As Individuum())
        If arr.Length = 0 Then
        Else
            'Debug.Print("Gaussche Mutation um Stützpunkte:")
            'Debug.Print("Mutiere Genom..." & vbCrLf &
            '            "Fitnesswert" & vbCrLf &
            '            "vor der Mutation" & vbTab & vbTab & "nach der Mutation" & vbCrLf)
            For Each member In arr
                SimpleMut(member)
            Next
        End If
        Return 0
    End Function

    ''' <summary>
    ''' Generates random numbers with gaussian distribution arround a given value
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    Private Function Gauss(ByVal x As Double)
        Randomize()
        Dim u1, u2, q, p As Double

        Do
            u1 = 2 * Rnd() - 1
            u2 = 2 * Rnd() - 1

            q = u1 * u1 + u2 * u2

        Loop Until q <> 0 And q < 1

        p = Sqrt(-2 * Math.Log(q) / q)

        ' quotient of 5 is deliberately chosen to slim the bell function
        If x = 0 Then
            x = Round((u1 * p) / 5, 5)
        Else
            x = Round((x + x * (u1 * p) / 5), 5)
        End If

        Return x
    End Function

End Class
