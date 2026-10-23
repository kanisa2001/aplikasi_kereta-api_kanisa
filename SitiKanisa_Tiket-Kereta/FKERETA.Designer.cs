
namespace SitiKanisa_Tiket_Kereta
{
    partial class FKERETA
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FKERETA));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtharga = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtkursi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbstatus = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbjenis = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LBLID = new System.Windows.Forms.Label();
            this.btntampil = new Guna.UI2.WinForms.Guna2Button();
            this.btnubah = new Guna.UI2.WinForms.Guna2Button();
            this.txtsearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnsimpan = new Guna.UI2.WinForms.Guna2Button();
            this.txtnmkereta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.tablekereta = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Hapus = new System.Windows.Forms.DataGridViewImageColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablekereta)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.tablekereta);
            this.panel1.Controls.Add(this.txtharga);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtkursi);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbstatus);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbjenis);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.LBLID);
            this.panel1.Controls.Add(this.btntampil);
            this.panel1.Controls.Add(this.btnubah);
            this.panel1.Controls.Add(this.txtsearch);
            this.panel1.Controls.Add(this.btnsimpan);
            this.panel1.Controls.Add(this.txtnmkereta);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1415, 714);
            this.panel1.TabIndex = 22;
            // 
            // txtharga
            // 
            this.txtharga.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtharga.Location = new System.Drawing.Point(159, 301);
            this.txtharga.Multiline = true;
            this.txtharga.Name = "txtharga";
            this.txtharga.Size = new System.Drawing.Size(491, 36);
            this.txtharga.TabIndex = 33;
            this.txtharga.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtharga_KeyPress);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 301);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 20);
            this.label6.TabIndex = 32;
            this.label6.Text = "Harga";
            // 
            // txtkursi
            // 
            this.txtkursi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtkursi.Location = new System.Drawing.Point(846, 241);
            this.txtkursi.Multiline = true;
            this.txtkursi.Name = "txtkursi";
            this.txtkursi.Size = new System.Drawing.Size(491, 36);
            this.txtkursi.TabIndex = 31;
            this.txtkursi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtkursi_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(713, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 30;
            this.label3.Text = "Jumlah Kursi";
            // 
            // cmbstatus
            // 
            this.cmbstatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbstatus.BackColor = System.Drawing.Color.Transparent;
            this.cmbstatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbstatus.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbstatus.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbstatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbstatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbstatus.ItemHeight = 30;
            this.cmbstatus.Location = new System.Drawing.Point(846, 171);
            this.cmbstatus.Name = "cmbstatus";
            this.cmbstatus.Size = new System.Drawing.Size(491, 36);
            this.cmbstatus.TabIndex = 29;
            this.cmbstatus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbstatus_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(713, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Status";
            // 
            // cmbjenis
            // 
            this.cmbjenis.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbjenis.BackColor = System.Drawing.Color.Transparent;
            this.cmbjenis.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbjenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbjenis.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbjenis.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbjenis.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbjenis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbjenis.ItemHeight = 30;
            this.cmbjenis.Location = new System.Drawing.Point(159, 235);
            this.cmbjenis.Name = "cmbjenis";
            this.cmbjenis.Size = new System.Drawing.Size(491, 36);
            this.cmbjenis.TabIndex = 27;
            this.cmbjenis.DropDown += new System.EventHandler(this.cmbjenis_DropDown);
            this.cmbjenis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbjenis_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Jenis Kereta";
            // 
            // LBLID
            // 
            this.LBLID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LBLID.AutoSize = true;
            this.LBLID.Location = new System.Drawing.Point(43, 132);
            this.LBLID.Name = "LBLID";
            this.LBLID.Size = new System.Drawing.Size(0, 20);
            this.LBLID.TabIndex = 25;
            this.LBLID.Visible = false;
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
            this.btntampil.Location = new System.Drawing.Point(1182, 301);
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
            this.btnubah.Location = new System.Drawing.Point(1021, 301);
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
            this.txtsearch.Location = new System.Drawing.Point(1093, 27);
            this.txtsearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.PlaceholderText = "Search";
            this.txtsearch.SelectedText = "";
            this.txtsearch.Size = new System.Drawing.Size(279, 31);
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
            this.btnsimpan.Location = new System.Drawing.Point(720, 301);
            this.btnsimpan.Name = "btnsimpan";
            this.btnsimpan.Size = new System.Drawing.Size(290, 33);
            this.btnsimpan.TabIndex = 9;
            this.btnsimpan.Text = "Simpan";
            this.btnsimpan.Click += new System.EventHandler(this.btnsimpan_Click);
            // 
            // txtnmkereta
            // 
            this.txtnmkereta.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtnmkereta.Location = new System.Drawing.Point(159, 171);
            this.txtnmkereta.Multiline = true;
            this.txtnmkereta.Name = "txtnmkereta";
            this.txtnmkereta.Size = new System.Drawing.Size(491, 36);
            this.txtnmkereta.TabIndex = 5;
            this.txtnmkereta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtnmkereta_KeyPress);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Nama Kereta";
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
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "Hapus";
            this.dataGridViewImageColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn2.Image")));
            this.dataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn2.MinimumWidth = 8;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Width = 202;
            // 
            // tablekereta
            // 
            this.tablekereta.AllowUserToAddRows = false;
            this.tablekereta.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.tablekereta.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tablekereta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablekereta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tablekereta.ColumnHeadersHeight = 22;
            this.tablekereta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tablekereta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2,
            this.Role,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Edit,
            this.Hapus});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tablekereta.DefaultCellStyle = dataGridViewCellStyle3;
            this.tablekereta.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tablekereta.Location = new System.Drawing.Point(0, 395);
            this.tablekereta.Name = "tablekereta";
            this.tablekereta.ReadOnly = true;
            this.tablekereta.RowHeadersVisible = false;
            this.tablekereta.RowHeadersWidth = 62;
            this.tablekereta.RowTemplate.Height = 28;
            this.tablekereta.Size = new System.Drawing.Size(1415, 319);
            this.tablekereta.TabIndex = 34;
            this.tablekereta.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tablekereta.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablekereta.ThemeStyle.HeaderStyle.Height = 22;
            this.tablekereta.ThemeStyle.ReadOnly = true;
            this.tablekereta.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablekereta.ThemeStyle.RowsStyle.Height = 28;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "ID";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "No";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nama Kereta";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Role
            // 
            this.Role.HeaderText = "Jenis Kereta";
            this.Role.MinimumWidth = 8;
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Jumlah Kursi";
            this.Column4.MinimumWidth = 8;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Status";
            this.Column5.MinimumWidth = 8;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Harga";
            this.Column6.MinimumWidth = 8;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
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
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Modern No. 20", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(507, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(298, 34);
            this.label12.TabIndex = 35;
            this.label12.Text = "DAFTAR KERETA";
            // 
            // FKERETA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1415, 714);
            this.Controls.Add(this.panel1);
            this.Name = "FKERETA";
            this.Text = "FKERETA";
            this.Load += new System.EventHandler(this.FKERETA_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablekereta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LBLID;
        private Guna.UI2.WinForms.Guna2Button btntampil;
        private Guna.UI2.WinForms.Guna2Button btnubah;
        private Guna.UI2.WinForms.Guna2TextBox txtsearch;
        private Guna.UI2.WinForms.Guna2Button btnsimpan;
        private System.Windows.Forms.TextBox txtnmkereta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.TextBox txtkursi;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox cmbstatus;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2ComboBox cmbjenis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtharga;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2DataGridView tablekereta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Hapus;
        private System.Windows.Forms.Label label12;
    }
}