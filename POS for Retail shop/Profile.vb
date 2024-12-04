Imports System.Data.SqlClient
Imports Xamarin.Forms

Public Class Profile

    Dim name As String
    Dim userId As String
    Dim per As String

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")

    Public Sub New(ByVal name As String, ByVal per As String, ByVal userId As String)
        InitializeComponent()
        Me.name = name
        Me.per = per
        Me.userId = userId

    End Sub

    Private Sub Profile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbluserId.Text = userId
        lblusername2.Text = name
        lblusername.Text = name
        loadDate()
    End Sub

    Private Sub loadDate()
        Dim emp_id As String
        Dim q1 As String = "select emp_id, Salary,Date_of_birth,Gender,email,address,phone,position,hire_date,EmploymentStatus from employee where user_id = @user_id"
        Dim command As New SqlCommand(q1, con)
        command.Parameters.AddWithValue("@user_id", userId)
        con.Open()
        Dim reader As SqlDataReader = command.ExecuteReader()
        If reader.HasRows Then
            reader.Read()
            emp_id = reader("emp_id")
            lbladdress.Text = reader("address")
            lbldateofbirth.Text = reader("Date_of_birth")
            lblgende.Text = reader("Gender")
            lblhiredate.Text = reader("hire_date")
            lblphonr.Text = reader("phone")
            lblposition.Text = reader("position")
            lblsalary.Text = reader("Salary")
            lblsatus.Text = reader("EmploymentStatus")
            lblemail.Text = reader("email")
        End If
        reader.Close()

        Dim today As Date = DateTime.Today
        Dim currentMonth As Integer = today.Month
        Dim currentYear As Integer = today.Year

        Dim q2 As String = "select PresentDays from Salary where EmployeeId = @emp_id and Month = @month and Year = @year"
        command.CommandText = q2
        command.Parameters.AddWithValue("@emp_id", emp_id)
        command.Parameters.AddWithValue("@month", currentMonth)
        command.Parameters.AddWithValue("@year", currentYear)
        Dim r2 As SqlDataReader = command.ExecuteReader()
        If r2.HasRows Then
            r2.Read()
            lblpresentday.Text = r2("PresentDays")
        End If
        r2.Close()
        con.Close()
    End Sub

End Class