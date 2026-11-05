using System;
using System.Data;
using System.Windows.Forms;

namespace SitiKanisa_Tiket_Kereta
{
    public partial class FPENUMPANG : Form
    {
        public FPENUMPANG()
        {
            InitializeComponent();
        }

        private void FPENUMPANG_Load(object sender, EventArgs e)
        {
            tablepenumpang.DataError += (s, ex) => { ex.Cancel = true; };

            cmdkelamin.Items.Clear();
            cmdkelamin.Items.Add("Laki-Laki");
            cmdkelamin.Items.Add("Perempuan");

            tampildata();
            bersih();
        }

        public void bersih()
        {
            txtnama.Clear();
            txtindetitas.Clear();
            txttelp.Clear();
            cmdkelamin.Text = "";
            LBLID.Text      = "";
        }

        public void tampildata()
        {
            tablepenumpang.Rows.Clear();
            db.crud("SELECT idpenumpang, nama, nik, jenis_kelamin, no_hp FROM penumpang ORDER BY idpenumpang");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                tablepenumpang.Rows.Add(
                    baris["idpenumpang"].ToString(),
                    baris["nama"].ToString(),
                    baris["nik"].ToString(),
                    baris["jenis_kelamin"].ToString(),
                    baris["no_hp"].ToString()
                );
                no++;
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            string nama       = txtnama.Text.Trim();
            string identitas  = txtindetitas.Text.Trim();
            string telp       = txttelp.Text.Trim();
            string jnskelamin = cmdkelamin.Text;

            if (nama == "" || identitas == "" || telp == "" || jnskelamin == "")
            {
                MessageBox.Show("Semua field harus diisi!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (db.eksekusi($"INSERT INTO penumpang (nama, nik, jenis_kelamin, no_hp) " +
                            $"VALUES ('{nama}', '{identitas}', '{jnskelamin}', '{telp}')"))
            {
                tampildata();
                bersih();
            }
        }

        private void btnubah_Click(object sender, EventArgs e)
        {
            if (LBLID.Text == "")
            {
                MessageBox.Show("Pilih data yang ingin diubah terlebih dahulu!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nama       = txtnama.Text.Trim();
            string identitas  = txtindetitas.Text.Trim();
            string telp       = txttelp.Text.Trim();
            string jnskelamin = cmdkelamin.Text;

            if (nama == "" || identitas == "" || telp == "" || jnskelamin == "")
            {
                MessageBox.Show("Semua field harus diisi!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (db.eksekusi($"UPDATE penumpang SET nama='{nama}', nik='{identitas}', " +
                            $"jenis_kelamin='{jnskelamin}', no_hp='{telp}' " +
                            $"WHERE idpenumpang='{LBLID.Text}'"))
            {
                tampildata();
                bersih();
            }
        }

        private void tablepenumpang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            // Kolom 5 = Edit — ambil berdasarkan id spesifik, bukan SELECT *
            if (kolom == 5)
            {
                string idp = tablepenumpang.Rows[baris].Cells[0].Value.ToString();
                db.crud($"SELECT * FROM penumpang WHERE idpenumpang = '{idp}'");

                if (db.ds.Tables[0].Rows.Count > 0)
                {
                    DataRow brs = db.ds.Tables[0].Rows[0];
                    LBLID.Text          = idp;
                    txtnama.Text        = brs["nama"].ToString();
                    txtindetitas.Text   = brs["nik"].ToString();
                    cmdkelamin.Text     = brs["jenis_kelamin"].ToString();
                    txttelp.Text        = brs["no_hp"].ToString();
                }
            }

            // Kolom 6 = Hapus
            if (kolom == 6)
            {
                string idp = tablepenumpang.Rows[baris].Cells[0].Value.ToString();
                DialogResult konfirmasi = MessageBox.Show(
                    "Yakin ingin menghapus data penumpang ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (konfirmasi == DialogResult.Yes)
                {
                    if (db.eksekusi($"DELETE FROM penumpang WHERE idpenumpang = '{idp}'"))
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
            tablepenumpang.Rows.Clear();
            string cari = txtsearch.Text;
            db.crud($"SELECT idpenumpang, nama, nik, jenis_kelamin, no_hp FROM penumpang " +
                    $"WHERE nama LIKE '%{cari}%' OR nik LIKE '%{cari}%' " +
                    $"OR jenis_kelamin LIKE '%{cari}%' OR no_hp LIKE '%{cari}%'");

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                tablepenumpang.Rows.Add(
                    baris["idpenumpang"].ToString(),
                    baris["nama"].ToString(),
                    baris["nik"].ToString(),
                    baris["jenis_kelamin"].ToString(),
                    baris["no_hp"].ToString()
                );
            }
        }

        private void txtnama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (txtnama.Text != "") txtindetitas.Focus(); }
        }

        private void txtindetitas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (txtindetitas.Text != "") { cmdkelamin.Focus(); cmdkelamin.DroppedDown = true; } }
        }

        private void cmdkelamin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (cmdkelamin.Text != "") txttelp.Focus(); }
        }

        private void txttelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (txttelp.Text != "") btnsimpan.Focus(); }
        }

        // Handler kosong yang dibutuhkan designer
        private void pnlbase_Paint(object sender, System.Windows.Forms.PaintEventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void txtnama_TextChanged(object sender, EventArgs e) { }
        private void txtindetitas_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void txttujuan_Click(object sender, EventArgs e) { }
        private void txttelp_TextChanged(object sender, EventArgs e) { }
        private void cmdkelamin_SelectedIndexChanged(object sender, EventArgs e) { }
        private void LBLID_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void btntampil_Click(object sender, EventArgs e) { tampildata(); }

        private void tablepenumpang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
