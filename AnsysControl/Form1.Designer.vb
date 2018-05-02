<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.OpenAnsys = New System.Windows.Forms.Button()
        Me.PostText = New System.Windows.Forms.Button()
        Me.ExitAnsys = New System.Windows.Forms.Button()
        Me.ReadScript = New System.Windows.Forms.Button()
        Me.ExecuteScriptX = New System.Windows.Forms.Button()
        Me.ExitMechanical = New System.Windows.Forms.Button()
        Me.SaveProject = New System.Windows.Forms.Button()
        Me.LoadnEditProject = New System.Windows.Forms.Button()
        Me.MeshUpdate = New System.Windows.Forms.Button()
        Me.ExecuteAPDL = New System.Windows.Forms.Button()
        Me.ExecuteMechanical = New System.Windows.Forms.Button()
        Me.SendAPDLCommands = New System.Windows.Forms.Button()
        Me.ReadResults = New System.Windows.Forms.Button()
        Me.GetAnsys = New System.Windows.Forms.Button()
        Me.automatisch = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'OpenAnsys
        '
        Me.OpenAnsys.Location = New System.Drawing.Point(12, 118)
        Me.OpenAnsys.Name = "OpenAnsys"
        Me.OpenAnsys.Size = New System.Drawing.Size(75, 23)
        Me.OpenAnsys.TabIndex = 0
        Me.OpenAnsys.Text = "Öffne Ansys"
        Me.OpenAnsys.UseVisualStyleBackColor = True
        '
        'PostText
        '
        Me.PostText.Location = New System.Drawing.Point(133, 118)
        Me.PostText.Name = "PostText"
        Me.PostText.Size = New System.Drawing.Size(75, 23)
        Me.PostText.TabIndex = 1
        Me.PostText.Text = "Text posten"
        Me.PostText.UseVisualStyleBackColor = True
        '
        'ExitAnsys
        '
        Me.ExitAnsys.Location = New System.Drawing.Point(12, 147)
        Me.ExitAnsys.Name = "ExitAnsys"
        Me.ExitAnsys.Size = New System.Drawing.Size(75, 36)
        Me.ExitAnsys.TabIndex = 2
        Me.ExitAnsys.Text = "Schließe Ansys"
        Me.ExitAnsys.UseVisualStyleBackColor = True
        '
        'ReadScript
        '
        Me.ReadScript.Location = New System.Drawing.Point(133, 147)
        Me.ReadScript.Name = "ReadScript"
        Me.ReadScript.Size = New System.Drawing.Size(113, 36)
        Me.ReadScript.TabIndex = 3
        Me.ReadScript.Text = "Skript detailiert Zeile für Zeile auslesen"
        Me.ReadScript.UseVisualStyleBackColor = True
        '
        'ExecuteScriptX
        '
        Me.ExecuteScriptX.Location = New System.Drawing.Point(133, 189)
        Me.ExecuteScriptX.Name = "ExecuteScriptX"
        Me.ExecuteScriptX.Size = New System.Drawing.Size(113, 56)
        Me.ExecuteScriptX.TabIndex = 4
        Me.ExecuteScriptX.Text = "Starte neues Mechanical, Lade Modell, Editiere Modell"
        Me.ExecuteScriptX.UseVisualStyleBackColor = True
        '
        'ExitMechanical
        '
        Me.ExitMechanical.Location = New System.Drawing.Point(425, 189)
        Me.ExitMechanical.Name = "ExitMechanical"
        Me.ExitMechanical.Size = New System.Drawing.Size(113, 36)
        Me.ExitMechanical.TabIndex = 6
        Me.ExitMechanical.Text = "Mechanical schließen"
        Me.ExitMechanical.UseVisualStyleBackColor = True
        '
        'SaveProject
        '
        Me.SaveProject.Location = New System.Drawing.Point(306, 147)
        Me.SaveProject.Name = "SaveProject"
        Me.SaveProject.Size = New System.Drawing.Size(113, 36)
        Me.SaveProject.TabIndex = 7
        Me.SaveProject.Text = "Aktuelles Projekt speichern"
        Me.SaveProject.UseVisualStyleBackColor = True
        '
        'LoadnEditProject
        '
        Me.LoadnEditProject.Location = New System.Drawing.Point(306, 189)
        Me.LoadnEditProject.Name = "LoadnEditProject"
        Me.LoadnEditProject.Size = New System.Drawing.Size(113, 36)
        Me.LoadnEditProject.TabIndex = 8
        Me.LoadnEditProject.Text = "Lade Projekt, Editiere Modell"
        Me.LoadnEditProject.UseVisualStyleBackColor = True
        '
        'MeshUpdate
        '
        Me.MeshUpdate.Location = New System.Drawing.Point(663, 147)
        Me.MeshUpdate.Name = "MeshUpdate"
        Me.MeshUpdate.Size = New System.Drawing.Size(113, 36)
        Me.MeshUpdate.TabIndex = 9
        Me.MeshUpdate.Text = "Mesh--> fine, update"
        Me.MeshUpdate.UseVisualStyleBackColor = True
        '
        'ExecuteAPDL
        '
        Me.ExecuteAPDL.Location = New System.Drawing.Point(544, 147)
        Me.ExecuteAPDL.Name = "ExecuteAPDL"
        Me.ExecuteAPDL.Size = New System.Drawing.Size(113, 36)
        Me.ExecuteAPDL.TabIndex = 10
        Me.ExecuteAPDL.Text = "APDL öffnen"
        Me.ExecuteAPDL.UseVisualStyleBackColor = True
        '
        'ExecuteMechanical
        '
        Me.ExecuteMechanical.Location = New System.Drawing.Point(425, 147)
        Me.ExecuteMechanical.Name = "ExecuteMechanical"
        Me.ExecuteMechanical.Size = New System.Drawing.Size(113, 36)
        Me.ExecuteMechanical.TabIndex = 11
        Me.ExecuteMechanical.Text = "Mechanical öffnen"
        Me.ExecuteMechanical.UseVisualStyleBackColor = True
        '
        'SendAPDLCommands
        '
        Me.SendAPDLCommands.Location = New System.Drawing.Point(544, 189)
        Me.SendAPDLCommands.Name = "SendAPDLCommands"
        Me.SendAPDLCommands.Size = New System.Drawing.Size(113, 36)
        Me.SendAPDLCommands.TabIndex = 12
        Me.SendAPDLCommands.Text = "Set von APDL Commands senden"
        Me.SendAPDLCommands.UseVisualStyleBackColor = True
        '
        'ReadResults
        '
        Me.ReadResults.Location = New System.Drawing.Point(663, 189)
        Me.ReadResults.Name = "ReadResults"
        Me.ReadResults.Size = New System.Drawing.Size(113, 36)
        Me.ReadResults.TabIndex = 13
        Me.ReadResults.Text = "ReadResults"
        Me.ReadResults.UseVisualStyleBackColor = True
        '
        'GetAnsys
        '
        Me.GetAnsys.Location = New System.Drawing.Point(12, 189)
        Me.GetAnsys.Name = "GetAnsys"
        Me.GetAnsys.Size = New System.Drawing.Size(75, 36)
        Me.GetAnsys.TabIndex = 14
        Me.GetAnsys.Text = "GetAnsys"
        Me.GetAnsys.UseVisualStyleBackColor = True
        '
        'automatisch
        '
        Me.automatisch.Location = New System.Drawing.Point(133, 261)
        Me.automatisch.Name = "automatisch"
        Me.automatisch.Size = New System.Drawing.Size(113, 43)
        Me.automatisch.TabIndex = 15
        Me.automatisch.Text = "Projekt öffnen, CAD laden, Projekt updaten"
        Me.automatisch.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 340)
        Me.Controls.Add(Me.automatisch)
        Me.Controls.Add(Me.GetAnsys)
        Me.Controls.Add(Me.ReadResults)
        Me.Controls.Add(Me.SendAPDLCommands)
        Me.Controls.Add(Me.ExecuteMechanical)
        Me.Controls.Add(Me.ExecuteAPDL)
        Me.Controls.Add(Me.MeshUpdate)
        Me.Controls.Add(Me.LoadnEditProject)
        Me.Controls.Add(Me.SaveProject)
        Me.Controls.Add(Me.ExitMechanical)
        Me.Controls.Add(Me.ExecuteScriptX)
        Me.Controls.Add(Me.ReadScript)
        Me.Controls.Add(Me.ExitAnsys)
        Me.Controls.Add(Me.PostText)
        Me.Controls.Add(Me.OpenAnsys)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents OpenAnsys As Button
    Friend WithEvents PostText As Button
    Friend WithEvents ExitAnsys As Button
    Friend WithEvents ReadScript As Button
    Friend WithEvents ExecuteScriptX As Button
    Friend WithEvents ExitMechanical As Button
    Friend WithEvents SaveProject As Button
    Friend WithEvents LoadnEditProject As Button
    Friend WithEvents MeshUpdate As Button
    Friend WithEvents ExecuteAPDL As Button
    Friend WithEvents ExecuteMechanical As Button
    Friend WithEvents SendAPDLCommands As Button
    Friend WithEvents ReadResults As Button
    Friend WithEvents GetAnsys As Button
    Friend WithEvents automatisch As Button
End Class
