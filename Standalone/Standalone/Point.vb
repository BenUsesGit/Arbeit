<Serializable()> Public Class Point
    Private iX As Double
    Private iY As Double
    Private iZ As Double
    Private iW As Double

    'new with all
    Public Sub New(ByVal x As Double, ByVal y As Double, ByVal z As Double, ByVal w As Double)
        iX = x
        iY = y
        iZ = z
        iW = w
    End Sub

    'new without wheights
    Public Sub New(ByVal x As Double, ByVal y As Double, ByVal z As Double)
        iX = x
        iY = y
        iZ = z
        iW = 1
    End Sub

    'new without wheight & z values
    Public Sub New(ByVal x As Double, ByVal y As Double)
        iX = x
        iY = y
        iZ = 0
        iW = 1
    End Sub

    Public Sub New()

    End Sub

    ' s = set ; g = get
    Public Sub sX(ByVal x As Double)
        iX = x
    End Sub

    Public Function gX()
        Return iX
    End Function

    Public Sub sY(ByVal y As Double)
        iY = y
    End Sub

    Public Function gY()
        Return iY
    End Function

    Public Sub sZ(ByVal z As Double)
        iZ = z
    End Sub

    Public Function gZ()
        Return iZ
    End Function

    Public Sub sW(ByVal w As Double)
        iW = w
    End Sub

    Public Function gW()
        Return iW
    End Function


End Class
