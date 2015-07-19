Option Explicit On
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Imports System.IO

Public Class Main
    Private Structure SHFILEINFO
        Public hIcon As IntPtr            ' : icon
        Public iIcon As Integer           ' : icondex
        Public dwAttributes As Integer    ' : SFGAO_ flags
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
        Public szTypeName As String
    End Structure

    Private Declare Auto Function SHGetFileInfo Lib "shell32.dll" _
            (ByVal pszPath As String, _
             ByVal dwFileAttributes As Integer, _
             ByRef psfi As SHFILEINFO, _
             ByVal cbFileInfo As Integer, _
             ByVal uFlags As Integer) As IntPtr

    Private Const SHGFI_ICON = &H100
    Private Const SHGFI_SMALLICON = &H1
    Private Const SHGFI_LARGEICON = &H0    ' Large icon
    Private Const SHGFI_USEFILEATTRIBUTES = &H10
    Private nIndex = 1
    Dim dearbol = 0
    Dim FileOpen = 0
    Public Extracting = 0
    Dim TaskbarProgress As Windows7ProgressBar

    Dim PkgPath As String
    Dim fd As FileStream
    Public Cabinet As List(Of Archivo)

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cabinet = New List(Of Archivo)
        Dim args As String()

        TaskbarProgress = New Windows7ProgressBar(Me)

        If Command.StartsWith("-ex ") Then
            args = Command.Replace("-ex ", "").Replace(""" """, "|").Replace("""", "").Split("|")
            Me.CenterToScreen()
            LoadPkg(args(0))

            dearbol = 1
            extraer(System.IO.Path.GetDirectoryName(PkgPath))
            End
        Else
            args = Command.Replace(""" """, "|").Replace("""", "").Split("|")

            If (Command.Length > 0) Then
                LoadPkg(args(0))

            End If
        End If

        Me.Show()

        estado.Text = "http://ps3zone.ifcaro.net"
    End Sub

    Sub pkgDirReg(ByVal pkgdir As Byte(), ByRef pkgdir_offset As Long, ByRef name_offset As UInt32, ByVal name As String, ByRef file_offset As UInt32, ByVal file_size As UInt32, ByVal type As UInt32)
        Dim i As Integer

        dword_to_byte(name_offset, pkgdir(pkgdir_offset + 0), pkgdir(pkgdir_offset + 1), pkgdir(pkgdir_offset + 2), pkgdir(pkgdir_offset + 3)) ' name_offset
        dword_to_byte(Len(name), pkgdir(pkgdir_offset + 4), pkgdir(pkgdir_offset + 5), pkgdir(pkgdir_offset + 6), pkgdir(pkgdir_offset + 7)) ' name_length
        dword_to_byte(0, pkgdir(pkgdir_offset + 8), pkgdir(pkgdir_offset + 9), pkgdir(pkgdir_offset + 10), pkgdir(pkgdir_offset + 11)) ' unk
        dword_to_byte(file_offset, pkgdir(pkgdir_offset + 12), pkgdir(pkgdir_offset + 13), pkgdir(pkgdir_offset + 14), pkgdir(pkgdir_offset + 15)) ' file_offset
        dword_to_byte(0, pkgdir(pkgdir_offset + 16), pkgdir(pkgdir_offset + 17), pkgdir(pkgdir_offset + 18), pkgdir(pkgdir_offset + 19)) ' unk2
        dword_to_byte(file_size, pkgdir(pkgdir_offset + 20), pkgdir(pkgdir_offset + 21), pkgdir(pkgdir_offset + 22), pkgdir(pkgdir_offset + 23)) ' file_size
        dword_to_byte(type, pkgdir(pkgdir_offset + 24), pkgdir(pkgdir_offset + 25), pkgdir(pkgdir_offset + 26), pkgdir(pkgdir_offset + 27)) ' type
        dword_to_byte(0, pkgdir(pkgdir_offset + 28), pkgdir(pkgdir_offset + 29), pkgdir(pkgdir_offset + 30), pkgdir(pkgdir_offset + 31)) ' unk3

        For i = 0 To Len(name) - 1
            pkgdir(name_offset + i) = Asc(Mid(name, i + 1, 1))
        Next

        pkgdir_offset = pkgdir_offset + 32
        file_offset = file_offset + file_size
        name_offset = name_offset + Len(name)

        While file_offset Mod 16 <> 0
            file_offset = file_offset + 1
        End While

        While name_offset Mod 16 <> 0
            name_offset = name_offset + 1
        End While
    End Sub

    Sub LoadPkg(Optional ByVal path As String = "")

        If path = "" Then
            od.FileName = ""
            od.Filter = "PS3 Pkg Files (*.pkg)|*.pkg"

            If od.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            path = od.FileName
        End If

        Cabinet.Clear()
        arbol.Nodes.Clear()
        lista.Items.Clear()

        Try
            fd.Close()
        Catch
        End Try

        PkgPath = ""

        fd = File.Open(path, FileMode.Open, FileAccess.ReadWrite)

        hdr = ReadPkgHeader(fd)

        If hdr.magic(0) = &H7F And hdr.magic(1) = Asc("P") And hdr.magic(2) = Asc("K") And hdr.magic(3) = Asc("G") Then
            Dim partes() As String
            Dim folder As String

            If hdr.type <> 1 And hdr.type <> "&H80000001" And hdr.type <> "&H80000002" Then
                MsgBox("This pkg is not supported.", MsgBoxStyle.Exclamation)
                fd.Close()
                Exit Sub
            End If

            fseek(fd, hdr.data_sec_offset)

            PropiedadesDelPkgToolStripMenuItem.Enabled = True
            PropiedadesDelPkgToolStripMenuItem1.Enabled = True

            partes = Split(hdr.name, "-")
            folder = Mid(partes(1), 1, Len(partes(1)) - 3)

            FileOpen = 1

            readcontent(fd, folder)

            getfolders(folder)
            readfolder(folder)

            pkgprop.filldata()

        Else
            MsgBox("This file is not supported.", MsgBoxStyle.Exclamation)
        End If

        PkgPath = System.IO.Path.GetFullPath(path)

    End Sub

    Sub AddFolder(ByVal name As String, Optional ByVal node As String = "")
        Dim carpetas() As String
        Dim path As String
        Dim existe As Boolean
        If (node = "") Then
            For i = 0 To arbol.Nodes.Count - 1
                If arbol.Nodes(i).Text = name Then
                    Exit Sub
                End If
            Next

            arbol.Nodes.Add(name, name)
        Else
            carpetas = Split(node, "\")
            For i = 1 To carpetas.Length
                Try
                    path = ""
                    For j = 0 To i - 1
                        If j > 0 Then
                            path = path & "\" & carpetas(j)
                        Else
                            path = carpetas(j)
                        End If
                    Next

                    Dim arr As TreeNode() = arbol.Nodes.Find(path, True)

                    If i < carpetas.Length Then
                        path = carpetas(i)
                    Else
                        path = name
                    End If

                    existe = False

                    If arr.Length > 0 Then

                        For j = 0 To arr(0).Nodes.Count - 1
                            If arr(0).Nodes(j).Text = path Then
                                existe = True
                            End If
                        Next

                    End If

                    If existe = False Then
                        arr(0).Nodes.Add(arr(0).FullPath & "\" & path, path)
                        arr(0).Expand()
                    End If
                Catch

                End Try
            Next
        End If
    End Sub

    Sub AddFile(ByVal name As String, ByVal size As Long)
        Dim shinfo As SHFILEINFO
        Dim oreg As New ListViewItem
        Dim arr() As String
        Dim ext As String
        Dim i As Integer
        Dim exists As Boolean

        shinfo = New SHFILEINFO()
        shinfo.szDisplayName = New String(Chr(0), 260)
        shinfo.szTypeName = New String(Chr(0), 80)

        arr = name.Split(".")
        ext = LCase("." & arr(arr.Count - 1))

        If size = -1 Then
            lista.Items.Add(name, 0)
        Else
            exists = False
            For i = 1 To Iconos.Images.Count - 1
                If (Iconos.Images.Keys(i) = ext) Then
                    oreg = lista.Items.Add(name, i)
                    oreg.SubItems.Add(RoundBytes(size))
                    exists = True
                    Exit For
                End If
            Next

            If (exists = False) Then
                SHGetFileInfo(ext, 0, shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON Or SHGFI_LARGEICON Or SHGFI_USEFILEATTRIBUTES)

                Dim myIcon As System.Drawing.Icon
                myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon)

                Iconos.Images.Add(LCase(ext), myIcon)

                SHGetFileInfo(ext, 0, shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON Or SHGFI_SMALLICON Or SHGFI_USEFILEATTRIBUTES)

                myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon)

                Icopec.Images.Add(LCase(ext), myIcon)

                oreg = lista.Items.Add(name, nIndex)
                oreg.SubItems.Add(RoundBytes(size))

                nIndex = nIndex + 1
            Else

            End If
        End If

    End Sub

    Sub getfolders(ByVal origin As String)
        AddFolder(origin)
        arbol.SelectedNode = arbol.Nodes(0)

        For Each r As Archivo In Cabinet
            If ((r.Type And &HFF) = "&h04") Then
                AddFolder(r.Name, r.Folder)
            End If
        Next
    End Sub

    Sub readfolder(ByVal name As String)
        Dim files As Integer = 0
        Dim folders As Integer = 0
        lista.Items.Clear()

        Dim sublist As List(Of Archivo)
        Dim i As Integer

        Try
            Dim arr As TreeNode() = arbol.Nodes.Find(name, True)

            For i = 0 To arr(0).Nodes.Count - 1
                AddFile(arr(0).Nodes(i).Text, -1)
                folders = folders + 1
            Next
        Catch

        End Try

        sublist = Cabinet.FindAll(Function(b As Archivo) b.Folder = name)
        For Each r As Archivo In sublist
            If (r.Type And &HFF) <> "&h04" Then
                AddFile(r.Name, r.Size)
                files = files + 1
            End If
        Next

        estado.Text = ""
        If files = 1 Then
            estado.Text = files & " file "
        ElseIf files > 0 Then
            estado.Text = files & " files "
        End If

        If estado.Text <> "" And folders > 0 Then
            estado.Text = estado.Text & "and "
        End If

        If folders = 1 Then
            estado.Text = estado.Text & folders & " folder"
        ElseIf folders > 0 Then
            estado.Text = estado.Text & folders & " folders"
        End If

    End Sub

    Function findFile(ByVal item As String, ByVal argument As String) As Boolean
        Return item.StartsWith(argument)
    End Function

    Private Sub arbol_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles arbol.AfterCollapse
        e.Node.ImageIndex = 0
        e.Node.SelectedImageIndex = 0
    End Sub

    Private Sub arbol_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles arbol.AfterExpand
        e.Node.ImageIndex = 1
        e.Node.SelectedImageIndex = 1
    End Sub

    Private Sub arbol_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles arbol.AfterSelect
        readfolder(arbol.SelectedNode.FullPath)
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        lista.View = View.LargeIcon
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        lista.View = View.SmallIcon
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton7.Click
        lista.View = View.List
    End Sub

    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton8.Click
        lista.View = View.Details
    End Sub

    Private Sub lista_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lista.DoubleClick
        Dim i As Integer
        If lista.SelectedItems.Count = 1 Then
            If lista.SelectedItems(0).ImageIndex = 0 Then
                For i = 0 To arbol.SelectedNode.Nodes.Count - 1
                    If arbol.SelectedNode.Nodes(i).Text = lista.SelectedItems(0).Text Then
                        arbol.SelectedNode = arbol.SelectedNode.Nodes(i)
                        readfolder(arbol.SelectedNode.FullPath)
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub


    Private Sub lista_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lista.DragDrop
        Dim MyFiles() As String
        ' Assign the files to an array.
        MyFiles = e.Data.GetData(DataFormats.FileDrop)

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            LoadPkg(MyFiles(0))
        End If
    End Sub

    Private Sub lista_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lista.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub lista_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lista.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right And FileOpen = 1 Then
            If (lista.SelectedItems.Count > 0) Then
                extract.Show(lista, e.X, e.Y)
            End If
        End If
    End Sub

    Private Sub lista_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lista.MouseUp
        If lista.SelectedItems.Count = 0 And e.Button = Windows.Forms.MouseButtons.Right And FileOpen = 1 Then
            noselec.Show(lista, e.X, e.Y)
        End If
    End Sub


    Private Sub lista_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles lista.Resize
        lista.Columns(0).Width = lista.Width - lista.Columns(1).Width - 10
    End Sub

    Sub extraer(Optional ByVal SelectedPath As String = "")
        Dim i, j As Integer
        Dim extrac As Boolean = True
        Dim path, pkgname As String
        Dim log As FileStream
        Dim logsw As StreamWriter

        winExtract.timestart = Now()
        winExtract.Timer.Enabled = True


        pkgname = System.IO.Path.GetFileNameWithoutExtension(PkgPath)

        If lista.SelectedItems.Count > 0 Or dearbol = 1 Then
            If SelectedPath.Equals("") Then
                If FolderBrowser.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
                SelectedPath = FolderBrowser.SelectedPath
            End If


            SelectedPath = SelectedPath & "\" & pkgname

            Directory.CreateDirectory(SelectedPath)

            log = New FileStream(SelectedPath & "\" & pkgname & ".log", FileMode.Create)
            logsw = New StreamWriter(log)

            Me.Enabled = False
            winExtract.Left = Me.Left + (Me.Width / 2) - winExtract.Width / 2
            winExtract.Top = Me.Top + (Me.Height / 2) - winExtract.Height / 2
            winExtract.Show()
        End If

        If dearbol = 1 Then
            For i = 0 To lista.Items.Count - 1
                lista.Items(i).Selected = True
            Next
        End If

        winExtract.actualsize = 0
        winExtract.totalsize = 0

        For i = 0 To lista.SelectedItems.Count - 1
            Dim sublist As List(Of Archivo)

            sublist = Cabinet.FindAll(Function(b As Archivo) b.Name = lista.SelectedItems(i).Text And b.Folder = arbol.SelectedNode.FullPath)

            If sublist.Count > 0 And (sublist(0).Type And &HFF) <> "&h04" Then

                winExtract.totalsize += sublist(0).Size
            Else
                sublist = Cabinet.FindAll(Function(b As Archivo) Mid(b.Folder, 1, Len(arbol.SelectedNode.FullPath & "\" & lista.SelectedItems(i).Text)) = arbol.SelectedNode.FullPath & "\" & lista.SelectedItems(i).Text)

                If sublist.Count > 0 Then
                    For j = 0 To sublist.Count - 1
                        winExtract.totalsize += sublist(j).Size
                    Next
                End If
            End If
            If extrac = False Then
                Exit For
            End If
        Next

        winExtract.total.Value = 0
        winExtract.total.Maximum = lista.SelectedItems.Count
        TaskbarProgress.Value = 0
        TaskbarProgress.Maximum = lista.SelectedItems.Count

        TaskbarProgress.State = ProgressBarState.Normal
        TaskbarProgress.ShowInTaskbar = True

        For i = 0 To lista.SelectedItems.Count - 1
            Dim sublist As List(Of Archivo)

            sublist = Cabinet.FindAll(Function(b As Archivo) b.Name = lista.SelectedItems(i).Text And b.Folder = arbol.SelectedNode.FullPath)

            If (sublist(0).Type And &HFF) = "&h04" Then
                If Directory.Exists(SelectedPath & "\" & lista.SelectedItems(i).Text) = False Then
                    Directory.CreateDirectory(SelectedPath & "\" & sublist(0).Folder & "\" & lista.SelectedItems(i).Text)
                End If
            End If

            If sublist.Count > 0 And (sublist(0).Type And &HFF) <> "&h04" Then
                extrac = extractfile(SelectedPath & "\" & sublist(0).Folder, lista.SelectedItems(i).Text, sublist(0).Offset, sublist(0).Size, sublist(0).Type)
                If extrac = True Then
                    If logsw IsNot Nothing Then logsw.WriteLine("[" & Now.ToString("hh:mm:ss") & "] " & sublist(0).Folder & "\" & lista.SelectedItems(i).Text & " " & sublist(0).Size.ToString("#,#") & " bytes OK")
                    winExtract.total.Increment(1)
                    TaskbarProgress.Value = winExtract.total.Value
                Else
                    If logsw IsNot Nothing Then logsw.WriteLine("[" & Now.ToString("hh:mm:ss") & "] " & sublist(0).Folder & "\" & lista.SelectedItems(i).Text & " FAILED")
                End If
            Else
                sublist = Cabinet.FindAll(Function(b As Archivo) Mid(b.Folder, 1, Len(arbol.SelectedNode.FullPath & "\" & lista.SelectedItems(i).Text)) = arbol.SelectedNode.FullPath & "\" & lista.SelectedItems(i).Text)

                If sublist.Count > 0 Then
                    winExtract.total.Maximum = winExtract.total.Maximum + sublist.Count
                    TaskbarProgress.Maximum = winExtract.total.Maximum
                    For j = 0 To sublist.Count - 1
                        path = SelectedPath & "\" & sublist(j).Folder
                        If Directory.Exists(path) = False Then
                            Directory.CreateDirectory(path)
                        End If
                        If (sublist(j).Type And &HFF) <> "&h04" Then
                            extrac = extractfile(path, sublist(j).Name, sublist(j).Offset, sublist(j).Size, sublist(j).Type)
                            If extrac = True Then
                                If logsw IsNot Nothing Then logsw.WriteLine("[" & Now.ToString("hh:mm:ss") & "] " & path.Replace(SelectedPath & "\", "") & "\" & sublist(j).Name & " " & sublist(j).Size.ToString("#,#") & " bytes OK")
                                winExtract.total.Increment(1)
                                TaskbarProgress.Value = winExtract.total.Value
                                winExtract.pertotal.Text = Math.Round(winExtract.total.Value / winExtract.total.Maximum * 100) & "%"
                            Else
                                If logsw IsNot Nothing Then logsw.WriteLine("[" & Now.ToString("hh:mm:ss") & "] " & path.Replace(SelectedPath & "\", "") & "\" & sublist(j).Name & " FAILED")
                                Exit For
                            End If
                        Else
                            Directory.CreateDirectory(path & "\" & sublist(j).Name)
                        End If
                    Next
                End If
            End If
            If extrac = False Then
                Exit For
            End If
        Next
        TaskbarProgress.ShowInTaskbar = False
        If dearbol = 1 Then
            dearbol = 0
        End If
        If extrac = True Then
            logsw.WriteLine("[" & Now.ToString("hh:mm:ss") & "] Extraction completed.")
            Extracting = 0
            winExtract.Close()
            Me.Enabled = True
        Else
            logsw.WriteLine("[" & Now.ToString("hh:mm:ss") & "] Extraction cancelled.")
        End If
        If logsw IsNot Nothing Then logsw.Close()
        If log IsNot Nothing Then log.Close()
    End Sub

    Function extractfile(ByVal path As String, ByVal name As String, ByVal offset As UInt64, ByVal size As UInt64, ByVal type As UInt32) As Boolean
        Dim buf() As Byte
        Dim extra As FileStream
        Dim j As UInt64

        If Directory.Exists(path) = False Then
            Directory.CreateDirectory(path)
        End If

        extra = File.Open(path & "\" & name, FileMode.Create, FileAccess.Write)

        Extracting = 1
        winExtract.file.Text = name
        winExtract.action.Text = "Seeking..."
        Application.DoEvents()

        AESEngine.free()

        fseek(fd, hdr.data_sec_offset + offset)
        winExtract.action.Text = "Decrypting..."

        If (size > Integer.MaxValue) Then
            winExtract.progress.Maximum = size / 1048576
        Else
            winExtract.progress.Maximum = size
        End If
        winExtract.progress.Value = 0

        If size >= 512 Then
            For j = 0 To size - 512 Step 512
                buf = decrypt(fd, 32, type And "&HFF000000")
                extra.Write(buf, 0, 512)
                If Extracting = 1 Then
                    If (size > Integer.MaxValue) Then
                        winExtract.progress.Value = j / 1048576
                    Else
                        winExtract.progress.Value = j
                    End If

                    winExtract.perfile.Text = Math.Round(winExtract.progress.Value / winExtract.progress.Maximum * 100) & "%"
                    winExtract.actualsize += 512
                    Application.DoEvents()
                Else
                    Me.Enabled = True
                    MsgBox("Extract cancelled", MsgBoxStyle.Exclamation)
                    extra.Close()
                    Return False
                    Exit Function
                End If
            Next
        End If
        Dim siz As UInt64

        If j < size Then
            siz = size - j

            buf = decrypt(fd, ((siz) / 16), type And "&HFF000000")

            If buf.Length < siz Then
                extra.Write(buf, 0, buf.Length)
                siz = siz - buf.Length
                buf = decrypt(fd, 1)
                extra.Write(buf, 0, siz)
            Else
                extra.Write(buf, 0, siz)
            End If
            winExtract.actualsize += siz

        End If
        winExtract.progress.Value = winExtract.progress.Maximum
        Application.DoEvents()
        extra.Close()
        Return True
    End Function

    Private Sub ExtraerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExtraerToolStripMenuItem.Click
        extraer()
    End Sub


    Private Sub arbol_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles arbol.DragDrop
        lista_DragDrop(sender, e)
    End Sub

    Private Sub arbol_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles arbol.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub arbol_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles arbol.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right And FileOpen = 1 Then
            Dim node As Windows.Forms.TreeNode

            node = arbol.GetNodeAt(e.X, e.Y)

            If node Is Nothing Then Exit Sub

            arbol.SelectedNode = node

            dearbol = 1
            extract.Show(arbol, e.X, e.Y)
        End If
    End Sub

    Private Sub arbol_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles arbol.MouseUp
        If FileOpen = 1 Then
            Dim node As Windows.Forms.TreeNode

            node = arbol.GetNodeAt(e.X, e.Y)

            If node Is Nothing And e.Button = Windows.Forms.MouseButtons.Right Then
                noselec.Show(arbol, e.X, e.Y)
            End If
        End If
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        LoadPkg()
    End Sub

    Private Sub PropiedadesDelPkgToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropiedadesDelPkgToolStripMenuItem1.Click
        pkgprop.Show()
    End Sub

    Private Sub PropiedadesDelPkgToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropiedadesDelPkgToolStripMenuItem.Click
        PropiedadesDelPkgToolStripMenuItem1_Click(sender, e)
    End Sub

    Private Sub AbrirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbrirToolStripMenuItem.Click
        LoadPkg()
    End Sub

    Private Sub AcercaDePkgViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcercaDePkgViewToolStripMenuItem.Click
        About.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SourceExtractMenuItem.Click
        extraer(System.IO.Path.GetDirectoryName(PkgPath))

    End Sub

    Private Sub AssociatepkgFilesWithThisApplicationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssociatepkgFilesWithThisApplicationToolStripMenuItem.Click
        UtilRegistry.DesasociarExtension(".pkg")
        UtilRegistry.AsociarExtension(".pkg", Application.ExecutablePath, "PS3 Installation file", "open", "PS3 Installation", Application.ExecutablePath & ",1")
        UtilRegistry.AsociarExtension(".pkg", Application.ExecutablePath, "-ex ", "PS3 Installation file", "Extract PS3 PKG here...", "Extract here", Application.ExecutablePath & ",1")
    End Sub
End Class
