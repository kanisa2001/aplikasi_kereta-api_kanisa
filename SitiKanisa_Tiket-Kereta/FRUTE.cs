using System;
using System.Data;
using System.Windows.Forms;

namespace SitiKanisa_Tiket_Kereta
{
    public partial class FRUTE : Form
    {
        public FRUTE()
        {
            InitializeComponent();
        }

        private void FRUTE_Load(object sender, EventArgs e)
        {
            tablerute.DataError += (s, ex) => { ex.Cancel = true; };
            isiStasiun();
            tampildata();
        }

        private void isiStasiun()
        {
            db.crud("SELECT nama_stasiun FROM stasiun ORDER BY nama_stasiun");
            cbasal.Items.Clear();
            cbtujuan.Items.Clear();
            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                cbasal.Items.Add(row["nama_stasiun"].ToString());
                cbtujuan.Items.Add(row["nama_stasiun"].ToString());
            }
        }

        public void bersih()
        {
            cbasal.Text   = "";
            cbtujuan.Text = "";
            LBLID.Text    = "";
        }

        public void tampildata()
        {
            tablerute.Rows.Clear();
            db.crud("SELECT rute.id_rute, asal.nama_stasiun AS stasiun_asal, " +
                    "tujuan.nama_stasiun AS stasiun_tujuan " +
                    "FROM rute " +
                    "INNER JOIN stasiun AS asal ON rute.id_stasiun_asal = asal.id_stasiun " +
                    "INNER JOIN stasiun AS tujuan ON rute.id_stasiun_tujuan = tujuan.id_stasiun " +
                    "ORDER BY rute.id_rute");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                tablerute.Rows.Add(
                    baris["id_rute"].ToString(),
                    no,
                    baris["stasiun_asal"].ToString(),
                    baris["stasiun_tujuan"].ToString()
                );
                no++;
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            string asal   = cbasal.Text;
            string tujuan = cbtujuan.Text;

            if (asal == "" || tujuan == "")
            {
                MessageBox.Show("Stasiun asal dan tujuan harus dipilih!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (asal == tujuan)
            {
                MessageBox.Show("Stasiun asal dan tujuan tidak boleh sama!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            db.crud($"SELECT id_stasiun FROM stasiun WHERE nama_stasiun = '{asal}'");
            if (db.ds.Tables[0].Rows.Count == 0) { MessageBox.Show("Stasiun asal tidak ditemukan!"); return; }
            string idAsal = db.ds.Tables[0].Rows[0]["id_stasiun"].ToString();

            db.crud($"SELECT id_stasiun FROM stasiun WHERE nama_stasiun = '{tujuan}'");
            if (db.ds.Tables[0].Rows.Count == 0) { MessageBox.Show("Stasiun tujuan tidak ditemukan!"); return; }
            string idTujuan = db.ds.Tables[0].Rows[0]["id_stasiun"].ToString();

            if (db.eksekusi($"INSERT INTO rute (id_stasiun_asal, id_stasiun_tujuan) " +
                            $"VALUES ('{idAsal}', '{idTujuan}')"))
            {
                MessageBox.Show("Data rute berhasil disimpan!", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            string asal   = cbasal.Text;
            string tujuan = cbtujuan.Text;

            if (asal == "" || tujuan == "")
            {
                MessageBox.Show("Stasiun asal dan tujuan harus dipilih!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            db.crud($"SELECT id_stasiun FROM stasiun WHERE nama_stasiun = '{asal}'");
            if (db.ds.Tables[0].Rows.Count == 0) { MessageBox.Show("Stasiun asal tidak ditemukan!"); return; }
            string idAsal = db.ds.Tables[0].Rows[0]["id_stasiun"].ToString();

            db.crud($"SELECT id_stasiun FROM stasiun WHERE nama_stasiun = '{tujuan}'");
            if (db.ds.Tables[0].Rows.Count == 0) { MessageBox.Show("Stasiun tujuan tidak ditemukan!"); return; }
            string idTujuan = db.ds.Tables[0].Rows[0]["id_stasiun"].ToString();

            if (db.eksekusi($"UPDATE rute SET id_stasiun_asal='{idAsal}', id_stasiun_tujuan='{idTujuan}' " +
                            $"WHERE id_rute='{LBLID.Text}'"))
            {
                tampildata();
                bersih();
            }
        }

        private void tablerute_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            // Kolom 4 = Edit
            if (kolom == 4)
            {
                string id = tablerute.Rows[baris].Cells[0].Value.ToString();
                db.crud($"SELECT rute.id_rute, asal.nama_stasiun AS stasiun_asal, " +
                        $"tujuan.nama_stasiun AS stasiun_tujuan " +
                        $"FROM rute " +
                        $"INNER JOIN stasiun AS asal ON rute.id_stasiun_asal = asal.id_stasiun " +
                        $"INNER JOIN stasiun AS tujuan ON rute.id_stasiun_tujuan = tujuan.id_stasiun " +
                        $"WHERE rute.id_rute = '{id}'");

                if (db.ds.Tables[0].Rows.Count > 0)
                {
                    DataRow brs = db.ds.Tables[0].Rows[0];
                    LBLID.Text    = id;
                    cbasal.Text   = brs["stasiun_asal"].ToString();
                    cbtujuan.Text = brs["stasiun_tujuan"].ToString();
                }
            }

            // Kolom 5 = Hapus
            if (kolom == 5)
            {
                string id = tablerute.Rows[baris].Cells[0].Value.ToString();
                DialogResult konfirmasi = MessageBox.Show(
                    "Yakin ingin menghapus data rute ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (konfirmasi == DialogResult.Yes)
                {
                    if (db.eksekusi($"DELETE FROM rute WHERE id_rute = '{id}'"))
                    {
                        tampildata();
                        bersih();
                    }
                }
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            tablerute.Rows.Clear();
            string cari = txtsearch.Text;
            db.crud($"SELECT rute.id_rute, asal.nama_stasiun AS stasiun_asal, " +
                    $"tujuan.nama_stasiun AS stasiun_tujuan " +
                    $"FROM rute " +
                    $"INNER JOIN stasiun AS asal ON rute.id_stasiun_asal = asal.id_stasiun " +
                    $"INNER JOIN stasiun AS tujuan ON rute.id_stasiun_tujuan = tujuan.id_stasiun " +
                    $"WHERE asal.nama_stasiun LIKE '%{cari}%' OR tujuan.nama_stasiun LIKE '%{cari}%'");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                tablerute.Rows.Add(
                    baris["id_rute"].ToString(), no,
                    baris["stasiun_asal"].ToString(),
                    baris["stasiun_tujuan"].ToString()
                );
                no++;
            }
        }

        // Handler kosong yang dibutuhkan designer
        private void btntampil_Click(object sender, EventArgs e) { tampildata(); }
        private void tablerute_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
