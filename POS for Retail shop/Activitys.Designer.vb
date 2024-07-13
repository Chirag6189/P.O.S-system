<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Activitys
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
        FlowLayoutPanel1 = New FlowLayoutPanel()
        Guna2ShadowPanel7 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Guna2ShadowPanel8 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        FlowLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Controls.Add(Guna2ShadowPanel7)
        FlowLayoutPanel1.Controls.Add(Guna2ShadowPanel8)
        FlowLayoutPanel1.Location = New Point(126, 121)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Padding = New Padding(10)
        FlowLayoutPanel1.Size = New Size(1430, 837)
        FlowLayoutPanel1.TabIndex = 0
        ' 
        ' Guna2ShadowPanel7
        ' 
        Guna2ShadowPanel7.BackColor = Color.Transparent
        Guna2ShadowPanel7.FillColor = Color.White
        Guna2ShadowPanel7.Location = New Point(13, 13)
        Guna2ShadowPanel7.Name = "Guna2ShadowPanel7"
        Guna2ShadowPanel7.ShadowColor = Color.Black
        Guna2ShadowPanel7.Size = New Size(529, 339)
        Guna2ShadowPanel7.TabIndex = 4
        ' 
        ' Guna2ShadowPanel8
        ' 
        Guna2ShadowPanel8.BackColor = Color.Transparent
        Guna2ShadowPanel8.FillColor = Color.White
        Guna2ShadowPanel8.Location = New Point(548, 13)
        Guna2ShadowPanel8.Name = "Guna2ShadowPanel8"
        Guna2ShadowPanel8.ShadowColor = Color.Black
        Guna2ShadowPanel8.Size = New Size(529, 339)
        Guna2ShadowPanel8.TabIndex = 5
        ' 
        ' Activitys
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Lavender
        ClientSize = New Size(1690, 1019)
        Controls.Add(FlowLayoutPanel1)
        Name = "Activitys"
        Text = "Activitys"
        FlowLayoutPanel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Guna2ShadowPanel7 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents Guna2ShadowPanel8 As Guna.UI2.WinForms.Guna2ShadowPanel
End Class
