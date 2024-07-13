Imports System.Data.SqlClient

Public Class Activitys

    Dim name As String
    Dim userId As String
    Dim per As String

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand

    Public Sub New(ByVal name As String, ByVal per As String, ByVal userId As String)
        InitializeComponent()
        Me.name = name
        Me.per = per
        Me.userId = userId
    End Sub

    Private Sub Activitys_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class