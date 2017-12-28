Imports System.Text.RegularExpressions, Splines.SplineHandler, System.IO
Public Class Population

    Private Elders As Individuum() = {}
    Private Youngsters As Individuum() = {}
    Private NextGen As Individuum() = {}
    Private EldersHome As String
    Private YoungstersHome As String
    Private NextGenHome As String
    Private Best As Double
    Private GenCount As Integer = 1
    Private IndiCount As Integer = 0
    Private handler As New SplineHandler
    Private cloner As New Clone

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

    Public Function gElders()
        Return Elders
    End Function

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

    Public Function gYoungsters()
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

    Public Function gNextGen()
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

    ' Searches the best individual of the current generation
    Public Sub sBest()
        Best = Elders(0).gFitness

        If Elders.Length = 0 Then
        Else
            For Each member In Elders
                Best = Math.Max(Best, member.gFitness)
            Next

        End If

        ' first generation catch
        If Youngsters.Length = 0 Then
        Else
            For Each member In Youngsters
                Best = Math.Max(Best, member.gFitness)
            Next
        End If
    End Sub

    Public Function gBest()
        Return Best
    End Function

    ' Adds an individual to the population. the count for number of individuals is increased, target determines to which generation the new individual belongs
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

    Public Sub incGenCount()
        GenCount = GenCount + 1
    End Sub

    Public Function gGenCount()
        Return GenCount
    End Function

    Public Function gIndiCount()
        Return IndiCount
    End Function

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
