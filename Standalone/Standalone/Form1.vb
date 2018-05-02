Public Class Form1
    Dim testEvo As Evo

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        NeuAusNix("D:\Skripte\VBA\Splines\Test")
        'i = 0
        'While i < 3
        '    testEvo.Cycle()
        '    i = i + 1
        'End While

        'While testEvo.champ > 0.1
        '    testEvo.Cycle()
        'End While

        'Dim fs As FileStream
        'Dim sw As StreamWriter
        'Dim mute As New Mutation

        'fs = New FileStream(p & "\Gauss.txt", FileMode.OpenOrCreate)
        'sw = New StreamWriter(fs)
        'i = 0
        'Dim t As Double = 3
        'While i < 1000
        '    sw.Write(mute.Gauss(t) & vbCrLf)
        '    i = i + 1
        'End While

        'sw.Close()
        'fs.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        testEvo.Cycle()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        testEvo.CreateIndi()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If testEvo IsNot Nothing Then
            Dim s As String
            testEvo.clearPop()
            s = testEvo.gLivingSpace
            testEvo = Nothing
            Debug.Print("Evolutionsdatei aus " & s & " gelöscht.")
        Else
            Debug.Print("Keine Evolutionsdatei dieses Namens vorhanden")
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' TODO change Sub that the velue from the textbox is used 
        NeuAusNix("D:\Skripte\VBA\Splines\Test")

        ' TODO make the Sub robust against faulty entries e.g. texts 
        Dim f As Double
        Dim mi, mg As Integer

        If ZielFitness.Text = "" Then
            f = 1000000
        Else
            f = ZielFitness.Text
        End If

        If MaxIndi.Text = "" Then
            mi = 100000000
        Else
            mi = MaxIndi.Text
        End If

        If MaxGen.Text = "" Then
            mg = 100000
        Else
            mg = MaxGen.Text
        End If

        If Not f = 1000000 And mi = 100000000 And mg = 0 Then
            Do Until testEvo.PopInfo(0) >= mi Or testEvo.PopInfo(1) >= mg Or testEvo.PopInfo(2) >= f
                testEvo.Cycle()
            Loop
        End If


    End Sub

    Public Sub NeuAusNix(ByVal s As String)
        Dim dgr As Integer = 4
        Dim xwerte As Double() = {0, 1, 2, 3, 4, 5}
        Dim ywerte As Double() = {0, 1, 2, 3, 4, 5}
        Dim kwerte As Double() = {0, 0, 0, 0, 1 / 3, 2 / 3, 1, 1, 1, 1}
        Dim points As Point()

        Dim i As Integer
        ReDim points(xwerte.Length - 1)
        For i = 0 To xwerte.Length - 1
            points(i) = New Point(xwerte(i), ywerte(i))
        Next

        Dim spline As New Spline(dgr, kwerte, points)
        spline.sID(0)

        Dim p As String = s
        Dim p1 As String = s & "\Hallo.txt"

        Dim handler As New SplineHandler
        Dim testIndi As New Individuum()
        testIndi.sGenome(handler.ReadAllSplines(p1))

        testEvo = New Evo(p, "Ichbinhier", testIndi, 20)

        TextBox1.Clear()
        TextBox1.AppendText(testEvo.gLivingSpace)
    End Sub

End Class
