Imports System.Windows.Forms

Public Class Dialog1

    Dim text As String

    Public Sub New(ByVal text As String, Optional image As Image = Nothing)
        InitializeComponent()
        Me.text = text
        If image IsNot Nothing Then
            PictureBox1.Image = image
        End If
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Dialog1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbltext.Text = "- " & text
    End Sub
End Class
