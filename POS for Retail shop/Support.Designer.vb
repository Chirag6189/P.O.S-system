<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Support
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
        Guna2ShadowPanel1 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Label1 = New Label()
        Label3 = New Label()
        Guna2ShadowPanel9.SuspendLayout()
        Guna2ShadowPanel1.SuspendLayout()
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
        Label2.Size = New Size(170, 32)
        Label2.TabIndex = 2
        Label2.Text = "Get Support"
        ' 
        ' Guna2ShadowPanel1
        ' 
        Guna2ShadowPanel1.BackColor = Color.Transparent
        Guna2ShadowPanel1.Controls.Add(Label3)
        Guna2ShadowPanel1.Controls.Add(Label1)
        Guna2ShadowPanel1.FillColor = Color.AliceBlue
        Guna2ShadowPanel1.Location = New Point(620, 376)
        Guna2ShadowPanel1.Margin = New Padding(15)
        Guna2ShadowPanel1.Name = "Guna2ShadowPanel1"
        Guna2ShadowPanel1.Radius = 10
        Guna2ShadowPanel1.ShadowColor = Color.Black
        Guna2ShadowPanel1.ShadowDepth = 50
        Guna2ShadowPanel1.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal
        Guna2ShadowPanel1.Size = New Size(425, 159)
        Guna2ShadowPanel1.TabIndex = 28
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label1.Location = New Point(25, 25)
        Label1.Name = "Label1"
        Label1.Size = New Size(197, 28)
        Label1.TabIndex = 2
        Label1.Text = "Call for support "
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Century Gothic", 25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label3.Location = New Point(151, 76)
        Label3.Name = "Label3"
        Label3.Size = New Size(207, 40)
        Label3.TabIndex = 3
        Label3.Text = "9913772148"
        ' 
        ' Support
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Lavender
        ClientSize = New Size(1690, 1019)
        Controls.Add(Guna2ShadowPanel1)
        Controls.Add(Guna2ShadowPanel9)
        Name = "Support"
        Text = "Support"
        Guna2ShadowPanel9.ResumeLayout(False)
        Guna2ShadowPanel9.PerformLayout()
        Guna2ShadowPanel1.ResumeLayout(False)
        Guna2ShadowPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Guna2ShadowPanel9 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents Guna2ShadowPanel1 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
End Class
