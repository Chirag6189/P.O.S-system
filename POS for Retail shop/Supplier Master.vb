Imports System.Data.SqlClient
Imports Xamarin.Forms.PlatformConfiguration.GTKSpecific

Public Class Supplier_Master

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand

    Private Sub Supplier_Master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.BringToFront()
        RefreshDataGridView()
    End Sub

    Private Sub RefreshDataGridView()
        Dim q1 As String = "select * from Supplier"
        Dim dataAdapter As New SqlDataAdapter(q1, con)
        Dim dataset As New DataSet()
        dataAdapter.Fill(dataset)

        DataGridView1.DataSource = dataset.Tables(0)

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

            txtsupplierId.Text = selectedRow.Cells("Supplier_ID").Value.ToString()
            txtSupplierName.Text = selectedRow.Cells("Supplier_Name").Value.ToString()
            txtPhone.Text = selectedRow.Cells("Phone").Value.ToString()
            txtEmail.Text = selectedRow.Cells("Email").Value.ToString()
            txtCity.Text = selectedRow.Cells("City").Value.ToString()
            txtState.Text = selectedRow.Cells("State").Value.ToString()
            txtAddress.Text = selectedRow.Cells("Address").Value.ToString()
            txtCountry.Text = selectedRow.Cells("Country").Value.ToString()
            txtNotes.Text = selectedRow.Cells("Notes").Value.ToString()
            txtZipCode.Text = selectedRow.Cells("Zip_Code").Value.ToString()
            txtCreatedAt.Text = selectedRow.Cells("Created_At").Value.ToString()
            txtUpdateAt.Text = selectedRow.Cells("Updated_At").Value.ToString()

        End If
    End Sub

    Private Sub Guna2ShadowPanel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2ShadowPanel1.Paint

    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        txtSupplierName.Text = ""
        txtAddress.Text = ""
        txtEmail.Text = ""
        txtCreatedAt.Text = ""
        txtCountry.Text = ""
        txtPhone.Text = ""
        txtNotes.Text = ""
        txtUpdateAt.Text = ""
        txtCity.Text = ""
        txtCreatedAt.Text = DateTime.Today.ToString("dd/MM/yyyy")
        txtUpdateAt.Text = "Not any Updated"
        Dim supplierId As String
        Dim q1 = "select no from Number where fild_name = 'Supplier_ID'"
        Dim command As New SqlCommand(q1, con)
        con.Open()
        Dim reader = command.ExecuteReader
        If reader.Read Then
            supplierId = "s" & reader("no").ToString
            reader.Close()

        Else
            reader.Close()
            Dim dilog1 As New Dialog1("Supplier_ID error.", My.Resources.icons8_error_48)
            dilog1.Text = "Error"
            dilog1.Show()
            Return
        End If
        con.Close()
        txtsupplierId.Text = supplierId
        txtSupplierName.Focus()
    End Sub

    Private Function checkSupplierData(supplierId As String) As Integer
        Dim q1 As String = "select COUNT(*) from Supplier where Supplier_ID = @Supplier_ID"
        Dim command As New SqlCommand(q1, con)
        command.Parameters.AddWithValue("@Supplier_ID", supplierId)
        con.Open()
        Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
        con.Close()
        Return count
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrEmpty(txtsupplierId.Text) Then
            Dim dilog As New Dialog1("Enter data in all fild for save information")
            dilog.Text = "Empty Fild"
            dilog.Show()
        Else
            Dim SupplierId As String = txtsupplierId.Text
            Dim Suppliername As String = txtSupplierName.Text
            Dim phone As String = txtPhone.Text
            Dim email As String = txtEmail.Text
            Dim Address As String = txtAddress.Text
            Dim City As String = txtCity.Text
            Dim Created As String = txtCreatedAt.Text
            Dim updateat As String = txtUpdateAt.Text
            Dim Country = txtCountry.Text
            Dim Notes = txtNotes.Text
            Dim state = txtState.Text
            Dim zipcode = txtZipCode.Text


            Dim checkSupplier = checkSupplierData(SupplierId)

            If checkSupplier = 0 Then
                Dim q2 = "insert into Supplier (Supplier_ID, Supplier_Name, Phone, Email, Address, City, State, Zip_Code, Country, Notes, Created_At, Updated_At) values(@Supplier_ID, @Supplier_Name, @Phone, @Email, @Address, @City, @State, @Zip_Code, @Country, @Notes, @Created_At, @Updated_At)"
                Dim command As New SqlCommand(q2, con)
                command.Parameters.AddWithValue("@Supplier_ID", SupplierId)
                command.Parameters.AddWithValue("@Supplier_Name", Suppliername)
                command.Parameters.AddWithValue("@Phone", phone)
                command.Parameters.AddWithValue("@Email", email)
                command.Parameters.AddWithValue("@Address", Address)
                command.Parameters.AddWithValue("@City", City)
                command.Parameters.AddWithValue("@Created_At", Created)
                command.Parameters.AddWithValue("@Updated_At", updateat)
                command.Parameters.AddWithValue("@Country", Country)
                command.Parameters.AddWithValue("@Notes", Notes)
                command.Parameters.AddWithValue("@State", state)
                command.Parameters.AddWithValue("@Zip_Code", zipcode)
                Dim dilog2 As New Dialog2("Do you want to add this Supplier?")
                dilog2.Text = "Add Supplier"
                If dilog2.ShowDialog = DialogResult.OK Then
                    con.Open()
                    command.ExecuteNonQuery()
                    con.Close()
                    Dim dilog1 As New Dialog1("Supplier Add successful.", My.Resources.icons8_pass_48)
                    dilog1.Text = "success"
                    dilog1.Show()
                    RefreshDataGridView()
                    Dim q3 = "update Number set no = no + 1 where fild_name = 'Supplier_ID'"
                    Dim command2 As New SqlCommand(q3, con)
                    con.Open()
                    command2.ExecuteNonQuery()
                    con.Close()
                End If


            ElseIf checkSupplier = 1 Then
                Dim updateDate As String = DateTime.Today.ToString("dd/MM/yyyy")
                Dim q2 = "update Supplier set Supplier_Name = @Supplier_Name, Phone = @Phone, Email = @Email, Address = @Address, City = @City, Updated_At = @Updated_At, Country = @Country, Notes = @Notes, State = @State, Zip_Code = @Zip_Code where Supplier_ID = @Supplier_ID"
                Dim command As New SqlCommand(q2, con)
                command.Parameters.AddWithValue("@Supplier_ID", SupplierId)
                command.Parameters.AddWithValue("@Supplier_Name", Suppliername)
                command.Parameters.AddWithValue("@Phone", phone)
                command.Parameters.AddWithValue("@Email", email)
                command.Parameters.AddWithValue("@Address", Address)
                command.Parameters.AddWithValue("@City", City)
                command.Parameters.AddWithValue("@Updated_At", updateDate)
                command.Parameters.AddWithValue("@Country", Country)
                command.Parameters.AddWithValue("@Notes", Notes)
                command.Parameters.AddWithValue("@State", state)
                command.Parameters.AddWithValue("@Zip_Code", zipcode)
                Dim dilog2 As New Dialog2("Do you want to update this Supplier Data?")
                dilog2.Text = "Update Supplier"
                If dilog2.ShowDialog = DialogResult.OK Then
                    con.Open()
                    Dim rowsaffected = command.ExecuteNonQuery
                    con.Close()

                    If rowsaffected > 0 Then
                        Dim dilog1 As New Dialog1("Supplier Data update successful.", My.Resources.icons8_pass_48)
                        dilog1.Text = "success"
                        dilog1.Show()
                        RefreshDataGridView()
                    Else
                        Dim dilog1 As New Dialog1("No any row Affected.", My.Resources.icons8_information_48)
                        dilog1.Text = "NO changes save"
                        dilog1.Show()
                        RefreshDataGridView()
                    End If
                End If

            Else

                Dim dilog1 As New Dialog1("Supplier data found more than 1 so erorr to update data.", My.Resources.icons8_error_48)
                dilog1.Text = "extra data found"
                dilog1.Show()
                RefreshDataGridView()
            End If

        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtAddress.Text = ""
        txtCity.Text = ""
        txtCountry.Text = ""
        txtCreatedAt.Text = ""
        txtEmail.Text = ""
        txtNotes.Text = ""
        txtState.Text = ""
        txtsupplierId.Text = ""
        txtSupplierName.Text = ""
        txtUpdateAt.Text = ""
        txtZipCode.Text = ""
        txtPhone.Text = ""
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        If String.IsNullOrEmpty(txtsupplierId.Text) Then
            Dim dilog1 As New Dialog1("No any Supplier selected please Select Supplier from Supplier list.", My.Resources.icons8_error_48)
            dilog1.Text = "select Supplier"
            dilog1.Show()
        Else
            Dim supplierid As String = txtsupplierId.Text
            Dim checkdata As Integer = checkSupplierData(Supplierid)
            If checkdata = 1 Then
                Dim q1 As String = " delete from Supplier where Supplier_ID = @Supplier_ID"
                Dim command As New SqlCommand(q1, con)
                command.Parameters.AddWithValue("@Supplier_ID", supplierid)
                Dim dilog2 As New Dialog2("Do you want to delete this Supplier Data?")
                dilog2.Text = "Delete Supplier"
                If dilog2.ShowDialog = DialogResult.OK Then
                    con.Open()
                    Dim rowsaffected = command.ExecuteNonQuery
                    con.Close()

                    If rowsaffected > 0 Then
                        Dim dilog4 As New Dialog1("Supplier Data delete successful.", My.Resources.icons8_pass_48)
                        dilog4.Text = "success"
                        dilog4.Show()
                        RefreshDataGridView()
                    Else
                        Dim dilog3 As New Dialog1("No any row Affected.", My.Resources.icons8_information_48)
                        dilog3.Text = "data not deleted"
                        dilog3.Show()
                        RefreshDataGridView()
                    End If
                End If
            Else
                Dim dilog1 As New Dialog1("Select Supplier from supplier list.", My.Resources.icons8_information_48)
                dilog1.Text = "Select supplier"
                dilog1.Show()
            End If
        End If
    End Sub

    Private Sub invalue_TextChanged(sender As Object, e As EventArgs) Handles invalue.TextChanged
        Dim value = invalue.Text
        If Not String.IsNullOrEmpty(value) Then
            Showsupplier(value)
        Else
            DataGridView1.DataSource = Nothing
        End If
    End Sub

    Private Sub Showsupplier(value As String)

        If value = "all" Then
            Dim query As String = "select * from Supplier"
            Dim cmd As New SqlCommand(query, con)

            Try
                Dim dataAdepter As New SqlDataAdapter(cmd)
                Dim DataSet As New DataSet()
                dataAdepter.Fill(DataSet)

                DataGridView1.DataSource = DataSet.Tables(0)
            Catch ex As Exception
                Dim dilog1 As New Dialog1("Error in Show Supplier : " & ex.Message, My.Resources.icons8_error_48)
                dilog1.Text = "ShowSupplier Error"
                dilog1.Show()
            End Try
        Else
            Dim query As String = ""

            If radio_Supplier_name.Checked Then
                query = "select * from Supplier where Supplier_Name like @value"
            ElseIf radio_Supplier_id.Checked Then
                query = "select * from Supplier where Supplier_ID like @value"
            ElseIf radio_Phone_id.Checked Then
                query = "select * from Supplier where Phone like @value"
            Else
                query = "select * from Supplier where Supplier_ID like @value or Supplier_Name like @value or Phone like @value or Address like @value or Zip_Code like @value or Created_At like @value or Notes like @value or Email like @value"
            End If
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@value", "%" & value & "%")

            Try
                Dim dataAdepter As New SqlDataAdapter(cmd)
                Dim DataSet As New DataSet()
                dataAdepter.Fill(DataSet)

                DataGridView1.DataSource = DataSet.Tables(0)
            Catch ex As Exception
                Dim dilog1 As New Dialog1("Error in Show Supplier : " & ex.Message, My.Resources.icons8_error_48)
                dilog1.Text = "ShowSupplier Error"
                dilog1.Show()
            End Try
        End If
    End Sub
End Class