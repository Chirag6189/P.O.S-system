Imports System.Data.SqlClient
Imports Xamarin.Forms.PlatformConfiguration.GTKSpecific

Public Class CustomerMaster

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand

    Private Sub CustomerMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.BringToFront()
        RefreshDataGridView()
    End Sub

    Private Sub RefreshDataGridView()
        Dim q1 As String = "select * from Customers"
        Dim dataAdapter As New SqlDataAdapter(q1, con)
        Dim dataset As New DataSet()
        dataAdapter.Fill(dataset)

        DataGridView1.DataSource = dataset.Tables(0)

    End Sub

    Private Function checkCustomerData(customerId As String) As Integer
        Dim q1 As String = "select COUNT(*) from Customers where CustomerID = @customerId"
        Dim command As New SqlCommand(q1, con)
        command.Parameters.AddWithValue("@customerId", customerId)
        con.Open()
        Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
        con.Close()
        Return count
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If String.IsNullOrEmpty(txtCustomerId.Text) Then
            Dim dilog As New Dialog1("Enter data in all fild for save information")
            dilog.Text = "Empty Fild"
            dilog.Show()
        Else
            Dim customerId = txtCustomerId.Text
            Dim customername = txtCustomerName.Text
            Dim dis = txtCustomerDis.Text
            Dim address = txtAddress.Text
            Dim city = txtCity.Text
            Dim country = txtCountry.Text
            Dim state = txtState.Text
            Dim email = txtEmail.Text
            Dim phone = txtPhone.Text
            Dim postalcode = txtPostalCode.Text
            Dim createAt = DateAndTime.Now.ToString("dd/MM/yyyy")


            Dim checkCustomer = checkCustomerData(customerId)

            If checkCustomer = 0 Then
                Dim q2 = "insert into Customers (CustomerID, Name, Dis, Email, Phone, Address, City, State, Country, PostalCode, CreatedAt) values(@CustomerID, @Name, @Dis, @Email, @Phone, @Address, @City, @State, @Country, @PostalCode, @CreatedAt)"
                Dim command As New SqlCommand(q2, con)
                command.Parameters.AddWithValue("@CustomerID", customerId)
                command.Parameters.AddWithValue("@Name", customername)
                command.Parameters.AddWithValue("@Dis", dis)
                command.Parameters.AddWithValue("@Email", email)
                command.Parameters.AddWithValue("@Phone", phone)
                command.Parameters.AddWithValue("@Address", address)
                command.Parameters.AddWithValue("@City", city)
                command.Parameters.AddWithValue("@State", state)
                command.Parameters.AddWithValue("@Country", country)
                command.Parameters.AddWithValue("@PostalCode", postalcode)
                command.Parameters.AddWithValue("@CreatedAt", createAt)
                Dim dilog2 As New Dialog2("Do you want to add this Customer?")
                dilog2.Text = "Add Customer"
                If dilog2.ShowDialog = DialogResult.OK Then
                    con.Open()
                    command.ExecuteNonQuery()
                    con.Close()
                    Dim dilog1 As New Dialog1("Customer Add successful.", My.Resources.icons8_pass_48)
                    dilog1.Text = "success"
                    dilog1.Show()
                End If


            ElseIf checkCustomer = 1 Then
                Dim q2 = "update Customers set Name = @Name, Dis = @Dis, Phone = @Phone, Email = @Email, Address = @Address, City = @City, State = @State, Country = @Country, PostalCode = @PostalCode, CreatedAt = @CreatedAt where CustomerID = @CustomerId"
                Dim command As New SqlCommand(q2, con)
                command.Parameters.AddWithValue("@CustomerId", customerId)
                command.Parameters.AddWithValue("@Name", customername)
                command.Parameters.AddWithValue("@Dis", dis)
                command.Parameters.AddWithValue("@Email", email)
                command.Parameters.AddWithValue("@Phone", phone)
                command.Parameters.AddWithValue("@Address", address)
                command.Parameters.AddWithValue("@City", city)
                command.Parameters.AddWithValue("@State", state)
                command.Parameters.AddWithValue("@Country", country)
                command.Parameters.AddWithValue("@PostalCode", postalcode)
                command.Parameters.AddWithValue("@CreatedAt", createAt)
                Dim dilog2 As New Dialog2("Do you want to update this Customer Data?")
                dilog2.Text = "Update Customer"
                If dilog2.ShowDialog = DialogResult.OK Then
                    con.Open()
                    Dim rowsaffected = command.ExecuteNonQuery
                    con.Close()

                    If rowsaffected > 0 Then
                        Dim dilog1 As New Dialog1("Customer Data update successful.", My.Resources.icons8_pass_48)
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

                Dim dilog1 As New Dialog1("Customer data found more than 1 so erorr to update data.", My.Resources.icons8_error_48)
                dilog1.Text = "extra data found"
                dilog1.Show()
                RefreshDataGridView()
            End If

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

            txtCustomerId.Text = selectedRow.Cells("CustomerID").Value.ToString()
            txtCustomerName.Text = selectedRow.Cells("Name").Value.ToString()
            txtCustomerDis.Text = selectedRow.Cells("Dis").Value.ToString()
            txtEmail.Text = selectedRow.Cells("Email").Value.ToString()
            txtPhone.Text = selectedRow.Cells("Phone").Value.ToString()
            txtAddress.Text = selectedRow.Cells("Address").Value.ToString()
            txtCity.Text = selectedRow.Cells("City").Value.ToString()
            txtState.Text = selectedRow.Cells("State").Value.ToString()
            txtCountry.Text = selectedRow.Cells("Country").Value.ToString()
            txtPostalCode.Text = selectedRow.Cells("PostalCode").Value.ToString()
            txtCustomerId.ReadOnly = True

        End If
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        txtCustomerId.Text = ""
        txtCustomerName.Text = ""
        txtCustomerDis.Text = "0"
        txtEmail.Text = ""
        txtPhone.Text = ""
        txtPostalCode.Text = ""
        txtCountry.Text = ""
        txtState.Text = ""
        txtAddress.Text = ""
        txtCity.Text = ""
        txtCustomerId.ReadOnly = False
        txtCustomerId.Focus()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtCustomerId.Text = ""
        txtCustomerName.Text = ""
        txtCustomerDis.Text = ""
        txtEmail.Text = ""
        txtPhone.Text = ""
        txtPostalCode.Text = ""
        txtCountry.Text = ""
        txtState.Text = ""
        txtAddress.Text = ""
        txtCity.Text = ""
        txtCustomerId.ReadOnly = False
        txtCustomerId.Focus()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        If String.IsNullOrEmpty(txtCustomerId.Text) Then
            Dim dilog1 As New Dialog1("No any Customer selected please Select Customer from Customer list.", My.Resources.icons8_error_48)
            dilog1.Text = "select Customer"
            dilog1.Show()
        Else
            Dim customer As String = txtCustomerId.Text
            Dim checkdata As Integer = checkCustomerData(customer)
            If checkdata = 1 Then
                Dim q1 As String = " delete from Customers where CustomerID = @CustomerID"
                Dim command As New SqlCommand(q1, con)
                command.Parameters.AddWithValue("@CustomerID", customer)
                Dim dilog2 As New Dialog2("Do you want to delete this Customer Data?")
                dilog2.Text = "Delete Customer"
                If dilog2.ShowDialog = DialogResult.OK Then
                    con.Open()
                    Dim rowsaffected = command.ExecuteNonQuery
                    con.Close()

                    If rowsaffected > 0 Then
                        Dim dilog4 As New Dialog1("Customer Data delete successful.", My.Resources.icons8_pass_48)
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
                Dim dilog1 As New Dialog1("Select Customer from employee list.", My.Resources.icons8_information_48)
                dilog1.Text = "Select Customer"
                dilog1.Show()
            End If
        End If
    End Sub

    Private Sub invalue_TextChanged(sender As Object, e As EventArgs) Handles invalue.TextChanged
        Dim value = invalue.Text
        If Not String.IsNullOrEmpty(value) Then
            Showcustomer(value)
        Else
            DataGridView1.DataSource = Nothing
        End If
    End Sub

    Private Sub Showcustomer(value As String)

        If value = "all" Then
            Dim query As String = "select * from Customers"
            Dim cmd As New SqlCommand(query, con)

            Try
                Dim dataAdepter As New SqlDataAdapter(cmd)
                Dim DataSet As New DataSet()
                dataAdepter.Fill(DataSet)

                DataGridView1.DataSource = DataSet.Tables(0)
            Catch ex As Exception
                Dim dilog1 As New Dialog1("Error in Show Customer : " & ex.Message, My.Resources.icons8_error_48)
                dilog1.Text = "ShowCustomer Error"
                dilog1.Show()
            End Try
        Else
            Dim query As String = ""

            If radio_cust_name.Checked Then
                query = "select * from Customers where Name like @value"
            ElseIf radio_cust_id.Checked Then
                query = "select * from Customers where CustomerId like @value"
            ElseIf radio_cust_phone.Checked Then
                query = "select * from Customers where Phone like @value"
            Else
                query = "select * from Customers where CustomerId like @value or Name like @value or Dis like @value or Phone like @value or Email like @value or City like @value or CreatedAt like @value"
            End If
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@value", "%" & value & "%")

            Try
                Dim dataAdepter As New SqlDataAdapter(cmd)
                Dim DataSet As New DataSet()
                dataAdepter.Fill(DataSet)

                DataGridView1.DataSource = DataSet.Tables(0)
            Catch ex As Exception
                Dim dilog1 As New Dialog1("Error in Show Customer : " & ex.Message, My.Resources.icons8_error_48)
                dilog1.Text = "ShowCustomer Error"
                dilog1.Show()
            End Try
        End If
    End Sub

End Class