
namespace SitiKanisa_Tiket_Kereta
{
    partial class FROLE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FROLE));
            this.btnsimpan = new Guna.UI2.WinForms.Guna2Button();
            this.txtket = new System.Windows.Forms.TextBox();
            this.txtrole = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LBLID = new System.Windows.Forms.Label();
            this.btntampil = new Guna.UI2.WinForms.Guna2Button();
            this.btnubah = new Guna.UI2.WinForms.Guna2Button();
            this.txtsearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.dgvrole = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Hapus = new System.Windows.Forms.DataGridViewImageColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvrole)).BeginInit();
            this.SuspendLayout();
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
            this.btnsimpan.Location = new System.Drawing.Point(966, 213);
            this.btnsimpan.Name = "btnsimpan";
            this.btnsimpan.Size = new System.Drawing.Size(155, 33);
            this.btnsimpan.TabIndex = 9;
            this.btnsimpan.Text = "Simpan";
            this.btnsimpan.Click += new System.EventHandler(this.btnsimpan_Click);
            // 
            // txtket
            // 
            this.txtket.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtket.Location = new System.Drawing.Point(722, 153);
            this.txtket.Multiline = true;
            this.txtket.Name = "txtket";
            this.txtket.Size = new System.Drawing.Size(399, 36);
            this.txtket.TabIndex = 15;
            this.txtket.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtket_KeyPress);
            // 
            // txtrole
            // 
            this.txtrole.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtrole.Location = new System.Drawing.Point(118, 153);
            this.txtrole.Multiline = true;
            this.txtrole.Name = "txtrole";
            this.txtrole.Size = new System.Drawing.Size(399, 36);
            this.txtrole.TabIndex = 5;
            this.txtrole.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtrole_KeyPress);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Nama Role";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(615, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Keterangan";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.dgvrole);
            this.panel1.Controls.Add(this.LBLID);
            this.panel1.Controls.Add(this.btntampil);
            this.panel1.Controls.Add(this.btnubah);
            this.panel1.Controls.Add(this.txtsearch);
            this.panel1.Controls.Add(this.txtket);
            this.panel1.Controls.Add(this.btnsimpan);
            this.panel1.Controls.Add(this.txtrole);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1167, 669);
            this.panel1.TabIndex = 20;
            // 
            // LBLID
            // 
            this.LBLID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LBLID.AutoSize = true;
            this.LBLID.Location = new System.Drawing.Point(12, 121);
            this.LBLID.Name = "LBLID";
            this.LBLID.Size = new System.Drawing.Size(26, 20);
            this.LBLID.TabIndex = 25;
            this.LBLID.Text = "ID";
            this.LBLID.Visible = false;
            // 
            // btntampil
            // 
            this.btntampil.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btntampil.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btntampil.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btntampil.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btntampil.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btntampil.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btntampil.ForeColor = System.Drawing.Color.White;
            this.btntampil.Location = new System.Drawing.Point(966, 283);
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
            this.btnubah.Location = new System.Drawing.Point(792, 213);
            this.btnubah.Name = "btnubah";
            this.btnubah.Size = new System.Drawing.Size(155, 33);
            this.btnubah.TabIndex = 23;
            this.btnubah.Text = "Ubah";
            this.btnubah.Click += new System.EventHandler(this.btnubah_Click_1);
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
            this.txtsearch.IconRight = ((System.Drawing.Image)(resources.GetObject("txtsearch.IconRight")));
            this.txtsearch.Location = new System.Drawing.Point(860, 36);
            this.txtsearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.PlaceholderText = "Search";
            this.txtsearch.SelectedText = "";
            this.txtsearch.Size = new System.Drawing.Size(279, 31);
            this.txtsearch.TabIndex = 22;
            this.txtsearch.TextChanged += new System.EventHandler(this.guna2TextBox1_TextChanged);
            // 
            // dgvrole
            // 
            this.dgvrole.AllowUserToAddRows = false;
            this.dgvrole.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvrole.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvrole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvrole.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvrole.ColumnHeadersHeight = 22;
            this.dgvrole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvrole.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2,
            this.Role,
            this.Edit,
            this.Hapus});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvrole.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvrole.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvrole.Location = new System.Drawing.Point(0, 322);
            this.dgvrole.Name = "dgvrole";
            this.dgvrole.ReadOnly = true;
            this.dgvrole.RowHeadersVisible = false;
            this.dgvrole.RowHeadersWidth = 62;
            this.dgvrole.RowTemplate.Height = 28;
            this.dgvrole.Size = new System.Drawing.Size(1167, 347);
            this.dgvrole.TabIndex = 26;
            this.dgvrole.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvrole.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvrole.ThemeStyle.HeaderStyle.Height = 22;
            this.dgvrole.ThemeStyle.ReadOnly = true;
            this.dgvrole.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvrole.ThemeStyle.RowsStyle.Height = 28;
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
            this.Column2.HeaderText = "Nama Role";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Role
            // 
            this.Role.HeaderText = "Keterangan";
            this.Role.MinimumWidth = 8;
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
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
            this.label12.Location = new System.Drawing.Point(457, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(250, 34);
            this.label12.TabIndex = 37;
            this.label12.Text = "DAFTAR ROLE";
            // 
            // FROLE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 669);
            this.Controls.Add(this.panel1);
            this.Name = "FROLE";
            this.Text = "user";
            this.Load += new System.EventHandler(this.FROLE_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvrole)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnsimpan;
        private System.Windows.Forms.TextBox txtket;
        private System.Windows.Forms.TextBox txtrole;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2TextBox txtsearch;
        private Guna.UI2.WinForms.Guna2Button btntampil;
        private Guna.UI2.WinForms.Guna2Button btnubah;
        private System.Windows.Forms.Label LBLID;
        private Guna.UI2.WinForms.Guna2DataGridView dgvrole;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Hapus;
        private System.Windows.Forms.Label label12;
    }
}