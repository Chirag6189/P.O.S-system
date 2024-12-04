Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Security.AccessControl

Public Class EmployeeMaster

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand

    Private Sub EmployeeMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.BringToFront()
        RefreshDataGridView()
    End Sub

    Private Sub RefreshDataGridView()
        Dim q1 As String = "select * from employee"
        Dim dataAdapter As New SqlDataAdapter(q1, con)
        Dim dataset As New DataSet()
        dataAdapter.Fill(dataset)

        DataGridView1.DataSource = dataset.Tables(0)

    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

            txtEmployeeId.Text = selectedRow.Cells("emp_id").Value.ToString()
            txtStatus.Text = selectedRow.Cells("EmploymentStatus").Value.ToString()
            txtEmployeeName.Text = selectedRow.Cells("emp_name").Value.ToString()
            txtUserId.Text = selectedRow.Cells("user_id").Value.ToString()
            txtPassword.Text = selectedRow.Cells("pass").Value.ToString()
            txtPermition.Text = selectedRow.Cells("permition").Value.ToString()
            txtSalary.Text = selectedRow.Cells("Salary").Value.ToString()

            Dim dob As String = selectedRow.Cells("Date_of_birth").Value.ToString()
            Dim dateValue As DateTime
            If DateTime.TryParseExact(dob, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dateValue) Then
                If dateValue >= txtDateOfBirth.MinDate AndAlso dateValue <= txtDateOfBirth.MaxDate Then
                    txtDateOfBirth.Value = dateValue
                End If
            End If

            Dim gender As String = selectedRow.Cells("Gender").Value.ToString()
            If gender = "MALE" Then
                RadioMale.Checked = True
            Else
                RadioFemale.Checked = True
            End If

            txtEmail.Text = selectedRow.Cells("email").Value.ToString()
            txtAddress.Text = selectedRow.Cells("address").Value.ToString()
            txtPhone.Text = selectedRow.Cells("phone").Value.ToString()
            txtPosition.Text = selectedRow.Cells("position").Value.ToString()

            Dim hiredate As String = selectedRow.Cells("hire_date").Value.ToString()
            Dim hireDateValue As DateTime
            If DateTime.TryParseExact(hiredate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, hireDateValue) Then
                If hireDateValue >= txtHireDate.MinDate AndAlso hireDateValue <= txtHireDate.MaxDate Then
                    txtHireDate.Value = hireDateValue
                End If
            End If
        End If
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        txtEmployeeName.Text = ""
        txtAddress.Text = ""
        txtEmail.Text = ""
        txtPassword.Text = ""
        txtPermition.Text = "-"
        txtPhone.Text = ""
        txtPosition.Text = "-"
        txtSalary.Text = ""
        txtStatus.Text = "-"
        txtUserId.Text = ""
        RadioFemale.Checked = False
        RadioMale.Checked = False
        txtHireDate.Value = Date.Today
        txtDateOfBirth.Value = Date.Today
        Dim emp_no As String
        Dim q1 = "select no from Number where fild_name = 'emp_id'"
        Dim command As New SqlCommand(q1, con)
        con.Open()
        Dim reader = command.ExecuteReader
        If reader.Read Then
            emp_no = "e" & reader("no").ToString
            reader.Close()

        Else
            reader.Close()
            Dim dilog1 As New Dialog1("emp_id error.", My.Resources.icons8_error_48)
            dilog1.Text = "Error"
            dilog1.Show()
            Return
        End If
        con.Close()
        txtEmployeeId.Text = emp_no
        txtEmployeeName.Focus()
    End Sub

    Private Function checkEmployeeData(employeeId As String) As Integer
        Dim q1 As String = "select COUNT(*) from employee where emp_id = @emp_id"
        Dim command As New SqlCommand(q1, con)
        command.Parameters.AddWithValue("@emp_id", employeeId)
        con.Open()
        Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
        con.Close()
        Return count
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If String.IsNullOrEmpty(txtEmployeeId.Text) Then
            Dim dilog As New Dialog1("Enter data in all fild for save information")
            dilog.Text = "Empty Fild"
            dilog.Show()
        Else
            Dim employeeId = txtEmployeeId.Text
            Dim employeename = txtEmployeeName.Text
            Dim userid = txtUserId.Text
            Dim pass = txtPassword.Text
            Dim permition = txtPermition.Text
            Dim salary = txtSalary.Text
            Dim DateOfBirth = txtDateOfBirth.Value.ToString("dd/MM/yyyy")
            Dim gender As String
            If RadioMale.Checked = True Then
                gender = RadioMale.Text
            ElseIf RadioFemale.Created = True Then
                gender = RadioFemale.Text
            Else
                gender = "Not Define"
            End If
            Dim email = txtEmail.Text
            Dim address = txtAddress.Text
            Dim phone = txtPhone.Text
            Dim position = txtPosition.Text
            Dim hireDate = txtHireDate.Value.ToString("dd/MM/yyyy")
            Dim EmploymentStatus = txtStatus.Text


            Dim checkEmployee = checkEmployeeData(employeeId)

            If checkEmployee = 0 Then
                Dim q2 = "insert into employee (emp_id, emp_name, user_id, pass, permition, Salary, Date_of_birth, Gender, email, address, phone, position, hire_date, EmploymentStatus) values(@emp_id, @emp_name, @user_id, @pass, @permition, @Salary, @Date_of_birth, @Gender, @email, @address, @phone, @position, @hire_date, @EmploymentStatus)"
                Dim command As New SqlCommand(q2, con)
                command.Parameters.AddWithValue("@emp_id", employeeId)
                command.Parameters.AddWithValue("@emp_name", employeename)
                command.Parameters.AddWithValue("@user_id", userid)
                command.Parameters.AddWithValue("@pass", pass)
                command.Parameters.AddWithValue("@permition", permition)
                command.Parameters.AddWithValue("@Salary", salary)
                command.Parameters.AddWithValue("@Date_of_birth", DateOfBirth)
                command.Parameters.AddWithValue("@Gender", gender)
                command.Parameters.AddWithValue("@email", email)
                command.Parameters.AddWithValue("@address", address)
                command.Parameters.AddWithValue("@phone", phone)
                command.Parameters.AddWithValue("@position", position)
                command.Parameters.AddWithValue("@hire_date", hireDate)
                command.Parameters.AddWithValue("@EmploymentStatus", EmploymentStatus)
                Dim dilog2 As New Dialog2("Do you want to add this Employee?")
                dilog2.Text = "Add Employee"
                If dilog2.ShowDialog = DialogResult.OK Then
                    con.Open()
                    command.ExecuteNonQuery()
                    con.Close()
                    Dim dilog1 As New Dialog1("Employee Add successful.", My.Resources.icons8_pass_48)
                    dilog1.Text = "success"
                    dilog1.Show()
                    RefreshDataGridView()
                    Dim q3 = "update Number set no = no + 1 where fild_name = 'emp_id'"
                    Dim command2 As New SqlCommand(q3, con)
                    con.Open()
                    command2.ExecuteNonQuery()
                    con.Close()
                End If


            ElseIf checkEmployee = 1 Then
                Dim q2 = "update employee set emp_name = @emp_name, user_id = @user_id, pass = @pass, permition = @permition, Salary = @Salary, Date_of_birth = @Date_of_birth, Gender = @Gender, email = @email, address = @address, phone = @phone, position = @position, hire_date = @hire_date, EmploymentStatus = @EmploymentStatus where emp_id = @emp_id"
                Dim command As New SqlCommand(q2, con)
                command.Parameters.AddWithValue("@emp_id", employeeId)
                command.Parameters.AddWithValue("@emp_name", employeename)
                command.Parameters.AddWithValue("@user_id", userid)
                command.Parameters.AddWithValue("@pass", pass)
                command.Parameters.AddWithValue("@permition", permition)
                command.Parameters.AddWithValue("@Salary", salary)
                command.Parameters.AddWithValue("@Date_of_birth", DateOfBirth)
                command.Parameters.AddWithValue("@Gender", gender)
                command.Parameters.AddWithValue("@email", email)
                command.Parameters.AddWithValue("@address", address)
                command.Parameters.AddWithValue("@phone", phone)
                command.Parameters.AddWithValue("@position", position)
                command.Parameters.AddWithValue("@hire_date", hireDate)
                command.Parameters.AddWithValue("@EmploymentStatus", EmploymentStatus)
                Dim dilog2 As New Dialog2("Do you want to update this Employee Data?")
                dilog2.Text = "Update Employee"
                If dilog2.ShowDialog = DialogResult.OK Then
                    con.Open()
                    Dim rowsaffected = command.ExecuteNonQuery
                    con.Close()

                    If rowsaffected > 0 Then
                        Dim dilog1 As New Dialog1("Employee Data update successful.", My.Resources.icons8_pass_48)
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

                Dim dilog1 As New Dialog1("Employee data found more than 1 so erorr to update data.", My.Resources.icons8_error_48)
                dilog1.Text = "extra data found"
                dilog1.Show()
                RefreshDataGridView()
            End If

        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtEmployeeName.Text = ""
        txtAddress.Text = ""
        txtEmail.Text = ""
        txtPassword.Text = ""
        txtPermition.Text = "-"
        txtPhone.Text = ""
        txtPosition.Text = "-"
        txtSalary.Text = ""
        txtStatus.Text = "-"
        txtUserId.Text = ""
        RadioFemale.Checked = False
        RadioMale.Checked = False
        txtHireDate.Value = Date.Today
        txtDateOfBirth.Value = Date.Today
        txtEmployeeId.Text = ""
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        If String.IsNullOrEmpty(txtEmployeeId.Text) Then
            Dim dilog1 As New Dialog1("No any Employee selected please Select Employee from Employee list.", My.Resources.icons8_error_48)
            dilog1.Text = "select Employee"
            dilog1.Show()
        Else
            Dim employee As String = txtEmployeeId.Text
            Dim checkdata As Integer = checkEmployeeData(employee)
            If checkdata = 1 Then
                Dim q1 As String = " delete from employee where emp_id = @emp_id"
                Dim command As New SqlCommand(q1, con)
                command.Parameters.AddWithValue("@emp_id", employee)
                Dim dilog2 As New Dialog2("Do you want to delete this Employee Data?")
                dilog2.Text = "Delete Employee"
                If dilog2.ShowDialog = DialogResult.OK Then
                    con.Open()
                    Dim rowsaffected = command.ExecuteNonQuery
                    con.Close()

                    If rowsaffected > 0 Then
                        Dim dilog4 As New Dialog1("Employee Data delete successful.", My.Resources.icons8_pass_48)
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
                Dim dilog1 As New Dialog1("Select Employee from employee list.", My.Resources.icons8_information_48)
                dilog1.Text = "Select Employee"
                dilog1.Show()
            End If
        End If
    End Sub

    Private Sub invalue_TextChanged(sender As Object, e As EventArgs) Handles invalue.TextChanged
        Dim value = invalue.Text
        If Not String.IsNullOrEmpty(value) Then
            Showemployee(value)
        Else
            DataGridView1.DataSource = Nothing
        End If
    End Sub

    Private Sub Showemployee(value As String)

        If value = "all" Then
            Dim query As String = "select * from employee"
            Dim cmd As New SqlCommand(query, con)

            Try
                Dim dataAdepter As New SqlDataAdapter(cmd)
                Dim DataSet As New DataSet()
                dataAdepter.Fill(DataSet)

                DataGridView1.DataSource = DataSet.Tables(0)
            Catch ex As Exception
                Dim dilog1 As New Dialog1("Error in Show Employee : " & ex.Message, My.Resources.icons8_error_48)
                dilog1.Text = "ShowEmployee Error"
                dilog1.Show()
            End Try
        Else
            Dim query As String = ""

            If radio_emp_name.Checked Then
                query = "select * from employee where emp_name like @value"
            ElseIf radio_emp_id.Checked Then
                query = "select * from employee where emp_id like @value"
            ElseIf radio_user_id.Checked Then
                query = "select * from employee where user_id like @value"
            Else
                query = "select * from employee where emp_id like @value or emp_name like @value or user_id like @value or permition like @value or Salary like @value or Date_of_birth like @value or Gender like @value or email like @value or address like @value or phone like @value "
            End If
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@value", "%" & value & "%")

            Try
                Dim dataAdepter As New SqlDataAdapter(cmd)
                Dim DataSet As New DataSet()
                dataAdepter.Fill(DataSet)

                DataGridView1.DataSource = DataSet.Tables(0)
            Catch ex As Exception
                Dim dilog1 As New Dialog1("Error in Show Employee : " & ex.Message, My.Resources.icons8_error_48)
                dilog1.Text = "ShowEmployee Error"
                dilog1.Show()
            End Try
        End If
    End Sub
End Class