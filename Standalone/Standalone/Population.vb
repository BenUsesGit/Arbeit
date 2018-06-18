Imports System.Text.RegularExpressions, Splines.SplineHandler, System.IO
''' <summary>
''' Class holds all individuals of type <see cref="Individuum"/> for an evolution.
''' </summary>
Public Class Population

    Private Elders As Individuum() = {} ' parent generation
    Private Youngsters As Individuum() = {} ' child generation
    Private NextGen As Individuum() = {} ' storage generation during evolutionary cycle
    Private EldersHome As String ' path to parent folder
    Private YoungstersHome As String ' path to child folder
    Private NextGenHome As String
    Private GenCount As Integer = 1 ' permanently counts the number of generations respectively cycles created for a population
    Private IndiCount As Integer = 0 ' permanently counts the number of individuals created for a population
    Private handler As New SplineHandler
    Private cloner As New Clone ' this object is needed to truly clone non-primitve datatypes
    Private Best As New Individuum

    'Private name As String

    'constructor
    Public Sub New()

    End Sub

    'constructor with given group of individuals, and a given location
    Public Sub New(ByVal g As Individuum(), ByVal p As String)
        sElders(g)
        sEldersHome(p)
    End Sub

    'Elders
    '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Sub sElders(ByVal g As Individuum())
        Dim i As Integer
        Elders = {}
        ReDim Elders(g.Length - 1)
        For i = 0 To g.Length - 1
            Elders(i) = g(i)
        Next
    End Sub

    Public Function gElders() As Individuum()
        Return Elders
    End Function

    ''' <param name="p">Path to the elders folder</param>
    Public Sub sEldersHome(ByVal p As String)
        Dim check As New Regex("\\Elders\\$")
        If check.IsMatch(p) Then
            EldersHome = p
        Else
            EldersHome = p & "\Elders\"
        End If
    End Sub

    Public Function gEldersHome() As String
        Return EldersHome
    End Function
    '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    'Youngsters
    '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Sub sYoungsters(ByVal g As Individuum())
        Dim i As Integer
        Youngsters = {}
        ReDim Youngsters(g.Length - 1)
        For i = 0 To g.Length - 1
            Youngsters(i) = g(i)
        Next
    End Sub

    Public Function gYoungsters() As Individuum()
        Return Youngsters
    End Function

    Public Sub sYoungstersHome(ByVal p As String)
        Dim check As New Regex("\\Youngsters\\$")
        If check.IsMatch(p) Then
            YoungstersHome = p
        Else
            YoungstersHome = p & "\Youngsters\"
        End If
    End Sub

    Public Function gYoungstersHome() As String
        Return YoungstersHome
    End Function
    '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    'NextGen
    '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Sub sNextGen(ByVal g As Individuum())
        Dim i As Integer
        NextGen = {}
        ReDim NextGen(g.Length - 1)
        For i = 0 To g.Length - 1
            NextGen(i) = g(i)
        Next
    End Sub

    Public Function gNextGen() As Individuum()
        Return NextGen
    End Function

    Public Sub sNextGenHome(ByVal p As String)
        Dim check As New Regex("\\NewGeneration\\$")
        If check.IsMatch(p) Then
            NextGenHome = p
        Else
            NextGenHome = p & "\NewGeneration\"
        End If
    End Sub

    Public Function gNextGenHome() As String
        Return NextGenHome
    End Function
    '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Searches the best individual of the current generation and stores it.
    ''' </summary>
    Public Sub sBest()
        Dim Err As Boolean
        If Elders.Length = 0 Then
            Debug.Print("Keine Zuweisung möglich, da Elterngeneration leer ist. Schaue in Kindgeneration...")
            Err = True
        Else
            Best.sFitness(Elders(0).gFitness)
            For Each member In Elders
                If Best.gFitness <= member.gFitness Then
                    Best = cloner.CloneDeep(member)
                End If
            Next
        End If

        If Youngsters.Length = 0 And Err Then
            Debug.Print("Keine Zuweisung möglich, da beide Generationen leer sind")
        Else
            For Each member In Youngsters
                If Best.gFitness <= member.gFitness Then
                    Best = cloner.CloneDeep(member)
                End If
            Next
        End If
    End Sub

    Public Function gBest() As Individuum
        Return Best
    End Function

    ''' <summary>
    ''' Adds an individual to the population.
    ''' </summary>
    ''' <param name="indi"><see cref="Individuum"/></param>
    ''' <param name="target">1,2,3 for Elders, Youngsters, nextGen</param>
    Public Sub AddIndi(ByVal indi As Individuum, ByVal target As Integer)
        ' TODO write a proper SplineWriteALL-Sub
        Select Case target
            ' target --> Elders
            Case 1
                indi.sName(IndiCount)
                ReDim Preserve Elders(Elders.Length)
                Elders(Elders.Length - 1) = indi
                IndiCount = IndiCount + 1
                WriteIndi(indi, target)

            ' target --> Youngsters
            Case 2
                indi.sName(IndiCount)
                ReDim Preserve Youngsters(Youngsters.Length)
                Youngsters(Youngsters.Length - 1) = indi
                IndiCount = IndiCount + 1
                WriteIndi(indi, target)

            ' target --> NextGen
            Case 3
                'indi.sName(IndiCount)
                'IndiCount = IndiCount + 1
                ReDim Preserve NextGen(NextGen.Length)
                NextGen(NextGen.Length - 1) = indi
                WriteIndi(indi, target)
        End Select
    End Sub

    ''' <summary>
    ''' increases generation count by 1
    ''' </summary>
    Public Sub incGenCount()
        GenCount += 1
    End Sub

    Public Function gGenCount() As Integer
        Return GenCount
    End Function

    Public Function gIndiCount() As Integer
        Return IndiCount
    End Function

    ''' <summary>
    ''' Writes the array of <see cref="Spline"/>s which build the genome of the individual into the corresponding a txt file
    ''' </summary>
    ''' <param name="indi"><see cref="Individuum"/></param>
    ''' <param name="target">1,2,3 for Elders, Youngsters, nextGen</param>
    Public Sub WriteIndi(ByVal indi As Individuum, ByVal target As Integer)
        Dim s As String
        Dim fs As FileStream
        Dim sw As StreamWriter

        Select Case target
            ' target --> Elders
            Case 1
                s = EldersHome & indi.gName & ".txt"
                fs = New FileStream(s, FileMode.OpenOrCreate)
                sw = New StreamWriter(fs)
                sw.Write("Fitness:" & vbTab & indi.gFitness & vbCrLf & "Genome" & vbCrLf)
                sw.Close()
                fs.Dispose()


                handler.WriteSpline(indi.gGenome, s)

            ' target --> Youngsters
            Case 2
                s = YoungstersHome & indi.gName & ".txt"
                fs = New FileStream(s, FileMode.OpenOrCreate)
                sw = New StreamWriter(fs)
                sw.Write("Fitness:" & vbTab & indi.gFitness & vbCrLf & "Genome" & vbCrLf)
                sw.Close()
                fs.Dispose()

                handler.WriteSpline(indi.gGenome, s)

            ' target --> nextGen
            Case 3
                s = NextGenHome & indi.gName & ".txt"
                fs = New FileStream(s, FileMode.OpenOrCreate)
                sw = New StreamWriter(fs)
                sw.Write("Fitness:" & vbTab & indi.gFitness & vbCrLf & "Genome" & vbCrLf)
                sw.Close()
                fs.Dispose()

                handler.WriteSpline(indi.gGenome, s)
        End Select
    End Sub

End Class
