Public Class IndieJoblist

    Private ident As Integer
    Dim List As String() = {"No Job yet"}


    Public Sub New()

    End Sub

    Public Sub New(i As Integer, ls As String())
        ident = i
        ls = List
    End Sub

    Public Sub New(i As Integer)
        ident = i
    End Sub

    Public Sub AppendJob(s As String)

        If List(0) = "No Job yet" And List.Length = 1 Then
            List(0) = s
        ElseIf List(0) = s Then
            Debug.Print("Job already on top of queue")
        Else
            ReDim Preserve List(List.Length)
            List(List.Length - 1) = s
            Debug.Print("Job added: " & Me.ident & " " & s)
        End If

    End Sub

    Public Function CurrentJob()
        Dim s As String
        If List.Length <> 0 Then
            s = List(0)
        Else
            s = "No Jobs"
        End If
        Return s
    End Function

    Public Sub JobDone(s As String)
        ' List has to be not empty
        If List.Length <> 0 Then
            If List(0) = "No Job yet" Then
                ReDim List(0)
                Debug.Print("Job done: " & Me.ident & " " & "No Job yet")
            Else
                Dim backupList As String() = List
                ReDim List(List.Length - 2)
                For i = 1 To backupList.Length - 1
                    List(i - 1) = backupList(i)
                Next
                Debug.Print("Job done: " & Me.ident & " " & backupList(0))
            End If
        Else
            Debug.Print("Job nicht in Liste, oder Liste leer")
        End If
    End Sub

    Public Function getIdent()
        Return ident
    End Function

End Class
