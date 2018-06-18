Public Class JobManager

    ''' <summary>
    ''' This class is for preventing unwanted handling of the <see cref="Log.nxtStep"/>-event and for keeping track of the jobs.
    ''' </summary>
    Private JobList As New List(Of Pair)
    Private CurrPair As New Pair(-1, -1)
    Private ID As Integer = Math.Ceiling(Rnd() * 1000) ' id was for debug purposes TODO delete


    Public Sub New()
        JobList.Add(CurrPair)
    End Sub

    ' uses events from the log-class
    WithEvents lg As Log

    ''' <summary>
    ''' Event fired, when <see cref="newJob(Object, eString)"/> concludes, that a new job is added
    ''' </summary>
    Public Event nextJob As EventHandler(Of EventArgs)
    ''' <summary>
    ''' Event under developoment
    ''' </summary>
    Public Event AllDone As EventHandler(Of EventArgs)

    ''' <summary>
    ''' Constructor. Initializes the class.
    ''' </summary>
    ''' <param name="l"></param>
    ''' <overloads>
    ''' Used when ther is already a running <see cref="Log"/>-instance.
    ''' </overloads>>
    Public Sub New(l As Log)
        Randomize()
        lg = l
        JobList.Add(CurrPair)
    End Sub

    ''' <summary>
    ''' Sub under development
    ''' </summary>
    ''' <param name="iArr"></param>
    Public Sub newList(iArr As Integer())
        For Each element In iArr

        Next
    End Sub

    ''' <summary>
    ''' Handles the <see cref="Log.nxtStep"/>-event.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="progress"><see cref="eString"/></param>
    ''' <remarks>Searches the current <see cref="Pair"/> of <see cref="JobList"/> determines wether the new job should be added oder rejected. Also deletes jobs that are done. 
    ''' The events raised to be handled by this sub can be raised in a very fast succession. Therefore a SyncLock ensures that this sub is only used by one thread at a time.</remarks>
    Private Sub newJob(sender As Object, progress As eString) Handles lg.nxtStep
        SyncLock Me
            Dim item As New Pair(progress.lstAction, progress.ident) ' marker for passed arguments
            Dim del As Integer = -1 ' marker for position of item to be deleted
            Dim found As Boolean = False ' marker if an item is already in list
            Dim reject As Boolean = False ' marker if exactly the same item is already in list --> reject
            Dim newItem As Boolean = False ' mrker if a new individual is to be added to the list
            Dim alldone As Boolean = False ' marker if all individuals are finished

            ' quick decicion to reject a new job if the same job is already safed as the current job
            If CurrPair.Job = item.Job And CurrPair.indi = item.indi Then
                reject = True
            Else
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
                                Case 60 ' session closed
                                    del = -20

                                Case 45 ' evaluation for given individual finished, write results into Pair
                                    item.res = progress.GetRes
                                    Debug.Print("All jobs done for " & item.indi & ". Individual removed from List " & ID & ".")
                                    alldone = CheckIfReady()

                                Case 50 ' evaluation for given individual finished, delete it from the list
                                    'del = JobList.IndexOf(element1)
                                    Debug.Print("All jobs done for " & item.indi & ". Individual removed from List " & ID & ".")
                                    alldone = CheckIfReady()

                                Case > element1.Job And item.Job < 50 ' all other cases: mark last job as done and add new one to the list
                                    Debug.Print("Job " & element1.Job & " done for individual " & item.indi & " in List " & ID & ".")
                                    element1.Job = item.Job
                                    Debug.Print("New Job " & item.Job & " added for " & item.indi & " in List " & ID & ".")
                            End Select
                            CurrPair = item ' save item into mamory of the job manager to reject the next job, if it is the same
                        End If
                        newItem = False
                        Exit For
                    Else
                        newItem = True
                        del = JobList.IndexOf(element1)
                    End If
                Next
            End If

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
            If newItem Then
                JobList.Add(item)
                JobList.RemoveAt(del)
                Debug.Print("Item removed from Joblist")
                Debug.Print("New Item in Job List: " & item.indi & " with Job " & item.Job)
            End If

            If Not reject And Not alldone Then
                RaiseEvent nextJob(Me, progress)
            ElseIf alldone Then
                Debug.Print("Results for all individuals collected.")
                Dim e As New EventArgs
                RaiseEvent alldone(Me, e)
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' Goes trough the joblist and checks whether all results are set 
    ''' </summary>
    ''' <returns>True if all results are set</returns>
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

    ''' <summary>
    ''' Sub under depelopment
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRes()
        Dim results As Double()() = {}
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
