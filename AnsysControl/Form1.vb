Public Class Form1

    Dim ac As Ansys

    Private Sub OpenAnsys_Click(sender As Object, e As EventArgs) Handles OpenAnsys.Click
        ac = New Ansys
    End Sub

    Private Sub PostText_Click(sender As Object, e As EventArgs) Handles PostText.Click
        ac.TtoA("HHallo iccch binn hhiiieeeer")
    End Sub

    Private Sub ExitAnsys_Click(sender As Object, e As EventArgs) Handles ExitAnsys.Click
        ac.ExitAnsys()
    End Sub

    Private Sub ReadScript_Click(sender As Object, e As EventArgs) Handles ReadScript.Click
        ac.TtoA(ac.ReadScript("D:\AnsysSimulationen\TestForAutomation.wbjn"))
        ac.AnsEnter()
    End Sub
End Class
