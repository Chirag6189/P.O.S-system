Imports System.Data.SqlClient

Public Class Receive_Purchase_Order

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand
    Dim userId As String

    Public Sub New(ByVal userId As String)
        InitializeComponent()
        Me.userId = userId
    End Sub

    Private Sub Receive_Purchase_Order_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPanding_PO()
    End Sub

    Private Sub LoadPanding_PO()
        Dim query As String = "SELECT p.PO_no, s.Supplier_Name, p.user_id, p.date " &
                      "FROM purchase_order p " &
                      "JOIN Supplier s ON p.Supplier_ID = s.Supplier_ID " &
                      "WHERE p.Status = @Status"

        Dim command As New SqlCommand(query, con)
        command.Parameters.AddWithValue("@Status", "Order Placed")

        Try
            con.Open()
            Dim reader As SqlDataReader = command.ExecuteReader()

            While reader.Read()
                Dim po_no As String = reader("PO_no").ToString()
                Dim suppliername As String = reader("Supplier_Name").ToString()
                Dim user_id As String = reader("user_id").ToString()
                Dim po_date As String = reader("date").ToString()

                cmbPO.Items.Add($"{po_no} - {suppliername} ({user_id}) {po_date}")
            End While

            reader.Close()
        Catch ex As Exception
            Dim dilog1 As New Dialog1("Error fetching P.O data: " & ex.Message, My.Resources.icons8_error_48)
            dilog1.Text = "Error"
            dilog1.Show()
        Finally
            con.Close()
        End Try
    End Sub
End Class