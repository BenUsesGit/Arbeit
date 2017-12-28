Imports System.IO, System.Text.RegularExpressions, System.Math, System.Object

Public Class Evo

    Private Pop As New Population
    Private livingSpace As String 'Space to store the population
    Private Curpath As String
    Private name As String
    Private cloner As New Clone
    Private mute As New Mutation
    Private reco As New Recombination
    Private fit As New Fitness
    Private sel As New Selection

    ' TODO OPTIONAL recreate whole project in a way, that all objects are saved and managed in a xml format

    'constructor
    Public Sub New()

    End Sub

    'location for the evolution with a name to take place, an individuum to get a starting population from and how many induviduals shall be created
    Public Sub New(ByVal p As String, ByVal n As String, ByVal start As Individuum, ByVal size As Integer)
        name = n
        livingSpace = p & "\" & name
        If Directory.Exists(livingSpace) Then
            'Directory.SetCurrentDirectory(livingSpace)
        Else
            Directory.CreateDirectory(livingSpace)
            'Directory.SetCurrentDirectory(livingSpace)
        End If

        FileNew(livingSpace)

        For Each member As Individuum In CreateStartPop(start, size)
            member.sFitness(fit.EvalFitness(member))
            Pop.AddIndi(member, 1)
        Next

        UpdatePop()

    End Sub

    'constructor in case a Population with given name at given location is already at hand
    Public Sub New(ByVal p As String, ByVal n As String)
        name = n
        livingSpace = p & "\" & name

        If Directory.Exists(p & "\" & name) Then
            livingSpace = p
            'Directory.SetCurrentDirectory(livingSpace)
            Debug.Print("Population gefunden")
        Else
            ' TODO setup proper error handling
        End If
    End Sub

    'location for the evolution to take place, a population to start with
    Public Sub New(ByVal p As String, ByVal n As String, ByVal start As Population)
        name = n
        livingSpace = p & "\" & name

        FileNew(livingSpace)

    End Sub

    'Updates population info, by writing its properties into txt file
    Public Sub UpdatePop()
        ' Updating the popinfo.txt - file

        Pop.sBest()

        Debug.Print(vbCrLf & "Bester aus Generation " & Pop.gGenCount & ":" & vbTab & Pop.gBest & vbCrLf)

        Dim fs As New FileStream(livingSpace & "\PopInfo.txt", FileMode.Create)
        Dim sw As New StreamWriter(fs)
        sw.Write("Beginn" & vbTab &
            "erzeugte Individuen:" & vbTab & Pop.gIndiCount & vbTab & "Generationen:" & vbTab & Pop.gGenCount & vbTab & "Ende")
        sw.Close()
        fs.Close()

        ' DEBUG -----------------------------------------------------------------------
        Debug.Print("Elders" & vbCrLf)
        For j = 0 To Pop.gElders.length - 1
            Debug.Print(Pop.gElders(j).gName & vbTab & Round(Pop.gElders(j).gFitness, 8))
        Next

        Debug.Print(vbCrLf)

        If Pop.gYoungsters.length = 0 Then
        Else
            Debug.Print("Youngsters" & vbCrLf)
            For j = 0 To Pop.gYoungsters.length - 1
                Debug.Print(Pop.gYoungsters(j).gName & vbTab & Round(Pop.gYoungsters(j).gFitness, 8) & vbTab)
            Next
        End If

        Debug.Print(vbCrLf)

        ' -----------------------------------------------------------------------------

    End Sub

    ' sets the living space
    Public Sub sLivingSpace(ByVal s As String)
        ' TODO make this sub robust against invalid strings
        livingSpace = s
    End Sub

    ' sets the living space
    Public Function gLivingSpace()
        Return livingSpace
    End Function

    ' simulates one evolutionary cycle, at this moment only with simple mating
    ' TODO several parameters are possible: True or False for different aspects of the evolutionary cycle to toggle on and of (mutation, recombination, selection etc.)
    ' also there could be parameters passed that indicate, which of the aspects to use, if there are more than one
    Public Sub Cycle()
        sel.BestSelect(Pop, 10)
        reco.SimpleMate(Pop.gNextGen, Pop)
        mute.SimpleMut(Pop.gNextGen)
        sel.FitPropSel(Pop, 10)

        ' building a new generation by shifting individuals from youngsters to elders and nextgen to youngsters
        If Pop.gYoungsters.Length = 0 Then
            If Pop.gNextGen.length = 0 Then
                Debug.Print("Keine Kindgenerationen vorhanden")
            Else
                Pop.sYoungsters({})
                For Each member In Pop.gNextGen
                    Pop.AddIndi(cloner.CloneDeep(member), 2)
                Next
            End If
        Else
            Pop.sElders(cloner.CloneDeep(Pop.gYoungsters))
            If Pop.gNextGen.Length = 0 Then
                Debug.Print("Keine Kindgenerationen vorhanden")
            Else
                Pop.sYoungsters({})
                For Each member In Pop.gNextGen
                    Pop.AddIndi(cloner.CloneDeep(member), 2)
                Next
            End If
        End If

        Pop.sNextGen({})

        ' write contents of the new build array into folders
        Directory.Delete(Pop.gEldersHome, True)
        Directory.CreateDirectory(Pop.gEldersHome)
        For Each member In Pop.gElders
            Pop.WriteIndi(member, 1)
        Next

        Directory.Delete(Pop.gYoungstersHome, True)
        Directory.CreateDirectory(Pop.gYoungstersHome)
        For Each member In Pop.gYoungsters
            Pop.WriteIndi(member, 2)
        Next

        Directory.Delete(Pop.gNextGenHome, True)
        Directory.CreateDirectory(Pop.gNextGenHome)

        Pop.incGenCount()

        UpdatePop()
    End Sub

    Public Function PopInfo()
        Dim arr As Object() = {}
        ReDim arr(2)

        arr(0) = Pop.gIndiCount
        arr(1) = Pop.gGenCount
        arr(2) = Pop.gBest

        Return arr
    End Function

    ' creates a new individual with random values determined by a given individual, the new individual is added to the actual generation
    Public Sub CreateIndi(ByVal indi As Individuum)
        Dim newIndi As Individuum
        newIndi = cloner.CloneDeep(indi)
        mute.SimpleMut(newIndi)
        If Pop.gYoungsters = 0 Then
            Pop.AddIndi(newIndi, 1)
        Else
            Pop.AddIndi(cloner.CloneDeep(newIndi), 2)
        End If
    End Sub

    ' creates a new individual with random values, base individual is a randomly chosen member of the actual generation
    Public Sub CreateIndi()
        Randomize()
        Dim newIndi As Individuum
        Dim r As Double = Rnd()
        Dim pos As Integer

        Debug.Print("Erschaffung eines neuen Individuums ...")

        ' first generation catch
        If Pop.gYoungsters.length = 0 Then
            ' robust programming in case there is no generation at all
            If Pop.gElders.length >= 1 Then
                pos = Round(r * (Pop.gElders.length - 1))
                newIndi = cloner.CloneDeep(Pop.gElders(pos))
                mute.SimpleMut(newIndi)
                Debug.Print("Basisindividuum: " & newIndi.gName)
                Pop.AddIndi(newIndi, 1)
                Debug.Print("Individuum wird der Elterngeneration hinzugefügt")
            Else
                ' TODO rewrite this part of sub in a way, that one can add a new individual to an empty population
                Debug.Print("Keine Generation zur auswahl eines Individuums vorhanden")
            End If

            ' normal way
        Else
            pos = Round(r * (Pop.gYoungsters.length - 1))
            newIndi = cloner.CloneDeep(Pop.gYoungsters(pos))
            mute.SimpleMut(newIndi)
            Debug.Print("Basisindividuum: " & newIndi.gName)
            Pop.AddIndi(newIndi, 2)
            Debug.Print("Individuum wird der Kindgeneration hinzugefügt")
        End If

        UpdatePop()

    End Sub

    ' clears all generations, population data and files
    Public Sub clearPop()

        ' TODO make Sub robust
        If Directory.Exists(livingSpace) Then
            Directory.SetCurrentDirectory(Directory.GetDirectoryRoot(livingSpace))
            Pop.sElders({})
            Pop.sYoungsters({})
            Pop.sNextGen({})
        Else
            Debug.Print("Datei nicht vorhanden")
        End If

        Directory.Delete(livingSpace, True)

    End Sub

    'creation of generation folders assumes the current directory is already the right one
    Private Sub FileNew(ByVal p As String)

        Pop.sEldersHome(p)
        Directory.CreateDirectory(Pop.gEldersHome)
        Pop.sYoungstersHome(p)
        Directory.CreateDirectory(Pop.gYoungstersHome)
        Pop.sNextGenHome(p)
        Directory.CreateDirectory(Pop.gNextGenHome)



        Dim fs As FileStream = File.Create(p & "\PopInfo.txt")
        fs.Close()

    End Sub

    ' https:// dotnet-snippets.de/snippet/pruefen-ob-datei-gerade-benutzt-wird/71
    Public Function FileInUse(ByVal sFile As String) As Boolean
        If System.IO.File.Exists(sFile) Then
            Try
                Dim F As Short = FreeFile()
                FileOpen(F, sFile, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FileClose(F)
            Catch
                Return True
            End Try
        End If
    End Function

    'creates a startpopulation with a given start individuum
    Private Function CreateStartPop(ByVal startI As Individuum, ByVal size As Integer)
        Dim group As Individuum()
        ReDim group(size - 1)

        Dim i As Integer
        For i = 0 To group.Length - 1
            group(i) = New Individuum()
            group(i) = mute.SimpleMut(cloner.CloneDeep(startI))
        Next

        Return group
    End Function

End Class
