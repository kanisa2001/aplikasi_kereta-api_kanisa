using System;
using System.Data;
using System.Windows.Forms;

namespace SitiKanisa_Tiket_Kereta
{
    public partial class FKERETA : Form
    {
        public FKERETA()
        {
            InitializeComponent();
        }

        private void FKERETA_Load(object sender, EventArgs e)
        {
            tablekereta.DataError += (s, ex) => { ex.Cancel = true; };

            isiJenis();

            cmbstatus.Items.Clear();
            cmbstatus.Items.Add("Sedang Beroperasi");
            cmbstatus.Items.Add("Tidak Sedang Beroperasi");
            cmbstatus.Items.Add("Tidak Beroperasi");

            tampildata();
            bersih();
        }

        public void bersih()
        {
            txtnmkereta.Clear();
            cmbjenis.Text  = "";
            txtkursi.Clear();
            cmbstatus.Text = "";
            txtharga.Clear();
            LBLID.Text     = "";
        }

        private void isiJenis()
        {
            cmbjenis.Items.Clear();
            db.crud("SELECT nama_jenis FROM jenis_kereta ORDER BY nama_jenis");
            foreach (DataRow row in db.ds.Tables[0].Rows)
                cmbjenis.Items.Add(row["nama_jenis"].ToString());
        }

        public void tampildata()
        {
            tablekereta.Rows.Clear();
            db.crud("SELECT kereta.idkereta, kereta.nama_kereta, jenis_kereta.nama_jenis, " +
                    "kereta.kapasitas, kereta.status, kereta.harga " +
                    "FROM kereta INNER JOIN jenis_kereta " +
                    "ON kereta.id_jenis_kereta = jenis_kereta.id_jenis_kereta ORDER BY kereta.idkereta");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                tablekereta.Rows.Add(
                    baris["idkereta"].ToString(),
                    no,
                    baris["nama_kereta"].ToString(),
                    baris["nama_jenis"].ToString(),
                    baris["kapasitas"].ToString(),
                    baris["status"].ToString(),
                    baris["harga"].ToString()
                );
                no++;
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            string nmk  = txtnmkereta.Text.Trim();
            string jns  = cmbjenis.Text;
            string krs  = txtkursi.Text.Trim();
            string stts = cmbstatus.Text;
            string hrg  = txtharga.Text.Trim();

            if (nmk == "" || jns == "" || krs == "" || stts == "" || hrg == "")
            {
                MessageBox.Show("Semua field harus diisi!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            db.crud($"SELECT id_jenis_kereta FROM jenis_kereta WHERE nama_jenis = '{jns}'");
            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Jenis kereta tidak ditemukan!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string idJenis = db.ds.Tables[0].Rows[0]["id_jenis_kereta"].ToString();

            if (db.eksekusi($"INSERT INTO kereta (nama_kereta, id_jenis_kereta, kapasitas, status, harga) " +
                            $"VALUES ('{nmk}', '{idJenis}', '{krs}', '{stts}', '{hrg}')"))
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

            string nmk  = txtnmkereta.Text.Trim();
            string jns  = cmbjenis.Text;
            string krs  = txtkursi.Text.Trim();
            string stts = cmbstatus.Text;
            string hrg  = txtharga.Text.Trim();

            if (nmk == "" || jns == "" || krs == "" || stts == "" || hrg == "")
            {
                MessageBox.Show("Semua field harus diisi!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            db.crud($"SELECT id_jenis_kereta FROM jenis_kereta WHERE nama_jenis = '{jns}'");
            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Jenis kereta tidak ditemukan!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string idJenis = db.ds.Tables[0].Rows[0]["id_jenis_kereta"].ToString();

            if (db.eksekusi($"UPDATE kereta SET nama_kereta='{nmk}', id_jenis_kereta='{idJenis}', " +
                            $"kapasitas='{krs}', status='{stts}', harga='{hrg}' " +
                            $"WHERE idkereta='{LBLID.Text}'"))
            {
                tampildata();
                bersih();
            }
        }

        private void tablekereta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            // Kolom 7 = Edit
            if (kolom == 7)
            {
                string idkereta = tablekereta.Rows[baris].Cells[0].Value.ToString();
                db.crud($"SELECT kereta.*, jenis_kereta.nama_jenis FROM kereta " +
                        $"INNER JOIN jenis_kereta ON kereta.id_jenis_kereta = jenis_kereta.id_jenis_kereta " +
                        $"WHERE idkereta = '{idkereta}'");

                if (db.ds.Tables[0].Rows.Count > 0)
                {
                    DataRow brs    = db.ds.Tables[0].Rows[0];
                    LBLID.Text     = idkereta;
                    txtnmkereta.Text = brs["nama_kereta"].ToString();
                    cmbjenis.Text  = brs["nama_jenis"].ToString();
                    txtkursi.Text  = brs["kapasitas"].ToString();
                    cmbstatus.Text = brs["status"].ToString();
                    txtharga.Text  = brs["harga"].ToString();
                }
            }

            // Kolom 8 = Hapus
            if (kolom == 8)
            {
                string idkereta = tablekereta.Rows[baris].Cells[0].Value.ToString();
                DialogResult konfirmasi = MessageBox.Show(
                    "Yakin ingin menghapus data kereta ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (konfirmasi == DialogResult.Yes)
                {
                    if (db.eksekusi($"DELETE FROM kereta WHERE idkereta = '{idkereta}'"))
                    {
                        tampildata();
                        bersih();
                    }
                }
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            tablekereta.Rows.Clear();
            string cari = txtsearch.Text;
            db.crud($"SELECT kereta.idkereta, kereta.nama_kereta, jenis_kereta.nama_jenis, " +
                    $"kereta.kapasitas, kereta.status, kereta.harga " +
                    $"FROM kereta INNER JOIN jenis_kereta " +
                    $"ON kereta.id_jenis_kereta = jenis_kereta.id_jenis_kereta " +
                    $"WHERE kereta.nama_kereta LIKE '%{cari}%' " +
                    $"OR jenis_kereta.nama_jenis LIKE '%{cari}%' " +
                    $"OR kereta.status LIKE '%{cari}%'");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                tablekereta.Rows.Add(
                    baris["idkereta"].ToString(), no,
                    baris["nama_kereta"].ToString(),
                    baris["nama_jenis"].ToString(),
                    baris["kapasitas"].ToString(),
                    baris["status"].ToString(),
                    baris["harga"].ToString()
                );
                no++;
            }
        }

        // Handler kosong yang dibutuhkan designer
        private void cmbjenis_DropDown(object sender, EventArgs e) { isiJenis(); }
        private void btntampil_Click(object sender, EventArgs e) { tampildata(); }
        private void tablekereta_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtnmkereta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (txtnmkereta.Text != "") cmbjenis.Focus(); }
        }
        private void cmbjenis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (cmbjenis.Text != "") cmbstatus.Focus(); }
        }
        private void cmbstatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (cmbstatus.Text != "") txtkursi.Focus(); }
        }
        private void txtkursi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (txtkursi.Text != "") txtharga.Focus(); }
        }
        private void txtharga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; if (txtharga.Text != "") btnsimpan.Focus(); }
        }
    }
}
