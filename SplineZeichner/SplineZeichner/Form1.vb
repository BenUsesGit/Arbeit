Imports System
Imports System.Type
Imports System.Activator
Imports System.Runtime.InteropServices
Imports Inventor
Imports System.Math
Imports System.Threading.Tasks
Imports System.Timers
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.IO

Public Class Form1

    Dim thisApp As Inventor.Application
    Dim oTG As TransientGeometry
    Dim InvOpen As Boolean              'Global Check for Inventor to be open or not
    Dim InvProc As Process
    Dim MatApp As Object

    Public Event InvReady()

    Public Sub New()
        Try
            thisApp = Marshal.GetActiveObject("Inventor.Application")
            oTG = thisApp.TransientGeometry
            InvOpen = True
        Catch ex As Exception
            InvOpen = False
        End Try

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

    Public Sub ChangeInvOpen(wert As Boolean)
        InvOpen = wert
        If InvOpen = True Then
            RaiseEvent InvReady()
        End If
    End Sub

    Public Sub ChangeThisApp(App As Inventor.Application)
        thisApp = App
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs)
        thisApp.Quit()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox2.Checked = True Then
            SplineByPolygon()
        Else
            SplineByInterpolation()
        End If
    End Sub

    Public Sub SplineByPolygon()
        '   a reference to the part component definition.
        ' This assumes that a part document is active.
        Dim oCompDef As PartComponentDefinition
        oCompDef = thisApp.ActiveDocument.ComponentDefinition

        ' Set a reference to the transient geometry collection.

        ' Create the collection that will contain the fit points for the regular spline.
        Dim oFitPoints As ObjectCollection
        oFitPoints = thisApp.TransientObjects.CreateObjectCollection

        ' Define the points to fit the spline through. The
        ' points are at (0,0), (2,2), (4,0), (6,4).

        Dim oPoints(0 To 9) As Point2d
        oPoints(1) = oTG.CreatePoint2d(0, 10)
        oFitPoints.Add(oPoints(1))

        oPoints(2) = oTG.CreatePoint2d(10, 10)
        oFitPoints.Add(oPoints(2))

        oPoints(3) = oTG.CreatePoint2d(10, 0)
        oFitPoints.Add(oPoints(3))

        oPoints(4) = oTG.CreatePoint2d(10, -10)
        oFitPoints.Add(oPoints(4))

        oPoints(5) = oTG.CreatePoint2d(0, -10)
        oFitPoints.Add(oPoints(5))

        oPoints(6) = oTG.CreatePoint2d(-10, -10)
        oFitPoints.Add(oPoints(6))

        oPoints(7) = oTG.CreatePoint2d(-10, 0)
        oFitPoints.Add(oPoints(7))

        oPoints(8) = oTG.CreatePoint2d(-10, 10)
        oFitPoints.Add(oPoints(8))

        oPoints(9) = oTG.CreatePoint2d(oPoints(1).X, oPoints(1).Y)
        oFitPoints.Add(oPoints(9))

        ' Splines mit Punkten in einem zufälligen Bereich erstellen
        If CheckBox1.Checked Then
            Dim i As Int32
            Dim _point As Point2d
            For i = 1 To oFitPoints.Count
                _point = oFitPoints(i)
                _point = PointVar(_point)
            Next i

            ' Erster un letzter Punkt des Splines sollen gleich sein
            oFitPoints.Remove(oFitPoints.Count)
            _point = oTG.CreatePoint2d(oFitPoints(1).X, oFitPoints(1).Y)
            oFitPoints.Add(_point)
        End If

        ' Create a work plane parallel to X-Y plane.
        Dim oWorkPlane As WorkPlane
        oWorkPlane = oCompDef.WorkPlanes.AddByPlaneAndOffset(oCompDef.WorkPlanes(oCompDef.WorkPlanes.Count), 25)

        'Create the first sketch on the X-Y plane
        Dim oSketch1 As PlanarSketch
        oSketch1 = oCompDef.Sketches.Add(oWorkPlane)

        ' Create the spline.
        Dim oSpline As SketchControlPointSpline
        oSpline = oSketch1.SketchControlPointSplines.Add(oFitPoints)

        'Create the second sketch.
        Dim oSketch2 As PlanarSketch
        'oSketch2 = oCompDef.Sketches.Add(oWorkPlane)

        ' Set a reference to the geometry of the spline on the first sketch
        Dim oBSplineCurve2d As BSplineCurve2d
        oBSplineCurve2d = oSpline.Geometry

        ' Create a fixed spline on the second sketch based
        ' on the geometry of the spline on the first sketch.
        Dim oFixedSpline As SketchFixedSpline
        'oFixedSpline = oSketch2.SketchFixedSplines.Add(oBSplineCurve2d)

    End Sub

    Public Sub SplineByInterpolation()
        '   a reference to the part component definition.
        ' This assumes that a part document is active.
        Dim oCompDef As PartComponentDefinition
        oCompDef = thisApp.ActiveDocument.ComponentDefinition

        ' Set a reference to the transient geometry collection.

        ' Create the collection that will contain the fit points for the regular spline.
        Dim oFitPoints As ObjectCollection
        oFitPoints = thisApp.TransientObjects.CreateObjectCollection

        ' Define the points to fit the spline through. The
        ' points are at (0,0), (2,2), (4,0), (6,4).
        Dim oPoints(0 To 9) As Point2d
        oPoints(1) = oTG.CreatePoint2d(0, 10)
        oFitPoints.Add(oPoints(1))

        oPoints(2) = oTG.CreatePoint2d(10, 10)
        oFitPoints.Add(oPoints(2))

        oPoints(3) = oTG.CreatePoint2d(10, 0)
        oFitPoints.Add(oPoints(3))

        oPoints(4) = oTG.CreatePoint2d(10, -10)
        oFitPoints.Add(oPoints(4))

        oPoints(5) = oTG.CreatePoint2d(0, -10)
        oFitPoints.Add(oPoints(5))

        oPoints(6) = oTG.CreatePoint2d(-10, -10)
        oFitPoints.Add(oPoints(6))

        oPoints(7) = oTG.CreatePoint2d(-10, 0)
        oFitPoints.Add(oPoints(7))

        oPoints(8) = oTG.CreatePoint2d(-10, 10)
        oFitPoints.Add(oPoints(8))

        oPoints(9) = oTG.CreatePoint2d(oPoints(1).X, oPoints(1).Y)
        oFitPoints.Add(oPoints(9))

        ' Splines mit Punkten in einem zufälligen Bereich erstellen
        If CheckBox1.Checked Then
            Dim i As Int32
            Dim _point As Point2d
            For i = 1 To oFitPoints.Count
                _point = oFitPoints(i)
                _point = PointVar(_point)
            Next i

            ' Erster un letzter Punkt des Splines sollen gleich sein
            oFitPoints.Remove(oFitPoints.Count)
            _point = oTG.CreatePoint2d(oFitPoints(1).X, oFitPoints(1).Y)
            oFitPoints.Add(_point)
        End If

        ' Create a work plane parallel to X-Y plane.
        Dim oWorkPlane As WorkPlane
        oWorkPlane = oCompDef.WorkPlanes.AddByPlaneAndOffset(oCompDef.WorkPlanes(oCompDef.WorkPlanes.Count), 20)

        'Create the first sketch on the X-Y plane
        Dim oSketch1 As PlanarSketch
        oSketch1 = oCompDef.Sketches.Add(oWorkPlane)

        ' Create the spline.
        Dim oSpline As SketchSpline
        oSpline = oSketch1.SketchSplines.Add(oFitPoints)

        ''Create the second sketch.
        'Dim oSketch2 As PlanarSketch
        'oSketch2 = oCompDef.Sketches.Add(oWorkPlane)

        '' Set a reference to the geometry of the spline on the first sketch
        'Dim oBSplineCurve2d As BSplineCurve2d
        'oBSplineCurve2d = oSpline.Geometry

        '' Create a fixed spline on the second sketch based
        '' on the geometry of the spline on the first sketch.
        'Dim oFixedSpline As SketchFixedSpline
        'oFixedSpline = oSketch2.SketchFixedSplines.Add(oBSplineCurve2d)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim oCompDef As PartComponentDefinition
        oCompDef = thisApp.ActiveDocument.ComponentDefinition

        If oCompDef.Features.LoftFeatures.Count > 0 Then
                For Each Loft In oCompDef.Features.LoftFeatures
                    Loft.Delete()
                Next
            Else
                For Each Sketch In oCompDef.Sketches
                    Sketch.Delete()
                Next
            End If

            If oCompDef.WorkPlanes.Count > 3 Then
                Dim i As Int32
                For i = 4 To oCompDef.WorkPlanes.Count - 1
                    If oCompDef.WorkPlanes.Count > 3 Then
                        oCompDef.WorkPlanes(i).Delete()
                    Else
                    End If
                Next i
            End If

        TextBox1.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        ListBox4.Items.Clear()
        ListBox1.Items.Clear()


    End Sub

    Public Sub InfoToSelection()

        Dim oDoc As PartDocument
        oDoc = thisApp.ActiveDocument

        Dim oObjCol As ObjectCollection
        oObjCol = thisApp.TransientObjects.CreateObjectCollection

        Dim oEntity As Object
        For Each oEntity In oDoc.SelectSet
            oObjCol.Add(oEntity)
        Next

        For Each oEntity In oObjCol
            Debug.Print(TypeDefinition(oEntity.Type))
        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        InfoToSelection()
    End Sub


    Public Sub SketchEntities()
        'this Sub analyses the first sketch of an active part-document for certain associated sketch-objects like points, lines, circles and so on. They are printed into the debug window
        Dim oSketchEnt As SketchEntitiesEnumerator
        Dim oSketch As Sketch
        Dim oDoc As PartDocument
        Dim oDef As PartComponentDefinition
        Dim oPlSketch As PlanarSketch

        oDoc = thisApp.ActiveDocument
        oDef = oDoc.ComponentDefinition
        oSketch = oDef.Sketches(1)

        oSketchEnt = oSketch.SketchEntities
        For Each SketchEntity In oSketchEnt

            ' Set a reference to the transient geometry object.

            ' Display type specific information.
            Select Case SketchEntity.Type
                'Case 83898880 'kSketchArcObject
                '    Dim oArc As SketchArc
                '    oArc = SketchEntity
                '    Debug.Print("Sketch Arc selected.")
                '    Debug.Print("  Center Point: " & oArc.CenterSketchPoint.Geometry.X &
                '            ", " & oArc.CenterSketchPoint.Geometry.Y)
                '    Debug.Print("  Start Point: " & oArc.StartSketchPoint.Geometry.X &
                '            ", " & oArc.StartSketchPoint.Geometry.Y)
                '    Debug.Print("  End Point: " & oArc.EndSketchPoint.Geometry.X &
                '            ", " & oArc.EndSketchPoint.Geometry.Y)
                '    Debug.Print("  Start Angle: " & oArc.StartAngle)
                '    Debug.Print("  Sweep Angle: " & oArc.SweepAngle)
                '    Debug.Print("  Radius: " & oArc.Radius)
                '    Debug.Print("  Length: " & oArc.Length)
                'Case 83899648 'kSketchCircleObject
                '    Dim oCircle As SketchCircle
                '    oCircle = SketchEntity
                '    Debug.Print("Sketch Circle selected.")
                '    Debug.Print("  Center Point: " & oCircle.CenterSketchPoint.Geometry.X &
                '            ", " & oCircle.CenterSketchPoint.Geometry.Y)
                '    Debug.Print("  Radius: " & oCircle.Radius)
                '    Debug.Print("  Area: " & oCircle.Area)

                '    ' Change the radius
                '    oCircle.Radius = oCircle.Radius * 1.5
                'Case 83899392 'kSketchEllipseObject
                '    Dim oEllipse As SketchEllipse
                '    oEllipse = SketchEntity
                '    Debug.Print("Sketch Ellipse selected.")
                '    Debug.Print("  Center Point: " & oEllipse.CenterSketchPoint.Geometry.X &
                '            ", " & oEllipse.CenterSketchPoint.Geometry.Y)
                '    Debug.Print("  Major Axis Vecotr: " & oEllipse.MajorAxisVector.X &
                '            ", " & oEllipse.MajorAxisVector.Y)
                '    Debug.Print("  Major Radius: " & oEllipse.MajorRadius)
                '    Debug.Print("  Minor Radius: " & oEllipse.MinorRadius)
                '    Debug.Print("  Area: " & oEllipse.Area)

                '    ' Modify the ellipse
                '    oEllipse.MajorAxisVector = oTG.CreateUnitVector2d(1, 1)
                '    oEllipse.MajorRadius = oEllipse.MajorRadius / 2
                '    oEllipse.MinorRadius = oEllipse.MinorRadius * 2
                'Case 83904768 'kSketchEllipticalArcObject
                '    Dim oEllipticalArc As SketchEllipticalArc
                '    oEllipticalArc = SketchEntity
                '    Debug.Print("Sketch Elliptical Arc selected.")
                '    Debug.Print("  Center Point: " & oEllipticalArc.CenterSketchPoint.Geometry.X &
                '            ", " & oEllipticalArc.CenterSketchPoint.Geometry.Y)
                '    Debug.Print("  Major Axis Vector: " & oEllipticalArc.MajorAxisVector.X &
                '            ", " & oEllipticalArc.MajorAxisVector.Y)
                '    Debug.Print("  Major Radius: " & oEllipticalArc.MajorRadius)
                '    Debug.Print("  Minor Radius: " & oEllipticalArc.MinorRadius)
                '    Debug.Print("  Start Angle: " & oEllipticalArc.StartAngle)
                '    Debug.Print("  Sweep Angle: " & oEllipticalArc.SweepAngle)
                '    Debug.Print("  Length: " & oEllipticalArc.Length)

                '    ' Modify the elliptical arc.
                '    oEllipticalArc.MajorAxisVector = oTG.CreateUnitVector2d(1, 1)
                '    oEllipticalArc.MajorRadius = oEllipticalArc.MajorRadius / 2
                '    oEllipticalArc.MinorRadius = oEllipticalArc.MinorRadius * 2
                'Case 83896064 'kSketchLineObject
                '    Dim oLine As SketchLine
                '    oLine = SketchEntity
                '    Debug.Print("Sketch Line selected.")
                '    Debug.Print("  Start Point: " & oLine.StartSketchPoint.Geometry.X &
                '            ", " & oLine.StartSketchPoint.Geometry.Y)
                '    Debug.Print("  End Point: " & oLine.EndSketchPoint.Geometry.X &
                '            ", " & oLine.EndSketchPoint.Geometry.Y)
                '    Debug.Print("  Length: " & oLine.Length)
                '    Debug.Print("  Centerline: " & oLine.Centerline)

                '    ' Toggle the centerline property.
                '    If oLine.Centerline Then
                '        oLine.Centerline = False
                '    Else
                '        oLine.Centerline = True
                '    End If
                'Case 83896576 'kSketchPointObject
                '    Dim oPoint As SketchPoint
                '    oPoint = SketchEntity
                '    Debug.Print("Sketch Point selected.")
                '    Debug.Print("  Position: " & oPoint.Geometry.X &
                '            ", " & oPoint.Geometry.Y)
                '    Debug.Print("  Hole Center: " & oPoint.HoleCenter)

                '    ' Toggle the hole center property.
                '    If oPoint.HoleCenter Then
                '        oPoint.HoleCenter = False
                '    Else
                '        oPoint.HoleCenter = True
                '    End If
                Case 83899136 'kSketchSplineObject
                    Dim oSpline As SketchSpline
                    oSpline = SketchEntity
                    Debug.Print("Sketch Spline selected.")
                    Debug.Print("  Length: " & oSpline.Length)
                    Debug.Print("  Closed: " & oSpline.Closed)
                    Debug.Print("  Fit Points (" & oSpline.FitPointCount & ")")
                    Dim i As Long
                    For i = 1 To oSpline.FitPointCount
                        Debug.Print("    Fit Point " & i & ": " & oSpline.FitPoint(i).Geometry.X &
                            ", " & oSpline.FitPoint(i).Geometry.Y)
                    Next

                Case 84017920 'kSketchControlpointSplineObject
                    Dim oCSpline As SketchControlPointSpline
                    oCSpline = SketchEntity
                    Debug.Print("Sketch Spline selected.")
                    Debug.Print("  Length: " & oCSpline.Length)
                    Debug.Print("  Control Points: (" & oCSpline.ControlPointCount & ")")
                    Dim i As Long
                    For i = 1 To oCSpline.ControlPointCount
                        Debug.Print("    Control Point " & i & ": " & oCSpline.ControlPoint(i).Geometry.X &
                            ", " & oCSpline.ControlPoint(i).Geometry.Y)
                    Next

            End Select
        Next

        ' Call the generic sketch entity methods.
        'Debug.Print("  Constraint count: " & SketchEntity.Constraints.Count)
        'Debug.Print("  Construction: " & SketchEntity.Construction)
        'Debug.Print("  Range: (" & SketchEntity.RangeBox.MinPoint.X & ", " &
        '                    SketchEntity.RangeBox.MinPoint.Y & ") - (" &
        '                    SketchEntity.RangeBox.MaxPoint.X & ", " &
        '                    SketchEntity.RangeBox.MaxPoint.Y & ")")
        'Debug.Print("  Reference: " & SketchEntity.Reference)
        'If SketchEntity.Reference Then
        '    Debug.Print("  Referenced Entity: " & TypeName(SketchEntity.ReferencedEntity))
        'End If
    End Sub



    Function PointVar(_point As Point2d) As Point2d
        Randomize()

        Dim i As Double
        i = Rnd()
        If i < 0.5 Then
            _point.X = _point.X + 0.8 * Rnd() * _point.X
            _point.Y = _point.Y + 0.8 * Rnd() * _point.Y
        Else
            _point.X = _point.X - 0.8 * Rnd() * _point.X
            _point.Y = _point.Y - 0.8 * Rnd() * _point.Y
        End If

    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        SketchEntities()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        _Loft()
    End Sub

    Public Sub _Loft()
        Dim oLoftDefinition As LoftDefinition
        Dim oCompDef As PartComponentDefinition
        Dim oSection As ObjectCollection

        oCompDef = thisApp.ActiveDocument.ComponentDefinition
        oSection = thisApp.TransientObjects.CreateObjectCollection

        For Each _sketch As PlanarSketch In oCompDef.Sketches
            oSection.Add(_sketch.Profiles.AddForSolid())
            _sketch.Visible = True
        Next

        oLoftDefinition = oCompDef.Features.LoftFeatures.CreateLoftDefinition(oSection, PartFeatureOperationEnum.kNewBodyOperation)
        oCompDef.Features.LoftFeatures.Add(oLoftDefinition)

        For Each _sketch As PlanarSketch In oCompDef.Sketches
            _sketch.Visible = True
        Next

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.MouseClick
        CheckBox3.Checked = False
        CheckBox2.Checked = True
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.MouseClick
        CheckBox2.Checked = False
        CheckBox3.Checked = True
    End Sub

    Function TypeDefinition(oTyp As Int32) As String
        Using MyReader As New Microsoft.VisualBasic.
                       FileIO.TextFieldParser(
                         "\\HATHOR\FG-Assi\Thom\200 Studentische Mitarbeiter\Szalkiewicz\Skripte\VBA\SplineZeichner\TypeDefinition.txt")
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)
            Dim currentRow As String()
            Dim foundType As Boolean
            foundType = False

            While Not (MyReader.EndOfData Or foundType = True)
                Try
                    currentRow = MyReader.ReadFields()
                    Dim currentField As Int32
                    currentField = Val(currentRow(0))

                    If currentField = oTyp Then
                        Return currentRow(1)
                        foundType = True
                    End If

                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    Debug.Print("Line " & ex.Message &
                    "is not valid and will be skipped.")
                End Try
            End While

            If foundType = False Then
                Return 0
            Else
                Return 0
            End If

        End Using
    End Function

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        SplineTrans()
    End Sub

    'Output of info of the marked spline
    'TODO export into an extra class
    Private Sub SplineTrans()
        Dim oSketchEnt As SketchEntity
        Dim selSet As SelectSet
        Dim oBSpline As BSplineCurve2d

        selSet = thisApp.ActiveDocument.SelectSet
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()

        Try
            oSketchEnt = selSet(1)
            If TypeOf oSketchEnt Is SketchControlPointSpline Or TypeOf oSketchEnt Is SketchSpline Then
                oBSpline = oSketchEnt.Geometry
                Dim SplinDat As Object
                SplinDat = GetBSplineInfoAndData(oBSpline)
                '--------------------------------------------------------------------------------------------------------------------------
                'transport spline data into excel
                'If TypeOf oSketchEnt Is SketchControlPointSpline Then
                '    IntoExcel(SplinDat)
                'Else
                '    TextBox1.AppendText("Vorerst können nur Splines übertragen werden, die über ein Konrtollpolygon definiert sind.")
                'End If
                '--------------------------------------------------------------------------------------------------------------------------

                TextBox1.Text = ("Info über Spline:" & vbCrLf & "Ordnung " & SplinDat(0) & vbCrLf &
                    SplinDat(1) & " Stützpunkte")

                Dim i As Integer
                i = 0
                For Each item As Double In SplinDat(2)
                    If i Mod 2 = 0 Then
                        ListBox1.Items.Add(Round(item, 3))
                    ElseIf i Mod 2 = 1 Then
                        ListBox2.Items.Add(Round(item, 3))
                    End If
                    i = i + 1
                Next

                TextBox1.AppendText(vbCrLf & SplinDat(3) & " Knoten : (")

                For Each item As Double In SplinDat(4)
                    TextBox1.AppendText(Round(item, 3) & ", ")
                Next

                TextBox1.AppendText(")" & vbCrLf)

                If SplinDat(6) = True Then
                    TextBox1.AppendText("Rational: Ja" & vbCrLf)
                Else
                    TextBox1.AppendText("Rational: Nein" & vbCrLf)
                End If

                If SplinDat(6) = True Then
                    TextBox1.AppendText("Gewichtungen: ")
                    For Each item As Double In SplinDat(5)
                        TextBox1.AppendText(item & ", ")
                    Next
                End If

                If SplinDat(7) = True Then
                    TextBox1.AppendText("Geschlossen: Ja" & vbCrLf)
                Else
                    TextBox1.AppendText("Geschlossen: Nein" & vbCrLf)
                End If

                If SplinDat(8) = True Then
                    TextBox1.AppendText("Periodisch: Ja" & vbCrLf)
                Else
                    TextBox1.AppendText("Periodisch: Nein" & vbCrLf)
                End If

                If TypeOf oSketchEnt Is SketchSpline Then
                    Dim oSketchSpline As SketchSpline
                    oSketchSpline = oSketchEnt
                    ListBox3.Items.Clear()
                    ListBox4.Items.Clear()
                    For j As Int32 = 1 To oSketchSpline.FitPointCount
                        ListBox3.Items.Add(Round(oSketchSpline.FitPoint(j).Geometry.X, 3))
                        ListBox4.Items.Add(Round(oSketchSpline.FitPoint(j).Geometry.Y, 3))
                    Next j
                End If

            Else
                TextBox1.Text = "Bitte einen Spline auswählen" & vbCrLf
            End If

        Catch ex As Exception
            TextBox1.Text = "Bitte einen Spline auswählen" & vbCrLf
        End Try

    End Sub

    'Function that returns Info of a given spline in an array (order, poles, knots, weights)
    Private Function GetBSplineInfoAndData(oBspline As BSplineCurve2d) As Object

        Dim order As Integer, NumPoles As Integer, NumKnots As Integer, IsRational As Boolean, IsClosed As Boolean
        Dim IsPeriodic As Boolean
        Dim AAll(8)

        oBspline.GetBSplineInfo(order, NumPoles, NumKnots, IsRational, IsPeriodic, IsClosed)

        Dim Apoles(0 To NumPoles) As Double, Aknots(0 To NumKnots) As Double, Aweights(0 To NumPoles) As Double

        oBspline.GetBSplineData(Apoles, Aknots, Aweights)

        AAll(0) = order
        AAll(1) = NumPoles
        AAll(2) = Apoles
        AAll(3) = NumKnots
        AAll(4) = Aknots
        AAll(5) = Aweights
        AAll(6) = IsRational
        AAll(7) = IsClosed
        AAll(8) = IsPeriodic

        GetBSplineInfoAndData = AAll

    End Function

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If thisApp Is Nothing And Not InvOpen Then

            Dim Inv As New AppStartWhenReady(Me)
            Inv.oApplication("Inventor")
            'Button7.Enabled = False

        Else
            MsgBox("Inventor ist bereits offen.")
        End If

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        MatApp = CreateObject("Matlab.Application")
        Dim Mat As New AppStartWhenReady(Me)
        Mat.oApplication("Matlab")
    End Sub

    Private Sub Form1_InvReady() Handles Me.InvReady
        Button7.Enabled = True
    End Sub

    'Method for writing all relevant spline-data into the xls-file "PunkteEinheitsKreis"
    'the format has to be *.xls so matlab can write back into it
    'TODO export into extra class; add more automation to fill-in of excel sheet
    'Public Sub IntoExcel(SplineDaten As Object)
    '    Dim oXL As Excel.Application
    '    Dim oWB As Excel.Workbook
    '    Dim oSheet As Excel.Worksheet
    '    Dim oRng As Excel.Range
    '    Dim startWrite As Boolean = False

    '    Try
    '        'Process.Start("Excel.exe")
    '        oXL = Marshal.GetActiveObject("Excel.Application")
    '        oWB = oXL.Workbooks.Open("D:\Skripte\Bens Spielwiese\PunkteEinheitsKreis.xls")
    '        oSheet = oWB.Worksheets("SplineData")
    '        startWrite = True
    '    Catch ex As Exception

    '    End Try

    '    'Begin with filling in Excel-sheet
    '    If startWrite = True Then
    '        oSheet.Activate()
    '        With oSheet
    '            'Clear old data
    '            .Range(.Cells(2, 1), .Cells(100, 9)).Clear()
    '            'Number of poles into excel
    '            .Cells(2, 1).Value = SplineDaten(1)
    '            'Poles x- and y-coordinates into excel
    '            Dim i As Integer = 0
    '            Dim j As Integer = 0
    '            For Each item In SplineDaten(2)
    '                If i Mod 2 = 0 Then
    '                    .Cells(j + 4, 1).Value = Round(item, 3)
    '                    j = j + 1
    '                Else
    '                End If
    '                i = i + 1
    '            Next
    '            j = 0
    '            For Each item In SplineDaten(2)
    '                If i Mod 2 = 0 Then
    '                Else
    '                    .Cells(j + 4, 2).Value = Round(item, 3)
    '                    j = j + 1
    '                End If
    '                i = i + 1
    '            Next
    '            'weights into excel
    '            i = 0
    '            If SplineDaten(6) = True Then
    '                For Each item In SplineDaten(5)
    '                    .Cells(i + 4, 4).Value = Round(item, 3)
    '                    i = i + 1
    '                Next
    '            Else
    '                For i = 0 To j - 1
    '                    .Cells(i + 4, 4).Value = 1
    '                Next i
    '            End If

    '            'Number of knots into excel
    '            .Cells(2, 5).Value = SplineDaten(3)
    '            i = 0
    '            'Knots into excel
    '            For Each item In SplineDaten(4)
    '                .Cells(i + 4, 5).Value = Round(item, 3)
    '                i = i + 1
    '            Next

    '            'order into excel
    '            .Cells(2, 6).Value = SplineDaten(0)

    '            'rational? into excel
    '            If SplineDaten(6) = True Then
    '                .Cells(2, 7).Value = "True"
    '            Else
    '                .Cells(2, 7).Value = "False"
    '            End If

    '            'closed? into excel
    '            If SplineDaten(7) = True Then
    '                .Cells(2, 8).Value = "True"
    '            Else
    '                .Cells(2, 8).Value = "False"
    '            End If

    '            'periodic? into excel
    '            If SplineDaten(8) = True Then
    '                .Cells(2, 9).Value = "True"
    '            Else
    '                .Cells(2, 9).Value = "False"
    '            End If

    '        End With

    '    End If

    '    oWB.Save()
    '    oWB.Close()

    'End Sub

    'Sub for writing Spline-Infos into a TextFile
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            'check whether a spline is selected
            If TypeOf thisApp.ActiveDocument.SelectSet(1) Is SketchControlPointSpline Or TypeOf thisApp.ActiveDocument.SelectSet(1) Is SketchSpline Then
                SplineTotext(thisApp.ActiveDocument.SelectSet(1))
            End If
        Catch ex As Exception
            TextBox1.AppendText("Bitte einen Spline auswählen")
        End Try

    End Sub

    Private Sub SplineTotext(CPSpline As SketchControlPointSpline)
        Dim CPSplineDef As BSplineCurve2d
        Dim SplinDat As Object
        CPSplineDef = CPSpline.Geometry
        SplinDat = GetBSplineInfoAndData(CPSpline)

        Dim oStreamWriter As New StreamWriter("\\HATHOR\FG-Assi\Thom\200 Studentische Mitarbeiter\Szalkiewicz\Skripte\InvToMatlab.txt")
        oStreamWriter.WriteLine("Hallo")

    End Sub

    Private Sub SplineTotext(CPSpline As SketchSpline)

    End Sub

End Class

' class for handling wait-times, for now the method is rather blunt and works only for the starting time of inventor
' TODO altering this class to handle all processes that include wait-times, such as opening new part-documents and such
' TODO make it work for Matlab as well
Class AppStartWhenReady
    Dim otimer As New Timer
    Dim ointerval As Int32
    Dim oAppReady As Boolean = False
    Dim Appname As String
    Dim Bezug As Form1

    Sub New(Caller As Form1)
        Bezug = Caller
    End Sub

    Public Sub oApplication(oApp As String) 'needs the string of the applications .exe
        Appname = oApp
        Process.Start(oApp + ".exe")
        AddHandler otimer.Elapsed, AddressOf Me.HandleTimer
        ointerval = 1000
        otimer.Interval = ointerval
        otimer.Start()
    End Sub

    Public Sub HandleTimer(sender As Object, e As EventArgs)
        Try
            Form1.ChangeThisApp(Marshal.GetActiveObject("Inventor.Application"))
            otimer.Stop()
            Debug.Print(Appname + " bereit")
            Bezug.ChangeInvOpen(True)
        Catch ex As Exception
            Debug.Print(Appname + " nocht nicht bereit")
        End Try

    End Sub

End Class


