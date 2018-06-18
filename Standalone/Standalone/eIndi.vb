''' <summary>
''' Class for storing groups of individuals and passing them between methods of different objects
''' </summary>
Public Class eIndi
    Inherits System.EventArgs
    ' TODO getter setter ?

    Public indis As Individuum()
    Public lstAction As Integer

    Sub New(Arr As Individuum(), ActionID As Integer)
        indis = Arr
        lstAction = ActionID
    End Sub

End Class
