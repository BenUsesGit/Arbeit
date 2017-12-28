Imports System.Text.RegularExpressions

Public Class TextHandlerForAnsys

    Public Sub New()

    End Sub

    Public Function CleanText(ByVal s As String) As String()
        Dim regex As Regex = New Regex(".*\n")
        Dim ss As String() = regex.Split(s)
        Return ss
    End Function

End Class
