Imports System.Windows.Forms
Imports System.IO

Public Class pkgprop
    Sub filldata()
        Dim i As Integer
        Dim item As ListViewItem
        details.Items.Clear()

        Me.filename.Text = hdr.name
        If hdr.type = &H1 Then
            item = details.Items.Add("PKG type")
            item.SubItems.Add("0x" & Hex(hdr.type).PadLeft(8, "0"))
            item.SubItems.Add("Debug")
        ElseIf hdr.type = "&H80000001" Then
            item = details.Items.Add("PKG type")
            item.SubItems.Add("0x" & Hex(hdr.type).PadLeft(8, "0"))
            item.SubItems.Add("Retail")
        ElseIf hdr.type = &H2 Then
            item = details.Items.Add("PKG type")
            item.SubItems.Add("0x" & Hex(hdr.type).PadLeft(8, "0"))
            item.SubItems.Add("Debug PSX/PSP")
        ElseIf hdr.type = "&H80000002" Then
            item = details.Items.Add("PKG type")
            item.SubItems.Add("0x" & Hex(hdr.type).PadLeft(8, "0"))
            item.SubItems.Add("Retail PSX/PSP")
        Else
            item = details.Items.Add("PKG type")
            item.SubItems.Add("0x" & Hex(hdr.type).PadLeft(8, "0"))
            item.SubItems.Add("Unknown")
        End If

        item = details.Items.Add("Header size")
        item.SubItems.Add("0x" & Hex(hdr.hdr_size).PadLeft(8, "0"))
        item.SubItems.Add(RoundBytes(hdr.hdr_size))

        item = details.Items.Add("Unknown 1")
        item.SubItems.Add("0x" & Hex(hdr.unk1).PadLeft(8, "0"))
        item.SubItems.Add(hdr.unk1)

        item = details.Items.Add("Section 2 offset")
        item.SubItems.Add("0x" & Hex(hdr.sec2_Offset).PadLeft(8, "0"))
        item.SubItems.Add(hdr.sec2_Offset)

        item = details.Items.Add("Number of files")
        item.SubItems.Add("0x" & Hex(hdr.num_files).PadLeft(8, "0"))
        item.SubItems.Add(hdr.num_files)

        item = details.Items.Add("Filesize")
        item.SubItems.Add("0x" & Hex(hdr.filesize).PadLeft(16, "0"))
        item.SubItems.Add(RoundBytes(hdr.filesize))

        item = details.Items.Add("Data section offset")
        item.SubItems.Add("0x" & Hex(hdr.data_sec_offset).PadLeft(16, "0"))
        item.SubItems.Add(hdr.data_sec_offset)

        item = details.Items.Add("Data size")
        item.SubItems.Add("0x" & Hex(hdr.size_data).PadLeft(16, "0"))
        item.SubItems.Add(RoundBytes(hdr.size_data))
        Me.qa_digest.Text = ""
        For i = 0 To 15
            Me.qa_digest.Text = Me.qa_digest.Text & Hex(hdr.qa_digest(i)).PadLeft(2, "0")
            If i < 15 Then Me.qa_digest.Text = Me.qa_digest.Text & " "
        Next
        Me.sha1.Text = ""
        For i = 0 To 19
            Me.sha1.Text = Me.sha1.Text & Hex(hdr.sha1(i)).PadLeft(2, "0")
            If i < 19 Then Me.sha1.Text = Me.sha1.Text & " "
        Next
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pkgprop_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Main.Enabled = True
    End Sub

    Private Sub pkgprop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        filldata()
    End Sub

End Class
