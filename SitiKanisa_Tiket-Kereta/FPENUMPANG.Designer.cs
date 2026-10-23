
namespace SitiKanisa_Tiket_Kereta
{
    partial class FPENUMPANG
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPENUMPANG));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtindetitas = new System.Windows.Forms.TextBox();
            this.txttujuan = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LBLID = new System.Windows.Forms.Label();
            this.btntampil = new Guna.UI2.WinForms.Guna2Button();
            this.btnubah = new Guna.UI2.WinForms.Guna2Button();
            this.txtsearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnsimpan = new Guna.UI2.WinForms.Guna2Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtnama = new System.Windows.Forms.TextBox();
            this.cmdkelamin = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txttelp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlbase = new Guna.UI2.WinForms.Guna2Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.tablepenumpang = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Hapus = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            this.pnlbase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablepenumpang)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "Hapus";
            this.dataGridViewImageColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn2.Image")));
            this.dataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn2.MinimumWidth = 8;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Width = 202;
            // 
            // txtindetitas
            // 
            this.txtindetitas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtindetitas.Location = new System.Drawing.Point(156, 222);
            this.txtindetitas.Multiline = true;
            this.txtindetitas.Name = "txtindetitas";
            this.txtindetitas.Size = new System.Drawing.Size(399, 36);
            this.txtindetitas.TabIndex = 33;
            this.txtindetitas.TextChanged += new System.EventHandler(this.txtindetitas_TextChanged);
            this.txtindetitas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtindetitas_KeyPress);
            // 
            // txttujuan
            // 
            this.txttujuan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txttujuan.AutoSize = true;
            this.txttujuan.Location = new System.Drawing.Point(588, 228);
            this.txttujuan.Name = "txttujuan";
            this.txttujuan.Size = new System.Drawing.Size(81, 20);
            this.txttujuan.TabIndex = 30;
            this.txttujuan.Text = "No Telpon";
            this.txttujuan.Click += new System.EventHandler(this.txttujuan_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "No Identitas";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // LBLID
            // 
            this.LBLID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LBLID.AutoSize = true;
            this.LBLID.Location = new System.Drawing.Point(37, 118);
            this.LBLID.Name = "LBLID";
            this.LBLID.Size = new System.Drawing.Size(26, 20);
            this.LBLID.TabIndex = 25;
            this.LBLID.Text = "ID";
            this.LBLID.Visible = false;
            this.LBLID.Click += new System.EventHandler(this.LBLID_Click);
            // 
            // btntampil
            // 
            this.btntampil.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btntampil.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btntampil.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btntampil.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btntampil.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btntampil.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btntampil.ForeColor = System.Drawing.Color.White;
            this.btntampil.Location = new System.Drawing.Point(955, 284);
            this.btntampil.Name = "btntampil";
            this.btntampil.Size = new System.Drawing.Size(155, 33);
            this.btntampil.TabIndex = 24;
            this.btntampil.Text = "Tampil";
            this.btntampil.Click += new System.EventHandler(this.btntampil_Click);
            // 
            // btnubah
            // 
            this.btnubah.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnubah.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnubah.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnubah.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnubah.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnubah.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnubah.ForeColor = System.Drawing.Color.White;
            this.btnubah.Location = new System.Drawing.Point(583, 284);
            this.btnubah.Name = "btnubah";
            this.btnubah.Size = new System.Drawing.Size(155, 33);
            this.btnubah.TabIndex = 23;
            this.btnubah.Text = "Ubah";
            this.btnubah.Click += new System.EventHandler(this.btnubah_Click);
            // 
            // txtsearch
            // 
            this.txtsearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtsearch.BorderRadius = 10;
            this.txtsearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtsearch.DefaultText = "";
            this.txtsearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtsearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtsearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtsearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtsearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtsearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtsearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtsearch.IconLeft = ((System.Drawing.Image)(resources.GetObject("txtsearch.IconLeft")));
            this.txtsearch.Location = new System.Drawing.Point(796, 14);
            this.txtsearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.PlaceholderText = "Search";
            this.txtsearch.SelectedText = "";
            this.txtsearch.Size = new System.Drawing.Size(259, 31);
            this.txtsearch.TabIndex = 22;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            // 
            // btnsimpan
            // 
            this.btnsimpan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnsimpan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnsimpan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnsimpan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnsimpan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnsimpan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnsimpan.ForeColor = System.Drawing.Color.White;
            this.btnsimpan.Location = new System.Drawing.Point(762, 284);
            this.btnsimpan.Name = "btnsimpan";
            this.btnsimpan.Size = new System.Drawing.Size(155, 33);
            this.btnsimpan.TabIndex = 9;
            this.btnsimpan.Text = "Simpan";
            this.btnsimpan.Click += new System.EventHandler(this.btnsimpan_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlbase);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1113, 714);
            this.panel1.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(588, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 36;
            this.label1.Text = "Jenis Kelamin";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtnama
            // 
            this.txtnama.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtnama.Location = new System.Drawing.Point(156, 153);
            this.txtnama.Multiline = true;
            this.txtnama.Name = "txtnama";
            this.txtnama.Size = new System.Drawing.Size(399, 36);
            this.txtnama.TabIndex = 35;
            this.txtnama.TextChanged += new System.EventHandler(this.txtnama_TextChanged);
            this.txtnama.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtnama_KeyPress);
            // 
            // cmdkelamin
            // 
            this.cmdkelamin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdkelamin.BackColor = System.Drawing.Color.Transparent;
            this.cmdkelamin.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmdkelamin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdkelamin.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmdkelamin.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmdkelamin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmdkelamin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmdkelamin.ItemHeight = 30;
            this.cmdkelamin.Location = new System.Drawing.Point(714, 153);
            this.cmdkelamin.Name = "cmdkelamin";
            this.cmdkelamin.Size = new System.Drawing.Size(387, 36);
            this.cmdkelamin.TabIndex = 34;
            this.cmdkelamin.SelectedIndexChanged += new System.EventHandler(this.cmdkelamin_SelectedIndexChanged);
            this.cmdkelamin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmdkelamin_KeyPress);
            // 
            // txttelp
            // 
            this.txttelp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txttelp.Location = new System.Drawing.Point(714, 222);
            this.txttelp.Multiline = true;
            this.txttelp.Name = "txttelp";
            this.txttelp.Size = new System.Drawing.Size(386, 36);
            this.txttelp.TabIndex = 31;
            this.txttelp.TextChanged += new System.EventHandler(this.txttelp_TextChanged);
            this.txttelp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttelp_KeyPress);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Nama Lengkap";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Edit";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.MinimumWidth = 8;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 202;
            // 
            // pnlbase
            // 
            this.pnlbase.Controls.Add(this.tablepenumpang);
            this.pnlbase.Controls.Add(this.label9);
            this.pnlbase.Controls.Add(this.label1);
            this.pnlbase.Controls.Add(this.btnsimpan);
            this.pnlbase.Controls.Add(this.txtnama);
            this.pnlbase.Controls.Add(this.label4);
            this.pnlbase.Controls.Add(this.cmdkelamin);
            this.pnlbase.Controls.Add(this.txtsearch);
            this.pnlbase.Controls.Add(this.txtindetitas);
            this.pnlbase.Controls.Add(this.btnubah);
            this.pnlbase.Controls.Add(this.txttelp);
            this.pnlbase.Controls.Add(this.btntampil);
            this.pnlbase.Controls.Add(this.txttujuan);
            this.pnlbase.Controls.Add(this.LBLID);
            this.pnlbase.Controls.Add(this.label2);
            this.pnlbase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlbase.Location = new System.Drawing.Point(0, 0);
            this.pnlbase.Name = "pnlbase";
            this.pnlbase.Size = new System.Drawing.Size(1113, 714);
            this.pnlbase.TabIndex = 19;
            this.pnlbase.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlbase_Paint);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Modern No. 20", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(282, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(483, 34);
            this.label9.TabIndex = 20;
            this.label9.Text = "TAMBAH DATA PENUMPANG";
            // 
            // tablepenumpang
            // 
            this.tablepenumpang.AllowUserToAddRows = false;
            this.tablepenumpang.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            this.tablepenumpang.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.tablepenumpang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablepenumpang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.tablepenumpang.ColumnHeadersHeight = 22;
            this.tablepenumpang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tablepenumpang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column2,
            this.Role,
            this.Column4,
            this.Column5,
            this.Edit,
            this.Hapus});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tablepenumpang.DefaultCellStyle = dataGridViewCellStyle12;
            this.tablepenumpang.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tablepenumpang.Location = new System.Drawing.Point(3, 368);
            this.tablepenumpang.Name = "tablepenumpang";
            this.tablepenumpang.ReadOnly = true;
            this.tablepenumpang.RowHeadersVisible = false;
            this.tablepenumpang.RowHeadersWidth = 62;
            this.tablepenumpang.RowTemplate.Height = 28;
            this.tablepenumpang.Size = new System.Drawing.Size(1113, 343);
            this.tablepenumpang.TabIndex = 37;
            this.tablepenumpang.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tablepenumpang.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablepenumpang.ThemeStyle.HeaderStyle.Height = 22;
            this.tablepenumpang.ThemeStyle.ReadOnly = true;
            this.tablepenumpang.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablepenumpang.ThemeStyle.RowsStyle.Height = 28;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "ID";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nama ";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Role
            // 
            this.Role.HeaderText = "No Identitas";
            this.Role.MinimumWidth = 8;
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Jenis Kelamin";
            this.Column4.MinimumWidth = 8;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "No Telpo";
            this.Column5.MinimumWidth = 8;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Edit.MinimumWidth = 8;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            // 
            // Hapus
            // 
            this.Hapus.HeaderText = "Hapus";
            this.Hapus.Image = ((System.Drawing.Image)(resources.GetObject("Hapus.Image")));
            this.Hapus.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Hapus.MinimumWidth = 8;
            this.Hapus.Name = "Hapus";
            this.Hapus.ReadOnly = true;
            // 
            // FPENUMPANG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 714);
            this.Controls.Add(this.panel1);
            this.Name = "FPENUMPANG";
            this.Text = "FPENUMPANG";
            this.Load += new System.EventHandler(this.FPENUMPANG_Load);
            this.panel1.ResumeLayout(false);
            this.pnlbase.ResumeLayout(false);
            this.pnlbase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablepenumpang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.TextBox txtindetitas;
        private System.Windows.Forms.Label txttujuan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LBLID;
        private Guna.UI2.WinForms.Guna2Button btntampil;
        private Guna.UI2.WinForms.Guna2Button btnubah;
        private Guna.UI2.WinForms.Guna2TextBox txtsearch;
        private Guna.UI2.WinForms.Guna2Button btnsimpan;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtnama;
        private Guna.UI2.WinForms.Guna2ComboBox cmdkelamin;
        private System.Windows.Forms.TextBox txttelp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private Guna.UI2.WinForms.Guna2Panel pnlbase;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2DataGridView tablepenumpang;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Hapus;
    }
}