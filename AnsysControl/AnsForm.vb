Imports System.Threading
Public Class AnsForm

    ' acE is just another reference to ac, it handles all the events raised by the ansys object
    WithEvents ac As New Ansys
    Dim a As Integer = 0
    Dim iArr As Integer() = {1, 2, 3, 4, 1337, 6, 7, 8, 12}

    Private Sub OpenAnsys_Click(sender As Object, e As EventArgs) Handles OpenAnsys.Click
        If ac Is Nothing Then
            ac = New Ansys
        End If
        ac.SetActFile(a)
        ac.Open()
    End Sub

    Private Sub PostText_Click(sender As Object, e As EventArgs) Handles PostText.Click
        ac.TtoA("HHallo iccch binn hhiiieeeer")
    End Sub

    Private Sub ExitAnsys_Click(sender As Object, e As EventArgs) Handles ExitAnsys.Click
        ac.ExitAnsys()
        ac = Nothing
    End Sub

    Private Sub ReadScript_Click(sender As Object, e As EventArgs) Handles ReadScript.Click

    End Sub

    Private Sub ExecuteScriptX_Click(sender As Object, e As EventArgs) Handles ExecuteScriptX.Click
        'ac.TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\NewApproach.wbjn" & Chr(34) & ")")
        ac.TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\StartnewMechanical.wbjn" & Chr(34) & ")")
        ac.AnsEnter()
        ac.TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\LoadCADModel.wbjn" & Chr(34) & ")")
        ac.AnsEnter()
        ac.TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\Edit.wbjn" & Chr(34) & ")")
        ac.AnsEnter()
    End Sub

    Private Sub UpdateMesh_Click(sender As Object, e As EventArgs)
        'ac.TtoA("RunScript(" & Chr(34) & "D:\Skripte\Python\PythonTest\PythonTest\PythonTest.py" & Chr(34) & ")")
        ac.TtoA("RunScript(" & Chr(34) & "D:\Skripte\Python\PythonTest\PythonTest\PythonTest.py" & Chr(34) & ")")
        ac.AnsEnter()
    End Sub

    Private Sub ExitMechanical_Click(sender As Object, e As EventArgs) Handles ExitMechanical.Click
        ac.TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\Exit.wbjn" & Chr(34) & ")")
        ac.AnsEnter()
    End Sub

    Private Sub SaveProject_Click(sender As Object, e As EventArgs) Handles SaveProject.Click
        ac.TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\SaveProject.wbjn" & Chr(34) & ")")
        ac.AnsEnter()
    End Sub

    Private Sub LoadnEditProject_Click(sender As Object, e As EventArgs) Handles LoadnEditProject.Click
        ac.TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\OpenProject.wbjn" & Chr(34) & ")")
        ac.AnsEnter()
    End Sub

    Private Sub MeshUpdate_Click(sender As Object, e As EventArgs) Handles MeshUpdate.Click
        ac.TtoA("RunScript(" & Chr(34) & "D:\Skripte\VBA\Splines\Test\Java.py" & Chr(34) & ")")
        ac.AnsEnter()
        ac.TtoA("pr(0)")
        ac.AnsEnter()
    End Sub

    Private Sub ExecuteAPDL_Click(sender As Object, e As EventArgs) Handles ExecuteAPDL.Click
        Dim Stamp As DateTime = DateTime.Now

        Debug.Print(CStr(Stamp))

        'ac.TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\StartAPDL.wbjn" & Chr(34) & ")")
        'ac.AnsEnter()
    End Sub

    Private Sub ExecuteMechanical_Click(sender As Object, e As EventArgs) Handles ExecuteMechanical.Click
        ac.TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\Edit.wbjn" & Chr(34) & ")")
        ac.AnsEnter()
    End Sub

    Private Sub SendAPDLCommands_Click(sender As Object, e As EventArgs) Handles SendAPDLCommands.Click
        ac.TtoA("RunScript(" & Chr(34) & "D:\AnsysSimulationen\SendAPDLCommands.wbjn" & Chr(34) & ")")
        ac.AnsEnter()
    End Sub

    Private Sub ReadResults_Click(sender As Object, e As EventArgs) Handles ReadResults.Click
        ac.TtoA("RunScript(" & Chr(34) & "D:\Skripte\Python\PythonTest\PythonTest\CompTest.py" & Chr(34) & ")")
        ac.AnsEnter()
    End Sub

    Private Sub GetAnsys_Click(sender As Object, e As EventArgs) Handles GetAnsys.Click
        ac.GetAnsys()
    End Sub

    Public Sub OnAnsRdy(sender As Object, e As EventArgs) Handles ac.ObjRdy
        'ac.slogfile("D:\Skripte\VBA\Splines\Test\Ichbinhier\")
    End Sub

    Private Sub automatisch_Click(sender As Object, e As EventArgs) Handles automatisch.Click
        EvalArr()
        'ac.Test()
    End Sub

    Public Sub EvalArr(sender As Object, e As eString) Handles ac.EvalRdy
        'Debug.Print("Thread3")
        ' e passes the identification of the individual, then ac.begin is passed the individual with the position next to the one given by e
        If Array.IndexOf(iArr, e.ident) < iArr.Length - 1 Then
            ac.Begin(iArr(Array.IndexOf(iArr, e.ident) + 1))
        End If
    End Sub

    Public Sub EvalArr()
        ac.TtoA("listOfResults = [""Total Deformation""]")
        ac.AnsEnter()
        If iArr.Length <> 0 Then
            ac.Begin(iArr(0))
        End If
    End Sub
End Class
