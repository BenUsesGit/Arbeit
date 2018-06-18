Imports System.Math
''' <summary>
''' class holds different methods for selecting individuals.
''' </summary>
Public Class Selection
    Private cloner As New Clone
    Private sort As New Sort
    'Dim ans As AnsysControl.Ansys

    Public Sub New() 'a As AnsysControl.Ansys)
        'ans = a
    End Sub

    ''' <summary>
    ''' Selection of the best individuals, input is a population and a number of individuals to be chosen, the choice for a individual is made alternating from the best individuals of
    ''' the older and the younger generation
    ''' </summary>
    ''' <param name="Pop"></param>
    ''' <param name="n">Number of individuals to be chosen</param>
    Public Sub BestSelect(ByVal Pop As Population, ByVal n As Integer)
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim trig As Boolean = True

        Debug.Print("Bestenselektion mit " & n & " Individuen" & vbCrLf)

        ' preparing elders and youngsters for selection by sorting them according to their fitness value
        Pop.sElders(sort.QuickSort(Pop.gElders, False))

        Debug.Print("Sortiere vorhandene Individuen nach Fitness..." & vbCrLf)

        ' first generation catch
        If Pop.gYoungsters.Length = 0 Then
        Else
            Pop.sYoungsters(sort.QuickSort(Pop.gYoungsters, False))
        End If

        If n > Pop.gElders.length + Pop.gYoungsters.length Then
            Debug.Print("Die Anzahl der zu wählenden Individuen für die nächste Generation ist zu groß. Es werden maximal " & Pop.gElders.length + Pop.gYoungsters.length &
                " Individuen gewählt." & vbCrLf)
        End If

        ' the loop picks alternating the best individuals from elders and youngsters. loop breaks if no individual can be chosen or if a given value is reached
        While Pop.gNextGen.length < n And k + j < Pop.gElders.length + Pop.gYoungsters.length
            If trig Then
                If j <= Pop.gElders.length - 1 Then
                    Pop.AddIndi(cloner.CloneDeep(Pop.gElders(j)), 3)
                    trig = Not trig
                    j = j + 1
                Else
                    trig = Not trig
                End If
            Else
                If k <= Pop.gYoungsters.length - 1 Then
                    Pop.AddIndi(cloner.CloneDeep(Pop.gYoungsters(k)), 3)
                    trig = Not trig
                    k = k + 1
                Else
                    trig = Not trig
                End If
            End If
        End While

        Debug.Print(k + j & " Individuen für eine neue Generation selektiert..." & vbCrLf _
                    & j & " Individuen aus der Elterngeneration" & vbCrLf _
                    & k & " Individuen aus der Kindgeneration" & vbCrLf)
    End Sub

    ''' <summary>
    ''' selection of fittest n individuals proportional to ther fitness value
    ''' </summary>
    ''' <param name="Pop"></param>
    ''' <param name="n">number of <see cref="Individuum"/>s to be selected</param>
    Public Sub FitPropSel(ByVal Pop As Population, ByVal n As Integer)
        Dim all As Individuum()
        ReDim all(Pop.gNextGen.length - 1)
        Dim pie As Double() = {}
        Dim i = 0
        Dim fit As Double = 0
        Randomize()
        Dim u As Double

        ' filling in all individuals into one array
        'Do Until all.Length - i = 0
        '    If i < Pop.gElders.length Then
        '        all(i) = Pop.gElders(i)
        '    Else
        '        all(i) = Pop.gYoungsters(i - Pop.gElders.length)
        '    End If
        '    i = i + 1
        'Loop

        all = sort.QuickSort(Pop.gNextGen, False)
        Pop.sNextGen({})

        For Each elem In all
            fit = fit + elem.gFitness()
            ReDim Preserve pie(pie.Length)
            pie(pie.Length - 1) = fit
        Next

        u = Rnd() * (fit / n)
        Dim j As Integer = 0

        For i = 1 To n
            While pie(j) < u
                j = j + 1
            End While
            u = u + fit / n
            Pop.AddIndi(all(j), 3)
        Next
    End Sub

End Class
