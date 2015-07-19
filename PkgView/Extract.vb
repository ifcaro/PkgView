Public Class winExtract
    Public timestart As Date
    Public actualsize As UInt64
    Public totalsize As UInt64

    Private Sub cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancel.Click
        Main.Extracting = 0
        Me.Close()
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        Dim timeelapsed, timeleftspan As TimeSpan
        timeelapsed = Now.Subtract(timestart)
        elapsed.Text = String.Format("{0:00}:{1:00}:{2:00}", Math.Truncate(timeelapsed.TotalHours), timeelapsed.Minutes, timeelapsed.Seconds)
        If (actualsize > 0) Then
            Try
                timeleftspan = New TimeSpan(Math.Round(timeelapsed.Ticks * totalsize / actualsize)).Subtract(timeelapsed)
                timeleft.Text = String.Format("{0:00}:{1:00}:{2:00}", Math.Truncate(timeleftspan.TotalHours), timeleftspan.Minutes, timeleftspan.Seconds)
            Catch ex As Exception
                timeleft.Text = "00:00:00"
            End Try
        End If

    End Sub

    Private Sub winExtract_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Timer.Enabled = False
        Main.Extracting = 0
    End Sub
End Class