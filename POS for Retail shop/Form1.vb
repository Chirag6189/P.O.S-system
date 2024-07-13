Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        User_Login.MdiParent = Me
        User_Login.StartPosition = FormStartPosition.CenterScreen
        User_Login.Show()
    End Sub
End Class