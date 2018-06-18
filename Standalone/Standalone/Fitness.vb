Imports AnsysControl
''' <summary>
''' Evaluates the fitness value of an individual
''' </summary>
Public Class Fitness

    ' TODO implement subs for cahngeing and managing the list of wanted results

    Private iArr As Individuum() ' array that holds all individuals that are to be evaluated
    Private iArrIndex As Integer = 0
    Private tarMass As Double ' target value for the mass of individual
    Private dStart As Double ' start value for deformation of the individual
    Private first As Boolean = True ' flag to determine whether start mass and dstart have to be set
    Private currStep As Integer ' value for current step in the evaluation of an individual

    Public Event EvalReady As EventHandler(Of EventArgs)

    WithEvents ans As New Ansys

    Public Sub New(a As Ansys)
        ans = a
    End Sub

    '' Evaluates the fitness on an individual
    'Public Function EvalFitness(ByVal indi As Individuum)
    '    Dim f As Double = 0
    '    'For Each member In indi.gGenome(0).gPoints
    '    '    f = f + member.gX
    '    'Next

    '    'For Each member In indi.gGenome
    '    '    Dim xs As Double = 0
    '    '    Dim ys As Double = 0
    '    '    Dim zs As Double = 0
    '    '    For Each element In member.gPoints
    '    '        zs = zs + element.gZ
    '    '        ys = ys + element.gY
    '    '        xs = xs + element.gX
    '    '    Next
    '    '    xs = xs / member.gNumPoints
    '    '    ys = ys / member.gNumPoints
    '    '    zs = zs / member.gNumPoints
    '    '    f = f + ((xs + ys + zs) / 3)
    '    'Next

    '    'f = indi.gGenome.Length / f

    '    ' ganz einfach: je näher der erste punkt des ersten splines des individuums an 5 ist, desto besser der fitnesswert, ist der wert negativ wird die fitness null
    '    If indi.gGenome(0).gPoint(0).gX < 0 Then
    '        f = 0
    '    Else
    '        f = indi.gGenome(0).gPoint(0).gX / 5
    '    End If

    '    Return f
    'End Function

    ''' <summary>
    ''' Evaluates the fitness of an individual
    ''' </summary>
    ''' <param name="indi"><see cref="Individuum"/></param>
    ''' <param name="e"><see cref="eString"/></param>
    ''' <returns>Return value is between 0 and 1</returns>
    Public Function EvalAns(ByVal indi As Individuum, e As eString) As Double
        Dim f As Double
        Dim res As Double = 0

        ' TODO write comprehensive mapping of resultposition and meaning of result

        Dim mass As Double = e.GetRes(e.GetRes().length - 1)

        ' if it is the first indi that is evaluated, set target mass
        If first Then
            tarMass = 0.5 * mass
            dStart = e.GetRes(1)
            first = False

            Debug.Print("First element in Evo! Target mass : " & tarMass & " start deformation : " & dStart)
        End If

        Dim deform As Double = e.GetRes(1)

        ' f gets better when the mass is near the targeted mass reduction, if deformation is less, f also gets better
        f = (tarMass / mass) * (1 - (deform / (2 * dStart)))

        indi.sEvaluated(True) ' set the flag as "evaluated" for for individual with passed individual ID 
        Return f
    End Function

    '' Sub adds an individual to a batch of individuals to be evaluated
    'Public Sub AddToBatch(indi As Individuum)
    '    ReDim Preserve iArr(iArr.Length)
    '    iArr(iArr.Length - 1) = indi
    'End Sub

    '' Sub adds an array of individuals to a batch of individuals to be evaluated
    'Public Sub SetNewBatch(Arr As Individuum())
    '    For Each element In Arr
    '        ReDim Preserve iArr(iArr.Length)
    '        iArr(iArr.Length - 1) = element
    '    Next
    'End Sub

    ' resets the internal array of individuals
    Public Sub Reset()
        ReDim iArr(0)
        iArrIndex = -1
    End Sub


    ''' <summary>
    ''' Method is triggered by <see cref="Ansys.EvalRdy"/> and raises itself the <see cref="EvalReady"/>-event to signal the <see cref="ansys"/>-object to start 
    ''' with the evaluation of the next individual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"><see cref="eString"/></param>
    Private Sub EvalArr(sender As Object, e As eString) Handles ans.EvalRdy
        Dim found As Boolean = False
        'If iArr.Length = 1 And iArrIndex = -1 Then
        'searches through the given array, if element is found start evaluation of next element in array
        For Each element In iArr
            If element.gName = e.ident Then
                element.sFitness(EvalAns(element, e))
                found = True
                If Array.IndexOf(iArr, element) < iArr.Length - 1 Then
                    ans.Begin(iArr(Array.IndexOf(iArr, element) + 1).gName)
                Else
                    Dim es As New eIndi(iArr, currStep + 10)
                    Reset()
                    RaiseEvent EvalReady(Me, es)
                End If
                Exit For
            Else
                found = False
            End If
        Next

        If Not found Then
            Debug.Print("Individuum nicht in der Liste gefunden!")
        End If
        'End If
    End Sub

    ''' <summary>
    ''' Order for ansys module to evaluate an array of individuals, ini flag says whether it is the first group of a population or not.
    ''' </summary>
    ''' <param name="Arr"></param>
    ''' <param name="ini"></param>
    Public Sub EvalArr(Arr As Individuum(), ini As Boolean)
        ReDim Preserve iArr(Arr.Length - 1) ' new initalization of iArr
        iArr = Arr
        iArrIndex = iArr(0).gName
        If ini Then
            currStep = -20
        Else
            currStep = -5
        End If
        ans.TtoA("listOfResults = [""Total Deformation""]")
        ans.AnsEnter()

        ans.Begin(iArr(iArrIndex).gName) 'this starts the back and forth process between ansys and fitness objects
    End Sub

    ' initial order for ansys module to evaluate an array of individuals
    'Public Sub EvalArr(i As Integer)
    '    currStep = i
    '    ans.TtoA("listOfResults = [""Total Deformation""]")
    '    ans.AnsEnter()
    '    If iArr.Length <> 0 Then
    '        ans.Begin(iArr(0).gName)
    '    End If
    'End Sub

    Public Sub sAns(a As Ansys)
        ans = a
    End Sub

    Public Function gAns() As Ansys
        Return ans
    End Function
End Class
