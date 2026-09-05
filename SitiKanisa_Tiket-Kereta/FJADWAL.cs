using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SitiKanisa_Tiket_Kereta
{
    public partial class FJADWAL : Form
    {
        public FJADWAL()
        {
            InitializeComponent();
        }

        public void tampildata()
        {
            tablejadwal.Rows.Clear();
            db.crud(@"SELECT jadwal.idjadwal, jadwal.id_kereta, kereta.nama_kereta,
                     rute.stasiun_asal, rute.stasiun_tujuan,
                     jadwal.tanggal, jadwal.jam_berangkat,
                     jadwal.jam_tiba, jadwal.Harga
              FROM jadwal
              INNER JOIN kereta ON kereta.idkereta = jadwal.id_kereta
              INNER JOIN rute ON rute.id_rute = jadwal.id_rute");

            foreach (DataRow item in db.ds.Tables[0].Rows)
            {
                string idj = item["idjadwal"].ToString();
                string nm = item["nama_kereta"].ToString();
                string sa = item["stasiun_asal"].ToString();
                string st = item["stasiun_tujuan"].ToString();
                string tggl = item["tanggal"].ToString();
                string jb = item["jam_berangkat"].ToString();
                string jt = item["jam_tiba"].ToString();
                string h = item["Harga"].ToString();
                tablejadwal.Rows.Add(idj, nm, sa, st, tggl, jb, jt, h); ;
            }
        }

        public void bersih()
        {
            cmbkereta.Text = "";
            txtstaw.Clear();
            txtsttuj.Clear();
            txttiba.Clear();
            txtberangkat.Clear();
            txtharga.Clear();
            txtkapasitas.Clear();
        }

        private void cmbkereta_DropDown(object sender, EventArgs e)
        {
            cmbkereta.Items.Clear();
            db.crud($"SELECT nama_kereta FROM kereta");
            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                cmbkereta.Items.Add(row["nama_kereta"].ToString());
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            string krt = cmbkereta.Text;
            string staw = txtstaw.Text;
            string sttuj = txtsttuj.Text;

            string tgglbrngkt = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string brngkt = txtberangkat.Text + ":00";
            string jamtb = txttiba.Text + ":00";

            string hrg = txtharga.Text;

            db.crud($"SELECT idkereta FROM kereta WHERE nama_kereta = '{krt}'");

            if (db.ds.Tables[0].Rows.Count > 0)
            {
                string idkereta = db.ds.Tables[0].Rows[0]["idkereta"].ToString();

                // Mengambil id rute berdasarkan stasiun asal dan tujuan
                db.crud($@"SELECT id_rute 
                   FROM rute 
                   WHERE stasiun_asal = '{staw}' 
                   AND stasiun_tujuan = '{sttuj}'");
                if (db.ds.Tables[0].Rows.Count > 0)
                {
                    string idrute = db.ds.Tables[0].Rows[0]["id_rute"].ToString();

                    // Simpan jadwal
                    db.crud($@"INSERT INTO jadwal
                       (idjadwal, id_kereta, id_rute, tanggal, jam_berangkat, jam_tiba, Harga)
                       VALUES
                       (null, '{idkereta}', '{idrute}', '{tgglbrngkt}', '{brngkt}', '{jamtb}','{hrg}')");

                    MessageBox.Show("Data jadwal berhasil disimpan!",
                                    "Informasi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    bersih();
                    tampildata();

                }
                else
                {
                    MessageBox.Show("Rute tidak ditemukan!",
                                    "Peringatan",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Kereta tidak ditemukan!",
                                "Peringatan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void cmbkereta_SelectedIndexChanged(object sender, EventArgs e)
        {
            string krt = cmbkereta.Text;

            if (krt == "")
            {
                txtstaw.Clear();
                txtsttuj.Clear();
                txtharga.Clear();
                txtkapasitas.Clear();
                return;
            }

            db.crud($@"SELECT rute.stasiun_asal, rute.stasiun_tujuan, kereta.harga, kereta.kapasitas FROM rute INNER JOIN kereta ON rute.id_kereta = kereta.idkereta WHERE kereta.nama_kereta = '{krt}'");

            if (db.ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = db.ds.Tables[0].Rows[0];

                txtstaw.Text = row["stasiun_asal"].ToString();
                txtsttuj.Text = row["stasiun_tujuan"].ToString();
                txtharga.Text = row["harga"].ToString();
                txtkapasitas.Text = row["kapasitas"].ToString();
            }
            else
            {
                txtstaw.Clear();
                txtsttuj.Clear();
                txtharga.Clear();
                txtkapasitas.Clear();
            }
        }

        private void FJADWAL_Load(object sender, EventArgs e)
        {
            tampildata();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void tablejadwal_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        
                int baris = e.RowIndex;
                int kolom = e.ColumnIndex;

            if (kolom == 8)
            {
                string idjadwal = tablejadwal.Rows[baris].Cells[0].Value.ToString();

                db.crud($@"SELECT jadwal.idjadwal, kereta.nama_kereta,
                      rute.stasiun_asal, rute.stasiun_tujuan,
                      jadwal.tanggal, jadwal.jam_berangkat,
                      jadwal.jam_tiba, jadwal.Harga
               FROM jadwal
               INNER JOIN kereta ON kereta.idkereta = jadwal.id_kereta
               INNER JOIN rute ON rute.id_rute = jadwal.id_rute
               WHERE jadwal.idjadwal = '{idjadwal}'");

                foreach (DataRow brs in db.ds.Tables[0].Rows)
                {
                    label9.Text = idjadwal;

                    cmbkereta.Text = "" + brs["nama_kereta"];
                    txtstaw.Text = "" + brs["stasiun_asal"];
                    txtsttuj.Text = "" + brs["stasiun_tujuan"];
                    dateTimePicker1.Text = "" + brs["tanggal"];
                    txtberangkat.Text = "" + brs["jam_berangkat"];
                    txttiba.Text = "" + brs["jam_tiba"];
                    txtharga.Text = "" + brs["Harga"];
                }
            }

            if (kolom == 9)
            {
                string id = tablejadwal.Rows[baris].Cells[0].Value.ToString();

                DialogResult hasil = MessageBox.Show(
                    "Yakin mau hapus data ini?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (hasil == DialogResult.Yes)
                {
                    db.crud($"DELETE FROM jadwal WHERE idjadwal = '{id}'");

                    tampildata();
                    bersih();
                }
            }
        }

        private void txtubah_Click(object sender, EventArgs e)
        {
            string krt = cmbkereta.Text;
            string staw = txtstaw.Text;
            string sttuj = txtsttuj.Text;
            string tgglbrngkt = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string brngkt = txtberangkat.Text + ":00";
            string jamtb = txttiba.Text + ":00";
            string hrg = txtharga.Text;

            // Cari ID kereta
            db.crud($"SELECT idkereta FROM kereta WHERE nama_kereta = '{krt}'");

            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Kereta tidak ditemukan!");
                return;
            }

            string idkereta = db.ds.Tables[0].Rows[0]["idkereta"].ToString();

            // Cari ID rute
            db.crud($"SELECT id_rute FROM rute WHERE stasiun_asal = '{staw}' AND stasiun_tujuan = '{sttuj}'");

            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Rute tidak ditemukan!");
                return;
            }

            string idrute = db.ds.Tables[0].Rows[0]["id_rute"].ToString();

            // Update data jadwal
            db.crud($@"UPDATE jadwal SET 
                id_kereta = '{idkereta}',
                id_rute = '{idrute}',
                tanggal = '{tgglbrngkt}',
                jam_berangkat = '{brngkt}',
                jam_tiba = '{jamtb}',
                Harga = '{hrg}'
                WHERE idjadwal = '{label9.Text}'");

            MessageBox.Show("Data jadwal berhasil diubah!",
                            "Informasi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            tampildata();
            bersih();
        }

        private void txttampildata_Click(object sender, EventArgs e)
        {
            string krt = cmbkereta.Text;
            string staw = txtstaw.Text;
            string sttuj = txtsttuj.Text;
            string tgglbrngkt = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string brngkt = txtberangkat.Text + ":00";
            string jamtb = txttiba.Text + ":00";
            string hrg = txtharga.Text;

            db.crud($"UPDATE jadwal SET id_kereta = (SELECT idkereta FROM kereta WHERE nama_kereta = '{krt}'), id_rute = (SELECT id_rute FROM rute WHERE stasiun_asal = '{staw}' AND stasiun_tujuan = '{sttuj}'), tanggal = '{tgglbrngkt}', jam_berangkat = '{brngkt}', jam_tiba = '{jamtb}', Harga = '{hrg}' WHERE idjadwal = {label9.Text}");

            tampildata();
            bersih();
        }
    }
    
}
