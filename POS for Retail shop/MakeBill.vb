Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class MakeBill
    Private connectionString As String = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
    Dim userId As String
    Dim conn As SqlConnection
    Dim Net_Amount As Integer = 0
    Dim Total_mrp As Integer = 0
    Dim Total_dis As Integer = 0
    Dim Total_items As Integer = 0
    Dim Total_qty As Integer = 0
    Dim Extra_dis As Integer = 0
    Dim dis As Integer = 0
    Dim Bill_no As Integer = 0
    Dim total_profit As Integer = 0
    Dim payment_details As String = "emty"

    Public Sub New(ByVal userId As String)
        InitializeComponent()
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
                lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MakeBill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.BringToFront()
        lblBranch.Text = "Branch = SDO"
        lblUser.Text = userId

        conn = New SqlConnection(connectionString)
        conn.Open()
        in_Qty.Text = "1"
        in_Customer_Id.Text = "0"
        Me.KeyPreview = True
        in_Code.Focus()
        Label9.Visible = False
        in_Extra_dis.Visible = False
        findCustomer()
        AddHandler Doc.PrintPage, AddressOf PrintDocument1_PrintPage
        PPD.Document = Doc
        'AddHandler PPD.KeyDown, AddressOf PPD_KeyDown
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles Item_List.SelectionChanged
        If Item_List.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = Item_List.SelectedRows(0)
            Dim mrp As Integer = Convert.ToInt32(selectedRow.Cells("Column3").Value)
            Dim dis As Integer = Convert.ToInt32(selectedRow.Cells("DataGridViewTextBoxColumn1").Value)
            in_Item_Dis.Text = mrp - dis.ToString()
        End If
    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyEventArgs) Handles in_payment_type.KeyDown
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrEmpty(in_payment_type.Text) Then
                MessageBox.Show("Please enter payment Type")
            Else
                Dim type As String = in_payment_type.Text
                Dim qury As String = "select name from Payment_type where type = @type"
                Dim cmd As New SqlCommand(qury, conn)
                cmd.Parameters.AddWithValue("@type", type)

                Try
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        in_Payment_Mode.Text = reader("name").ToString()
                    Else
                        MessageBox.Show("Payment Type not found.")
                    End If
                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("Error retrieving Customer details: " & ex.Message)
                End Try
                If type = "0" Then
                    in_Receive_Amount.Focus()
                End If

            End If
        End If
    End Sub

    Private Sub TextBox11_KeyDown(sender As Object, e As KeyEventArgs) Handles in_Receive_Amount.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim cash As Integer = Convert.ToInt32(in_Receive_Amount.Text)
            in_Change.Text = cash - Net_Amount.ToString()
        End If
    End Sub

    Private Sub ShowSaveOptionsMessageBox()
        Dim result As DialogResult = MessageBox.Show("Do you want to save this bill?", "Save Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

        If result = DialogResult.Yes Then
            SaveBill()
            Dim printResult As DialogResult = MessageBox.Show("Print Bill No = " & Bill_no, "Print Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If printResult = DialogResult.Yes Then
                PrintBill(Bill_no)
            End If

            ResetForm()
        End If
    End Sub

    Private Sub CalculatebillProfit(productId As String, qty As Integer)
        Dim qury As String = "SELECT (sale_rate - total_cost) AS profit FROM product WHERE prod_id = @ProductId"
        Dim cmd As New SqlCommand(qury, conn)
        cmd.Parameters.AddWithValue("@ProductId", productId)

        Try
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                total_profit += Convert.ToInt32(reader("profit")) * qty
                reader.Close()
            Else
                reader.Close()
                MessageBox.Show("Bill profit error.")
            End If

        Catch ex As Exception
            MessageBox.Show("Error retrieving bill profit: " & ex.Message)
        End Try
    End Sub

    Private Sub SaveBill()
        If in_payment_type.Text = "0" Then
            payment_details = Net_Amount.ToString() & " > " & in_Change.Text
        Else
            payment_details = Net_Amount.ToString() & " > 0.00"
        End If

        Dim qury As String = "select no from Number where fild_name = @fild"
        Dim cmd As New SqlCommand(qury, conn)
        cmd.Parameters.AddWithValue("@fild", "bill_no")

        Try
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Bill_no = reader("no") + 1
                reader.Close()

                Using con As New SqlConnection(connectionString)
                    con.Open()
                    Dim updateCmd As New SqlCommand("update Number set no = @no where fild_name = @fild", con)
                    updateCmd.Parameters.AddWithValue("@no", Bill_no)
                    updateCmd.Parameters.AddWithValue("@fild", "bill_no")
                    updateCmd.ExecuteNonQuery()
                End Using
            Else
                reader.Close()
                MessageBox.Show("Bill number error.")
                Return
            End If

        Catch ex As Exception
            MessageBox.Show("Error retrieving bill number: " & ex.Message)
            Return
        End Try

        For Each row As DataGridViewRow In Item_List.Rows
            If Not row.IsNewRow Then
                CalculatebillProfit(row.Cells("Column6").Value, Convert.ToInt32(row.Cells("Column2").Value))
            End If
        Next

        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim transaction As SqlTransaction = conn.BeginTransaction()

            Try
                Dim billQuery As String = "INSERT INTO bill (bill_no, user_id, customer_id, total_mrp, total_dis, extra_dis, net_amount, total_profit, total_item, total_qty, payment_type, payment_details, narration, date, time) " &
                                      "VALUES (@bill_no, @user_id, @customer_id, @total_mrp, @total_dis, @extra_dis, @net_amount, @total_profit, @total_item, @total_qty, @payment_type, @payment_details, @narration, @date, @time)"
                Dim billCmd As New SqlCommand(billQuery, conn, transaction)
                billCmd.Parameters.AddWithValue("@bill_no", Bill_no)
                billCmd.Parameters.AddWithValue("@user_id", userId)
                billCmd.Parameters.AddWithValue("@customer_id", in_Customer_Id.Text)
                billCmd.Parameters.AddWithValue("@total_mrp", Total_mrp)
                billCmd.Parameters.AddWithValue("@total_dis", Total_dis)
                billCmd.Parameters.AddWithValue("@extra_dis", dis)
                billCmd.Parameters.AddWithValue("@net_amount", Net_Amount)
                billCmd.Parameters.AddWithValue("@total_profit", total_profit.ToString())
                billCmd.Parameters.AddWithValue("@total_item", Total_items)
                billCmd.Parameters.AddWithValue("@total_qty", Total_qty)
                billCmd.Parameters.AddWithValue("@payment_type", in_payment_type.Text)
                billCmd.Parameters.AddWithValue("@payment_details", payment_details)
                billCmd.Parameters.AddWithValue("@narration", in_Narration.Text)
                billCmd.Parameters.AddWithValue("@date", lblDate.Text)
                billCmd.Parameters.AddWithValue("@time", lblClock.Text)
                billCmd.ExecuteNonQuery()

                For Each row As DataGridViewRow In Item_List.Rows
                    If Not row.IsNewRow Then
                        Dim itemQuery As String = "INSERT INTO bill_items (bill_no, prod_id, prod_name, mrp, sale_rate, dis, qty, total_rate) " &
                                              "VALUES (@bill_no, @prod_id, @prod_name, @mrp, @sale_rate, @dis, @qty, @total_rate)"
                        Dim itemCmd As New SqlCommand(itemQuery, conn, transaction)
                        itemCmd.Parameters.AddWithValue("@bill_no", Bill_no)
                        itemCmd.Parameters.AddWithValue("@prod_id", row.Cells("Column6").Value)
                        itemCmd.Parameters.AddWithValue("@prod_name", row.Cells("Column1").Value)
                        itemCmd.Parameters.AddWithValue("@mrp", row.Cells("Column3").Value)
                        itemCmd.Parameters.AddWithValue("@sale_rate", row.Cells("DataGridViewTextBoxColumn1").Value)
                        itemCmd.Parameters.AddWithValue("@dis", row.Cells("Column4").Value)
                        itemCmd.Parameters.AddWithValue("@qty", row.Cells("Column2").Value)
                        itemCmd.Parameters.AddWithValue("@total_rate", row.Cells("Column5").Value)
                        itemCmd.ExecuteNonQuery()

                        Dim updateProductQuery As String = "UPDATE product SET qty = qty - @quantity, sold_qty = sold_qty + @quantity WHERE prod_id = @prod_id"
                        Dim updateProductCmd As New SqlCommand(updateProductQuery, conn, transaction)
                        updateProductCmd.Parameters.AddWithValue("@quantity", row.Cells("Column2").Value)
                        updateProductCmd.Parameters.AddWithValue("@prod_id", row.Cells("Column6").Value)
                        updateProductCmd.ExecuteNonQuery()
                    End If
                Next

                transaction.Commit()

            Catch ex As Exception

                transaction.Rollback()
                MessageBox.Show("Error saving bill: " & ex.Message)
            End Try
        End Using

        in_Last_Bill_No.Text = Bill_no.ToString()
        in_Last_Bill_Amount.Text = payment_details


    End Sub

    Private Sub ResetForm()
        Net_Amount = 0
        Total_mrp = 0
        Total_dis = 0
        Total_items = 0
        Total_qty = 0
        Extra_dis = 0
        dis = 0
        Bill_no = 0
        total_profit = 0

        in_Code.Clear()
        in_Qty.Text = "1"
        in_Total_MRP.Clear()
        in_Total_Dis.Clear()
        in_Narration.Clear()
        in_Net_Amount.Clear()
        in_payment_type.Clear()
        in_Payment_Mode.Clear()
        in_Receive_Amount.Clear()
        in_Change.Clear()
        in_Total_Item.Clear()
        in_Extra_dis.Clear()
        in_Total_Qty.Clear()
        in_Item_Dis.Clear()
        lbl_Net_Amount.Text = "₹ 0.00"
        in_Customer_Id.Text = "0"
        Label9.Visible = False
        in_Extra_dis.Visible = False
        findCustomer()

        Item_List.Rows.Clear()
        in_Code.Focus()
    End Sub

    Private Sub PrintBill(billNumber As Integer)
        Dim Pagehigth As Integer = 500 + (Total_items * 13)
        If (in_Customer_Id.Text = "0") Then
            Pagehigth += 0
        Else
            Pagehigth += 36
        End If
        If String.IsNullOrEmpty(in_Receive_Amount.Text) Or String.IsNullOrEmpty(in_Change.Text) Then
            Pagehigth += 0
        Else
            Pagehigth += 13
        End If
        If String.IsNullOrEmpty(in_Narration.Text) Then
            Pagehigth += 0
        Else
            Pagehigth += 13
        End If
        Doc.DefaultPageSettings.PaperSize = New PaperSize("MyBill", 250, Pagehigth)
        PPD.WindowState = FormWindowState.Maximized
        PPD.PrintPreviewControl.AutoZoom = True

        PPD.Document = Doc
        PPD.ShowDialog()
    End Sub

    Private Sub add_product()
        Dim productCode As String = in_Code.Text.Trim()
        Dim quantity As Integer

        If String.IsNullOrEmpty(in_Qty.Text) Then
            quantity = 1
        Else
            If Not Integer.TryParse(in_Qty.Text, quantity) Then
                MessageBox.Show("Invalid quantity. Please enter a valid number.")
                Return
            End If
        End If

        Dim query As String = "SELECT prod_name, prod_mrp, disc, sale_rate, qty FROM product WHERE prod_id = @ProductCode"
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@ProductCode", productCode)

        Try
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim productName As String = reader("prod_name").ToString()
                Dim mrp As Integer = Convert.ToInt32(reader("prod_mrp"))
                Dim disc As Integer = Convert.ToInt32(reader("disc"))
                Dim saleRate As Integer = Convert.ToInt32(reader("sale_rate"))
                Dim availableQuantity As Integer = Convert.ToInt32(reader("qty"))

                If quantity > availableQuantity Then
                    MessageBox.Show("Item '" & productName & "' is out of stock.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    in_Code.Clear()
                    in_Code.Focus()
                    reader.Close()
                    Return
                End If

                Dim totalRate As Integer = quantity * saleRate
                Net_Amount += totalRate
                lbl_Net_Amount.Text = "₹ " + Net_Amount.ToString() + ".00"
                in_Net_Amount.Text = Net_Amount.ToString()
                Total_mrp += mrp * quantity
                in_Total_MRP.Text = Total_mrp.ToString()
                Total_dis += (mrp - saleRate) * quantity
                in_Total_Dis.Text = Total_dis.ToString()
                Total_qty += quantity
                in_Total_Qty.Text = Total_qty.ToString()
                Total_items = Item_List.Rows.Count
                in_Total_Item.Text = Total_items.ToString
                Dim row_index As Integer = Item_List.Rows.Add(productCode, productName, quantity, mrp, disc, saleRate, totalRate)
                Item_List.FirstDisplayedScrollingRowIndex = row_index
            Else
                MessageBox.Show("Product not found.")
            End If
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error retrieving product details: " & ex.Message)
        End Try

        in_Qty.Text = "1"
    End Sub



    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles Item_List.KeyDown
        If e.KeyCode = Keys.F4 AndAlso e.Modifiers = Keys.Shift Or e.KeyCode = Keys.Delete Then
            If Item_List.SelectedRows.Count > 0 Then
                Dim rowIndex As Integer = Item_List.SelectedRows(0).Index

                Dim totalRate As Integer = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("Column5").Value)
                Dim mrp As Integer = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("Column3").Value)
                Dim salerate As Integer = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("DataGridViewTextBoxColumn1").Value)
                Dim dis As Integer = mrp - salerate
                Dim qty As Integer = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("Column2").Value)

                Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    Item_List.Rows.Remove(Item_List.SelectedRows(0))
                    Net_Amount -= totalRate
                    lbl_Net_Amount.Text = "₹ " + Net_Amount.ToString() + ".00"
                    in_Net_Amount.Text = Net_Amount.ToString()
                    Total_mrp -= mrp * qty
                    in_Total_MRP.Text = Total_mrp
                    Total_dis -= dis * qty
                    in_Total_Dis.Text = Total_dis
                    Total_qty -= qty
                    in_Total_Qty.Text = Total_qty
                    Total_items -= 1
                    in_Total_Item.Text = Total_items.ToString
                End If
            End If
        End If
    End Sub

    Private Sub findCustomer()
        If String.IsNullOrEmpty(in_Customer_Id.Text) Then
            in_Customer_Name.Clear()

        Else
            Dim custo_code As String = in_Customer_Id.Text
            Dim qury As String = "select Name,Dis from Customers where CustomerID = @custo_code"
            Dim cmd As New SqlCommand(qury, conn)
            cmd.Parameters.AddWithValue("@custo_code", custo_code)

            Try
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                If reader.Read() Then
                    in_Customer_Name.Text = reader("Name").ToString()
                    Extra_dis = Convert.ToInt32(reader("Dis"))
                Else
                    MessageBox.Show("Customer not found.")
                End If
                reader.Close()
            Catch ex As Exception
                MessageBox.Show("Error retrieving Customer details: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles Doc.PrintPage
        Dim TitleStyle As New Font("Bauhaus 93", 20, FontStyle.Bold)
        Dim font1 As New Font("Arial", 8)
        Dim comman As New Font("Arial", 6, FontStyle.Regular)
        Dim font As New Font("Arial", 10)
        Dim boldFont As New Font("Arial", 7, FontStyle.Bold)
        Dim brush As New SolidBrush(Color.Black)
        Dim yPosition As Integer = 100
        Dim lineHeight As Integer = font.GetHeight(e.Graphics) + 0.5

        Dim left As New StringFormat
        Dim center As New StringFormat
        Dim right As New StringFormat

        left.Alignment = StringAlignment.Near
        center.Alignment = StringAlignment.Center
        right.Alignment = StringAlignment.Far

        Dim Rect1 As New Rectangle(5, 55, 240, 32)
        Dim Rect2 As New Rectangle(5, 40, 240, 20)
        e.Graphics.DrawString("***TAX-INVOICE***", comman, Brushes.Black, Rect2, center)
        e.Graphics.DrawString("The Retail Spot", TitleStyle, Brushes.Black, Rect1, center)
        e.Graphics.DrawString("JADHAV SUPERMART LLP", comman, brush, 5, 100)
        yPosition += lineHeight - 4
        e.Graphics.DrawString("SHOP NO GF 17 TO 30, FIRST FLOOR 9 TO 16,", comman, brush, 5, yPosition)
        yPosition += lineHeight - 4
        e.Graphics.DrawString("MADHURAM ARCADE,DINDOLI-KHARVASA RD,", comman, brush, 5, yPosition)
        yPosition += lineHeight - 4
        e.Graphics.DrawString("DINDOLI,SURAT,GUJARAT", comman, brush, 5, yPosition)
        yPosition += lineHeight - 4
        e.Graphics.DrawString("SURAT-394210", comman, brush, 5, yPosition)
        yPosition += lineHeight - 4
        e.Graphics.DrawString("'CALL FOR HOME DELIVERY' PH:9712749444", boldFont, Brushes.Black, New Rectangle(0, yPosition, 250, 20), center)
        yPosition += lineHeight - 4
        e.Graphics.DrawString("GSTNO:24ABSFA2987M1ZP", comman, brush, 5, yPosition)
        yPosition += lineHeight - 2
        e.Graphics.DrawString("BILL_NO: ", comman, brush, 5, yPosition)
        yPosition -= lineHeight - 14
        e.Graphics.DrawString(Bill_no.ToString(), boldFont, brush, 45, yPosition)
        e.Graphics.DrawString("DT/TIME: " & DateTime.Now.ToString("dd-MM-yyyy hh:mmtt"), comman, brush, 120, yPosition + 1)
        yPosition += lineHeight - 2
        e.Graphics.DrawString("Cust.ID: " & in_Customer_Id.Text, comman, brush, 5, yPosition)
        e.Graphics.DrawString("Name:" & in_Customer_Name.Text, boldFont, brush, 60, yPosition - 1)
        yPosition += lineHeight - 2
        e.Graphics.DrawString("------------------------------------------------------------------------------------", comman, brush, 3, yPosition)
        yPosition += lineHeight - 7

        e.Graphics.DrawString("code", comman, Brushes.Black, New Rectangle(5, yPosition, 30, 10), left)
        e.Graphics.DrawString("Item", comman, Brushes.Black, New Rectangle(35, yPosition, 72, 10), left)
        e.Graphics.DrawString("Qty", comman, Brushes.Black, New Rectangle(109, yPosition, 20, 10), left)
        e.Graphics.DrawString("MRP", comman, Brushes.Black, New Rectangle(129, yPosition, 36, 10), left)
        e.Graphics.DrawString("Rate", comman, Brushes.Black, New Rectangle(165, yPosition, 36, 10), left)
        e.Graphics.DrawString("Amount", comman, Brushes.Black, New Rectangle(201, yPosition, 36, 10), left)
        yPosition += lineHeight - 5
        e.Graphics.DrawString("------------------------------------------------------------------------------------", comman, brush, 3, yPosition)
        yPosition += lineHeight - 5

        For Each row As DataGridViewRow In Item_List.Rows
            If Not row.IsNewRow Then
                e.Graphics.DrawString(row.Cells("Column6").Value.ToString(), comman, brush, New Rectangle(5, yPosition, 30, 10), left)
                e.Graphics.DrawString(row.Cells("Column1").Value.ToString(), comman, brush, New Rectangle(35, yPosition, 72, 10), left)
                e.Graphics.DrawString(row.Cells("Column2").Value.ToString(), comman, brush, New Rectangle(109, yPosition, 20, 10), left)
                e.Graphics.DrawString(row.Cells("Column3").Value.ToString(), comman, brush, New Rectangle(129, yPosition, 36, 10), left)
                e.Graphics.DrawString(row.Cells("DataGridViewTextBoxColumn1").Value.ToString(), comman, brush, New Rectangle(165, yPosition, 36, 10), left)
                e.Graphics.DrawString(row.Cells("Column5").Value.ToString(), comman, brush, New Rectangle(201, yPosition, 36, 10), left)
                yPosition += lineHeight - 4
            End If
        Next

        yPosition += lineHeight - 15
        e.Graphics.DrawString("------------------------------------------------------------------------------------", comman, brush, 3, yPosition)
        yPosition += lineHeight - 7

        e.Graphics.DrawString("Total Lines : " & Total_items.ToString(), comman, brush, 5, yPosition)
        e.Graphics.DrawString(Total_qty.ToString(), comman, brush, New Rectangle(109, yPosition, 20, 10), left)
        e.Graphics.DrawString(Total_mrp.ToString(), comman, brush, New Rectangle(129, yPosition, 36, 10), left)
        e.Graphics.DrawString(Total_mrp - Total_dis.ToString(), comman, brush, New Rectangle(201, yPosition, 36, 10), left)
        yPosition += lineHeight - 3
        e.Graphics.DrawString("M/C''CCE:CASH-2 ''" & userId.ToString(), comman, brush, 5, yPosition)
        If (in_Customer_Id.Text = "0") Then
            yPosition += lineHeight - 5
        Else
            yPosition += lineHeight - 4
            e.Graphics.DrawString("Bill Scheme Discount :                  " & dis.ToString(), comman, brush, New Rectangle(0, yPosition, 250, 10), center)
            yPosition += lineHeight - 5
            e.Graphics.DrawString("------------------------------------------------", comman, brush, 3, yPosition)
            yPosition += lineHeight - 5
            e.Graphics.DrawString("Bill/GV Extra Discount      :         " & dis.ToString(), comman, brush, 5, yPosition)
            yPosition += lineHeight - 5
        End If
        e.Graphics.DrawString("--------------------", comman, brush, New Rectangle(0, yPosition, 245, 10), right)
        yPosition += lineHeight - 3
        e.Graphics.DrawString("Net Bill Value : ", font1, brush, New Rectangle(24, yPosition, 250, 15), center)
        e.Graphics.DrawString(Net_Amount.ToString(), boldFont, brush, New Rectangle(190, yPosition, 50, 15), right)
        yPosition += lineHeight - 3
        e.Graphics.DrawString("--------------------", comman, brush, New Rectangle(0, yPosition, 245, 10), right)
        yPosition += lineHeight - 3
        e.Graphics.DrawString(in_Payment_Mode.Text.ToString() & "          " & Net_Amount.ToString(), comman, brush, New Rectangle(0, yPosition, 250, 15), center)
        If String.IsNullOrEmpty(in_Receive_Amount.Text) Or String.IsNullOrEmpty(in_Change.Text) Then
            yPosition += lineHeight - 4
        Else
            yPosition += lineHeight - 5
            e.Graphics.DrawString("Recd. Cash=" & in_Receive_Amount.Text.ToString() & ".00    *Change : " & in_Change.Text.ToString() & ".00", comman, brush, New Rectangle(0, yPosition, 250, 10), center)
        End If
        If String.IsNullOrEmpty(in_Narration.Text) Then
            yPosition += lineHeight - 4
        Else
            yPosition += lineHeight - 3
            e.Graphics.DrawString(in_Narration.Text.ToString(), comman, brush, New Rectangle(0, yPosition, 250, 15), center)
            yPosition += lineHeight - 4
        End If
        e.Graphics.DrawString("Your Savings on MRP:Rs." & Total_dis + dis.ToString() & ".00", font1, brush, 10, yPosition)
        yPosition += lineHeight - 4
        e.Graphics.DrawString("=>Many Products(like Pulses) are already", comman, brush, 10, yPosition)
        yPosition += lineHeight - 5
        e.Graphics.DrawString("discounted & include your Savings.", comman, brush, 10, yPosition)
        yPosition += lineHeight + 5
        e.Graphics.DrawString("SUBJECT TO SURAT JURISDICTION.", comman, brush, 5, yPosition)
        yPosition += lineHeight - 5
        e.Graphics.DrawString("PRICE INCL ALL TAXES.", comman, brush, 5, yPosition)
        yPosition += lineHeight - 5
        e.Graphics.DrawString("THE GOODS SOLD AS PART OF THIS BILL ARE INTENDED FOR END", comman, brush, 5, yPosition)
        yPosition += lineHeight + 4
        e.Graphics.DrawString("USER CONSUMPTION AND NOT FOR RESALE.", comman, brush, 5, yPosition)
        yPosition += lineHeight - 5
        e.Graphics.DrawString("NO EXCHANGE WITHOUT BILL.", comman, brush, 5, yPosition)
        yPosition += lineHeight - 5
        e.Graphics.DrawString("Thank you:). Visit again.", comman, brush, New Rectangle(0, yPosition, 250, 15), center)
        yPosition += lineHeight - 5
        e.Graphics.DrawString("FSSAI NO: 10721031000498", comman, brush, 5, yPosition)
    End Sub

    Private Sub MakeBill_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        conn.Close()
    End Sub

    Private Sub MakeBill_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            If in_Code.Focused Then
                in_payment_type.Focus()
                If String.IsNullOrEmpty(in_Customer_Id.Text) Then
                    Label9.Visible = False
                    in_Extra_dis.Visible = False
                Else
                    If in_Customer_Id.Text = "0" Then

                    Else
                        Label9.Visible = True
                        in_Extra_dis.Visible = True
                    End If
                    dis = (Net_Amount * Extra_dis) / 100
                    in_Extra_dis.Text = dis.ToString()
                    Net_Amount -= dis
                    lbl_Net_Amount.Text = "₹ " + Net_Amount.ToString() + ".00"
                    in_Net_Amount.Text = Net_Amount.ToString()
                End If
            Else
                in_Code.Focus()
            End If
        End If


        If in_Code.Focused Then
            If e.KeyCode = Keys.Enter Then
                add_product()
                in_Code.Clear()
            End If
        End If

        If e.KeyCode = Keys.F4 Then
            If in_Code.Focused Then
                in_Qty.Focus()
            End If
        End If

        If e.KeyCode = Keys.C AndAlso e.Modifiers = Keys.Alt Then
            in_Customer_Id.Focus()
        End If


        If e.KeyCode = Keys.F10 Then
            If String.IsNullOrEmpty(in_payment_type.Text) Then
                MessageBox.Show("Please enter payment type for save bill")
            Else
                If in_payment_type.Focused Or in_Receive_Amount.Focused Then
                    ShowSaveOptionsMessageBox()
                Else
                    MessageBox.Show("please go to payment section and complete payment")
                End If
            End If
        End If
    End Sub

    Private Sub in_Payment_Mode_Enter(sender As Object, e As EventArgs) Handles in_Payment_Mode.Enter
        in_Receive_Amount.Focus()
    End Sub

    Private Sub in_Customer_Id_Leave(sender As Object, e As EventArgs) Handles in_Customer_Id.Leave
        findCustomer()
    End Sub

    Private Sub in_Code_Enter(sender As Object, e As EventArgs) Handles in_Customer_Id.Enter, in_Code.Enter, in_Qty.Enter, in_Customer_Name.Enter, in_Last_Bill_No.Enter, in_Last_Bill_Amount.Enter, in_Narration.Enter, in_Total_Item.Enter, in_Total_Qty.Enter, in_Item_Dis.Enter
        in_Extra_dis.Clear()
        Net_Amount += dis
        dis = 0
        lbl_Net_Amount.Text = "₹ " + Net_Amount.ToString() + ".00"
        in_Net_Amount.Text = Net_Amount.ToString()
        Label9.Visible = False
        in_Extra_dis.Visible = False
    End Sub

End Class