Imports System.Text

Public Class About

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim changes As New StringBuilder

        Icon.Image = Main.Icon.ToBitmap
        AppName.Text = Application.ProductName
        Me.Text = "About " & Application.ProductName & " " & Application.ProductVersion.Split(".")(0) & "." & Application.ProductVersion.Split(".")(1)
        Version.Text = "Version " & Application.ProductVersion.Split(".")(0) & "." & Application.ProductVersion.Split(".")(1) & " © 2010-2012 Ifcaro"
        Website.Text = "http://ps3zone.ifcaro.net"

        changes.AppendLine("PkgView Changelog")
        changes.AppendLine("")
        changes.AppendLine("Version 1.3")
        changes.AppendLine("---------------------------------")
        changes.AppendLine("Added support to psx/psp packages.")
        changes.AppendLine("Added Windows 7 Taskbar progress.")
        changes.AppendLine("")
        changes.AppendLine("Version 1.2")
        changes.AppendLine("---------------------------------")
        changes.AppendLine("FIX: Still failing to extract large files.")
        changes.AppendLine("FIX: Structure parsing issues.")
        changes.AppendLine("FIX: Other extraction issues.")
        changes.AppendLine("More descriptive extraction window.")
        changes.AppendLine("Optimized extraction of retail pkg.")
        changes.AppendLine("Now creates an extraction log.")
        changes.AppendLine("")
        changes.AppendLine("Version 1.1")
        changes.AppendLine("---------------------------------")
        changes.AppendLine("FIX: Fails to extract big files")
        changes.AppendLine("")
        changes.AppendLine("Version 1.0")
        changes.AppendLine("---------------------------------")
        changes.AppendLine("FIX: PARAM.SFO doesn't appear")
        changes.AppendLine("FIX: Emprty folders doesn't appear")
        changes.AppendLine("Add support for retail pkg (thanks to Mathieulh's app).")
        changes.AppendLine("Now accepts pkg files passed as parameter.")
        changes.AppendLine("Added support to drag pkg files to the application.")
        changes.AppendLine("Added Extract to source folder option")
        changes.AppendLine("Added .pkg file association")
        changes.AppendLine("Added Extract here at Windows' contextual menu")
        changes.AppendLine("")
        changes.AppendLine("BETA 2")
        changes.AppendLine("---------------------------------")
        changes.AppendLine("FIX: Folders were listed also as files.")
        changes.AppendLine("FIX: Checks if the pkg is debug.")
        changes.AppendLine("Added folders extraction.")
        changes.AppendLine("Improved extraction algorithm.")
        changes.AppendLine("")
        changes.AppendLine("BETA 1")
        changes.AppendLine("---------------------------------")
        changes.AppendLine("First version")

        changelog.Text = changes.ToString
        changelog.Select(0, 0)
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Me.Close()
    End Sub

    Private Sub Website_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Website.LinkClicked
        Shell("cmd /c start " & sender.text)
    End Sub
End Class