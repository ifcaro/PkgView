<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class About
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
        Me.Icon = New System.Windows.Forms.PictureBox
        Me.btnOk = New System.Windows.Forms.Button
        Me.AppName = New System.Windows.Forms.Label
        Me.Version = New System.Windows.Forms.Label
        Me.Website = New System.Windows.Forms.LinkLabel
        Me.changelog = New System.Windows.Forms.TextBox
        CType(Me.Icon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Icon
        '
        Me.Icon.Location = New System.Drawing.Point(28, 21)
        Me.Icon.Name = "Icon"
        Me.Icon.Size = New System.Drawing.Size(32, 32)
        Me.Icon.TabIndex = 0
        Me.Icon.TabStop = False
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(155, 248)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 1
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'AppName
        '
        Me.AppName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppName.Location = New System.Drawing.Point(77, 9)
        Me.AppName.Name = "AppName"
        Me.AppName.Size = New System.Drawing.Size(295, 29)
        Me.AppName.TabIndex = 5
        Me.AppName.Text = "Appname"
        Me.AppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Version
        '
        Me.Version.Location = New System.Drawing.Point(77, 38)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(295, 24)
        Me.Version.TabIndex = 6
        Me.Version.Text = "Version 0.0 - © 2010-2012 Ifcaro"
        Me.Version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Website
        '
        Me.Website.Location = New System.Drawing.Point(12, 62)
        Me.Website.Name = "Website"
        Me.Website.Size = New System.Drawing.Size(360, 23)
        Me.Website.TabIndex = 7
        Me.Website.TabStop = True
        Me.Website.Text = "http://www.ifcaro.net"
        Me.Website.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'changelog
        '
        Me.changelog.BackColor = System.Drawing.SystemColors.Control
        Me.changelog.Location = New System.Drawing.Point(12, 93)
        Me.changelog.Multiline = True
        Me.changelog.Name = "changelog"
        Me.changelog.ReadOnly = True
        Me.changelog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.changelog.Size = New System.Drawing.Size(360, 149)
        Me.changelog.TabIndex = 3
        '
        'About
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 283)
        Me.Controls.Add(Me.Website)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.AppName)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.changelog)
        Me.Controls.Add(Me.Icon)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "About"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "About"
        CType(Me.Icon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Icon As System.Windows.Forms.PictureBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents AppName As System.Windows.Forms.Label
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents Website As System.Windows.Forms.LinkLabel
    Friend WithEvents changelog As System.Windows.Forms.TextBox
End Class
