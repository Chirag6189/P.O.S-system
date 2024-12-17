<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Product_list
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
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Product_list))
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Guna2ShadowPanel9 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        invalue = New Guna.UI2.WinForms.Guna2TextBox()
        DataGridView1 = New Guna.UI2.WinForms.Guna2DataGridView()
        Guna2ShadowPanel9.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Guna2ShadowPanel9
        ' 
        Guna2ShadowPanel9.BackColor = Color.Transparent
        Guna2ShadowPanel9.Controls.Add(invalue)
        Guna2ShadowPanel9.FillColor = Color.MidnightBlue
        Guna2ShadowPanel9.Location = New Point(-5, 0)
        Guna2ShadowPanel9.Margin = New Padding(15)
        Guna2ShadowPanel9.Name = "Guna2ShadowPanel9"
        Guna2ShadowPanel9.ShadowColor = Color.Black
        Guna2ShadowPanel9.ShadowDepth = 50
        Guna2ShadowPanel9.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal
        Guna2ShadowPanel9.Size = New Size(813, 75)
        Guna2ShadowPanel9.TabIndex = 31
        ' 
        ' invalue
        ' 
        invalue.BorderColor = Color.DimGray
        invalue.BorderRadius = 10
        invalue.CustomizableEdges = CustomizableEdges1
        invalue.DefaultText = ""
        invalue.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        invalue.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        invalue.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        invalue.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        invalue.FillColor = Color.WhiteSmoke
        invalue.FocusedState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        invalue.Font = New Font("Century Gothic", 13F, FontStyle.Bold)
        invalue.ForeColor = Color.Black
        invalue.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        invalue.IconLeft = CType(resources.GetObject("invalue.IconLeft"), Image)
        invalue.Location = New Point(17, 12)
        invalue.Margin = New Padding(4)
        invalue.Name = "invalue"
        invalue.PasswordChar = ChrW(0)
        invalue.PlaceholderText = "Search"
        invalue.SelectedText = ""
        invalue.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        invalue.Size = New Size(692, 45)
        invalue.TabIndex = 7
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.White
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = Color.Gainsboro
        DataGridViewCellStyle1.SelectionForeColor = Color.Black
        DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridView1.BorderStyle = BorderStyle.FixedSingle
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = Color.MidnightBlue
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = Color.White
        DataGridViewCellStyle2.SelectionBackColor = Color.MidnightBlue
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridView1.ColumnHeadersHeight = 40
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = Color.White
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 11F)
        DataGridViewCellStyle3.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(164), CByte(179), CByte(227))
        DataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        DataGridView1.DefaultCellStyle = DataGridViewCellStyle3
        DataGridView1.GridColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        DataGridView1.Location = New Point(-1, 65)
        DataGridView1.MultiSelect = False
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = Color.White
        DataGridViewCellStyle4.Font = New Font("Segoe UI", 11F)
        DataGridViewCellStyle4.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = Color.White
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        DataGridView1.RowHeadersVisible = False
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = Color.White
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = Color.Gainsboro
        DataGridViewCellStyle5.SelectionForeColor = Color.Black
        DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle5
        DataGridView1.RowTemplate.Height = 30
        DataGridView1.Size = New Size(720, 343)
        DataGridView1.TabIndex = 36
        DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(CByte(198), CByte(185), CByte(247))
        DataGridView1.ThemeStyle.AlternatingRowsStyle.Font = New Font("Segoe UI", 9F)
        DataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = SystemColors.ControlText
        DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.FromArgb(CByte(164), CByte(179), CByte(227))
        DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        DataGridView1.ThemeStyle.BackColor = Color.White
        DataGridView1.ThemeStyle.GridColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        DataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(CByte(65), CByte(95), CByte(197))
        DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridView1.ThemeStyle.HeaderStyle.Font = New Font("Segoe UI", 9F)
        DataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.White
        DataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridView1.ThemeStyle.HeaderStyle.Height = 40
        DataGridView1.ThemeStyle.ReadOnly = True
        DataGridView1.ThemeStyle.RowsStyle.BackColor = Color.White
        DataGridView1.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        DataGridView1.ThemeStyle.RowsStyle.Font = New Font("Segoe UI", 9F)
        DataGridView1.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        DataGridView1.ThemeStyle.RowsStyle.Height = 30
        DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(CByte(164), CByte(179), CByte(227))
        DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        ' 
        ' Product_list
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(717, 404)
        Controls.Add(DataGridView1)
        Controls.Add(Guna2ShadowPanel9)
        Name = "Product_list"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Product_list"
        Guna2ShadowPanel9.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Guna2ShadowPanel9 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents invalue As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents DataGridView1 As Guna.UI2.WinForms.Guna2DataGridView
End Class
