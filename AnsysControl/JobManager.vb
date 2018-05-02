' This class is for bugfixing purposes. It filters multiple orders of the same type and guarantees that each job is executed only once.
Public Class JobManager

    Private JobList As New List(Of Pair)
    Private CurrPair As New Pair(-1, -1)
    Private ID As Integer = Math.Ceiling(Rnd() * 1000) ' id was for debug purposes TODO delete

    ' constructor 1
    Public Sub New()
        JobList.Add(CurrPair)
    End Sub

    ' uses events from the log-class
    WithEvents lg As Log

    ' fires an event if a job is finished
    Public Event nextJob As EventHandler(Of EventArgs)
    Public Event AllDone As EventHandler(Of EventArgs)

    ' constructor 2
    Public Sub New(l As Log)
        Randomize()
        lg = l
        JobList.Add(CurrPair)
    End Sub

    Public Sub newList(iArr As Integer())
        For Each element In iArr

        Next
    End Sub

    ' sub handles the nect step event of the log-class
    Public Sub newJob(sender As Object, progress As eString) Handles lg.nxtStep
        SyncLock Me
            Dim item As New Pair(progress.lstAction, progress.ident) ' marker for passed arguments
            Dim del As Integer = -1 ' marker for position of item to be deleted
            Dim found As Boolean = False ' marker if an item is already in list
            Dim reject As Boolean = False ' marker if exactly the same item is already in list --> reject
            Dim alldone As Boolean = False ' marker if all individuals are finished
            ' go through all jobs in job list
            For Each element1 In JobList
                ' if the individual is already in the list...
                If element1.indi = item.indi Then
                    ' ... check whether it has also the same job already running, if so, reject it
                    If element1.Job = item.Job Then
                        reject = True
                        Debug.Print("Job rejected. Is already in List " & ID)
                        ' otherwise mark the old job as finished, add the new job to joblist
                    Else
                        found = True
                        Select Case item.Job
                            ' session closed
                            Case 60
                                del = -20
                            ' evaluation for given individual finished, write results into Pair
                            Case 45
                                item.res = progress.GetRes
                            ' evaluation for given individual finished, delete it from the list
                            Case 50
                                'del = JobList.IndexOf(element1)
                                Debug.Print("All jobs done for " & item.indi & ". Individual removed from List " & ID & ".")
                                alldone = CheckIfReady()
                            ' all other cases: mark last job as done and add new one to the list
                            Case > element1.Job And item.Job < 50
                                Debug.Print("Job " & element1.Job & " done for individual " & item.indi & " in List " & ID & ".")
                                element1.Job = item.Job
                                CurrPair.indi = item.indi
                                CurrPair.Job = item.Job
                                found = True
                                Debug.Print("New Job " & item.Job & " added for " & item.indi & " in List " & ID & ".")
                        End Select
                    End If
                    Exit For
                End If
            Next

            '' in case the individual is not in the list at all, add it
            'If Not found And Not reject Then
            '    JobList.Add(item)
            '    Debug.Print("New Job " & item.Job & " added for " & item.indi & " in List " & ID & ".")
            'End If

            '' remove item with given position
            'If del > -1 Then
            '    JobList.RemoveAt(del)
            'End If

            '' in case the ansys-session is closed 
            'If del = -20 Then
            '    JobList.Clear()
            '    CurrPair = New Pair(-1, -1)
            '    JobList.Add(CurrPair)
            'End If

            If Not reject And Not alldone Then
                RaiseEvent nextJob(Me, progress)
            ElseIf alldone Then
                Debug.Print("Results for all individuals collected.")
                Dim e As New EventArgs
                RaiseEvent alldone(Me, e)
            End If
        End SyncLock
    End Sub

    ' sub goes trough the joblist and checks whether all results are set
    Private Function CheckIfReady()
        Dim b As Boolean = False
        Dim count As Integer = 0
        For Each item In JobList
            If item.res.Length < 1 Then
                count += 1
                If count >= 2 Then
                    b = False
                    Exit For
                End If
            Else
                b = True
            End If
        Next
        Return b
    End Function

    Public Function GetRes()
        Dim results As Integer()() = {}
        For Each element In JobList
            ReDim Preserve results(results.Length)(element.res.Length - 1)
            results(results.Length - 1) = element.res
        Next
        JobList.Clear()
        CurrPair = New Pair(-1, -1)
        JobList.Add(CurrPair)
        Return results
    End Function

End Class
