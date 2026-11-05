using System;
using System.Data;
using System.Windows.Forms;

namespace SitiKanisa_Tiket_Kereta
{
    public partial class FPETUGAS : Form
    {
        public FPETUGAS()
        {
            InitializeComponent();
        }

        private void FPETUGAS_Load(object sender, EventArgs e)
        {
            tablepetugas.DataError += (s, ex) => { ex.Cancel = true; };
            tablepetugas.Columns["Column6"].Visible = false; // sembunyikan kolom id

            // Isi dropdown role dari database
            cbrole.Items.Clear();
            db.crud("SELECT nama_role FROM role ORDER BY nama_role");
            foreach (DataRow row in db.ds.Tables[0].Rows)
                cbrole.Items.Add(row["nama_role"].ToString());

            cbstatus.Items.Clear();
            cbstatus.Items.Add("Aktif");
            cbstatus.Items.Add("Tidak Aktif");

            tampildata();
        }

        public void bersih()
        {
            txtuser.Clear();
            txtpass.Clear();
            cbrole.Text    = "";
            cbstatus.Text  = "";
            LBLID.Text     = "";
        }

        public void tampildata()
        {
            tablepetugas.Rows.Clear();
            db.crud("SELECT idp, username, role.nama_role, status " +
                    "FROM petugas INNER JOIN role ON petugas.idrole = role.idRole " +
                    "ORDER BY idp");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                tablepetugas.Rows.Add(
                    baris["idp"].ToString(),
                    no,
                    baris["username"].ToString(),
                    baris["nama_role"].ToString(),
                    baris["status"].ToString()
                );
                no++;
            }
        }

        // Simpan petugas baru
        private void btnsimpan_Click_1(object sender, EventArgs e)
        {
            string user   = txtuser.Text.Trim();
            string pass   = txtpass.Text.Trim();
            string status = cbstatus.Text;
            string role   = cbrole.Text;

            if (user == "" || pass == "" || status == "" || role == "")
            {
                MessageBox.Show("Semua field harus diisi!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil idRole berdasarkan nama role
            db.crud($"SELECT idRole FROM role WHERE nama_role = '{role}'");
            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Role tidak ditemukan!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string idrole = db.ds.Tables[0].Rows[0]["idRole"].ToString();

            if (db.eksekusi($"INSERT INTO petugas (username, password, idrole, status) " +
                            $"VALUES ('{user}', '{pass}', '{idrole}', '{status}')"))
            {
                tampildata();
                bersih();
            }
        }

        // Simpan perubahan petugas
        private void btnubah_Click_1(object sender, EventArgs e)
        {
            if (LBLID.Text == "")
            {
                MessageBox.Show("Pilih data yang ingin diubah terlebih dahulu!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string user   = txtuser.Text.Trim();
            string pass   = txtpass.Text.Trim();
            string status = cbstatus.Text;
            string role   = cbrole.Text;

            if (user == "" || pass == "" || status == "" || role == "")
            {
                MessageBox.Show("Semua field harus diisi!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            db.crud($"SELECT idRole FROM role WHERE nama_role = '{role}'");
            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Role tidak ditemukan!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string idrole = db.ds.Tables[0].Rows[0]["idRole"].ToString();

            if (db.eksekusi($"UPDATE petugas SET username='{user}', password='{pass}', " +
                            $"idrole='{idrole}', status='{status}' WHERE idp='{LBLID.Text}'"))
            {
                tampildata();
                bersih();
            }
        }

        private void tablepetugas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            // Kolom 5 = Edit
            if (kolom == 5)
            {
                string idp = tablepetugas.Rows[baris].Cells[0].Value.ToString();
                db.crud($"SELECT petugas.*, role.nama_role FROM petugas " +
                        $"INNER JOIN role ON petugas.idrole = role.idRole " +
                        $"WHERE idp = '{idp}'");

                if (db.ds.Tables[0].Rows.Count > 0)
                {
                    DataRow brs = db.ds.Tables[0].Rows[0];
                    LBLID.Text     = idp;
                    txtuser.Text   = brs["username"].ToString();
                    txtpass.Text   = brs["password"].ToString();
                    cbstatus.Text  = brs["status"].ToString();
                    cbrole.Text    = brs["nama_role"].ToString();
                }
            }

            // Kolom 6 = Hapus
            if (kolom == 6)
            {
                string idp = tablepetugas.Rows[baris].Cells[0].Value.ToString();
                DialogResult konfirmasi = MessageBox.Show(
                    "Yakin ingin menghapus data petugas ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (konfirmasi == DialogResult.Yes)
                {
                    if (db.eksekusi($"DELETE FROM petugas WHERE idp = '{idp}'"))
                    {
                        tampildata();
                        bersih();
                    }
                }
            }
        }

        // Pencarian live
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            tablepetugas.Rows.Clear();
            db.crud($"SELECT idp, username, role.nama_role, status " +
                    $"FROM petugas INNER JOIN role ON petugas.idrole = role.idRole " +
                    $"WHERE username LIKE '%{txtsearch.Text}%' " +
                    $"OR role.nama_role LIKE '%{txtsearch.Text}%'");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                tablepetugas.Rows.Add(
                    baris["idp"].ToString(),
                    no,
                    baris["username"].ToString(),
                    baris["nama_role"].ToString(),
                    baris["status"].ToString()
                );
                no++;
            }
        }

        private void txtuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (txtuser.Text != "") txtpass.Focus(); }
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (txtpass.Text != "") cbstatus.Focus(); }
        }

        private void cbstatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (cbstatus.Text != "") cbrole.Focus(); }
        }

        private void cbrole_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (cbrole.Text != "") btnsimpan.Focus(); }
        }

        // Handler kosong yang dibutuhkan designer
        private void btnsimpan_Click(object sender, EventArgs e) { }
        private void cbrole_DropDown(object sender, EventArgs e) { }
        private void cbstatus_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cbstatus_DropDown(object sender, EventArgs e) { }
        private void btnubah_Click(object sender, EventArgs e) { }
        private void btntampil_Click(object sender, EventArgs e) { tampildata(); }
        private void LBLID_Click(object sender, EventArgs e) { }
        private void guna2GradientPanel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e) { }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) { }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e) { }
    }
}
