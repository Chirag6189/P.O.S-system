Imports System.Data.SqlClient
Imports System.Web
Imports System.Windows.Forms

Public Class Add_Product

    Dim prod_id As String
    Dim prod_name As String
    Dim product_barcode As String
    Dim reorder_level As String
    Dim supplier_id As String
    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand

    Private Sub get_prod_id()
        Dim q1 As String = "select no from Number where fild_name = 'prod_id'"
        Dim command As New SqlCommand(q1, con)
        con.Open()
        prod_id = command.ExecuteScalar()
        con.Close()
    End Sub

    Private Sub updateProdId()
        Dim q1 As String = "update Number set no = no+1 where fild_name = 'prod_id'"
        Dim command As New SqlCommand(q1, con)
        con.Open()
        command.ExecuteNonQuery()
        con.Close()
    End Sub

    Private Sub Add_Product_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        get_prod_id()
        txt_prod_id.Text = prod_id
        txt_prod_name.Focus()
    End Sub

    Private Sub bt_save_Click(sender As Object, e As EventArgs) Handles bt_save.Click
        prod_name = txt_prod_name.Text
        product_barcode = txt_barcode.Text
        reorder_level = txt_reorder_level.Text
        supplier_id = txtSupplier_Id.Text
        If String.IsNullOrEmpty(txt_barcode.Text) Or String.IsNullOrEmpty(txt_prod_name.Text) Then
            Dim dilog As New Dialog1("Fill all fild for add product.", My.Resources.icons8_information_48)
            dilog.Text = "empty fild"
            dilog.Show
        Else
            Dim q1 = "insert into ALL_Product (prod_id, prod_name, reorder_level, barcode, Supplier_ID) values (@prod_id, @prod_name, @reorder_level, @barcode, @Supplier_ID)"
            Dim command As New SqlCommand(q1, con)
            command.Parameters.AddWithValue("@prod_id", prod_id)
            command.Parameters.AddWithValue("@prod_name", prod_name)
            command.Parameters.AddWithValue("@reorder_level", reorder_level)
            command.Parameters.AddWithValue("@barcode", product_barcode)
            command.Parameters.AddWithValue("@Supplier_ID", supplier_id)
            con.Open
            command.ExecuteNonQuery
            con.Close
            updateProdId
        End If
        DialogResult = DialogResult.OK
        Close
    End Sub

    Private Sub bt_cancel_Click(sender As Object, e As EventArgs) Handles bt_cancel.Click
        DialogResult = DialogResult.Cancel
        Close
    End Sub
End Class
