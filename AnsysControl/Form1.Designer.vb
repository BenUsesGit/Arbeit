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
        Me.ReadScript.Size = New System.Drawing.Size(75, 36)
        Me.ReadScript.TabIndex = 3
        Me.ReadScript.Text = "Skript auslesen"
        Me.ReadScript.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
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
End Class
