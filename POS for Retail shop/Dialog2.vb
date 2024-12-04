Imports System.Windows.Forms

Public Class Dialog2

    Dim text As String

    Public Sub New(ByVal text As String)
        InitializeComponent()
        Me.text = text
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Dialog2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbltext.Text = "- " & text
        Guna2Button1.Select()
    End Sub

End Class
