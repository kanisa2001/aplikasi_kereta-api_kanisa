using System;
using System.Data;
using System.Windows.Forms;

namespace SitiKanisa_Tiket_Kereta
{
    public partial class FROLE : Form
    {
        public FROLE()
        {
            InitializeComponent();
        }

        private void FROLE_Load(object sender, EventArgs e)
        {
            dgvrole.DataError += (s, ex) => { ex.Cancel = true; };
            tampildata();
            dgvrole.Columns["Column3"].Visible = false; // sembunyikan kolom id
        }

        public void bersih()
        {
            txtrole.Clear();
            txtket.Clear();
            LBLID.Text = "";
        }

        public void tampildata()
        {
            dgvrole.Rows.Clear();
            db.crud("SELECT idRole, nama_role, keterangan FROM role ORDER BY idRole");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                dgvrole.Rows.Add(
                    baris["idRole"].ToString(),
                    no,
                    baris["nama_role"].ToString(),
                    baris["keterangan"].ToString()
                );
                no++;
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            string role = txtrole.Text.Trim();
            string ket  = txtket.Text.Trim();

            if (role == "")
            {
                MessageBox.Show("Nama role tidak boleh kosong!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtrole.Focus();
                return;
            }

            if (db.eksekusi($"INSERT INTO role (nama_role, keterangan) VALUES ('{role}', '{ket}')"))
            {
                tampildata();
                bersih();
            }
        }

        private void btnubah_Click_1(object sender, EventArgs e)
        {
            if (LBLID.Text == "")
            {
                MessageBox.Show("Pilih data yang ingin diubah terlebih dahulu!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string role = txtrole.Text.Trim();
            string ket  = txtket.Text.Trim();

            if (role == "")
            {
                MessageBox.Show("Nama role tidak boleh kosong!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtrole.Focus();
                return;
            }

            if (db.eksekusi($"UPDATE role SET nama_role='{role}', keterangan='{ket}' WHERE idRole={LBLID.Text}"))
            {
                tampildata();
                bersih();
            }
        }

        private void dgvrole_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            // Kolom 4 = Edit, kolom 5 = Hapus
            if (kolom == 4)
            {
                string idRole = dgvrole.Rows[baris].Cells[0].Value.ToString();
                db.crud($"SELECT * FROM role WHERE idRole = '{idRole}'");
                if (db.ds.Tables[0].Rows.Count > 0)
                {
                    DataRow brs = db.ds.Tables[0].Rows[0];
                    LBLID.Text    = idRole;
                    txtrole.Text  = brs["nama_role"].ToString();
                    txtket.Text   = brs["keterangan"].ToString();
                }
            }

            if (kolom == 5)
            {
                string idRole = dgvrole.Rows[baris].Cells[0].Value.ToString();
                DialogResult konfirmasi = MessageBox.Show(
                    "Yakin ingin menghapus data ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (konfirmasi == DialogResult.Yes)
                {
                    if (db.eksekusi($"DELETE FROM role WHERE idRole = '{idRole}'"))
                    {
                        tampildata();
                        bersih();
                    }
                }
            }
        }

        // Pencarian live — gunakan tampildata agar id tetap tersimpan di kolom 0
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            dgvrole.Rows.Clear();
            db.crud($"SELECT idRole, nama_role, keterangan FROM role WHERE nama_role LIKE '%{txtsearch.Text}%'");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                dgvrole.Rows.Add(
                    baris["idRole"].ToString(),
                    no,
                    baris["nama_role"].ToString(),
                    baris["keterangan"].ToString()
                );
                no++;
            }
        }

        private void btntampil_Click(object sender, EventArgs e)
        {
            tampildata();
        }

        private void txtrole_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtrole.Text != "") txtket.Focus();
            }
        }

        private void txtket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtket.Text != "") btnsimpan.Focus();
            }
        }
    }
}
