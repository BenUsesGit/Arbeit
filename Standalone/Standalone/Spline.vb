
<Serializable()> Public Class Spline

    Private ID As Integer
    Private degree As Integer
    Private order As Integer
    Private knots As Double()
    Private NumKnots As Integer
    Private Points As Point()
    Private NumPoints As Integer
    Private closed As Boolean

    Public Sub New()

    End Sub

    'Überladung new1
    Public Sub New(ByVal dgr As Integer, ByVal knts As Double(), ByVal pnts As Point())
        degree = dgr
        order = dgr - 1

        NumKnots = knts.Length
        NumPoints = pnts.Length

        knots = knts
        Points = pnts

        SCheck(knots, Points, order)

        'check if spline is closed
        If Points(0).gX = Points(Points.Length - 1).gX And Points(0).gY = Points(Points.Length - 1).gY Then
            closed = True
        End If

    End Sub

    'sets the ID from given integer
    Public Sub sID(ByVal i As Integer)
        ID = i
    End Sub

    'gets the ID
    Public Function gID()
        Return ID
    End Function

    'sets the degree from given integer, also sets the order
    Public Sub sDegree(ByVal i As Integer)
        degree = i
        order = degree - 1
    End Sub

    'gets the degree
    Public Function gDegree()
        Return degree
    End Function

    'sets the order from given integer, also sets the degree
    Public Sub sOrder(ByVal i As Integer)
        order = i
        degree = i + 1
    End Sub

    'gets the order
    Public Function gOrder()
        Return order
    End Function

    'sets the knots from given array of doubles
    Public Sub sKnots(ByVal Arr As Double())
        Dim i As Integer
        ReDim knots(Arr.Length - 1)
        For i = 0 To Arr.Length - 1
            knots(i) = Arr(i)
        Next
        NumKnots = knots.Length
    End Sub

    'gets the knots
    Public Function gKnots()
        Return knots
    End Function

    'sets points from given array of points
    Public Sub sPoints(ByVal parr As Point())
        If SCheck(knots, parr, order) Then
            ReDim Points(parr.Length - 1) ' clear existing array
            Points = parr
        End If
        NumPoints = Points.Length
    End Sub

    'gets points
    Public Function gPoints()
        Return Points
    End Function

    'sets one point at given position in array
    Public Sub sPoint(ByVal i As Integer, ByVal p As Point)
        'i represents the index of the point of the spline
        If i <= Points.Length - 1 Then
            Points(i) = p
        End If
    End Sub

    'gets one point at given postition in array
    Public Function gPoint(ByVal i As Integer)
        'i represents the index of the point of the spline
        If i <= Points.Length - 1 Then
            Return Points(i)
        Else
            Return 0
        End If
    End Function

    'sets the number ob control points of the Spline
    Public Sub sNumPoints(ByVal num As Integer)
        ReDim Preserve Points(num - 1)
        NumPoints = Points.Length
    End Sub

    'sets the number ob sontrol points of the Spline
    Public Function gNumPoints()
        Return NumPoints
    End Function

    'sets the close-parameter of the spline
    Public Sub sClose(ByVal b As Boolean)
        closed = b
    End Sub

    'gets the close parameter
    Public Function gClose()
        Return closed
    End Function

    'checks wether the spline is drawable: it needs the knots, the points and the order
    Private Function SCheck(ByVal k As Double(), ByVal p As Point(), ByVal o As Integer)
        Dim w As Boolean

        If k Is Nothing Or p Is Nothing Then
            w = True
        End If

        ' Checksum if Spline parameters have the right numbers to create the spline
        If k.Length = o + p.Length + 1 Then
            w = True
        Else
            w = False
            Debug.Print("Prüfsumme des Splines geht nicht auf")
            Return w
        End If

        'check whether number of repetetive points at beginning and end of knot vector matches the order of the spline
        Dim i As Integer
        For i = 1 To o
            Dim checksum2 As Integer = o + p.Length + 1 - i

            If Not k(i - 1) = k(i) And k(order + p.Length + 1 - i) = k(order + p.Length - i) Then
                w = False
            End If
        Next

        Return w

    End Function


End Class
