<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class pkgprop
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(pkgprop))
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.filename = New System.Windows.Forms.TextBox
        Me.qa_digest = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.sha1 = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Items = New System.Windows.Forms.ColumnHeader
        Me.data = New System.Windows.Forms.ColumnHeader
        Me.value = New System.Windows.Forms.ColumnHeader
        Me.details = New System.Windows.Forms.ListView
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(155, 316)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(75, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Content ID:"
        '
        'filename
        '
        Me.filename.Location = New System.Drawing.Point(79, 12)
        Me.filename.Name = "filename"
        Me.filename.ReadOnly = True
        Me.filename.Size = New System.Drawing.Size(294, 20)
        Me.filename.TabIndex = 2
        Me.filename.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'qa_digest
        '
        Me.qa_digest.Location = New System.Drawing.Point(74, 237)
        Me.qa_digest.Name = "qa_digest"
        Me.qa_digest.ReadOnly = True
        Me.qa_digest.Size = New System.Drawing.Size(299, 20)
        Me.qa_digest.TabIndex = 26
        Me.qa_digest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 240)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "QA digest:"
        '
        'sha1
        '
        Me.sha1.Location = New System.Drawing.Point(15, 283)
        Me.sha1.Name = "sha1"
        Me.sha1.ReadOnly = True
        Me.sha1.Size = New System.Drawing.Size(358, 20)
        Me.sha1.TabIndex = 28
        Me.sha1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 267)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 13)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "SHA1:"
        '
        'Items
        '
        Me.Items.Text = ""
        Me.Items.Width = 119
        '
        'data
        '
        Me.data.Text = "Data"
        Me.data.Width = 130
        '
        'value
        '
        Me.value.Text = "Value"
        Me.value.Width = 102
        '
        'details
        '
        Me.details.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Items, Me.data, Me.value})
        Me.details.FullRowSelect = True
        Me.details.GridLines = True
        Me.details.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.details.HoverSelection = True
        Me.details.Location = New System.Drawing.Point(15, 38)
        Me.details.MultiSelect = False
        Me.details.Name = "details"
        Me.details.Size = New System.Drawing.Size(358, 183)
        Me.details.TabIndex = 29
        Me.details.UseCompatibleStateImageBehavior = False
        Me.details.View = System.Windows.Forms.View.Details
        '
        'pkgprop
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(385, 353)
        Me.Controls.Add(Me.details)
        Me.Controls.Add(Me.sha1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.qa_digest)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.filename)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.OK_Button)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "pkgprop"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Pkg properties"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents filename As System.Windows.Forms.TextBox
    Friend WithEvents qa_digest As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents sha1 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Items As System.Windows.Forms.ColumnHeader
    Friend WithEvents data As System.Windows.Forms.ColumnHeader
    Friend WithEvents value As System.Windows.Forms.ColumnHeader
    Friend WithEvents details As System.Windows.Forms.ListView

End Class
