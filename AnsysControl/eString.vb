
Public Class eString
    Inherits System.EventArgs

    Public progress As String() ' holds the current progress e.g. "project ready"
    Public ident As Integer ' holds the identification of the individual, that is currently under evaluation
    Public lstAction As Integer ' last action that was performed for given indiviual

    Public Sub New(s As String())
        progress = s
        If progress.Length >= 4 Then
            lstAction = progress(0)
            ident = progress(2)
        Else
            ReDim progress(0)
            progress(0) = "Fehler bei der Ergeignisserstellung"
        End If
    End Sub

    Public Function GetRes()
        Dim results As Integer() = {}
        For i = 4 To progress.Length
            If i Mod 2 = 0 Then
                ReDim Preserve results(results.Length)
                results(results.Length - 1) = progress(i)
            End If
        Next
        Return results
    End Function

End Class
