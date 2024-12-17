Imports System.Data.SqlClient
Imports System.Transactions

Public Class Receive_Purchase_Order

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand
    Dim receive_id As Integer
    Dim userId As String
    Dim suppName As String
    Dim user_id As String
    Dim po_date As String
    Dim status As String
    Dim po_num As Integer
    Dim r_total_item As Integer
    Dim r_total_qty As Integer
    Dim o_total_item As String
    Dim o_total_qty As String
    Dim O_total_cost As String
    Dim r_total_cost As Integer


    Public Sub New(ByVal userId As String)
        InitializeComponent()
        Me.userId = userId
    End Sub

    Private Sub Receive_Purchase_Order_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPanding_PO()
        Me.KeyPreview = True
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

                cmbPO.Items.Add($"({po_no} - {suppliername} = {user_id} = {po_date})")
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

    Private Sub cmbPO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPO.SelectedIndexChanged
        Dim selectedPO = cmbPO.SelectedItem.ToString
        Dim no_str As String = selectedPO.Split("-"c)(0).Trim
        Dim PO_no As String = no_str.Substring(1)
        If (PO_no = "elect Purchase Order") Then
            txtPO_no.Text = ""
            txtReceived_by.Text = ""
            txtPO_date.Text = ""
            txtPlaced_by.Text = ""
            txtSupplierName.Text = ""
            txtStatus.Text = "P.O Status"
            txtStatus.ForeColor = Color.Lavender
            txtStatus.BorderColor = Color.Lavender
        Else
            txtPO_no.Text = PO_no
            lblbilPono.Text = PO_no
            lblpono.Text = PO_no
            po_num = Convert.ToInt64(PO_no)
            fill_detail(PO_no)
            txtProductCode.Focus()
        End If
    End Sub

    Private Sub fill_detail(PO_no As String)
        Dim q1 As String = "SELECT p.user_id, p.Status, p.date, s.Supplier_Name, p.total_cost, p.total_item, p.total_qty " &
                   "FROM purchase_order p " &
                   "JOIN Supplier s ON p.Supplier_ID = s.Supplier_ID " &
                   "WHERE p.PO_no = @PO_no"
        Dim command As New SqlCommand(q1, con)
        command.Parameters.AddWithValue("@PO_no", Convert.ToInt64(PO_no))

        Try
            con.Open()
            Dim reader As SqlDataReader = command.ExecuteReader()

            If reader.Read() Then
                ' Ensure reader has rows to avoid accessing null values
                suppName = reader("Supplier_Name").ToString()
                user_id = reader("user_id").ToString()
                po_date = reader("date").ToString()
                status = reader("Status").ToString()
                O_total_cost = reader("total_cost").ToString()
                o_total_item = reader("total_item").ToString()
                o_total_qty = reader("total_qty").ToString()

                txtPO_date.Text = po_date
                txtPlaced_by.Text = user_id
                txtSupplierName.Text = suppName
                txtReceived_by.Text = userId
                orderAmount.Text = O_total_cost
                orderitem.Text = o_total_item
                orderqty.Text = o_total_qty
                lblReceive_by.Text = userId
                lblReceive_date.Text = DateTime.Now.ToString("dd/MM/yy")


                If (status = "Order Placed") Then
                    txtStatus.Text = "Not Received"
                    txtStatus.ForeColor = Color.Tomato
                    txtStatus.BorderColor = Color.Tomato
                ElseIf (status = "qty panding") Then
                    txtStatus.Text = "Panding"
                    txtStatus.ForeColor = Color.Orange
                    txtStatus.BorderColor = Color.Orange
                ElseIf (status = "received successful") Then
                    txtStatus.Text = "Received Successfully"
                    txtStatus.ForeColor = Color.PaleGreen
                    txtStatus.BorderColor = Color.PaleGreen
                ElseIf (status = "extra qty received") Then
                    txtStatus.Text = "Extra Received"
                    txtStatus.ForeColor = Color.Yellow
                    txtStatus.BorderColor = Color.Yellow
                End If
            End If
            reader.Close()
        Catch ex As Exception
            Dim dilog1 As New Dialog1("Error filling P.O data in textbox: " & ex.Message, My.Resources.icons8_error_48)
            dilog1.Text = "Error"
            dilog1.Show()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub getProductInfo(prod_code)
        Dim q1 As String = "select prod_name, mrp, dis, sale_rate, cost, qty from purchase_order_items where prod_id = @prod_id and PO_no = @PO_no"
        Dim comm As New SqlCommand(q1, con)
        comm.Parameters.AddWithValue("@prod_id", prod_code)
        comm.Parameters.AddWithValue("@PO_no", po_num)
        con.Open()
        Dim reader As SqlDataReader = comm.ExecuteReader()
        If reader.Read() Then
            txtProductName.Text = reader("prod_name")
            txtProductMRP.Text = reader("mrp")
            txtProductDis.Text = reader("dis")
            txtSaleRate.Text = reader("sale_rate")
            txtTotalCost.Text = reader("cost")
            txto_qty.Text = reader("qty")
            txtQty.Focus()
        Else
            Dim dilog1 As New Dialog1("Product Not available in Purchase order.", My.Resources.icons8_error_48)
            dilog1.Text = "Error"
            dilog1.Show()
        End If
        reader.Close()
        con.Close()
    End Sub

    Private Sub txtProductCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProductCode.KeyDown
        If e.KeyCode = Keys.F9 Then
            Dim itemSelectionForm As New Product_list(po_num)
            If itemSelectionForm.ShowDialog = DialogResult.OK Then
                ' Retrieve the selected item code from the modal form
                txtProductCode.Text = itemSelectionForm.SelectedItemCode
                Dim prod_code = txtProductCode.Text
                getProductInfo(prod_code)
            End If
        End If
        Dim select_text As String = ""
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrEmpty(txtProductCode.Text) Then
                Dim dilog1 As New Dialog1("Please enter product code...", My.Resources.icons8_information_48)
                dilog1.Text = "Enter product code"
                dilog1.Show()
            Else
                'If String.IsNullOrEmpty(cmbPO.Text) Then
                '    Dim p_code = txtProductCode.Text
                '    Dim query As String = "SELECT p.PO_no, s.Supplier_Name, p.user_id, p.date " &
                '      "FROM purchase_order p " &
                '      "JOIN Supplier s ON p.Supplier_ID = s.Supplier_ID " &
                '      "WHERE p.Status = @Status"
                '    'cmbPO.Items.Add($"({po_no} - {suppliername} = {user_id} = {po_date})")
                '    Dim q1 As String = "select a.Supplier_ID, s.Supplier_Name, s.Notes from ALL_Product a join Supplier s on a.Supplier_ID = s.Supplier_ID where a.prod_id = @prod_id"
                '    Dim com As New SqlCommand(q1, con)
                '    com.Parameters.AddWithValue("@prod_id", p_code)
                '    con.Open()
                '    Dim reader As SqlDataReader = com.ExecuteReader()
                '    If reader.Read() Then
                '        Dim supp_id As String = reader("Supplier_ID")
                '        Dim supp_name As String = reader("Supplier_Name")
                '        Dim note As String = reader("Notes")
                '        select_text = $"{supp_id} - {supp_name} ({note})"
                '        reader.Close()
                '    Else
                '        reader.Close()
                '        Dim dilog11 As New Dialog1("product not found...", My.Resources.icons8_error_48)
                '        dilog11.Text = "Error"
                '        dilog11.Show()
                '    End If
                '    con.Close()
                '    cmbPO.SelectedItem = select_text
                '    getProductInfo(p_code)
                'Else
                '    Dim prod_code = txtProductCode.Text
                '    getProductInfo(prod_code)
                'End If
                Dim prod_code = txtProductCode.Text
                getProductInfo(prod_code)
            End If
        End If
    End Sub

    Private Sub txtQty_KeyDown(sender As Object, e As KeyEventArgs) Handles txtQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrEmpty(txtQty.Text) Then
                Dim dilog1 As New Dialog1("Please enter received qty...", My.Resources.icons8_information_48)
                dilog1.Text = "Enter received qty"
                dilog1.Show()
            Else
                txtMfgDate.Focus()
            End If
        End If
    End Sub

    Private Sub txtMfgDate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMfgDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrEmpty(txtMfgDate.Text) Then
                Dim dilog1 As New Dialog1("Please enter product manufacturing date...", My.Resources.icons8_information_48)
                dilog1.Text = "Enter date"
                dilog1.Show()
            Else
                txtExpDate.Focus()
            End If
        End If
    End Sub

    Private Sub Add_Product()
        If String.IsNullOrEmpty(txtProductCode.Text) Or String.IsNullOrEmpty(txtQty.Text) Or String.IsNullOrEmpty(txtExpDate.Text) Or String.IsNullOrEmpty(txtMfgDate.Text) Then
            Dim dilog1 As New Dialog1("Please enter all fild data...", My.Resources.icons8_information_48)
            dilog1.Text = "empty fild"
            dilog1.Show()
        Else
            Dim prod_code As String = txtProductCode.Text
            Dim prod_name As String = txtProductName.Text
            Dim qty As String = txtQty.Text
            Dim cost As String = txtTotalCost.Text
            Dim mrp As String = txtProductMRP.Text
            Dim dis As String = txtProductDis.Text
            Dim sale_rate As String = txtSaleRate.Text
            Dim mfg_date As String = txtMfgDate.Text
            Dim exp_date As String = txtExpDate.Text
            Dim order_qty As String = txto_qty.Text

            Item_List.Rows.Add(prod_code, prod_name, qty, cost, mrp, dis, sale_rate, mfg_date, exp_date, order_qty)
            r_total_item += 1
            r_total_qty += Convert.ToInt64(qty)
            r_total_cost += (cost * qty)
            receiveamount.Text = r_total_cost.ToString()
            receiveitem.Text = r_total_item.ToString()
            receiveqty.Text = r_total_qty.ToString()
            lblreceive_amount.Text = r_total_cost.ToString & ".00"
            lblreceivr_item.Text = r_total_item.ToString()
            lblreceive_qty.Text = r_total_qty.ToString()
            lblpanding_qty.Text = (Convert.ToInt64(o_total_qty) - r_total_qty)


            txtProductCode.Clear()
            txtProductName.Clear()
            txtQty.Clear()
            txtTotalCost.Clear()
            txtProductMRP.Clear()
            txtProductDis.Clear()
            txtSaleRate.Clear()
            txtMfgDate.Clear()
            txtExpDate.Clear()
            txtProductCode.Focus()

        End If
    End Sub



    Private Sub txtExpDate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtExpDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrEmpty(txtExpDate.Text) Then
                Dim dilog1 As New Dialog1("Please enter product expiry date...", My.Resources.icons8_information_48)
                dilog1.Text = "Enter date"
                dilog1.Show()
            Else
                Add_Product()
            End If
        End If
    End Sub

    Private Sub Item_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Item_List.KeyDown
        If e.KeyCode = Keys.F4 AndAlso e.Modifiers = Keys.Shift Or e.KeyCode = Keys.Delete Then
            If Item_List.SelectedRows.Count > 0 Then
                Dim rowIndex As Integer = Item_List.SelectedRows(0).Index
                Dim qty As String = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("Column2").Value)
                Dim cost As String = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("Column7").Value)

                r_total_item -= 1
                r_total_qty -= qty
                r_total_cost -= (cost * qty)

                Dim dilog2 As New Dialog2("Are you sure you want to delete this item?")
                dilog2.Text = "Confirmation"
                If dilog2.ShowDialog = DialogResult.OK Then
                    Item_List.Rows.Remove(Item_List.SelectedRows(0))
                    receiveamount.Text = r_total_cost.ToString()
                    receiveitem.Text = r_total_item.ToString()
                    receiveqty.Text = r_total_qty.ToString()
                    lblreceive_amount.Text = r_total_cost.ToString & ".00"
                    lblreceivr_item.Text = r_total_item.ToString()
                    lblreceive_qty.Text = r_total_qty.ToString()
                    lblpanding_qty.Text = (Convert.ToInt64(o_total_qty) - r_total_qty)
                End If
            End If
        End If
    End Sub

    Private Sub get_receive_id()
        Dim q1 As String = "select no from Number where fild_name = 'receive_id' "
        Dim command As New SqlCommand(q1, con)
        con.Open()
        receive_id = command.ExecuteScalar()
        con.Close()

        Dim q2 As String = "update Number set no = no+1 where fild_name = 'receive_id'"
        Dim command2 As New SqlCommand(q2, con)
        con.Open()
        command2.ExecuteNonQuery()
        con.Close()
    End Sub

    Private Sub insert_into_receivr_table()
        get_receive_id()
        con.Open()
        Dim transaction As SqlTransaction = con.BeginTransaction()

        'Try
        Dim POQuery As String = "insert into receive_PO(receive_id, po_no, receive_user, receive_date, receive_time, received_amount, total_receive_item, total_receive_qty)" &
                                    "values(@receive_id, @po_no, @receive_user, @receive_date, @receive_time, @received_amount, @total_receive_item, @total_receive_qty)"
            Dim billCmd As New SqlCommand(POQuery, con, transaction)
            billCmd.Parameters.AddWithValue("@receive_id", receive_id)
            billCmd.Parameters.AddWithValue("@po_no", po_num)
            billCmd.Parameters.AddWithValue("@receive_user", userId)
            billCmd.Parameters.AddWithValue("@receive_date", DateTime.Now.ToString("dd/MM/yy"))
            billCmd.Parameters.AddWithValue("@receive_time", DateTime.Now.ToString("hh:mm:ss tt"))
            billCmd.Parameters.AddWithValue("@received_amount", r_total_cost.ToString())
            billCmd.Parameters.AddWithValue("@total_receive_item", r_total_item.ToString())
            billCmd.Parameters.AddWithValue("@total_receive_qty", r_total_qty.ToString())
            billCmd.ExecuteNonQuery()

            For Each row As DataGridViewRow In Item_List.Rows
                If Not row.IsNewRow Then
                    Dim itemQuery As String = "insert into receive_po_item(receive_id, po_no, prod_id, prod_name, mrp, dis, sale_rate, cost, qty, mfg_date, exp_date)" &
                                              "values (@receive_id, @po_no, @prod_id, @prod_name, @mrp, @dis, @sale_rate, @cost, @qty, @mfg_date, @exp_date)"
                    Dim itemCmd As New SqlCommand(itemQuery, con, transaction)
                    itemCmd.Parameters.AddWithValue("@receive_id", receive_id)
                    itemCmd.Parameters.AddWithValue("@po_no", po_num)
                    itemCmd.Parameters.AddWithValue("@prod_id", row.Cells("Column6").Value)
                    itemCmd.Parameters.AddWithValue("@prod_name", row.Cells("Column1").Value)
                    itemCmd.Parameters.AddWithValue("@mrp", row.Cells("Column3").Value)
                    itemCmd.Parameters.AddWithValue("@dis", row.Cells("Column4").Value)
                    itemCmd.Parameters.AddWithValue("@sale_rate", row.Cells("DataGridViewTextBoxColumn1").Value)
                    itemCmd.Parameters.AddWithValue("@cost", row.Cells("Column7").Value)
                    itemCmd.Parameters.AddWithValue("@qty", row.Cells("Column2").Value)
                    itemCmd.Parameters.AddWithValue("@mfg_date", row.Cells("Mfg_date").Value)
                    itemCmd.Parameters.AddWithValue("@exp_date", row.Cells("Exp_date").Value)
                    itemCmd.ExecuteNonQuery()
                End If
            Next

            transaction.Commit()

            'Catch ex As Exception

            '    transaction.Rollback()
            '    Dim dilog1 As New Dialog1("Error receiveing PO: " & ex.Message, My.Resources.icons8_error_48)
            '    dilog1.Text = "Error"
            '    dilog1.Show()
            'End Try
            con.Close()
    End Sub

    Private Sub update_purchase_table()
        Dim order_qty As Integer = Convert.ToInt64(o_total_qty)
        Dim receive_qty As Integer = r_total_qty
        Dim panding_qty As Integer = order_qty - receive_qty
        Dim status As String = "no"
        If panding_qty > 0 Then
            status = "qty panding"
        ElseIf panding_qty = 0 Then
            status = "received successful"
        Else
            status = "extra qty received"
        End If
        con.Open()
        Dim transaction As SqlTransaction = con.BeginTransaction()

        Try
            Dim POQuery As String = "update purchase_order set Status = @status, panding_qty = @panding_qty, received_date = @received_date, received_time = @received_time, received_user = @received_user where PO_no = @PO_no"
            Dim billCmd As New SqlCommand(POQuery, con, transaction)
            billCmd.Parameters.AddWithValue("@status", status)
            billCmd.Parameters.AddWithValue("@panding_qty", panding_qty.ToString())
            billCmd.Parameters.AddWithValue("@received_date", DateTime.Now.ToString("dd/MM/yy"))
            billCmd.Parameters.AddWithValue("@received_time", DateTime.Now.ToString("hh:mm:ss tt"))
            billCmd.Parameters.AddWithValue("@received_user", userId)
            billCmd.Parameters.AddWithValue("@PO_no", po_num)
            billCmd.ExecuteNonQuery()

            For Each row As DataGridViewRow In Item_List.Rows
                If Not row.IsNewRow Then
                    Dim p_qty As Integer = (Convert.ToInt64(row.Cells("order_qty").Value) - Convert.ToInt64(row.Cells("Column2").Value))
                    Dim itemQuery As String = "update purchase_order_items set received_qty = @received_qty, panding_qty = @panding_qty where PO_no = @PO_no and prod_id = @prod_id"
                    Dim itemCmd As New SqlCommand(itemQuery, con, transaction)
                    itemCmd.Parameters.AddWithValue("@received_qty", row.Cells("Column2").Value)
                    itemCmd.Parameters.AddWithValue("@panding_qty", p_qty.ToString())
                    itemCmd.Parameters.AddWithValue("@PO_no", po_num)
                    itemCmd.Parameters.AddWithValue("@prod_id", row.Cells("Column6").Value)
                    itemCmd.ExecuteNonQuery()
                End If
            Next

            transaction.Commit()

        Catch ex As Exception

            transaction.Rollback()
            Dim dilog1 As New Dialog1("Error update PO: " & ex.Message, My.Resources.icons8_error_48)
            dilog1.Text = "Error"
            dilog1.Show()
        End Try
        con.Close()
    End Sub

    Private Sub add_inventory()
        con.Open()
        Dim transaction As SqlTransaction = con.BeginTransaction()

        Try
            For Each row As DataGridViewRow In Item_List.Rows
                If Not row.IsNewRow Then
                    Dim q1 As String = "select no from Number where fild_name = 'lot_no' "
                    Dim command As New SqlCommand(q1, con, transaction)
                    Dim lot_no As Integer = command.ExecuteScalar()

                    Dim q2 As String = "update Number set no = no+1 where fild_name = 'lot_no'"
                    Dim command2 As New SqlCommand(q2, con, transaction)
                    command2.ExecuteNonQuery()

                    Dim profit As Integer = (Convert.ToInt64(row.Cells("DataGridViewTextBoxColumn1").Value) - Convert.ToInt64(row.Cells("Column7").Value)) * Convert.ToInt64(row.Cells("Column2").Value)
                    Dim itemQuery As String = "insert into inventory(lot_no, prod_id, prod_name, prod_mrp, disc, sale_rate, total_cost, qty, mfg_date, exp_date, po_no, receive_date, receive_time, last_sale_date, totale_profit, covered_profit, received_qty)" &
                                              "values (@lot_no, @prod_id, @prod_name, @prod_mrp, @disc, @sale_rate, @total_cost, @qty, @mfg_date, @exp_date, @po_no, @receive_date, @receive_time, @last_sale_date, @totale_profit, @covered_profit, @received_qty)"
                    Dim itemCmd As New SqlCommand(itemQuery, con, transaction)
                    itemCmd.Parameters.AddWithValue("@lot_no", lot_no)
                    itemCmd.Parameters.AddWithValue("@prod_id", row.Cells("Column6").Value)
                    itemCmd.Parameters.AddWithValue("@prod_name", row.Cells("Column1").Value)
                    itemCmd.Parameters.AddWithValue("@prod_mrp", Convert.ToInt64(row.Cells("Column3").Value))
                    itemCmd.Parameters.AddWithValue("@disc", Convert.ToInt64(row.Cells("Column4").Value))
                    itemCmd.Parameters.AddWithValue("@sale_rate", Convert.ToInt64(row.Cells("DataGridViewTextBoxColumn1").Value))
                    itemCmd.Parameters.AddWithValue("@total_cost", Convert.ToInt64(row.Cells("Column7").Value))
                    itemCmd.Parameters.AddWithValue("@qty", Convert.ToInt64(row.Cells("Column2").Value))
                    itemCmd.Parameters.AddWithValue("@mfg_date", row.Cells("Mfg_date").Value)
                    itemCmd.Parameters.AddWithValue("@exp_date", row.Cells("Exp_date").Value)
                    itemCmd.Parameters.AddWithValue("@po_no", po_num)
                    itemCmd.Parameters.AddWithValue("@receive_date", DateTime.Now.ToString("dd/MM/yy"))
                    itemCmd.Parameters.AddWithValue("@receive_time", DateTime.Now.ToString("hh:mm:ss tt"))
                    itemCmd.Parameters.AddWithValue("@last_sale_date", "empty")
                    itemCmd.Parameters.AddWithValue("@totale_profit", profit)
                    itemCmd.Parameters.AddWithValue("@covered_profit", "empty")
                    itemCmd.Parameters.AddWithValue("@received_qty", row.Cells("Column2").Value)
                    itemCmd.ExecuteNonQuery()
                End If
            Next

            transaction.Commit()

        Catch ex As Exception

            transaction.Rollback()
            Dim dilog1 As New Dialog1("Error adding in inventory: " & ex.Message, My.Resources.icons8_error_48)
            dilog1.Text = "Error"
            dilog1.Show()
        End Try
        con.Close()
    End Sub

    Private Sub change_ui()
        Dim q1 As String = "select Status from purchase_order where PO_no = @po_no"
        Dim com As New SqlCommand(q1, con)
        com.Parameters.AddWithValue("@po_no", po_num)
        con.Open()
        Dim saving_status As String = com.ExecuteScalar()
        con.Close()
        card1.Visible = False
        If (saving_status = "Order Placed") Then
            txtStatus.Text = "Not Received"
            txtStatus.ForeColor = Color.Tomato
            txtStatus.BorderColor = Color.Tomato
        ElseIf (saving_status = "qty panding") Then
            txtStatus.Text = "Panding"
            txtStatus.ForeColor = Color.Orange
            txtStatus.BorderColor = Color.Orange
        ElseIf (saving_status = "received successful") Then
            txtStatus.Text = "Received Successfully"
            txtStatus.ForeColor = Color.PaleGreen
            txtStatus.BorderColor = Color.PaleGreen
        ElseIf (saving_status = "extra qty received") Then
            txtStatus.Text = "Extra Received"
            txtStatus.ForeColor = Color.Yellow
            txtStatus.BorderColor = Color.Yellow
        End If
    End Sub

    Private Sub receive_bt_Click(sender As Object, e As EventArgs) Handles receive_bt.Click
        Dim p_qty As Integer = o_total_qty - r_total_qty
        If (o_total_qty = p_qty) Then
            Dim dilog1 As New Dialog1("no any item added in table for receiveing PO", My.Resources.icons8_error_48)
            dilog1.Text = "add items"
            dilog1.Show()
        Else
            Dim dilog2 As New Dialog2("Are you sure you want to Receive this purchase order?")
            dilog2.Text = "Confirmation"
            If dilog2.ShowDialog = DialogResult.OK Then
                insert_into_receivr_table()
                update_purchase_table()
                add_inventory()
                change_ui()
            End If
        End If

    End Sub

    Private Sub clear_bt_Click(sender As Object, e As EventArgs) Handles clear_bt.Click
        Dim mdidorm As New Receive_Purchase_Order(userId)
        mdidorm.Show()
        mdidorm.WindowState = FormWindowState.Maximized
        mdidorm.BringToFront()
        Me.Close()
    End Sub

    Private Sub txtQty_Enter(sender As Object, e As EventArgs) Handles txtQty.Enter
        cmbPO.Enabled = False
    End Sub
End Class