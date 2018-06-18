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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.BeginEvolution = New System.Windows.Forms.Button()
        Me.ZielFitness = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MaxGen = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.MaxIndi = New System.Windows.Forms.TextBox()
        Me.AnsStart = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(28, 132)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Cycle"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(28, 90)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 36)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Neues Individuum"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(153, 37)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(367, 20)
        Me.TextBox1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(82, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Arbeitspfad"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(109, 90)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(103, 36)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "Lösche aktuelle Evolution"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'BeginEvolution
        '
        Me.BeginEvolution.Location = New System.Drawing.Point(218, 90)
        Me.BeginEvolution.Name = "BeginEvolution"
        Me.BeginEvolution.Size = New System.Drawing.Size(103, 36)
        Me.BeginEvolution.TabIndex = 6
        Me.BeginEvolution.Text = "Beginne Evolution"
        Me.BeginEvolution.UseVisualStyleBackColor = True
        '
        'ZielFitness
        '
        Me.ZielFitness.Location = New System.Drawing.Point(482, 76)
        Me.ZielFitness.Name = "ZielFitness"
        Me.ZielFitness.Size = New System.Drawing.Size(100, 20)
        Me.ZielFitness.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(396, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Ziel Fitnesswert"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(382, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Max Generationen"
        '
        'MaxGen
        '
        Me.MaxGen.Location = New System.Drawing.Point(482, 102)
        Me.MaxGen.Name = "MaxGen"
        Me.MaxGen.Size = New System.Drawing.Size(100, 20)
        Me.MaxGen.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(353, 131)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Max erzeugte Individuen"
        '
        'MaxIndi
        '
        Me.MaxIndi.Location = New System.Drawing.Point(482, 128)
        Me.MaxIndi.Name = "MaxIndi"
        Me.MaxIndi.Size = New System.Drawing.Size(100, 20)
        Me.MaxIndi.TabIndex = 11
        '
        'AnsStart
        '
        Me.AnsStart.Location = New System.Drawing.Point(28, 197)
        Me.AnsStart.Name = "AnsStart"
        Me.AnsStart.Size = New System.Drawing.Size(103, 36)
        Me.AnsStart.TabIndex = 13
        Me.AnsStart.Text = "Starte Ansys"
        Me.AnsStart.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(740, 265)
        Me.Controls.Add(Me.AnsStart)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.MaxIndi)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.MaxGen)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ZielFitness)
        Me.Controls.Add(Me.BeginEvolution)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents BeginEvolution As Button
    Friend WithEvents ZielFitness As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents MaxGen As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents MaxIndi As TextBox
    Friend WithEvents AnsStart As Button
End Class
