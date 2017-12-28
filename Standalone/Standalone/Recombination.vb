Imports System.Math

Public Class Recombination
    Dim cloner As New Clone
    Dim fit As New Fitness

    ' Simulates a simple mating process by choosing mating partners randomly by choosing the max values of each control point, input is the parent group, output is the childrengroup
    Public Sub SimpleMate(ByVal parents As Individuum(), ByVal Pop As Population)
        Randomize()
        Dim mem As Individuum() = {}
        Dim i, first, second As Integer

        For i = 0 To parents.Length - 1
            first = 0
            second = 0
            ' Prevent that the mating partners are the same
            Do
                first = Ceiling(Rnd() * parents.Length - 1)
                second = Ceiling(Rnd() * parents.Length - 1)
            Loop Until first <> second

            Dim firstSp As Spline() = parents(first).gGenome
            Dim secondSp As Spline() = parents(second).gGenome
            Dim childSp As Spline() = {}

            ' TODO check whether both genomes have the same number of splines and if both splines have the same number of points
            For Each member In firstSp
                ReDim Preserve childSp(childSp.Length)
                childSp(childSp.Length - 1) = cloner.CloneDeep(firstSp(childSp.Length - 1))
                'Debug.Print(childSp(childSp.Length - 1).gPoint(3).gX & " " & childSp(childSp.Length - 1).gPoint(3).gY)
                Dim firstPts As Point() = firstSp(childSp.Length - 1).gPoints
                Dim secondPts As Point() = secondSp(childSp.Length - 1).gPoints
                Dim childPts As Point() = {}

                For Each component In firstPts
                    ReDim Preserve childPts(childPts.Length)
                    Dim j As Integer = childPts.Length - 1
                    childPts(j) = New Point(Max(firstPts(j).gX, secondPts(j).gX), Max(firstPts(j).gY, secondPts(j).gY))
                Next

                childSp(childSp.Length - 1).sPoints(childPts)
            Next

            ReDim Preserve mem(mem.Length)
            Dim child As Individuum = cloner.CloneDeep(parents(first))
            child.sGenome(childSp)
            child.sFitness(fit.EvalFitness(child))
            mem(i) = child
        Next
        Pop.sNextGen(mem)
    End Sub

End Class
