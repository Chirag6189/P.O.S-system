Public Class Receive_Purchase_Order

    Dim userId As String

    Public Sub New(ByVal userId As String)
        InitializeComponent()
        Me.userId = userId
    End Sub

    Private Sub Receive_Purchase_Order_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class