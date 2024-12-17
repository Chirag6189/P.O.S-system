Imports System.Data.SqlClient

Public Class Product_list
    Public Property SelectedItemCode As String

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand
    Dim PO_no As Integer

    Public Sub New(ByVal PO_no As Integer)
        InitializeComponent()
        Me.PO_no = PO_no
        RefreshDataGridView()
    End Sub

    Private Sub Product_list_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        invalue.Text = "O.P No = " & PO_no.ToString()
        RefreshDataGridView()
        DataGridView1.Focus()
        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.Rows(0).Selected = True
        End If
    End Sub

    Private Sub RefreshDataGridView()
        Dim q1 As String = "select prod_id, prod_name, qty, received_qty, panding_qty from purchase_order_items where PO_no = @PO_no "
        Dim cmd As New SqlCommand(q1, con)
        cmd.Parameters.AddWithValue("@PO_no", PO_no)
        Dim dataAdapter As New SqlDataAdapter(cmd)
        Dim dataset As New DataSet()
        dataAdapter.Fill(dataset)

        DataGridView1.DataSource = dataset.Tables(0)

    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If DataGridView1.SelectedRows.Count > 0 Then
                Dim rowIndex As Integer = DataGridView1.SelectedRows(0).Index

                SelectedItemCode = DataGridView1.Rows(rowIndex).Cells("prod_id").Value
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub
End Class