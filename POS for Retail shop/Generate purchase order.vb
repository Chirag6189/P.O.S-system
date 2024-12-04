Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Net
Imports System.Text.Json
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Generate_purchase_order

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand
    Dim supp_id As String
    Dim supplier_id As String
    Dim net_amount As Integer = 0
    Dim total_cost As Integer = 0
    Dim total_Mrp As Integer = 0
    Dim total_saleRate As Integer = 0
    Dim total_profit As Integer = 0
    Dim total_item As Integer = 0
    Dim total_qty As Integer = 0
    Dim PO_No As Integer
    Dim invoice_no As Integer
    Dim userId As String
    Dim subtotal As Integer = 0
    Dim tax As Integer = 0
    Dim shipping_and_other As Integer = 0


    Private Sub Generate_purchase_order_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSuppliers()
        Me.KeyPreview = True
        txt_tax.Text = "0"
        txt_shipping.Text = "0"
    End Sub

    Public Sub New(ByVal userId As String)
        InitializeComponent()
        Me.userId = userId
    End Sub

    Private Sub LoadSuppliers()
        Dim query As String = "SELECT Supplier_ID, Supplier_Name, Notes FROM Supplier"
        Dim command As New SqlCommand(query, con)

        Try
            con.Open()
            Dim reader As SqlDataReader = command.ExecuteReader()

            While reader.Read()
                Dim supplierID As String = reader("Supplier_ID").ToString()
                Dim supplierName As String = reader("Supplier_Name").ToString()
                Dim notes As String = reader("Notes").ToString()

                cmbSupplier.Items.Add($"{supplierID} - {supplierName} ({notes})")
            End While

            reader.Close()
        Catch ex As Exception
            Dim dilog1 As New Dialog1("Error fetching supplier data: " & ex.Message, My.Resources.icons8_error_48)
            dilog1.Text = "Error"
            dilog1.Show()
        Finally
            con.Close()
        End Try
    End Sub

    Function getSupplierName(id As String) As String
        Dim q1 As String = "SELECT Supplier_Name FROM Supplier WHERE Supplier_ID = @id"
        Dim command As New SqlCommand(q1, con)
        command.Parameters.AddWithValue("@id", id)
        Dim supplierName As String = String.Empty

        Try
            con.Open()
            supplierName = command.ExecuteScalar().ToString()
        Catch ex As Exception
            Dim dilog1 As New Dialog1("Error fetching supplier Name: " & ex.Message, My.Resources.icons8_error_48)
            dilog1.Text = "Error"
            dilog1.Show()
        Finally
            con.Close()
        End Try

        Return supplierName
    End Function

    Private Sub cmbSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged
        Dim selectedSupplier = cmbSupplier.SelectedItem.ToString
        Dim supplierID = selectedSupplier.Split("-"c)(0).Trim

        txtSupplierId.Text = supplierID
        txtSupplierName.Text = getSupplierName(supplierID)
        supplier_id = supplierID
    End Sub

    Private Sub checkSupplier(prod_code As String)
        Dim q2 As String = "select Supplier_Id from ALL_Product where prod_id = @prod_id or barcode = @prod_id"
        Dim command1 As New SqlCommand(q2, con)
        command1.Parameters.AddWithValue("@prod_id", prod_code)
        con.Open()
        supp_id = command1.ExecuteScalar()
        con.Close()
    End Sub

    Private Sub getProductInfo(prod_code As String)
        Dim q1 As String = "select COUNT(*) from product where prod_id = @prod_id or Barcode = @prod_id"
        Dim command As New SqlCommand(q1, con)
        command.Parameters.AddWithValue("@prod_id", prod_code)
        con.Open()
        Dim count As Integer = command.ExecuteScalar()
        con.Close()
        If count = 1 Then
            checkSupplier(prod_code)
            If supp_id = supplier_id Then
                Dim q3 As String = "select prod_id, prod_name, prod_mrp, disc, sale_rate, total_cost from product where prod_id = @prod_code or Barcode = @prod_code"
                Dim command3 As New SqlCommand(q3, con)
                command3.Parameters.AddWithValue("@prod_code", prod_code)
                con.Open()
                Try
                    Dim reader As SqlDataReader = command3.ExecuteReader()

                    If reader.Read() Then
                        txtProductCode.Text = reader("prod_id")
                        txtProductName.Text = reader("prod_name")
                        txtProductMRP.Text = reader("prod_mrp")
                        txtProductDis.Text = reader("disc")
                        txtSaleRate.Text = reader("sale_rate")
                        txtTotalCost.Text = reader("total_cost")
                        txtProductMRP.Focus()
                        reader.Close()
                    Else
                        reader.Close()
                        Dim dilog1 As New Dialog1("product fill error.", My.Resources.icons8_error_48)
                        dilog1.Text = "Error"
                        dilog1.Show()
                    End If

                Catch ex As Exception
                    Dim dilog1 As New Dialog1("Error retrieving product data: " & ex.Message, My.Resources.icons8_error_48)
                    dilog1.Text = "Error"
                    dilog1.Show()
                End Try
                con.Close()
            Else
                Dim dilog1 As New Dialog1("please select supplier (" & supp_id & ") for add this product.", My.Resources.icons8_information_48)
                dilog1.Text = "wrong Supplier"
                dilog1.Show()
            End If
        ElseIf count = 0 Then
            Dim q4 As String = "select COUNT(*) from All_Product where prod_id = @prod_id or barcode = @prod_id"
            Dim command4 As New SqlCommand(q4, con)
            command4.Parameters.AddWithValue("@prod_id", prod_code)
            con.Open()
            Dim count1 As Integer = command4.ExecuteScalar()
            con.Close()
            If count1 = 0 Then
                Dim dilog1 As New Dialog1("No, any product found for this code add this product than try again!", My.Resources.icons8_information_48)
                dilog1.Text = "Product not found"
                dilog1.Show()
            ElseIf count1 = 1 Then
                checkSupplier(prod_code)
                If supp_id = supplier_id Then
                    Dim q3 As String = "select prod_id, prod_name from ALL_Product where prod_id = @prod_code or barcode = @prod_code"
                    Dim command3 As New SqlCommand(q3, con)
                    command3.Parameters.AddWithValue("@prod_code", prod_code)
                    con.Open()
                    Try
                        Dim reader As SqlDataReader = command3.ExecuteReader()

                        If reader.Read() Then
                            txtProductCode.Text = reader("prod_id")
                            txtProductName.Text = reader("prod_name")
                            txtProductMRP.Focus()
                            reader.Close()
                        Else
                            reader.Close()
                            Dim dilog1 As New Dialog1("product fill error.", My.Resources.icons8_error_48)
                            dilog1.Text = "Error"
                            dilog1.Show()
                        End If

                    Catch ex As Exception
                        Dim dilog1 As New Dialog1("Error retrieving product data: " & ex.Message, My.Resources.icons8_error_48)
                        dilog1.Text = "Error"
                        dilog1.Show()
                    End Try
                    con.Close()
                Else
                    Dim dilog1 As New Dialog1("please select supplier (" & supp_id & ") for add this product.", My.Resources.icons8_information_48)
                    dilog1.Text = "wrong Supplier"
                    dilog1.Show()
                End If
            Else
                Dim dilog1 As New Dialog1("Check product in database product is more than 1 so check and remove another product", My.Resources.icons8_information_48)
                dilog1.Text = "more then 1 product found"
                dilog1.Show()
            End If

        Else
            Dim dilog1 As New Dialog1("Check product in database product is more than 1 so check and remove another product", My.Resources.icons8_information_48)
            dilog1.Text = "more then 1 product found"
            dilog1.Show()
        End If
    End Sub

    Private Sub txtProductCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProductCode.KeyDown
        Dim select_text As String = ""
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrEmpty(txtProductCode.Text) Then
                Dim dilog1 As New Dialog1("Please enter product code...", My.Resources.icons8_information_48)
                dilog1.Text = "Enter product code"
                dilog1.Show()
            Else
                If String.IsNullOrEmpty(cmbSupplier.Text) Then
                    Dim prod_code = txtProductCode.Text
                    Dim q1 As String = "select a.Supplier_ID, s.Supplier_Name, s.Notes from ALL_Product a join Supplier s on a.Supplier_ID = s.Supplier_ID where a.prod_id = @prod_id"
                    Dim com As New SqlCommand(q1, con)
                    com.Parameters.AddWithValue("@prod_id", prod_code)
                    con.Open()
                    Dim reader As SqlDataReader = com.ExecuteReader()
                    If reader.Read() Then
                        Dim supp_id As String = reader("Supplier_ID")
                        Dim supp_name As String = reader("Supplier_Name")
                        Dim note As String = reader("Notes")
                        select_text = $"{supp_id} - {supp_name} ({note})"
                        reader.Close()
                    Else
                        reader.Close()
                        Dim dilog11 As New Dialog1("product not found...", My.Resources.icons8_error_48)
                        dilog11.Text = "Error"
                        dilog11.Show()
                    End If
                    con.Close()
                    cmbSupplier.SelectedItem = select_text
                    getProductInfo(prod_code)
                Else
                    Dim prod_code = txtProductCode.Text
                    getProductInfo(prod_code)
                End If
            End If
        End If
    End Sub

    Private Sub txtProductMRP_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProductMRP.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtProductDis.Focus()
        End If
    End Sub

    Private Sub txtProductDis_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProductDis.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim mrp As Integer = Convert.ToInt32(txtProductMRP.Text)
            Dim dis As Integer = mrp * (Convert.ToInt32(txtProductDis.Text) / 100)
            Dim sale_rate As Integer = mrp - dis
            txtSaleRate.Text = sale_rate.ToString()
            txtSaleRate.Focus()
        End If
    End Sub

    Private Sub txtSaleRate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSaleRate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtTotalCost.Focus()
        End If
    End Sub

    Private Sub txtTotalCost_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTotalCost.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtQty.Focus()
        End If
    End Sub

    Private Sub Addproduct()
        Dim prod_code As String = txtProductCode.Text
        Dim prod_name As String = txtProductName.Text
        Dim prod_mrp As String = txtProductMRP.Text
        Dim prod_dis As String = txtProductDis.Text
        Dim sale_rate As String = txtSaleRate.Text
        Dim prod_cost As String = txtTotalCost.Text
        Dim qty As String = txtQty.Text
        total_qty += qty
        in_Total_Qty.Text = total_qty.ToString()
        Dim total_rate As Integer = Convert.ToInt32(prod_cost) * Convert.ToInt32(qty)
        total_Mrp += Convert.ToInt32(prod_mrp) * Convert.ToInt32(qty)
        lblTotalMRP.Text = "₹ " + total_Mrp.ToString() + ".00"
        tax = Convert.ToInt32(txt_tax.Text)
        shipping_and_other = Convert.ToInt32(txt_shipping.Text)
        Label18.Text = "₹ " + tax.ToString() + ".00"
        Label22.Text = "₹ " + shipping_and_other.ToString() + ".00"
        total_cost += total_rate
        net_amount = total_cost + tax + shipping_and_other
        lbl_Net_Amount.Text = "₹ " + net_amount.ToString() + ".00"
        lblTotalCost.Text = "₹ " + total_cost.ToString() + ".00"
        total_saleRate += Convert.ToInt32(sale_rate) * Convert.ToInt32(qty)
        lblTotalSaleRare.Text = "₹ " + total_saleRate.ToString() + ".00"
        total_profit = total_saleRate - total_cost - tax - shipping_and_other
        lblTotalProfit.Text = "₹ " + total_profit.ToString() + ".00"
        Item_List.Rows.Add(prod_code, prod_name, qty, prod_cost, prod_mrp, prod_dis, sale_rate, total_rate)
        txtProductCode.Clear()
        txtProductName.Clear()
        txtProductMRP.Clear()
        txtProductDis.Clear()
        txtSaleRate.Clear()
        txtTotalCost.Clear()
        txtQty.Clear()
        txtProductCode.Focus()
        total_item += 1
        in_Total_Item.Text = total_item.ToString()
    End Sub

    Private Sub txtQty_KeyDown(sender As Object, e As KeyEventArgs) Handles txtQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrEmpty(txtQty.Text) Then
                Dim dilog1 As New Dialog1("Please enter product Qty...", My.Resources.icons8_information_48)
                dilog1.Text = "Enter product Qty"
                dilog1.Show
            Else
                Addproduct()
            End If
        End If
    End Sub

    Private Sub txtProductMRP_Enter(sender As Object, e As EventArgs) Handles txtProductMRP.Enter
        cmbSupplier.Enabled = False
    End Sub

    Private Sub Item_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Item_List.KeyDown
        If e.KeyCode = Keys.F4 AndAlso e.Modifiers = Keys.Shift Or e.KeyCode = Keys.Delete Then
            If Item_List.SelectedRows.Count > 0 Then
                Dim rowIndex As Integer = Item_List.SelectedRows(0).Index
                Dim salerate As Integer = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("DataGridViewTextBoxColumn1").Value)
                Dim totalRate As Integer = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("Column5").Value)
                Dim mrp As Integer = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("Column3").Value)
                Dim dis As Integer = mrp - salerate
                Dim qty As Integer = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("Column2").Value)

                Dim dilog2 As New Dialog2("Are you sure you want to delete this item?")
                dilog2.Text = "Confirmation"
                If dilog2.ShowDialog = DialogResult.OK Then
                    Item_List.Rows.Remove(Item_List.SelectedRows(0))
                    total_cost -= totalRate
                    net_amount = total_cost + tax + shipping_and_other
                    lbl_Net_Amount.Text = "₹ " + net_amount.ToString() + ".00"
                    lblTotalCost.Text = "₹ " + total_cost.ToString() + ".00"
                    total_Mrp -= mrp * qty
                    lblTotalMRP.Text = "₹ " + total_Mrp.ToString() + ".00"
                    total_saleRate -= salerate * qty
                    lblTotalSaleRare.Text = "₹ " + total_saleRate.ToString() + ".00"
                    total_profit = total_saleRate - total_cost - tax - shipping_and_other
                    lblTotalProfit.Text = "₹ " + total_profit.ToString() + ".00"
                    total_item -= 1
                    in_Total_Item.Text = total_item.ToString
                    total_qty -= qty
                    in_Total_Qty.Text = total_qty.ToString()
                End If
            End If
        End If
    End Sub

    Private Sub Item_List_SelectionChanged(sender As Object, e As EventArgs) Handles Item_List.SelectionChanged
        If Item_List.SelectedRows.Count > 0 Then
            Dim rowIndex As Integer = Item_List.SelectedRows(0).Index
            Dim salerate As Integer = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("DataGridViewTextBoxColumn1").Value)
            Dim product_cost As Integer = Convert.ToInt32(Item_List.Rows(rowIndex).Cells("Column7").Value)
            Dim itme_profit As Integer = salerate - product_cost
            in_Item_profit.Text = itme_profit.ToString()
        End If
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        If String.IsNullOrEmpty(txt_tax.Text) And String.IsNullOrEmpty(txt_shipping.Text) Then
            Dim dilog1 As New Dialog1("Please enter tax and shipping/other charges!", My.Resources.icons8_information_48)
            dilog1.Text = "Enter bill charges"
            dilog1.Show()
        Else
            ShowSaveOptionsMessageBox()
        End If
    End Sub

    Private Sub ShowSaveOptionsMessageBox()
        Dim dilog2 As New Dialog2("Do you want to save this purchase order?")
        dilog2.Text = "Confirmation"
        If dilog2.ShowDialog = DialogResult.OK Then
            SavePO()
            Dim dilog1 As New Dialog2("Print P.O. No = " & PO_No.ToString())
            dilog1.Text = "Print copy"
            If dilog1.ShowDialog = DialogResult.OK Then
                Print(PO_No)
            End If

        End If
    End Sub

    Private Sub Print(PO_no As Integer)
        Dim pageSize As New PaperSize("A4", 827, 1169)
        Doc.DefaultPageSettings.PaperSize = pageSize
        Doc.DefaultPageSettings.Margins = New Margins(50, 50, 50, 50)

        ' Attach the PrintPage event handler
        AddHandler Doc.PrintPage, AddressOf PrintPageHandler

        ' Show print preview
        PPD.Document = Doc
        PPD.WindowState = FormWindowState.Maximized
        PPD.PrintPreviewControl.AutoZoom = True
        PPD.ShowDialog()
    End Sub

    Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim pen As New Pen(Color.Black, 1)
        Dim boldPen As New Pen(Color.Black, 2)
        Dim fontTitle2 As New Font("Arial", 15, FontStyle.Bold)
        Dim fontTitle1 As New Font("Gill Sans MT", 30, FontStyle.Bold)
        Dim fontHeader As New Font("Arial", 12, FontStyle.Bold)
        Dim fontHeader1 As New Font("Arial", 12, FontStyle.Regular)
        Dim fontBody As New Font("Arial", 10, FontStyle.Regular)
        Dim fontBody1 As New Font("Arial", 10, FontStyle.Bold)
        Dim fontFooter As New Font("Arial", 10, FontStyle.Italic)

        ' Layout positions
        Dim startX As Integer = e.MarginBounds.Left
        Dim startY As Integer = e.MarginBounds.Top
        Dim contentWidth As Integer = e.MarginBounds.Width
        Dim lineHeight As Integer = fontBody.Height + 5

        ' ----- HEADER -----
        ' Company Name and Contact Details
        g.DrawString("JADHAV SUPERMART LLP", fontTitle2, Brushes.Black, startX, startY + 6)
        g.DrawString("SHOP NO GF 17 TO 30,", fontBody, Brushes.Black, startX + 2, startY + lineHeight + 24)
        g.DrawString("MADHURAM ARCADE,KHARVASA RD,", fontBody, Brushes.Black, startX + 2, (startY + 22) + lineHeight * 2)
        g.DrawString("DINDOLI,SURAT,GUJARAT, SURAT-394210", fontBody, Brushes.Black, startX + 2, (startY + 17) + lineHeight * 3)
        g.DrawString("Phone: 97127 49444, Fax: (417) 000 00 00", fontBody, Brushes.Black, startX + 2, (startY + 12) + lineHeight * 4)

        ' Invoice Title
        g.DrawString("INVOICE", fontTitle1, Brushes.Black, startX + contentWidth - 200, startY)

        ' Invoice Details (Invoice #, Date)
        g.DrawString($"Invoice #: " & invoice_no.ToString(), fontBody, Brushes.Black, (startX + 10) + contentWidth - 200, startY + 16 + lineHeight * 2)
        g.DrawString($"Date: {DateTime.Now.ToShortDateString()}", fontBody, Brushes.Black, (startX + 10) + contentWidth - 200, startY + 14 + lineHeight * 3)

        startY += lineHeight * 6

        ' ----- BILL TO / SHIP TO -----
        Dim boxHeight As Integer = lineHeight * 2
        Dim boxWidth As Integer = ((contentWidth - 20) / 2) + 10
        g.DrawRectangle(boldPen, startX, startY, boxWidth, boxHeight)
        g.DrawRectangle(boldPen, startX + boxWidth, startY, boxWidth, boxHeight)
        g.DrawRectangle(pen, startX, boxHeight + startY, boxWidth, lineHeight * 5)
        g.DrawRectangle(pen, startX + boxWidth, boxHeight + startY, boxWidth, lineHeight * 5)

        Dim q1 As String = "select Company_Name, Phone, Address, City, Zip_Code from Supplier where Supplier_ID = @id"
        Dim com As New SqlCommand(q1, con)
        Dim Sup_com As String = "data_not_found"
        Dim Sup_Phone As String = "data_not_found"
        Dim Sup_address As String = "data_not_found"
        Dim Sup_City As String = "data_not_found"
        Dim Sup_zip_code As String = "data_not_found"
        com.Parameters.AddWithValue("@id", txtSupplierId.Text)
        con.Open()
        Dim reader As SqlDataReader = com.ExecuteReader()
        If reader.Read() Then
            Sup_com = reader("Company_Name")
            Sup_Phone = reader("Phone")
            Sup_address = reader("Address")
            Sup_City = reader("City")
            Sup_zip_code = reader("Zip_Code")
        End If
        reader.Close()
        con.Close()
        ' Bill To Section
        g.DrawString("Bill To:", fontHeader, Brushes.Black, startX + 5, startY + 12)
        g.DrawString(txtSupplierName.Text, fontBody, Brushes.Black, startX + 5, startY + lineHeight + 26)
        g.DrawString(Sup_com, fontBody, Brushes.Black, startX + 5, startY + lineHeight + 26 * 2)
        g.DrawString(Sup_address, fontBody, Brushes.Black, startX + 5, startY + lineHeight + 26 * 3)
        g.DrawString(Sup_City & ", " & Sup_zip_code, fontBody, Brushes.Black, startX + 5, startY + lineHeight + 26 * 4)
        Dim q2 As String = "select emp_name,phone,email from employee where user_id = @id"
        Dim com1 As New SqlCommand(q2, con)
        Dim user_name As String = "data_not_found"
        Dim user_phone As String = "data_not_found"
        Dim user_email As String = "data_not_found"
        com1.Parameters.AddWithValue("@id", userId)
        con.Open()
        Dim reader1 As SqlDataReader = com1.ExecuteReader()
        If reader1.Read() Then
            user_name = reader1("emp_name")
            user_phone = reader1("phone")
            user_email = reader1("email")
        End If
        reader1.Close()
        con.Close()
        ' Ship To Section
        g.DrawString("Ship To:", fontHeader, Brushes.Black, startX + boxWidth + 5, startY + 12)
        g.DrawString(user_name.ToUpper(), fontBody, Brushes.Black, startX + boxWidth + 5, startY + lineHeight + 26)
        g.DrawString("JADHAV SUPERMART LLP", fontBody, Brushes.Black, startX + boxWidth + 5, startY + lineHeight + 26 * 2)
        g.DrawString("MADHURAM ARCADE,DINDOLI-KHARVASA RD", fontBody, Brushes.Black, startX + boxWidth + 5, startY + lineHeight + 26 * 3)
        g.DrawString("SURAT-394210", fontBody, Brushes.Black, startX + boxWidth + 5, startY + lineHeight + 26 * 4)

        Dim boxWidth1 As Integer = (((contentWidth - 20) / 2) + 10) / 3
        g.DrawRectangle(boldPen, startX, boxHeight + startY + lineHeight * 5, boxWidth1, boxHeight)
        g.DrawString("SALESPERSON", fontBody1, Brushes.Black, startX + 5, (boxHeight + startY + lineHeight * 5) + lineHeight - 10)
        g.DrawRectangle(boldPen, startX + boxWidth1, boxHeight + startY + lineHeight * 5, boxWidth1, boxHeight)
        g.DrawString("P.O.NUMBER", fontBody1, Brushes.Black, (startX + 14) + boxWidth1, (boxHeight + startY + lineHeight * 5) + lineHeight - 10)
        g.DrawRectangle(boldPen, startX + boxWidth1 * 2, boxHeight + startY + lineHeight * 5, boxWidth1, boxHeight)
        g.DrawString("REQUISITIONER", fontBody1, Brushes.Black, (startX + 5) + (boxWidth1 * 2), (boxHeight + startY + lineHeight * 5) + lineHeight - 10)
        Dim boxWidth2 As Integer = (((contentWidth - 20) / 2) + 10) / 2
        Dim boxWidth3 As Integer = ((((contentWidth - 20) / 2) + 10) / 2) / 2
        g.DrawRectangle(boldPen, startX + boxWidth1 * 3, boxHeight + startY + lineHeight * 5, boxWidth2, boxHeight)
        g.DrawString("SHIPPED VIA", fontBody1, Brushes.Black, (startX + boxWidth1 * 3) + 40, (boxHeight + startY + lineHeight * 5) + lineHeight - 10)
        g.DrawRectangle(boldPen, startX + boxWidth1 * 3 + boxWidth2, boxHeight + startY + lineHeight * 5, boxWidth3, boxHeight)
        g.DrawString("F.O.B.", fontBody1, Brushes.Black, (startX + boxWidth1 * 3 + boxWidth2) + 20, (boxHeight + startY + lineHeight * 5) + lineHeight - 14)
        g.DrawString("POINT", fontBody1, Brushes.Black, (startX + boxWidth1 * 3 + boxWidth2) + 20, (boxHeight + startY + lineHeight * 5) + lineHeight)
        g.DrawRectangle(boldPen, startX + boxWidth1 * 3 + boxWidth2 + boxWidth3, boxHeight + startY + lineHeight * 5, boxWidth3, boxHeight)
        g.DrawString("TERMS", fontBody1, Brushes.Black, (startX + boxWidth1 * 3 + boxWidth2 + boxWidth3) + 20, (boxHeight + startY + lineHeight * 5) + lineHeight - 10)


        g.DrawRectangle(pen, startX, (boxHeight * 2) + startY + lineHeight * 5, boxWidth1, boxHeight)
        g.DrawString(txtSupplierName.Text, fontBody, Brushes.Black, startX + 5, ((boxHeight * 2) + startY + lineHeight * 5) + lineHeight - 10)
        g.DrawRectangle(pen, startX + boxWidth1, (boxHeight * 2) + startY + lineHeight * 5, boxWidth1, boxHeight)
        g.DrawString(PO_No.ToString(), fontBody, Brushes.Black, (startX + 5) + boxWidth1, ((boxHeight * 2) + startY + lineHeight * 5) + lineHeight - 10)
        g.DrawRectangle(pen, startX + boxWidth1 * 2, (boxHeight * 2) + startY + lineHeight * 5, boxWidth1, boxHeight)
        g.DrawString(user_name, fontBody, Brushes.Black, (startX + 5) + (boxWidth1 * 2), ((boxHeight * 2) + startY + lineHeight * 5) + lineHeight - 10)
        g.DrawRectangle(pen, startX + boxWidth1 * 3, (boxHeight * 2) + startY + lineHeight * 5, boxWidth2, boxHeight)
        g.DrawString("Transport", fontBody, Brushes.Black, (startX + boxWidth1 * 3) + 5, ((boxHeight * 2) + startY + lineHeight * 5) + lineHeight - 10)
        g.DrawRectangle(pen, startX + boxWidth1 * 3 + boxWidth2, (boxHeight * 2) + startY + lineHeight * 5, boxWidth3, boxHeight)
        g.DrawString("Store", fontBody, Brushes.Black, (startX + boxWidth1 * 3 + boxWidth2) + 5, ((boxHeight * 2) + startY + lineHeight * 5) + lineHeight - 10)
        g.DrawRectangle(pen, startX + boxWidth1 * 3 + boxWidth2 + boxWidth3, (boxHeight * 2) + startY + lineHeight * 5, boxWidth3, boxHeight)
        g.DrawString("empty", fontBody, Brushes.Black, (startX + boxWidth1 * 3 + boxWidth2 + boxWidth3) + 5, ((boxHeight * 2) + startY + lineHeight * 5) + lineHeight - 10)

        startY += (boxHeight + (lineHeight * 5)) + 20 + boxHeight * 2

        Dim boxHeight1 As Integer = lineHeight + (lineHeight / 2)
        g.DrawRectangle(boldPen, startX, startY, boxWidth1 - 20, boxHeight1)
        g.DrawRectangle(boldPen, startX + boxWidth1 - 20, startY, (boxWidth1 * 2) - 20, boxHeight1)
        Dim boxWidth4 As Integer = ((((contentWidth - 20) / 2) + 10) - boxWidth3) / 2
        g.DrawRectangle(boldPen, (startX + boxWidth1 * 3) - (20 * 2), startY, boxWidth4 - 20, boxHeight1)
        g.DrawRectangle(boldPen, (startX + boxWidth1 * 3 + boxWidth4) - (20 * 3), startY, boxWidth4 - 20, boxHeight1)
        g.DrawRectangle(boldPen, (startX + boxWidth1 * 3 + boxWidth4 * 2) - (20 * 4), startY, boxWidth3 - 20, boxHeight1)
        g.DrawRectangle(boldPen, (startX + boxWidth1 * 3 + boxWidth4 * 2) - (20 * 4) + (boxWidth3 - 20), startY, boxWidth3 + 10, boxHeight1)


        ' ----- TABLE HEADER -----(lineHeight + 8) + ((((lineHeight + 8) * 2) - boxHeight1) * 3)
        Dim tableHeight As Integer = (boxHeight1 + (lineHeight / 2)) + (lineHeight * total_item)
        Dim colWidths As Integer() = {boxWidth1 - 20, (boxWidth1 * 2) - 20, boxWidth4 - 20, boxWidth4 - 20, boxWidth3 - 20, boxWidth3 + 10}
        Dim tableStartX As Integer = startX

        ' Draw Columns
        g.DrawRectangle(pen, tableStartX, startY, contentWidth, tableHeight) ' Outer Table
        For i = 1 To colWidths.Length - 1
            g.DrawLine(pen, tableStartX + colWidths.Take(i).Sum(), startY, tableStartX + colWidths.Take(i).Sum(), startY + tableHeight)
        Next

        ' Add Headers
        Dim headers As String() = {"CODE", "PRODUCT NAME", "MRP", "COST", "QTY", "TOTAL"}
        For i = 0 To headers.Length - 1
            g.DrawString(headers(i), fontHeader, Brushes.Black, tableStartX + colWidths.Take(i).Sum() + 5, startY + 5)
        Next

        startY += lineHeight * 2

        Dim currentY As Integer = startY
        For Each row As DataGridViewRow In Item_List.Rows
            If Not row.IsNewRow Then
                g.DrawString(row.Cells("Column6").Value.ToString(), fontBody, Brushes.Black, tableStartX + 5, currentY)
                g.DrawString(row.Cells("Column1").Value.ToString(), fontBody, Brushes.Black, tableStartX + colWidths(0) + 5, currentY)
                g.DrawString(row.Cells("Column3").Value.ToString(), fontBody, Brushes.Black, tableStartX + colWidths(0) + colWidths(1) + 5, currentY)
                g.DrawString(row.Cells("Column7").Value.ToString(), fontBody, Brushes.Black, tableStartX + colWidths(0) + colWidths(1) + colWidths(2) + 5, currentY)
                g.DrawString(row.Cells("Column2").Value.ToString(), fontBody, Brushes.Black, tableStartX + colWidths(0) + colWidths(1) + colWidths(2) + colWidths(3) + 5, currentY)
                g.DrawString(row.Cells("Column5").Value.ToString(), fontBody, Brushes.Black, tableStartX + colWidths(0) + colWidths(1) + colWidths(2) + colWidths(3) + colWidths(4) + 5, currentY)
                currentY += lineHeight
            End If
        Next
        g.DrawRectangle(pen, (startX + boxWidth1 * 3 + boxWidth4 * 2) - (20 * 4) + (boxWidth3 - 20), currentY, boxWidth3 + 10, boxHeight1 * 3)
        ' Footer
        g.DrawString("SUBTOTAL", fontBody, Brushes.Black, startX + 480, currentY + lineHeight)
        g.DrawString(total_cost.ToString(), fontBody, Brushes.Black, tableStartX + colWidths(0) + colWidths(1) + colWidths(2) + colWidths(3) + colWidths(4) + 5, currentY + lineHeight)
        g.DrawString("SALES TAX", fontBody, Brushes.Black, startX + 480, currentY + (lineHeight * 2))
        g.DrawString(tax.ToString(), fontBody, Brushes.Black, tableStartX + colWidths(0) + colWidths(1) + colWidths(2) + colWidths(3) + colWidths(4) + 5, currentY + (lineHeight * 2))
        g.DrawString("SHIPPING & OTHER", fontBody, Brushes.Black, startX + 480, currentY + (lineHeight * 3))
        g.DrawString(shipping_and_other.ToString(), fontBody, Brushes.Black, tableStartX + colWidths(0) + colWidths(1) + colWidths(2) + colWidths(3) + colWidths(4) + 5, currentY + (lineHeight * 3))

        currentY += boxHeight1 * 3
        g.DrawRectangle(boldPen, (startX + boxWidth1 * 3 + boxWidth4 * 2) - (20 * 4) + (boxWidth3 - 20), currentY, boxWidth3 + 10, boxHeight1)
        g.DrawString("TOTAL due", fontBody, Brushes.Black, startX + 480, currentY + 5)
        g.DrawString(net_amount.ToString(), fontBody, Brushes.Black, tableStartX + colWidths(0) + colWidths(1) + colWidths(2) + colWidths(3) + colWidths(4) + 5, currentY + 5)

        currentY += 105

        g.DrawString("Make all checks payable to JADHAV SUPERMART LLP", fontFooter, Brushes.Black, startX, currentY)
        g.DrawString("if you have any questions concerning this invoice, contact :-", fontFooter, Brushes.Black, startX, currentY + lineHeight)
        g.DrawString(user_name, fontFooter, Brushes.Black, startX + 350, currentY + lineHeight * 2)
        g.DrawString(user_phone & " - 9913772148", fontFooter, Brushes.Black, startX + 350, currentY + lineHeight * 3)
        g.DrawString(user_email, fontFooter, Brushes.Black, startX + 350, currentY + lineHeight * 4)
        g.DrawString("Thank you for your business!", fontHeader, Brushes.Black, startX, currentY + (lineHeight * 6))
    End Sub

    Private Sub get_invoice_no()
        Dim q1 As String = "select no from Number where fild_name = 'invoice_no' "
        Dim command As New SqlCommand(q1, con)
        con.Open()
        invoice_no = command.ExecuteScalar()
        con.Close()

        Dim q2 As String = "update Number set no = no+1 where fild_name = 'invoice_no'"
        Dim command2 As New SqlCommand(q2, con)
        con.Open()
        command2.ExecuteNonQuery()
        con.Close()
    End Sub

    Private Sub get_PO_No()
        Dim q1 As String = "select no from Number where fild_name = 'PO_No' "
        Dim command As New SqlCommand(q1, con)
        con.Open()
        PO_No = command.ExecuteScalar()
        con.Close()

        Dim q2 As String = "update Number set no = no+1 where fild_name = 'PO_No'"
        Dim command2 As New SqlCommand(q2, con)
        con.Open()
        command2.ExecuteNonQuery()
        con.Close()
    End Sub

    Private Sub SavePO()
        get_invoice_no()
        get_PO_No()
        con.Open()
        Dim transaction As SqlTransaction = con.BeginTransaction()

        Try
            Dim POQuery As String = "insert into purchase_order(invoice, PO_no, user_id, Supplier_ID, total_MRP, total_sale_rate, total_cost, total_tax, Shipping_and_other, net_amount, total_profit, total_item, total_qty, narration, date, time, Status)" &
                                    "values(@invoice, @PO_no, @user_id, @Supplier_ID, @total_MRP, @total_sale_rate, @total_cost, @total_tax, @Shipping_and_other, @net_amount, @total_profit, @total_item, @total_qty, @narration, @date, @time , @Status)"
            Dim billCmd As New SqlCommand(POQuery, con, transaction)
            billCmd.Parameters.AddWithValue("@invoice", invoice_no)
            billCmd.Parameters.AddWithValue("@PO_no", PO_No)
            billCmd.Parameters.AddWithValue("@user_id", userId)
            billCmd.Parameters.AddWithValue("@Supplier_ID", txtSupplierId.Text)
            billCmd.Parameters.AddWithValue("@total_MRP", total_Mrp.ToString())
            billCmd.Parameters.AddWithValue("@total_sale_rate", total_saleRate.ToString())
            billCmd.Parameters.AddWithValue("@total_cost", total_cost.ToString())
            billCmd.Parameters.AddWithValue("@total_tax", tax.ToString())
            billCmd.Parameters.AddWithValue("@Shipping_and_other", shipping_and_other.ToString())
            billCmd.Parameters.AddWithValue("@net_amount", net_amount.ToString())
            billCmd.Parameters.AddWithValue("@total_profit", total_profit.ToString())
            billCmd.Parameters.AddWithValue("@total_item", in_Total_Item.Text)
            billCmd.Parameters.AddWithValue("@total_qty", in_Total_Qty.Text)
            billCmd.Parameters.AddWithValue("@narration", in_Narration.Text)
            billCmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("dd/MM/yyyy"))
            billCmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("hh:mm:ss tt"))
            billCmd.Parameters.AddWithValue("@Status", "Order Placed")
            billCmd.ExecuteNonQuery()

            For Each row As DataGridViewRow In Item_List.Rows
                If Not row.IsNewRow Then
                    Dim itemQuery As String = "insert into purchase_order_items(PO_no, prod_id, prod_name, mrp, sale_rate, dis, cost, qty)" &
                                              "values (@PO_no, @prod_id, @prod_name, @mrp, @sale_rate, @dis, @cost, @qty)"
                    Dim itemCmd As New SqlCommand(itemQuery, con, transaction)
                    itemCmd.Parameters.AddWithValue("@PO_no", PO_No)
                    itemCmd.Parameters.AddWithValue("@prod_id", row.Cells("Column6").Value)
                    itemCmd.Parameters.AddWithValue("@prod_name", row.Cells("Column1").Value)
                    itemCmd.Parameters.AddWithValue("@mrp", row.Cells("Column3").Value)
                    itemCmd.Parameters.AddWithValue("@sale_rate", row.Cells("DataGridViewTextBoxColumn1").Value)
                    itemCmd.Parameters.AddWithValue("@dis", row.Cells("Column4").Value)
                    itemCmd.Parameters.AddWithValue("@cost", row.Cells("Column7").Value)
                    itemCmd.Parameters.AddWithValue("@qty", row.Cells("Column2").Value)
                    itemCmd.ExecuteNonQuery()

                End If
            Next

            transaction.Commit()

        Catch ex As Exception

            transaction.Rollback()
            Dim dilog1 As New Dialog1("Error saving PO: " & ex.Message, My.Resources.icons8_error_48)
            dilog1.Text = "Error"
            dilog1.Show()
        End Try
        con.Close()
    End Sub

    Private Sub txt_tax_Leave(sender As Object, e As EventArgs) Handles txt_tax.Leave
        If String.IsNullOrEmpty(txt_tax.Text) Then
            txt_tax.Text = "0"
        End If
        tax = Convert.ToInt64(txt_tax.Text)
        net_amount = total_cost + tax + shipping_and_other
        lbl_Net_Amount.Text = "₹ " + net_amount.ToString() + ".00"
        Label18.Text = "₹ " + tax.ToString() + ".00"
        total_profit = total_saleRate - total_cost - tax - shipping_and_other
        lblTotalProfit.Text = "₹ " + total_profit.ToString() + ".00"
    End Sub

    Private Sub txt_shipping_Leave(sender As Object, e As EventArgs) Handles txt_shipping.Leave
        If String.IsNullOrEmpty(txt_shipping.Text) Then
            txt_shipping.Text = "0"
        End If
        shipping_and_other = Convert.ToInt64(txt_shipping.Text)
        net_amount = total_cost + tax + shipping_and_other
        lbl_Net_Amount.Text = "₹ " + net_amount.ToString() + ".00"
        Label22.Text = "₹ " + shipping_and_other.ToString() + ".00"
        total_profit = total_saleRate - total_cost - tax - shipping_and_other
        lblTotalProfit.Text = "₹ " + total_profit.ToString() + ".00"
    End Sub
End Class
