Imports System.Runtime.CompilerServices
Imports Guna.UI2.WinForms
Imports Microsoft.SqlServer.Server
Imports Xamarin.Forms

Public Class Home_Page
    Dim name As String
    Dim userId As String
    Dim per As String
    Public Sub New(ByVal name As String, ByVal per As String, ByVal userId As String)
        InitializeComponent()
        Me.name = name
        Me.per = per
        Me.userId = userId
        UpdateButtonsVisibility()

    End Sub

    Dim sidebarExpand As Boolean = True
    Dim activeform As Form

    Private Sub sidebarTransition_Tick(sender As Object, e As EventArgs) Handles sidebarTransition.Tick
        If sidebarExpand Then
            SideBar.Width -= 10
            If SideBar.Width <= 46 Then
                sidebarExpand = False
                sidebarTransition.Stop()

            End If
            lblname.Hide()
        Else
            SideBar.Width += 10
            If SideBar.Width >= 234 Then
                sidebarExpand = True
                sidebarTransition.Stop()

            End If
            lblname.Show()
        End If
    End Sub

    Private Sub UpdateButtonsVisibility()

        Dim buttons As Guna2Button() = {pnldashboard1, pnlActivitys, pnlAnalysis, pnlProduct, pnlStaff, pnlBranch, pnlProfile, pnlSupport, pnlLogout}

        For Each btn As Guna2Button In buttons
            btn.Visible = False
        Next

        If per = "all" Then
            For Each btn As Guna2Button In buttons
                btn.Visible = True
            Next
        Else
            Dim visibleButtons As Integer() = {2, 4, 7, 8, 9}
            For Each index As Integer In visibleButtons
                buttons(index - 1).Visible = True
            Next
        End If

        For Each btn As Guna2Button In buttons
            If btn.Visible Then
                SideBar.Controls.Add(btn)
            End If
        Next

    End Sub

    Private Sub UpdateButtonStyles(clickedButton As Guna2Button)
        Dim buttons As Guna2Button() = {pnldashboard1, pnlActivitys, pnlAnalysis, pnlProduct, pnlStaff, pnlBranch, pnlProfile, pnlSupport}

        For Each btn As Guna2Button In buttons
            If btn Is clickedButton Then
                btn.BackColor = Color.Lavender
                btn.ForeColor = Color.Black

                Select Case btn.Name
                    Case "pnldashboard1"
                        btn.Image = My.Resources.icons8_dashboard_black_48
                    Case "pnlActivitys"
                        btn.Image = My.Resources.icons8_new_job_black_48
                    Case "pnlAnalysis"
                        btn.Image = My.Resources.icons8_analysis_black_48
                    Case "pnlProduct"
                        btn.Image = My.Resources.icons8_product_black_48
                    Case "pnlStaff"
                        btn.Image = My.Resources.icons8_staff_black_48
                    Case "pnlBranch"
                        btn.Image = My.Resources.icons8_branch_black_48
                    Case "pnlProfile"
                        btn.Image = My.Resources.icons8_user_black_48
                    Case "pnlSupport"
                        btn.Image = My.Resources.icons8_support_black_48
                End Select

            Else
                btn.BackColor = Color.Transparent
                btn.ForeColor = Color.White

                Select Case btn.Name
                    Case "pnldashboard1"
                        btn.Image = My.Resources.icons8_dashboard_weigth_48
                    Case "pnlActivitys"
                        btn.Image = My.Resources.icons8_new_job_weigth_48
                    Case "pnlAnalysis"
                        btn.Image = My.Resources.icons8_analysis_weigth_48
                    Case "pnlProduct"
                        btn.Image = My.Resources.icons8_product_weigth_48
                    Case "pnlStaff"
                        btn.Image = My.Resources.icons8_staff_weigth_48
                    Case "pnlBranch"
                        btn.Image = My.Resources.icons8_branch_weigth_48
                    Case "pnlProfile"
                        btn.Image = My.Resources.icons8_user_weigth_48
                    Case "pnlSupport"
                        btn.Image = My.Resources.icons8_support_weigth_48
                End Select
            End If
        Next
    End Sub

    Private Sub openChildForm(childForm As Form, btnSender As Object)
        If activeform IsNot Nothing Then
            activeform.Close()
        End If
        activeform = childForm
        childForm.TopLevel = False
        childForm.FormBorderStyle = FormBorderStyle.None
        childForm.Dock = DockStyle.Fill
        Me.Paneldisplay.Controls.Add(childForm)
        Me.Paneldisplay.Tag = childForm
        childForm.BringToFront()
        childForm.Show()
        lblpagename.Text = childForm.Text
        btnclosepage.Visible = True

        UpdateButtonStyles(btnSender)
    End Sub

    Private Sub btnSidebox_Click(sender As Object, e As EventArgs) Handles btnSidebox.Click
        sidebarTransition.Start()
    End Sub

    Private Sub pnldashboard_Click(sender As Object, e As EventArgs) Handles pnldashboard1.Click
        pnldashboard1.BackColor = Color.Lavender
        pnldashboard1.ForeColor = Color.Black

        openChildForm(New dashboardvb(name, per, userId), sender)
    End Sub

    Private Sub pnlActivitys_Click(sender As Object, e As EventArgs) Handles pnlActivitys.Click
        openChildForm(New Activitys(name, per, userId), sender)
    End Sub

    Private Sub pnlAnalysis_Click(sender As Object, e As EventArgs) Handles pnlAnalysis.Click
        openChildForm(New Analysis(name, per, userId), sender)
    End Sub

    Private Sub pnlProduct_Click(sender As Object, e As EventArgs) Handles pnlProduct.Click
        openChildForm(New Product(name, per, userId), sender)
    End Sub

    Private Sub pnlStaff_Click(sender As Object, e As EventArgs) Handles pnlStaff.Click
        openChildForm(New Staff(name, per, userId), sender)
    End Sub

    Private Sub pnlBranch_Click(sender As Object, e As EventArgs) Handles pnlBranch.Click
        openChildForm(New Branch(), sender)
    End Sub

    Private Sub pnlProfile_Click(sender As Object, e As EventArgs) Handles pnlProfile.Click
        openChildForm(New Profile(name, per, userId), sender)
    End Sub

    Private Sub pnlSupport_Click(sender As Object, e As EventArgs) Handles pnlSupport.Click
        openChildForm(New Support(), sender)
    End Sub

    Private Sub pnlLogout_Click(sender As Object, e As EventArgs) Handles pnlLogout.Click
        Me.Close()
        User_Login.Close()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles btnclosepage.Click
        If activeform IsNot Nothing Then
            activeform.Close()
        End If
        lblpagename.Text = "Home"
        btnclosepage.Visible = False
    End Sub

    Private Sub Home_Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class