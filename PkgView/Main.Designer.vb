<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.arbol = New System.Windows.Forms.TreeView
        Me.carpetas = New System.Windows.Forms.ImageList(Me.components)
        Me.lista = New System.Windows.Forms.ListView
        Me.Nombre = New System.Windows.Forms.ColumnHeader
        Me.Tamaño = New System.Windows.Forms.ColumnHeader
        Me.Iconos = New System.Windows.Forms.ImageList(Me.components)
        Me.Icopec = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripMenuItem
        Me.AbrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.GuardarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GuardarComoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AssociatepkgFilesWithThisApplicationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AyudaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AcercaDePkgViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.estado = New System.Windows.Forms.ToolStripStatusLabel
        Me.extract = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SourceExtractMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExtraerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.PropiedadesDelPkgToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.noselec = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PropiedadesDelPkgToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.od = New System.Windows.Forms.OpenFileDialog
        Me.sd = New System.Windows.Forms.SaveFileDialog
        Me.FolderBrowser = New System.Windows.Forms.FolderBrowserDialog
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.extract.SuspendLayout()
        Me.noselec.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 49)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.arbol)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lista)
        Me.SplitContainer1.Size = New System.Drawing.Size(578, 324)
        Me.SplitContainer1.SplitterDistance = 191
        Me.SplitContainer1.TabIndex = 3
        '
        'arbol
        '
        Me.arbol.AllowDrop = True
        Me.arbol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.arbol.ImageIndex = 0
        Me.arbol.ImageList = Me.carpetas
        Me.arbol.Location = New System.Drawing.Point(0, 0)
        Me.arbol.Name = "arbol"
        Me.arbol.SelectedImageIndex = 0
        Me.arbol.Size = New System.Drawing.Size(191, 324)
        Me.arbol.TabIndex = 0
        '
        'carpetas
        '
        Me.carpetas.ImageStream = CType(resources.GetObject("carpetas.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.carpetas.TransparentColor = System.Drawing.Color.Transparent
        Me.carpetas.Images.SetKeyName(0, "folderClosed.ico")
        Me.carpetas.Images.SetKeyName(1, "folderOpen.ico")
        '
        'lista
        '
        Me.lista.AllowDrop = True
        Me.lista.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Nombre, Me.Tamaño})
        Me.lista.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lista.LargeImageList = Me.Iconos
        Me.lista.Location = New System.Drawing.Point(0, 0)
        Me.lista.Name = "lista"
        Me.lista.Size = New System.Drawing.Size(383, 324)
        Me.lista.SmallImageList = Me.Icopec
        Me.lista.TabIndex = 0
        Me.lista.UseCompatibleStateImageBehavior = False
        Me.lista.View = System.Windows.Forms.View.Details
        '
        'Nombre
        '
        Me.Nombre.Text = "Name"
        Me.Nombre.Width = 276
        '
        'Tamaño
        '
        Me.Tamaño.Text = "Size"
        Me.Tamaño.Width = 100
        '
        'Iconos
        '
        Me.Iconos.ImageStream = CType(resources.GetObject("Iconos.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Iconos.TransparentColor = System.Drawing.Color.Transparent
        Me.Iconos.Images.SetKeyName(0, "folderOpenbig.ico")
        '
        'Icopec
        '
        Me.Icopec.ImageStream = CType(resources.GetObject("Icopec.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Icopec.TransparentColor = System.Drawing.Color.Transparent
        Me.Icopec.Images.SetKeyName(0, "folderClosed.ico")
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.AyudaToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(578, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox1, Me.AbrirToolStripMenuItem, Me.ToolStripSeparator2, Me.GuardarToolStripMenuItem, Me.GuardarComoToolStripMenuItem, Me.ToolStripSeparator1, Me.SalirToolStripMenuItem})
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.ArchivoToolStripMenuItem.Text = "File"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Enabled = False
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(121, 22)
        Me.ToolStripTextBox1.Text = "New"
        '
        'AbrirToolStripMenuItem
        '
        Me.AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem"
        Me.AbrirToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
        Me.AbrirToolStripMenuItem.Text = "Open..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(118, 6)
        '
        'GuardarToolStripMenuItem
        '
        Me.GuardarToolStripMenuItem.Enabled = False
        Me.GuardarToolStripMenuItem.Name = "GuardarToolStripMenuItem"
        Me.GuardarToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
        Me.GuardarToolStripMenuItem.Text = "Save"
        '
        'GuardarComoToolStripMenuItem
        '
        Me.GuardarComoToolStripMenuItem.Enabled = False
        Me.GuardarComoToolStripMenuItem.Name = "GuardarComoToolStripMenuItem"
        Me.GuardarComoToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
        Me.GuardarComoToolStripMenuItem.Text = "Save as..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(118, 6)
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
        Me.SalirToolStripMenuItem.Text = "Exit"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AssociatepkgFilesWithThisApplicationToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'AssociatepkgFilesWithThisApplicationToolStripMenuItem
        '
        Me.AssociatepkgFilesWithThisApplicationToolStripMenuItem.Name = "AssociatepkgFilesWithThisApplicationToolStripMenuItem"
        Me.AssociatepkgFilesWithThisApplicationToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.AssociatepkgFilesWithThisApplicationToolStripMenuItem.Text = "Associate .pkg files with this application"
        '
        'AyudaToolStripMenuItem
        '
        Me.AyudaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AcercaDePkgViewToolStripMenuItem})
        Me.AyudaToolStripMenuItem.Name = "AyudaToolStripMenuItem"
        Me.AyudaToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.AyudaToolStripMenuItem.Text = "Help"
        '
        'AcercaDePkgViewToolStripMenuItem
        '
        Me.AcercaDePkgViewToolStripMenuItem.Name = "AcercaDePkgViewToolStripMenuItem"
        Me.AcercaDePkgViewToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.AcercaDePkgViewToolStripMenuItem.Text = "About PkgView"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripSeparator3, Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripButton5, Me.ToolStripButton6, Me.ToolStripButton7, Me.ToolStripButton8})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(578, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Enabled = False
        Me.ToolStripButton1.Image = Global.PkgView.My.Resources.Resources.nuevo
        Me.ToolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Silver
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "New"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.PkgView.My.Resources.Resources.abrir
        Me.ToolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Silver
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "Open"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Enabled = False
        Me.ToolStripButton3.Image = Global.PkgView.My.Resources.Resources.guardar
        Me.ToolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Silver
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton3.Text = "Save"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = Global.PkgView.My.Resources.Resources.icono
        Me.ToolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Silver
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton5.Text = "Big icons"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Image = Global.PkgView.My.Resources.Resources.icopec
        Me.ToolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Silver
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton6.Text = "Small icons"
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton7.Image = Global.PkgView.My.Resources.Resources.lista
        Me.ToolStripButton7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Silver
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton7.Text = "List"
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton8.Image = Global.PkgView.My.Resources.Resources.detalles
        Me.ToolStripButton8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Silver
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton8.Text = "Details"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.estado})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 373)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(578, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'estado
        '
        Me.estado.Name = "estado"
        Me.estado.Size = New System.Drawing.Size(0, 17)
        '
        'extract
        '
        Me.extract.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SourceExtractMenuItem, Me.ExtraerToolStripMenuItem, Me.ToolStripSeparator4, Me.PropiedadesDelPkgToolStripMenuItem})
        Me.extract.Name = "context"
        Me.extract.Size = New System.Drawing.Size(196, 76)
        '
        'SourceExtractMenuItem
        '
        Me.SourceExtractMenuItem.Name = "SourceExtractMenuItem"
        Me.SourceExtractMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.SourceExtractMenuItem.Text = "Extract to source folder"
        '
        'ExtraerToolStripMenuItem
        '
        Me.ExtraerToolStripMenuItem.Name = "ExtraerToolStripMenuItem"
        Me.ExtraerToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.ExtraerToolStripMenuItem.Text = "Extract..."
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(192, 6)
        '
        'PropiedadesDelPkgToolStripMenuItem
        '
        Me.PropiedadesDelPkgToolStripMenuItem.Enabled = False
        Me.PropiedadesDelPkgToolStripMenuItem.Name = "PropiedadesDelPkgToolStripMenuItem"
        Me.PropiedadesDelPkgToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.PropiedadesDelPkgToolStripMenuItem.Text = "Pkg properties"
        '
        'noselec
        '
        Me.noselec.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PropiedadesDelPkgToolStripMenuItem1})
        Me.noselec.Name = "noselec"
        Me.noselec.Size = New System.Drawing.Size(151, 26)
        '
        'PropiedadesDelPkgToolStripMenuItem1
        '
        Me.PropiedadesDelPkgToolStripMenuItem1.Enabled = False
        Me.PropiedadesDelPkgToolStripMenuItem1.Name = "PropiedadesDelPkgToolStripMenuItem1"
        Me.PropiedadesDelPkgToolStripMenuItem1.Size = New System.Drawing.Size(150, 22)
        Me.PropiedadesDelPkgToolStripMenuItem1.Text = "Pkg properties"
        '
        'od
        '
        Me.od.FileName = "OpenFileDialog1"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(578, 395)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Main"
        Me.Text = "PkgView v1.3 [Extract only] - (C) 2010-2012 Ifcaro"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.extract.ResumeLayout(False)
        Me.noselec.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents arbol As System.Windows.Forms.TreeView
    Friend WithEvents lista As System.Windows.Forms.ListView
    Friend WithEvents Iconos As System.Windows.Forms.ImageList
    Friend WithEvents carpetas As System.Windows.Forms.ImageList
    Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AbrirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GuardarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GuardarComoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AyudaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AcercaDePkgViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents estado As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents extract As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExtraerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton7 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton8 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Icopec As System.Windows.Forms.ImageList
    Friend WithEvents Nombre As System.Windows.Forms.ColumnHeader
    Friend WithEvents Tamaño As System.Windows.Forms.ColumnHeader
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PropiedadesDelPkgToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents noselec As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PropiedadesDelPkgToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents od As System.Windows.Forms.OpenFileDialog
    Friend WithEvents sd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents FolderBrowser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents SourceExtractMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AssociatepkgFilesWithThisApplicationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
