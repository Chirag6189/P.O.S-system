<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Product
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Product))
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Guna2ShadowPanel9 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        lbluserId = New Label()
        PictureBox1 = New PictureBox()
        Label2 = New Label()
        lblName = New Label()
        Guna2ShadowPanel1 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        invalue = New Guna.UI2.WinForms.Guna2TextBox()
        Label1 = New Label()
        Label3 = New Label()
        Guna2ShadowPanel2 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        DataGridView1 = New Guna.UI2.WinForms.Guna2DataGridView()
        Guna2ShadowPanel3 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        searchcode = New RadioButton()
        searchMRP = New RadioButton()
        searchname = New RadioButton()
        Guna2ShadowPanel9.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Guna2ShadowPanel1.SuspendLayout()
        Guna2ShadowPanel2.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        Guna2ShadowPanel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Guna2ShadowPanel9
        ' 
        Guna2ShadowPanel9.BackColor = Color.Transparent
        Guna2ShadowPanel9.Controls.Add(lbluserId)
        Guna2ShadowPanel9.Controls.Add(PictureBox1)
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
        Guna2ShadowPanel9.TabIndex = 25
        ' 
        ' lbluserId
        ' 
        lbluserId.AutoSize = True
        lbluserId.Font = New Font("Century Gothic", 20F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbluserId.ForeColor = Color.White
        lbluserId.Location = New Point(1536, 17)
        lbluserId.Name = "lbluserId"
        lbluserId.Size = New Size(70, 32)
        lbluserId.TabIndex = 4
        lbluserId.Text = "User"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(1478, 14)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(52, 40)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 3
        PictureBox1.TabStop = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Century Gothic", 20F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.White
        Label2.Location = New Point(23, 17)
        Label2.Name = "Label2"
        Label2.Size = New Size(190, 32)
        Label2.TabIndex = 2
        Label2.Text = "Product Page"
        ' 
        ' lblName
        ' 
        lblName.AutoSize = True
        lblName.Font = New Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblName.ForeColor = Color.DimGray
        lblName.Location = New Point(47, 112)
        lblName.Name = "lblName"
        lblName.Size = New Size(144, 19)
        lblName.TabIndex = 26
        lblName.Text = "Welcome Chirag,"
        ' 
        ' Guna2ShadowPanel1
        ' 
        Guna2ShadowPanel1.BackColor = Color.Transparent
        Guna2ShadowPanel1.Controls.Add(invalue)
        Guna2ShadowPanel1.FillColor = Color.AliceBlue
        Guna2ShadowPanel1.Location = New Point(1257, 207)
        Guna2ShadowPanel1.Margin = New Padding(15)
        Guna2ShadowPanel1.Name = "Guna2ShadowPanel1"
        Guna2ShadowPanel1.Radius = 10
        Guna2ShadowPanel1.ShadowColor = Color.Black
        Guna2ShadowPanel1.ShadowDepth = 50
        Guna2ShadowPanel1.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal
        Guna2ShadowPanel1.Size = New Size(409, 73)
        Guna2ShadowPanel1.TabIndex = 27
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
        invalue.Font = New Font("Century Gothic", 14F, FontStyle.Bold)
        invalue.ForeColor = Color.Black
        invalue.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        invalue.IconLeft = CType(resources.GetObject("invalue.IconLeft"), Image)
        invalue.Location = New Point(20, 13)
        invalue.Margin = New Padding(5, 5, 5, 5)
        invalue.Name = "invalue"
        invalue.PasswordChar = ChrW(0)
        invalue.PlaceholderText = "Search"
        invalue.SelectedText = ""
        invalue.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        invalue.Size = New Size(367, 45)
        invalue.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Century Gothic", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = SystemColors.ActiveCaptionText
        Label1.Location = New Point(67, 131)
        Label1.Name = "Label1"
        Label1.Size = New Size(472, 23)
        Label1.TabIndex = 28
        Label1.Text = "Search your product with name and product code"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Century Gothic", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = SystemColors.ActiveCaptionText
        Label3.Location = New Point(67, 154)
        Label3.Name = "Label3"
        Label3.Size = New Size(281, 23)
        Label3.TabIndex = 29
        Label3.Text = "Type ""all"" for show all product"
        ' 
        ' Guna2ShadowPanel2
        ' 
        Guna2ShadowPanel2.BackColor = Color.Transparent
        Guna2ShadowPanel2.Controls.Add(DataGridView1)
        Guna2ShadowPanel2.FillColor = Color.AliceBlue
        Guna2ShadowPanel2.Location = New Point(24, 286)
        Guna2ShadowPanel2.Margin = New Padding(15)
        Guna2ShadowPanel2.Name = "Guna2ShadowPanel2"
        Guna2ShadowPanel2.Radius = 10
        Guna2ShadowPanel2.ShadowColor = Color.Black
        Guna2ShadowPanel2.ShadowDepth = 50
        Guna2ShadowPanel2.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal
        Guna2ShadowPanel2.Size = New Size(1642, 709)
        Guna2ShadowPanel2.TabIndex = 30
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
        DataGridView1.Location = New Point(23, 19)
        DataGridView1.Margin = New Padding(10)
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
        DataGridView1.Size = New Size(1594, 658)
        DataGridView1.TabIndex = 1
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
        ' Guna2ShadowPanel3
        ' 
        Guna2ShadowPanel3.BackColor = Color.Transparent
        Guna2ShadowPanel3.Controls.Add(searchcode)
        Guna2ShadowPanel3.Controls.Add(searchMRP)
        Guna2ShadowPanel3.Controls.Add(searchname)
        Guna2ShadowPanel3.FillColor = Color.AliceBlue
        Guna2ShadowPanel3.Location = New Point(24, 207)
        Guna2ShadowPanel3.Margin = New Padding(15)
        Guna2ShadowPanel3.Name = "Guna2ShadowPanel3"
        Guna2ShadowPanel3.Radius = 10
        Guna2ShadowPanel3.ShadowColor = Color.Black
        Guna2ShadowPanel3.ShadowDepth = 50
        Guna2ShadowPanel3.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal
        Guna2ShadowPanel3.Size = New Size(628, 73)
        Guna2ShadowPanel3.TabIndex = 31
        ' 
        ' searchcode
        ' 
        searchcode.AutoSize = True
        searchcode.Checked = True
        searchcode.Font = New Font("Century Gothic", 13F, FontStyle.Bold)
        searchcode.Location = New Point(432, 22)
        searchcode.Name = "searchcode"
        searchcode.Size = New Size(186, 26)
        searchcode.TabIndex = 2
        searchcode.TabStop = True
        searchcode.Text = "Search With Code"
        searchcode.UseVisualStyleBackColor = True
        ' 
        ' searchMRP
        ' 
        searchMRP.AutoSize = True
        searchMRP.Font = New Font("Century Gothic", 13F, FontStyle.Bold)
        searchMRP.Location = New Point(230, 22)
        searchMRP.Name = "searchMRP"
        searchMRP.Size = New Size(172, 26)
        searchMRP.TabIndex = 1
        searchMRP.Text = "Search With MRP"
        searchMRP.UseVisualStyleBackColor = True
        ' 
        ' searchname
        ' 
        searchname.AutoSize = True
        searchname.Font = New Font("Century Gothic", 13F, FontStyle.Bold)
        searchname.Location = New Point(19, 21)
        searchname.Name = "searchname"
        searchname.Size = New Size(190, 26)
        searchname.TabIndex = 0
        searchname.Text = "Search With Name"
        searchname.UseVisualStyleBackColor = True
        ' 
        ' Product
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Lavender
        ClientSize = New Size(1690, 1019)
        Controls.Add(Guna2ShadowPanel3)
        Controls.Add(Guna2ShadowPanel2)
        Controls.Add(Label3)
        Controls.Add(Label1)
        Controls.Add(Guna2ShadowPanel1)
        Controls.Add(lblName)
        Controls.Add(Guna2ShadowPanel9)
        Name = "Product"
        Text = "Product"
        Guna2ShadowPanel9.ResumeLayout(False)
        Guna2ShadowPanel9.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Guna2ShadowPanel1.ResumeLayout(False)
        Guna2ShadowPanel2.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        Guna2ShadowPanel3.ResumeLayout(False)
        Guna2ShadowPanel3.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Guna2ShadowPanel9 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents lbluserId As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lblName As Label
    Friend WithEvents Guna2ShadowPanel1 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents invalue As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Guna2ShadowPanel2 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents DataGridView1 As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents Guna2ShadowPanel3 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents searchname As RadioButton
    Friend WithEvents searchcode As RadioButton
    Friend WithEvents searchMRP As RadioButton
End Class
