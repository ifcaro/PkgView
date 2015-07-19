<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winExtract
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(winExtract))
        Me.file = New System.Windows.Forms.Label
        Me.progress = New System.Windows.Forms.ProgressBar
        Me.cancel = New System.Windows.Forms.Button
        Me.total = New System.Windows.Forms.ProgressBar
        Me.action = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.timeleft = New System.Windows.Forms.Label
        Me.elapsed = New System.Windows.Forms.Label
        Me.perfile = New System.Windows.Forms.Label
        Me.pertotal = New System.Windows.Forms.Label
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'file
        '
        Me.file.Location = New System.Drawing.Point(12, 26)
        Me.file.Name = "file"
        Me.file.Size = New System.Drawing.Size(222, 17)
        Me.file.TabIndex = 0
        Me.file.Text = "Filename"
        '
        'progress
        '
        Me.progress.Location = New System.Drawing.Point(12, 46)
        Me.progress.Name = "progress"
        Me.progress.Size = New System.Drawing.Size(222, 14)
        Me.progress.TabIndex = 1
        '
        'cancel
        '
        Me.cancel.Location = New System.Drawing.Point(86, 132)
        Me.cancel.Name = "cancel"
        Me.cancel.Size = New System.Drawing.Size(75, 23)
        Me.cancel.TabIndex = 2
        Me.cancel.Text = "Cancel"
        Me.cancel.UseVisualStyleBackColor = True
        '
        'total
        '
        Me.total.Location = New System.Drawing.Point(12, 102)
        Me.total.Name = "total"
        Me.total.Size = New System.Drawing.Size(222, 14)
        Me.total.TabIndex = 3
        '
        'action
        '
        Me.action.Location = New System.Drawing.Point(12, 9)
        Me.action.Name = "action"
        Me.action.Size = New System.Drawing.Size(222, 17)
        Me.action.TabIndex = 4
        Me.action.Text = "Action"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Elapsed time:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Time left:"
        '
        'timeleft
        '
        Me.timeleft.Location = New System.Drawing.Point(108, 86)
        Me.timeleft.Name = "timeleft"
        Me.timeleft.Size = New System.Drawing.Size(53, 13)
        Me.timeleft.TabIndex = 8
        Me.timeleft.Text = "00:00:00"
        Me.timeleft.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'elapsed
        '
        Me.elapsed.Location = New System.Drawing.Point(108, 68)
        Me.elapsed.Name = "elapsed"
        Me.elapsed.Size = New System.Drawing.Size(53, 13)
        Me.elapsed.TabIndex = 7
        Me.elapsed.Text = "00:00:00"
        Me.elapsed.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'perfile
        '
        Me.perfile.Location = New System.Drawing.Point(189, 26)
        Me.perfile.Name = "perfile"
        Me.perfile.Size = New System.Drawing.Size(45, 17)
        Me.perfile.TabIndex = 9
        Me.perfile.Text = "0%"
        Me.perfile.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'pertotal
        '
        Me.pertotal.Location = New System.Drawing.Point(189, 82)
        Me.pertotal.Name = "pertotal"
        Me.pertotal.Size = New System.Drawing.Size(45, 17)
        Me.pertotal.TabIndex = 10
        Me.pertotal.Text = "0%"
        Me.pertotal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Timer
        '
        Me.Timer.Interval = 1000
        '
        'winExtract
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(246, 167)
        Me.Controls.Add(Me.pertotal)
        Me.Controls.Add(Me.perfile)
        Me.Controls.Add(Me.timeleft)
        Me.Controls.Add(Me.elapsed)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.action)
        Me.Controls.Add(Me.total)
        Me.Controls.Add(Me.cancel)
        Me.Controls.Add(Me.progress)
        Me.Controls.Add(Me.file)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "winExtract"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Extracting..."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents file As System.Windows.Forms.Label
    Friend WithEvents progress As System.Windows.Forms.ProgressBar
    Friend WithEvents cancel As System.Windows.Forms.Button
    Friend WithEvents total As System.Windows.Forms.ProgressBar
    Friend WithEvents action As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents timeleft As System.Windows.Forms.Label
    Friend WithEvents elapsed As System.Windows.Forms.Label
    Friend WithEvents perfile As System.Windows.Forms.Label
    Friend WithEvents pertotal As System.Windows.Forms.Label
    Friend WithEvents Timer As System.Windows.Forms.Timer
End Class
