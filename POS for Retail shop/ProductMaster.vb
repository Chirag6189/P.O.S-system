Imports System.Data.SqlClient

Public Class ProductMaster

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand
    Dim userId As String

    Public Sub New(ByVal userId As String)
        InitializeComponent()
        Me.userId = userId
    End Sub

    Private Sub ProductMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.BringToFront()
    End Sub

    Private Sub Showproduct(value As String)

        If value = "all" Then
            Dim query As String = "select * from product"
            Dim cmd As New SqlCommand(query, con)

            Try
                Dim dataAdepter As New SqlDataAdapter(cmd)
                Dim DataSet As New DataSet()
                dataAdepter.Fill(DataSet)

                DataGridView1.DataSource = DataSet.Tables(0)
            Catch ex As Exception
                Dim dilog As New Dialog1("Error in Showproduct Fuction : " & ex.Message, My.Resources.icons8_error_48)
                dilog.Text = "Error"
                dilog.Show()
            End Try
        Else
            Dim query As String = ""

            If searchname.Checked Then
                query = "select * from product where prod_name like @value"
            ElseIf searchMRP.Checked Then
                query = "select * from product where prod_mrp = @value1"
            ElseIf searchcode.Checked Then
                query = "select * from product where prod_id like @value"
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
                Dim dilog As New Dialog1("Error in Showproduct Fuction : " & ex.Message, My.Resources.icons8_error_48)
                dilog.Text = "Error"
                dilog.Show()
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

    Private Sub ShowLowStock()
        Dim query As String = "SELECT * FROM [dbo].[product] WHERE qty <= reorder_level;"
        Dim cmd As New SqlCommand(query, con)

        Try
            Dim dt As New DataTable()
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            Dim dilog As New Dialog1("Error in ShowLowStock Fuction : " & ex.Message, My.Resources.icons8_error_48)
            dilog.Text = "Error"
            dilog.Show()
        End Try
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs)
        ShowLowStock()
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Dim dilog As New Add_Product()
        dilog.Show()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim mdidorm As New Generate_purchase_order(userId)
        mdidorm.Show()
        mdidorm.WindowState = FormWindowState.Maximized
        mdidorm.BringToFront()
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Dim mdidorm As New Receive_Purchase_Order(userId)
        mdidorm.Show()
        mdidorm.WindowState = FormWindowState.Maximized
        mdidorm.BringToFront()
    End Sub
End Class