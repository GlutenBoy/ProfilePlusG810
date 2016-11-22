Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        If LogiLedInit() Then
            LogitechLED.LogiLedSaveCurrentLighting()
        Else
            MessageBox.Show("Error in LogiLedInit!")
            End
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LogitechLED.LogiLedSetLighting(50, 15, 2)
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        LogitechLED.LogiLedShutdown()
    End Sub
End Class
