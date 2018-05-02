Imports AnsysControl
Public Class Fitness

    Dim ac As Ansys
    Dim iArr As Integer()

    Public Sub New(a As Ansys)
        ac = a
    End Sub

    Public Sub SetNewBatch(Arr As Integer())

    End Sub

    ' Evaluates the fitness on an individual
    Public Function EvalFitness(ByVal indi As Individuum)
        Dim f As Double = 0
        'For Each member In indi.gGenome(0).gPoints
        '    f = f + member.gX
        'Next

        'For Each member In indi.gGenome
        '    Dim xs As Double = 0
        '    Dim ys As Double = 0
        '    Dim zs As Double = 0
        '    For Each element In member.gPoints
        '        zs = zs + element.gZ
        '        ys = ys + element.gY
        '        xs = xs + element.gX
        '    Next
        '    xs = xs / member.gNumPoints
        '    ys = ys / member.gNumPoints
        '    zs = zs / member.gNumPoints
        '    f = f + ((xs + ys + zs) / 3)
        'Next

        'f = indi.gGenome.Length / f

        ' ganz einfach: je näher der erste punkt des ersten splines des individuums an 5 ist, desto besser der fitnesswert, ist der wert negativ wird die fitness null
        If indi.gGenome(0).gPoint(0).gX < 0 Then
            f = 0
        Else
            f = indi.gGenome(0).gPoint(0).gX / 5
        End If

        Return f
    End Function


    ' Sends Commands to the Ansysmodule to retrieve results of a simulation
    Public Function EvalAns(ByVal indi As Individuum)
        Dim f As Double = 0


        Return f
    End Function



End Class
