<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Branch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Guna2ShadowPanel9 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Label2 = New Label()
        Label1 = New Label()
        Guna2ShadowPanel9.SuspendLayout()
        SuspendLayout()
        ' 
        ' Guna2ShadowPanel9
        ' 
        Guna2ShadowPanel9.BackColor = Color.Transparent
        Guna2ShadowPanel9.Controls.Add(Label2)
        Guna2ShadowPanel9.FillColor = Color.MidnightBlue
        Guna2ShadowPanel9.Location = New Point(24, 24)
        Guna2ShadowPanel9.Margin = New Padding(15)
        Guna2ShadowPanel9.Name = "Guna2ShadowPanel9"
        Guna2ShadowPanel9.Radius = 10
        Guna2ShadowPanel9.ShadowColor = Color.Black
        Guna2ShadowPanel9.ShadowDepth = 50
        Guna2ShadowPanel9.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal
        Guna2ShadowPanel9.Size = New Size(1642, 73)
        Guna2ShadowPanel9.TabIndex = 27
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Century Gothic", 20F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.White
        Label2.Location = New Point(23, 17)
        Label2.Name = "Label2"
        Label2.Size = New Size(146, 32)
        Label2.TabIndex = 2
        Label2.Text = "All Branch"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Century Gothic", 20F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(734, 495)
        Label1.Name = "Label1"
        Label1.Size = New Size(220, 32)
        Label1.TabIndex = 28
        Label1.Text = "No branch here"
        ' 
        ' Branch
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Lavender
        ClientSize = New Size(1690, 1019)
        Controls.Add(Label1)
        Controls.Add(Guna2ShadowPanel9)
        Name = "Branch"
        Text = "Branch"
        Guna2ShadowPanel9.ResumeLayout(False)
        Guna2ShadowPanel9.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Guna2ShadowPanel9 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
