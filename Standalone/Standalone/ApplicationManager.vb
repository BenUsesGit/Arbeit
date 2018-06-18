Public Class ApplicationManager
    ' TODO include CAD Module

    Public Event AnsRdy As EventHandler(Of EventArgs)

    WithEvents ac As New AnsysControl.Ansys

    Public Sub New()

    End Sub

    Public Sub startAns()
        ac.Open()
    End Sub

    Public Sub sAns(a As AnsysControl.Ansys)
        ac = a
    End Sub

    Public Function gAns()
        Return ac
    End Function

    Private Sub SetRdy(sender As Object, e As EventArgs) Handles ac.ObjRdy
        RaiseEvent AnsRdy(Me, e)
    End Sub


End Class
