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
        Me.components = New System.ComponentModel.Container()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Button4 = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.ListBox4 = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(36, 39)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(127, 38)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Draw Spline"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(36, 93)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(127, 41)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Alles Löschen"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(36, 277)
        Me.Button3.Margin = New System.Windows.Forms.Padding(2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(127, 43)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Info über Art der markierten Objekte"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(36, 354)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(139, 175)
        Me.TextBox1.TabIndex = 3
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(36, 156)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(127, 42)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "Elemente anzeigen"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(211, 51)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(103, 17)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "Zufällige Punkte"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(36, 216)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(127, 42)
        Me.Button5.TabIndex = 6
        Me.Button5.Text = "Erhebung schaffen"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Checked = True
        Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox2.Location = New System.Drawing.Point(211, 106)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(98, 17)
        Me.CheckBox2.TabIndex = 7
        Me.CheckBox2.Text = "Kontrollpolygon"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(315, 106)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(84, 17)
        Me.CheckBox3.TabIndex = 8
        Me.CheckBox3.Text = "Interpolation"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(182, 277)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(127, 42)
        Me.Button6.TabIndex = 9
        Me.Button6.Text = "Info über markierten Spline"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(200, 356)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ListBox1.Size = New System.Drawing.Size(66, 173)
        Me.ListBox1.TabIndex = 10
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(272, 356)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ListBox2.Size = New System.Drawing.Size(66, 173)
        Me.ListBox2.TabIndex = 11
        '
        'ListBox3
        '
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.Location = New System.Drawing.Point(360, 355)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ListBox3.Size = New System.Drawing.Size(66, 173)
        Me.ListBox3.TabIndex = 12
        '
        'ListBox4
        '
        Me.ListBox4.FormattingEnabled = True
        Me.ListBox4.Location = New System.Drawing.Point(432, 356)
        Me.ListBox4.Name = "ListBox4"
        Me.ListBox4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ListBox4.Size = New System.Drawing.Size(66, 173)
        Me.ListBox4.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 339)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Allgemeine Spline Info"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(197, 322)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(141, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Punkte des Kontrollpolygons"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(252, 339)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(14, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "X"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(324, 339)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 13)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Y"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(484, 339)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Y"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(412, 339)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(14, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "X"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(357, 322)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "InterpolationsPunkte"
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(602, 39)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(77, 38)
        Me.Button7.TabIndex = 21
        Me.Button7.Text = "Öffne Inventor"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(694, 39)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(73, 38)
        Me.Button8.TabIndex = 22
        Me.Button8.Text = "Öffne Matlab"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(182, 216)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(127, 42)
        Me.Button9.TabIndex = 23
        Me.Button9.Text = "Spline > Matlab"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1015, 540)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox4)
        Me.Controls.Add(Me.ListBox3)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents Button4 As Button
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Button5 As Button
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents Button6 As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents ListBox3 As ListBox
    Friend WithEvents ListBox4 As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
End Class
