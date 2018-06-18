Imports System.IO, System.Text.RegularExpressions, System.Math, System.Object
''' <summary>
''' 'god-class'. Class manages the whole evelution-process. 
''' </summary>
Public Class Evo

    Private Pop As New Population
    Private livingSpace As String 'Space to store the population
    Private Curpath As String
    Private name As String
    Private cloner As New Clone
    Private ans As New AnsysControl.Ansys
    Private mute As New Mutation
    Private reco As New Recombination
    Private sel As New Selection

    WithEvents fit As New Fitness(ans)

    Public Event CycleComplete As EventHandler(Of eIndi)

    ' TODO OPTIONAL recreate whole project in a way, that all objects are saved and managed in xml format

    'constructor
    Public Sub New(AnsysInstance As AnsysControl.Ansys)
        ' Start of needed programms
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        ans = AnsysInstance
        fit.sAns(AnsysInstance)
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    End Sub

    ''' <param name="p">Path to directory for evolution to take place</param>
    ''' <param name="n">Name of the folder to be created</param>
    ''' <param name="start">Individuum to derive a startpopulaiton from</param>
    ''' <param name="size">Number of individuals for start population</param>
    Public Sub NewEvo(ByVal p As String, ByVal n As String, ByVal start As Individuum, ByVal size As Integer)

        ' Start of needed programms
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        'ans = New AnsysControl.Ansys
        'fit = f
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        ' population Stuff
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        name = n
        livingSpace = p & "\" & name & "\"
        If Directory.Exists(livingSpace) Then
            'Directory.SetCurrentDirectory(livingSpace)
        Else
            Directory.CreateDirectory(livingSpace)
            'Directory.SetCurrentDirectory(livingSpace)
        End If

        FileNew(livingSpace)

        'For Each member As Individuum In CreateStartPop(start, size)
        '    fit.AddToBatch(member)
        '    'member.sFitness(fit.EvalFitness(member))
        'Next

        fit.EvalArr(CreateStartPop(start, size), True)

        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    End Sub


    'Public Sub NewEvo(ByVal p As String, ByVal n As String)
    '    name = n
    '    livingSpace = p & "\" & name

    '    If Directory.Exists(p & "\" & name) Then
    '        livingSpace = p
    '        'Directory.SetCurrentDirectory(livingSpace)
    '        Debug.Print("Population gefunden")
    '    Else
    '        ' TODO setup proper error handling
    '    End If
    'End Sub

    ''' <overloads>In case a starting population is already at hand.</overloads>
    '''<param name="n">Name of evolution folder</param>
    '''<param name="p">Path to evolution folder</param>
    Public Sub New(ByVal p As String, ByVal n As String, ByVal start As Population)
        name = n
        livingSpace = p & "\" & name

        FileNew(livingSpace)

    End Sub

    ''' <summary>
    ''' set <see cref="AnsysControl.Ansys"/>-property
    ''' </summary>
    ''' <param name="a"></param>
    Public Sub sAns(a As AnsysControl.Ansys)
        ans = a
    End Sub

    ''' <summary>
    ''' get <see cref="AnsysControl.Ansys"/>-property
    ''' </summary>
    ''' <returns></returns>
    Public Function gAns()
        Return ans
    End Function

    ''' <summary>
    ''' Updates population info, by writing its properties into txt file
    ''' </summary>
    Public Sub UpdatePop()
        ' Updating the popinfo.txt - file

        Pop.sBest()

        Debug.Print(vbCrLf & "Bester aus Generation " & Pop.gGenCount & ":" & vbTab & Pop.gBest.gfitness & vbCrLf)

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

    ''' <summary>
    ''' Sets living space property
    ''' </summary>
    ''' <param name="s">path to evolution folder</param>
    Public Sub sLivingSpace(ByVal s As String)
        ' TODO make this sub robust against invalid strings
        livingSpace = s
    End Sub

    ' gets the living space
    Public Function gLivingSpace()
        Return livingSpace
    End Function

    ''' <summary>
    ''' Simulates one evolutionary cycle.
    ''' </summary>
    Public Sub Cycle()
        ' TODO several parameters are possible: True or False for different aspects of the evolutionary cycle to toggle on and of (mutation, recombination, selection etc.)
        ' also there could be parameters passed that indicate, which of the aspects to use, if there are more than one
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

    ''' <summary>
    ''' Simulates one evolutionary cycle. Is triggered by <see cref="Fitness.EvalReady"/>
    ''' </summary>
    ''' <param name="e"><see cref="eIndi"/></param>
    Public Sub Cycle1(sender As Object, e As eIndi) Handles fit.EvalReady
        ' TODO several parameters are possible: True or False for different aspects of the evolutionary cycle to toggle on and of (mutation, recombination, selection etc.)
        ' also there could be parameters passed that indicate, which of the aspects to use, if there are more than one
        Select Case e.lstAction
            Case -10 ' initalization of population, no cycle, just add indis to pop
                For Each element In e.indis
                    Pop.AddIndi(element, 1)
                Next
                Exit Sub
            Case 0
                If AnyUnEval(e.lstAction) Then ' make sure prerequisites are met for best selection
                    sel.BestSelect(Pop, 5)
                    e.lstAction += 10
                    Cycle1(Me, e)
                End If
            Case 5
                sel.BestSelect(Pop, 5)
                e.lstAction += 5
                Cycle1(Me, e)
            Case 10
                If AnyUnEval(e.lstAction) Then ' make sure prerequisites are met for simple mate
                    reco.SimpleMate(Pop.gNextGen, Pop)
                    e.lstAction += 10
                    Cycle1(Me, e)
                End If
            Case 15
                reco.SimpleMate(Pop.gNextGen, Pop)
                e.lstAction += 5
                Cycle1(Me, e)
            Case 20
                If AnyUnEval(e.lstAction) Then ' make sure prerequisites are met for simple mut
                    mute.SimpleMut(Pop.gNextGen)
                    e.lstAction += 10
                    Cycle1(Me, e)
                End If
            Case 25
                mute.SimpleMut(Pop.gNextGen)
                e.lstAction += 5
                Cycle1(Me, e)
            Case 30
                If AnyUnEval(e.lstAction) Then ' make sure prerequisites are met for fitness proportional selection
                    sel.FitPropSel(Pop, 5)
                    e.lstAction += 10
                    Cycle1(Me, e)
                End If
            Case 35
                sel.FitPropSel(Pop, 5)
                e.lstAction += 5
                Cycle1(Me, e)
            Case 40
                'do nothing ?

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

                ' write contents of the new built array into folders
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

                RaiseEvent CycleComplete(Me, e)
        End Select
    End Sub

    Public Sub CycleUI()
        Dim cycleArr As New eIndi(Pop.gElders, 0)
        Cycle1(Me, cycleArr)
    End Sub

    ''' <summary>
    ''' checks if all individuals are evaluated, if not, all unevaluated individuals are passed for evaluation
    ''' </summary>
    ''' <param name="i"></param>
    ''' <returns>True if any individual is not evaluated yet</returns> 
    Private Function AnyUnEval(i As Integer)
        Dim flag As Boolean = True
        Dim unevalArr As Individuum() = {}

        For Each element In Pop.gElders
            If Not element.gEvaluated Then
                ReDim Preserve unevalArr(unevalArr.Length)
                unevalArr(unevalArr.Length - 1) = element
                flag = False
            End If
        Next

        For Each element In Pop.gYoungsters
            If Not element.gevaluated Then
                ReDim Preserve unevalArr(unevalArr.Length)
                unevalArr(unevalArr.Length - 1) = element
                flag = False
            End If
        Next

        For Each element In Pop.gNextGen
            If Not element.gevaluated Then
                ReDim Preserve unevalArr(unevalArr.Length)
                unevalArr(unevalArr.Length - 1) = element
                flag = False
            End If
        Next

        If Not flag Then
            fit.EvalArr(unevalArr, False) ' passing the unevaluated individuals to fitness
        End If
        Return flag
    End Function

    'Public Function PopInfo()
    '    Dim arr As Object() = {}
    '    ReDim arr(2)

    '    arr(0) = Pop.gIndiCount
    '    arr(1) = Pop.gGenCount
    '    arr(2) = Pop.gBest

    '    Return arr
    'End Function

    ''' <summary>
    ''' Creates a new individual with random values determined by a given individual.
    ''' </summary>
    ''' <param name="indi"><see cref="Individuum"/></param>
    ''' <remarks> The new individual is added to the current generation</remarks>
    Public Sub CreateIndi(ByVal indi As Individuum)
        Dim newIndi As Individuum
        newIndi = cloner.CloneDeep(indi)
        mute.SimpleMut(newIndi)
        If Pop.gYoungsters.Length = 0 Then
            Pop.AddIndi(newIndi, 1)
        Else
            Pop.AddIndi(cloner.CloneDeep(newIndi), 2)
        End If
    End Sub

    ''' <overloads>
    ''' 
    ''' </overloads>
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

    ''' <summary>
    ''' clears all generations, population data and files
    ''' </summary>
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

    ''' <summary>
    ''' Creation of generation folders assumes the current directory is already the right one
    ''' </summary>
    ''' <param name="p">Path to the evolution folder</param>
    Private Sub FileNew(ByVal p As String)

        Pop.sEldersHome(p)
        Directory.CreateDirectory(Pop.gEldersHome)
        Pop.sYoungstersHome(p)
        Directory.CreateDirectory(Pop.gYoungstersHome)
        Pop.sNextGenHome(p)
        Directory.CreateDirectory(Pop.gNextGenHome)
        ans.slogfile(livingSpace)

        Dim fs As FileStream = File.Create(p & "\PopInfo.txt")
        fs.Close()
        fs.Dispose()
    End Sub

    ' https:// dotnet-snippets.de/snippet/pruefen-ob-datei-gerade-benutzt-wird/71
    'Public Function FileInUse(ByVal sFile As String) As Boolean
    '    If System.IO.File.Exists(sFile) Then
    '        Try
    '            Dim F As Short = FreeFile()
    '            FileOpen(F, sFile, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
    '            FileClose(F)
    '        Catch
    '            Return True
    '        End Try
    '    End If
    'End Function

    ''' <summary>
    ''' Creates a startpopulation with a given start individual
    ''' </summary>
    ''' <param name="startI">Startindividual to derive from</param>
    ''' <param name="size">Number of individuals</param>
    ''' <returns></returns>
    Private Function CreateStartPop(ByVal startI As Individuum, ByVal size As Integer)
        Dim group As Individuum()
        ReDim group(size - 1)

        Dim i As Integer
        For i = 0 To group.Length - 1
            group(i) = New Individuum()
            group(i) = mute.SimpleMut(cloner.CloneDeep(startI))
            group(i).sName(i)
        Next

        Return group
    End Function

    ' adding individuals to population after evaluation is finished
    'Public Sub IndiToPop(sender As Object, e As eIndi) Handles fit.EvalReady
    '    For Each element In e.indis
    '        Pop.AddIndi(element, 1)
    '    Next
    'End Sub

End Class
