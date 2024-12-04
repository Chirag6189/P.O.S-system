Imports System.Data.SqlClient

Public Class Product

    Dim name As String
    Dim userId As String
    Dim per As String
    Public Sub New(ByVal name As String, ByVal per As String, ByVal userId As String)
        InitializeComponent()
        Me.name = name
        Me.per = per
        Me.userId = userId
    End Sub

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand

    Private Sub Showproduct(value As String)

        If value = "all" Then
            Dim query As String = "select prod_id, prod_name, qty, prod_mrp ,disc , sale_rate from product"
            Dim cmd As New SqlCommand(query, con)

            Try
                Dim dataAdepter As New SqlDataAdapter(cmd)
                Dim DataSet As New DataSet()
                dataAdepter.Fill(DataSet)

                DataGridView1.DataSource = DataSet.Tables(0)
            Catch ex As Exception
                MessageBox.Show("Error in ShowLowStock Fuction : " & ex.Message)
            End Try
        Else
            Dim query As String = ""

            If searchname.Checked Then
                query = "select prod_id, prod_name, qty, prod_mrp, disc, sale_rate from product where prod_name like @value"
            ElseIf searchMRP.Checked Then
                query = "select prod_id, prod_name, qty, prod_mrp, disc, sale_rate from product where prod_mrp = @value1"
            ElseIf searchcode.Checked Then
                query = "select prod_id, prod_name, qty, prod_mrp, disc, sale_rate from product where prod_id like @value"
            End If
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@value", "%" & value & "%")
            cmd.Parameters.AddWithValue("@value1", value)

            Try
                Dim dataAdepter As New SqlDataAdapter(cmd)
                Dim DataSet As New DataSet()
                dataAdepter.Fill(DataSet)

                DataGridView1.DataSource = DataSet.Tables(0)
            Catch ex As Exception
                MessageBox.Show("Error in show Product : " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub invalue_TextChanged(sender As Object, e As EventArgs) Handles invalue.TextChanged
        Dim value = invalue.Text
        If Not String.IsNullOrEmpty(value) Then
            Showproduct(value)
        Else
            DataGridView1.DataSource = Nothing
        End If
    End Sub

    Private Sub Product_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.DataSource = Nothing
        lbluserId.Text = userId
    End Sub
End Class