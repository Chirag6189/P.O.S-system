Imports System.Data.SqlClient
Imports Guna.UI2.WinForms
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Activitys

    Dim name As String
    Dim userId As String
    Dim per As String

    Dim con As New SqlConnection("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dhiraj_sons;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Dim cmd As SqlCommand

    Public Sub New(ByVal name As String, ByVal per As String, ByVal userId As String)
        InitializeComponent()
        Me.name = name
        Me.per = per
        Me.userId = userId
    End Sub

    Private Sub Activitys_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbluserId.Text = userId
        lblName.Text = "Welcome " & name & ", "
        UpdateButtonsVisibility()
    End Sub

    Private Sub UpdateButtonsVisibility()

        Dim panal As Guna2ShadowPanel() = {pnlMakeBill, pnlEmployeeMaster, pnlProductMaster, pnlSearchBill, pnlCustomerMaster, pnlSupplierMaster}

        For Each pnl As Guna2ShadowPanel In panal
            pnl.Visible = False
        Next

        If per = "all" Then
            For Each pnl As Guna2ShadowPanel In panal
                pnl.Visible = True
            Next
        Else
            Dim visiblepanal As Integer() = {1}
            For Each index As Integer In visiblepanal
                panal(index - 1).Visible = True
            Next
        End If

        For Each pnl As Guna2ShadowPanel In panal
            If pnl.Visible Then
                showActivity.Controls.Add(pnl)
            End If
        Next

    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Dim mdidorm As New MakeBill(userId)
        mdidorm.Show()
        mdidorm.WindowState = FormWindowState.Maximized
        mdidorm.BringToFront()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim mdidorm As New EmployeeMaster()
        mdidorm.Show()
        mdidorm.WindowState = FormWindowState.Maximized
        mdidorm.BringToFront()
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Dim mdidorm As New CustomerMaster()
        mdidorm.Show
        mdidorm.WindowState = FormWindowState.Maximized
        mdidorm.BringToFront
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Dim mdidorm As New ProductMaster(userId)
        mdidorm.Show()
        mdidorm.WindowState = FormWindowState.Maximized
        mdidorm.BringToFront()
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        Dim mdidorm As New Supplier_Master()
        mdidorm.Show()
        mdidorm.WindowState = FormWindowState.Maximized
        mdidorm.BringToFront()
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click

    End Sub
End Class