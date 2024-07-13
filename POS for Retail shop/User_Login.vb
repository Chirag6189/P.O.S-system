Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class User_Login

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand

    Private Function GetUserName(ByVal userId As String) As String
        Dim name As String
        Dim q3 As String = "select emp_name from employee where user_id = @value1"
        Using cmd As New SqlCommand(q3, con)
            cmd.Parameters.AddWithValue("@value1", userId)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                name = Convert.ToString(reader("emp_name"))
            End If
            reader.Close()
        End Using
        Return name
    End Function

    Private Function getUserPermission(ByVal userID As String) As String
        Dim permission As String
        Dim q2 As String = "select permition from employee where user_id = @value1"
        Using cmd As New SqlCommand(q2, con)
            cmd.Parameters.AddWithValue("@value1", userID)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                permission = Convert.ToString(reader("permition"))
            End If
            reader.Close()
        End Using
        Return permission
    End Function

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Static Dim user As String = txtUserId.Text
        Static Dim pass As String = txtpassword.Text
        con.Open()
        Dim qury As String
        qury = "select * from employee where user_id = @value1 and pass = @value2"
        cmd = New SqlCommand(qury, con)
        cmd.Parameters.AddWithValue("@value1", user)
        cmd.Parameters.AddWithValue("@value2", pass)
        Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds)
        Dim a As Integer
        a = ds.Tables(0).Rows.Count
        If a = 0 Then
            MessageBox.Show("login faile")
            con.Close()
        Else
            Dim name As String = GetUserName(user)
            Dim per As String = getUserPermission(user)
            Dim mdidorm As New Home_Page(name, per, user)
            Me.Hide()
            mdidorm.Show()

            con.Close()
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        MessageBox.Show("for 'Forgeot UserID or Password ?' login on Admin Id", "Forgeot details")
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        MessageBox.Show("for 'create account' login on Admin Id", "create account")
    End Sub

End Class