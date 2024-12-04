Imports System.Data.SqlClient

Public Class Staff
    Dim name As String
    Dim userId As String
    Dim per As String

    Public Sub New(ByVal name As String, ByVal per As String, ByVal userId As String)
        InitializeComponent()
        Me.name = name
        Me.per = per
        Me.userId = userId
    End Sub

    Private Sub Staff_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbluserId.Text = userId
        CreateAttendancePanels()
    End Sub

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand

    Private Function GetTodaysAttendance() As DataTable
        Dim query As String = "SELECT e.emp_id, e.emp_name, a.Status 
                           FROM employee e
                           LEFT JOIN Attendance a ON e.emp_id = a.EmployeeId AND a.Date = @Today"
        Dim attendanceTable As New DataTable

        Dim command As New SqlCommand(query, con)
        command.Parameters.AddWithValue("@Today", DateTime.Today)
        con.Open()
        Using reader As SqlDataReader = command.ExecuteReader()
            attendanceTable.Load(reader)
        End Using
        con.Close()

        Return attendanceTable
    End Function

    Private Sub CreateAttendancePanels()
        Dim attendanceTable As DataTable = GetTodaysAttendance()
        Dim panelHeight As Integer = 50 ' Set the height of each panel
        Dim panelWidth As Integer = pnllist.Width - 50 ' Set the width of each panel (with some margin)
        Dim panelMargin As Integer = 5 ' Set the margin between panels
        Dim topMargin As Integer = 30 ' Top margin for the list

        ' Clear existing panels
        pnllist.Controls.Clear()

        ' Create and add header panel
        Dim headerPanel As New Panel With {
            .Height = 40,
            .Width = panelWidth,
            .Top = 25,
            .Left = 25,
            .BackColor = Color.Gray
        }

        ' Create and add header labels
        Dim headerIndexLabel As New Label With {
            .Text = "No.",
            .AutoSize = True,
            .ForeColor = Color.White,
            .FlatStyle = FontStyle.Bold,
            .Font = New Font("Arial", 11, FontStyle.Bold),
            .Left = 10
        }
        headerIndexLabel.Top = (headerPanel.Height - headerIndexLabel.Height) / 2
        headerPanel.Controls.Add(headerIndexLabel)

        Dim headerIdLabel As New Label With {
            .Text = "Employee ID",
            .AutoSize = True,
            .ForeColor = Color.White,
            .FlatStyle = FontStyle.Bold,
            .Font = New Font("Arial", 11, FontStyle.Bold),
            .Left = 50
        }
        headerIdLabel.Top = (headerPanel.Height - headerIdLabel.Height) / 2
        headerPanel.Controls.Add(headerIdLabel)

        Dim headerNameLabel As New Label With {
            .Text = "Employee Name",
            .AutoSize = True,
            .ForeColor = Color.White,
            .FlatStyle = FontStyle.Bold,
            .Font = New Font("Arial", 11, FontStyle.Bold),
            .Left = 150
        }
        headerNameLabel.Top = (headerPanel.Height - headerNameLabel.Height) / 2
        headerPanel.Controls.Add(headerNameLabel)

        Dim headerStatusLabel As New Label With {
            .Text = "Status",
            .AutoSize = True,
            .ForeColor = Color.White,
            .FlatStyle = FontStyle.Bold,
            .Font = New Font("Arial", 11, FontStyle.Bold),
            .Left = panelWidth - 60
        }
        headerStatusLabel.Top = (headerPanel.Height - headerStatusLabel.Height) / 2
        headerPanel.Controls.Add(headerStatusLabel)

        pnllist.Controls.Add(headerPanel)

        For i As Integer = 0 To attendanceTable.Rows.Count - 1
            Dim employeeId As String = attendanceTable.Rows(i)("emp_id").ToString()
            Dim employeeName As String = attendanceTable.Rows(i)("emp_name").ToString()
            Dim status As String = If(IsDBNull(attendanceTable.Rows(i)("Status")), "Absent", attendanceTable.Rows(i)("Status").ToString())

            Dim newPanel As New Panel With {
                .Height = panelHeight,
                .Width = panelWidth,
                .Top = topMargin + headerPanel.Height + i * (panelHeight + panelMargin),
                .Left = 25,
                .BackColor = If(i Mod 2 = 0, Color.LightGray, Color.DarkGray)
            }

            ' Create labels for the index, employee ID, and name
            Dim indexLabel As New Label With {
                .Text = (i + 1).ToString(),
                .AutoSize = True,
                .ForeColor = Color.Black,
                .FlatStyle = FontStyle.Bold,
                .Font = New Font("Arial", 10, FontStyle.Bold),
                .Left = 10
            }
            indexLabel.Top = (panelHeight - indexLabel.Height) / 2
            newPanel.Controls.Add(indexLabel)

            Dim idLabel As New Label With {
                .Text = employeeId,
                .AutoSize = True,
                .ForeColor = Color.Black,
                .FlatStyle = FontStyle.Bold,
                .Font = New Font("Arial", 10, FontStyle.Bold),
                .Left = 50
            }
            idLabel.Top = (panelHeight - idLabel.Height) / 2
            newPanel.Controls.Add(idLabel)

            Dim nameLabel As New Label With {
                .Text = employeeName,
                .AutoSize = True,
                .ForeColor = Color.Black,
                .FlatStyle = FontStyle.Bold,
                .Font = New Font("Arial", 10, FontStyle.Bold),
                .Left = 150
            }
            nameLabel.Top = (panelHeight - nameLabel.Height) / 2
            newPanel.Controls.Add(nameLabel)

            ' Create a status indicator (dot)
            Dim statusDot As New Label With {
                .Width = 20,
                .Height = 20,
                .BackColor = If(status = "Present", Color.Green, Color.Red),
                .Left = panelWidth - 30,
                .BorderStyle = BorderStyle.FixedSingle
            }
            statusDot.Top = (panelHeight - statusDot.Height) / 2
            newPanel.Controls.Add(statusDot)

            ' Add the panel to the MainPanel
            pnllist.Controls.Add(newPanel)
        Next
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click

        Dim employeeId As String = txtEmployeeId.Text

        Dim today As Date = DateTime.Today
        Dim inTime As DateTime = DateTime.Now

        Dim query As String = "SELECT COUNT(*) FROM Attendance WHERE EmployeeId = @EmployeeId AND Date = @Date AND InTime IS NOT NULL"

        Dim command As New SqlCommand(query, con)
        command.Parameters.AddWithValue("@EmployeeId", employeeId)
        command.Parameters.AddWithValue("@Date", today)
        con.Open()
        Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())

        If count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Do you want to update InTime for this employee?", "Confirmation", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                query = "UPDATE Attendance SET InTime = @InTime WHERE EmployeeId = @EmployeeId AND Date = @Date"
                command.CommandText = query
                command.Parameters.AddWithValue("@InTime", inTime)
                command.ExecuteNonQuery()
                MessageBox.Show("InTime updated successfully.")
            End If
        Else
            query = "INSERT INTO Attendance (EmployeeId, Date, InTime, Status) VALUES (@EmployeeId, @Date, @InTime, 'Present')"
            command.CommandText = query
            command.Parameters.AddWithValue("@InTime", inTime)
            command.ExecuteNonQuery()
            MessageBox.Show("InTime recorded successfully.")
        End If
        con.Close()

        updateSalary(employeeId)
        ' Update the attendance list
        CreateAttendancePanels()
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click

        Dim employeeId As String = txtEmployeeId.Text

        Dim today As Date = DateTime.Today
        Dim outTime As DateTime = DateTime.Now
        Dim query As String = "SELECT COUNT(*) FROM Attendance WHERE EmployeeId = @EmployeeId AND Date = @Date AND OutTime IS NOT NULL"

        Dim command As New SqlCommand(query, con)
        command.Parameters.AddWithValue("@EmployeeId", employeeId)
        command.Parameters.AddWithValue("@Date", today)
        con.Open()
        Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())

        If count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Do you want to update OutTime for this employee?", "Confirmation", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                query = "UPDATE Attendance SET OutTime = @OutTime WHERE EmployeeId = @EmployeeId AND Date = @Date"
                command.CommandText = query
                command.Parameters.AddWithValue("@OutTime", outTime)
                command.ExecuteNonQuery()
                MessageBox.Show("OutTime updated successfully.")
            End If
        Else
            Dim query1 As String = "UPDATE Attendance SET OutTime = @OutTime WHERE EmployeeId = @EmployeeId AND Date = @Date AND InTime IS NOT NULL AND OutTime IS NULL"
            command.Parameters.AddWithValue("@OutTime", outTime)
            command.CommandText = query1
            Dim rowsAffected As Integer = command.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("OutTime recorded successfully.")
            Else
                MessageBox.Show("Failed to record OutTime. Ensure that InTime has been recorded first.")
            End If
        End If
        con.Close()

        updateSalary(employeeId)
        ' Update the attendance list
        CreateAttendancePanels()
    End Sub

    Private Sub updateSalary(employeeId As String)
        Dim today As Date = DateTime.Today
        Dim currentMonth As Integer = today.Month
        Dim currentYear As Integer = today.Year

        checkdataformonth(employeeId, currentMonth, currentYear)

        Dim totalDays As Integer = DateTime.DaysInMonth(currentYear, currentMonth)
        Dim PresentDays As Integer = getpresentDays(employeeId, currentMonth, currentYear)
        Dim AbsenrDay As Integer = totalDays - PresentDays
        Dim dailyRate As Decimal = getDailyRate(employeeId, currentMonth, currentYear)
        Dim SalaryAmount As Decimal = dailyRate * PresentDays

        Dim q1 As String = "update Salary set TotalDays = @TotalDays, PresentDays = @PresentDays, AbsentDays = @AbsentDays, SalaryAmount = @SalaryAmount where EmployeeId = @employeeId and Month = @Month and Year = @Year"
        Dim Command As New SqlCommand(q1, con)
        Command.Parameters.AddWithValue("@TotalDays", totalDays)
        Command.Parameters.AddWithValue("@PresentDays", PresentDays)
        Command.Parameters.AddWithValue("@AbsentDays", AbsenrDay)
        Command.Parameters.AddWithValue("@SalaryAmount", SalaryAmount)
        Command.Parameters.AddWithValue("@employeeId", employeeId)
        Command.Parameters.AddWithValue("@Month", currentMonth)
        Command.Parameters.AddWithValue("@Year", currentYear)
        con.Open()
        Command.ExecuteNonQuery()
        con.Close()

    End Sub

    Private Sub checkdataformonth(employeeId As String, currentMonth As Integer, currentyear As Integer)
        Dim q1 As String = "SELECT COUNT(*) FROM Salary WHERE EmployeeId = @employeeId and Month = @Month and Year = @Year"
        Dim command As New SqlCommand(q1, con)
        command.Parameters.AddWithValue("@employeeId", employeeId)
        command.Parameters.AddWithValue("@Month", currentMonth)
        command.Parameters.AddWithValue("@Year", currentyear)
        con.Open()
        Dim count As Integer = Convert.ToInt32(command.ExecuteScalar)

        If count = 0 Then
            Dim q2 As String = "insert into Salary (EmployeeId, Month, Year, TotalDays, PresentDays, AbsentDays, SalaryAmount) values ( @employeeId, @Month, @Year, 0,0,0,0)"
            command.CommandText = q2
            command.ExecuteNonQuery()
        End If
        con.Close()
    End Sub

    Private Function getpresentDays(employeeId As String, currentMonth As Integer, currentyear As Integer) As Integer
        Dim q1 As String = "select count(*) from Attendance where EmployeeId = @employeeId and month(Date) = @currentMonth and year(Date) = @currentyear and Status = 'Present'"
        Dim command As New SqlCommand(q1, con)
        command.Parameters.AddWithValue("@employeeId", employeeId)
        command.Parameters.AddWithValue("@currentMonth", currentMonth)
        command.Parameters.AddWithValue("@currentyear", currentyear)
        con.Open()
        Dim presentDay As Integer = Convert.ToInt32(command.ExecuteScalar())
        con.Close()
        Return presentDay
    End Function

    Private Function getDailyRate(employeeId As String, currentMonth As Integer, currentYear As Integer) As Decimal
        Dim q1 As String = "select Salary from employee where emp_id = @employeeId"
        Dim command As New SqlCommand(q1, con)
        command.Parameters.AddWithValue("@employeeId", employeeId)
        con.Open()
        Dim totalSalary As Decimal = Convert.ToDecimal(command.ExecuteScalar())
        con.Close()
        Dim totalDay As Integer = DateTime.DaysInMonth(currentYear, currentMonth)
        Dim dailyRate As Decimal = totalSalary / totalDay
        Return dailyRate
    End Function

End Class
