Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class dashboardvb
    Dim name As String
    Dim userId As String
    Dim per As String

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand
    Dim monthlyGoals As Integer = 0

    Public Sub New(ByVal name As String, ByVal per As String, ByVal userId As String)
        InitializeComponent()
        Me.name = name
        Me.per = per
        Me.userId = userId
        StartClock()
    End Sub

    Private Sub StartClock()
        Dim bgWorker As New System.ComponentModel.BackgroundWorker
        AddHandler bgWorker.DoWork, Sub(sender, e)
                                        While True
                                            UpdateTimeLabel()
                                            Threading.Thread.Sleep(1000)
                                        End While
                                    End Sub

        bgWorker.RunWorkerAsync()
    End Sub

    Private Sub UpdateTimeLabel()
        Try
            If lblClock.InvokeRequired Then
                lblClock.Invoke(Sub() UpdateTimeLabel())
            Else
                lblClock.Text = DateTime.Now.ToString("hh:mm:ss tt")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub UpdateMonthlyTotalSales(label As Label)
        Dim query As String = "SELECT SUM(CAST(net_amount AS INT)) AS MonthlyTotalSales FROM [dbo].[bill] WHERE YEAR(CONVERT(DATE, [date], 103)) = YEAR(GETDATE()) AND MONTH(CONVERT(DATE, [date], 103)) = MONTH(GETDATE());"
        Dim cmd As New SqlCommand(query, con)

        Try
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                label.Text = "₹ " & result.ToString() & ".00"
            Else
                label.Text = "₹ 0.00"
            End If
        Catch ex As Exception
            MessageBox.Show("Error un UpdateMonthlyTotalSales Fuction : " & ex.Message)
        End Try
    End Sub

    Private Sub UpdateMonthlySalesProgress()

        Dim query As String = "SELECT SUM(CAST(net_amount AS INT)) AS MonthlyTotalSales FROM [dbo].[bill] WHERE YEAR(CONVERT(DATE, [date], 103)) = YEAR(GETDATE()) AND MONTH(CONVERT(DATE, [date], 103)) = MONTH(GETDATE());"
        Dim cmd As New SqlCommand(query, con)

        Try
            Dim result As Object = cmd.ExecuteScalar()
            Dim monthlyTotalSales As Integer = 0
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                monthlyTotalSales = Convert.ToInt32(result)
            End If

            Dim progressPercentage As Integer = 0
            If monthlyGoals > 0 Then
                progressPercentage = Math.Min(100, CInt((monthlyTotalSales / monthlyGoals) * 100))
            End If

            CircleProgressBar1.Value = progressPercentage
            lblgoals.Text = progressPercentage & " %"
            lblGoalAmoount.Text = "₹ " & monthlyGoals & ".00"

        Catch ex As Exception
            MessageBox.Show("Error in UpdateMonthlySalesProgress Fuction : " & ex.Message)
        End Try
    End Sub


    Private Sub dashboardvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblName.Text = "Hi " & name & ", "
        con.Open()
        TopSellingItem()
        LoadTodaysBills()
        ShowLowStock()
        FetchCurrentMonthGoal()
        Dim firstDate As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
        Dim lastDate As DateTime = firstDate.AddMonths(1).AddDays(-1)
  
        Label15.Text = firstDate.ToString("dd/MM/yyyy")
        Label16.Text = lastDate.ToString("dd/MM/yyyy")
        Label5.Text = DateTime.Now.ToString("dd/MM/yyyy")
        Label9.Text = DateTime.Now.ToString("dd/MM/yyyy")
        timerdataload.Interval = 500
        timerdataload.Start()

    End Sub

    Private Sub ShowLowStock()
        Dim query As String = "SELECT prod_id, prod_name, qty, reorder_level FROM [dbo].[product] WHERE qty <= reorder_level;"
        Dim cmd As New SqlCommand(query, con)

        Try
            Dim dt As New DataTable()
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(dt)
            DataGridView2.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error in ShowLowStock Fuction : " & ex.Message)
        End Try
    End Sub

    Private Sub loadDashboardData(paymentType As String, label As Label)
        Dim query As String = "SELECT SUM(CAST(net_amount AS INT)) AS TotalNetAmount FROM [dbo].[bill] WHERE CONVERT(DATE, [date], 103) = CONVERT(DATE, GETDATE(), 103) AND payment_type = @PaymentType;"
        Dim cmd As New SqlCommand(query, con)
        cmd.Parameters.AddWithValue("@PaymentType", paymentType)

        Try
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                label.Text = "₹ " & result.ToString() & ".00"
            Else
                label.Text = "₹ 0.00"
            End If
        Catch ex As Exception
            MessageBox.Show("Error in loadDashboardData fuction : " & ex.Message)
        End Try
    End Sub

    Private Sub LoadTodaysBills()
        Dim query As String = "SELECT bill_no, user_id, customer_id, total_item, net_amount, payment_type, time, " &
                          "DATEADD(SECOND, DATEDIFF(SECOND, '00:00:00', TRY_CONVERT(TIME, [time])), TRY_CONVERT(DATETIME, [date], 103)) AS DateTimeCombined " &
                          "FROM [dbo].[bill] " &
                          "WHERE TRY_CONVERT(DATE, [date], 103) = CONVERT(DATE, GETDATE(), 103) " &
                          "ORDER BY DateTimeCombined DESC;"
        Dim cmd As New SqlCommand(query, con)

        Try
            Dim dt As New DataTable()
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(dt)
            DataGridView1.DataSource = dt
            DataGridView1.Columns("DateTimeCombined").Visible = False
            lblTotalBillToday.Text = (DataGridView1.Rows.Count - 1).ToString()
        Catch ex As Exception
            MessageBox.Show("Error in LoadTodaysBills Function: " & ex.Message)
        End Try
    End Sub


    Private Sub LoadTotalSals()
        Dim query As String = "SELECT SUM(CAST(net_amount AS INT)) AS TotalNetAmount FROM [dbo].[bill] WHERE CONVERT(DATE, [date], 103) = CONVERT(DATE, GETDATE(), 103);"
        Dim cmd As New SqlCommand(query, con)

        Try
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                lblTotalSalsToday.Text = "₹ " & result.ToString() & ".00"
            Else
                lblTotalSalsToday.Text = "₹ 0.00"
            End If
        Catch ex As Exception
            MessageBox.Show("Error in LoadTotalSals Fuction : " & ex.Message)
        End Try
    End Sub

    Private Sub TopSellingItem()
        Dim query As String = "SELECT TOP 10 bi.prod_id, bi.prod_name, SUM(CAST(bi.qty AS INT)) AS total_quantity FROM bill b JOIN bill_items bi ON b.bill_no = bi.bill_no WHERE YEAR(CONVERT(DATE, b.date, 103)) = YEAR(GETDATE()) AND MONTH(CONVERT(DATE, b.date, 103)) = MONTH(GETDATE()) GROUP BY bi.prod_id, bi.prod_name ORDER BY total_quantity DESC;
"
        Dim cmd As New SqlCommand(query, con)

        Try
            Dim dt As New DataTable()
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(dt)
            DataGridView3.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error in TopSellingItem Fuction : " & ex.Message)
        End Try
    End Sub

    Private Sub LoadTodayProfit()
        Dim query As String = "SELECT SUM(CAST(total_profit AS INT)) AS TotalProfit FROM [dbo].[bill] WHERE CONVERT(DATE, [date], 103) = CONVERT(DATE, GETDATE(), 103);"
        Dim cmd As New SqlCommand(query, con)

        Try
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                lblProfit.Text = "₹ " & result.ToString() & ".00"
            Else
                lblProfit.Text = "₹ 0.00"
            End If
        Catch ex As Exception
            MessageBox.Show("Error in LoadTodayProfit Fuction : " & ex.Message)
        End Try
    End Sub

    Private Sub dashboardvb_Leave(sender As Object, e As EventArgs) Handles Me.Leave
        UpdateGoalStatus()
        timerdataload.Stop()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Private Sub timerdataload_Tick(sender As Object, e As EventArgs) Handles timerdataload.Tick
        LoadTodaysBills()
        ShowLowStock()
        loadDashboardData("0", lblTotalCaseToday)
        loadDashboardData("24", cardPaymentToday)
        loadDashboardData("27", lblMobilPaymentToday)
        LoadTotalSals()
        UpdateMonthlyTotalSales(lblMonthTotal)
        UpdateMonthlySalesProgress()
        LoadTodayProfit()
        TopSellingItem()

    End Sub

    Private Sub Guna2ToggleSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2ToggleSwitch1.CheckedChanged

        If timerdataload.Enabled Then
            timerdataload.Stop()
        Else
            timerdataload.Start()
        End If
    End Sub

    Private Function IsCurrentMonthGoalSet() As Boolean
        Dim query As String = "SELECT COUNT(*) FROM [dbo].[monthly_goals] WHERE YEAR([date]) = YEAR(GETDATE()) AND MONTH([date]) = MONTH(GETDATE());"
        Dim cmd As New SqlCommand(query, con)

        Try
            Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return result > 0
        Catch ex As Exception
            MessageBox.Show("Error checking current month's goal: " & ex.Message)
            Return False
        End Try
    End Function

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        If IsCurrentMonthGoalSet Then
            Dim result = MessageBox.Show("Your goal for this month is already set. Do you want to update it with a new amount?", "Update Monthly Goal", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                UpdateCurrentMonthGoal
            End If
        Else
            SetNewMonthlyGoal
        End If
    End Sub

    Private Sub UpdateCurrentMonthGoal()
        Dim goalAmountInput As String = InputBox("Enter the new monthly sales goal amount:", "Update Monthly Goal")
        Dim goalAmount As Integer
        If Integer.TryParse(goalAmountInput, goalAmount) Then
            Dim query As String = "UPDATE [dbo].[monthly_goals] SET goal_amount = @GoalAmount, [user] = @user WHERE YEAR([date]) = YEAR(GETDATE()) AND MONTH([date]) = MONTH(GETDATE());"
            Dim cmd As New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@GoalAmount", goalAmount)
            cmd.Parameters.AddWithValue("@user", userId)

            Try
                cmd.ExecuteNonQuery()
                MessageBox.Show("Monthly goal updated successfully!")
                monthlyGoals = goalAmount
                UpdateMonthlySalesProgress()
                lblGoalAmoount.Text = "₹ " & monthlyGoals & ".00"
            Catch ex As Exception
                MessageBox.Show("Error in UpdateCurrentMonthGoal Fuction : " & ex.Message)
            End Try
        Else
            MessageBox.Show("Please enter a valid goal amount.")
        End If
    End Sub

    Private Sub SetNewMonthlyGoal()
        Dim goalAmountInput As String = InputBox("Enter the monthly sales goal amount:", "Set Monthly Goal")
        Dim goalAmount As Integer
        If Integer.TryParse(goalAmountInput, goalAmount) Then
            Dim currentDate As DateTime = DateTime.Now
            Dim status As String = "0%"
            Dim query As String = "INSERT INTO [dbo].[monthly_goals] ([date], [goal_amount], [status], [user]) VALUES (@Date, @GoalAmount, @Status, @User);"
            Dim cmd As New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@Date", currentDate)
            cmd.Parameters.AddWithValue("@GoalAmount", goalAmount)
            cmd.Parameters.AddWithValue("@Status", status)
            cmd.Parameters.AddWithValue("@User", userId)

            Try
                cmd.ExecuteNonQuery()
                MessageBox.Show("Monthly goal set successfully!")
                monthlyGoals = goalAmount
                UpdateMonthlySalesProgress()
                FetchCurrentMonthGoal()
            Catch ex As Exception
                MessageBox.Show("Error in SetNewMonthlyGoal
Fuction : " & ex.Message)
            End Try
        Else
            MessageBox.Show("Please enter a valid goal amount.")
        End If
    End Sub

    Private Sub UpdateGoalStatus()
        Dim query As String = "UPDATE [dbo].[monthly_goals] SET status = @status WHERE YEAR([date]) = YEAR(GETDATE()) AND MONTH([date]) = MONTH(GETDATE());"
        Dim cmd As New SqlCommand(query, con)

        cmd.Parameters.AddWithValue("@status", lblgoals.Text)

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub FetchCurrentMonthGoal()
        Dim query As String = "SELECT TOP 1 goal_amount FROM [dbo].[monthly_goals] WHERE YEAR([date]) = YEAR(GETDATE()) AND MONTH([date]) = MONTH(GETDATE()) ORDER BY [date] DESC;"
        Dim cmd As New SqlCommand(query, con)

        Try
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                monthlyGoals = Convert.ToInt32(result)
                lblGoalAmoount.Text = "₹ " & monthlyGoals & ".00"
            Else
                monthlyGoals = 0
                lblGoalAmoount.Text = "₹ " & monthlyGoals & ".00"
            End If
        Catch ex As Exception
            MessageBox.Show("Error fetching current month's goal: " & ex.Message)
        End Try
    End Sub


End Class