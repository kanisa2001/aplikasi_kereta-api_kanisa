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

            db.crud($@"SELECT jadwal.idjadwal, kereta.nama_kereta,
               asal.nama_stasiun AS stasiun_asal,
               tujuan.nama_stasiun AS stasiun_tujuan,
               jadwal.tanggal, jadwal.jam_berangkat,
               jadwal.jam_tiba, jadwal.Harga
               FROM jadwal
               INNER JOIN kereta ON jadwal.id_kereta = kereta.idkereta
               INNER JOIN rute ON jadwal.id_rute = rute.id_rute
               INNER JOIN stasiun AS asal ON rute.id_stasiun_asal = asal.id_stasiun
               INNER JOIN stasiun AS tujuan ON rute.id_stasiun_tujuan = tujuan.id_stasiun");

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

                tablejadwal.Rows.Add(idj, nm, sa, st, tggl, jb, jt, h);
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
            string rute = cmbrute.Text;

            string tgglbrngkt = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string brngkt = txtberangkat.Text + ":00";
            string jamtb = txttiba.Text + ":00";
            string hrg = txtharga.Text;

            db.crud($@"SELECT idkereta FROM kereta
               WHERE nama_kereta = '{krt}'");

            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Kereta tidak ditemukan!");
                return;
            }

            string idkereta = db.ds.Tables[0].Rows[0]["idkereta"].ToString();

            string idrute = rute.Split('-')[0].Trim();

            db.crud($@"SELECT id_rute FROM rute
               WHERE id_rute = '{idrute}'");

            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Rute tidak ditemukan!");
                return;
            }

            db.crud($@"INSERT INTO jadwal
               (idjadwal, id_kereta, id_rute, tanggal, jam_berangkat, jam_tiba, Harga)
               VALUES
               (null, '{idkereta}', '{idrute}', '{tgglbrngkt}', '{brngkt}', '{jamtb}', '{hrg}')");

            MessageBox.Show("Data jadwal berhasil disimpan!",
                            "Informasi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            bersih();
            tampildata();
        }

        private void cmbkereta_SelectedIndexChanged(object sender, EventArgs e)
        {
            string krt = cmbkereta.Text;

            if (krt == "")
            {
                txtharga.Clear();
                txtkapasitas.Clear();
                return;
            }

            db.crud($@"SELECT harga, kapasitas
               FROM kereta
               WHERE nama_kereta = '{krt}'");

            if (db.ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = db.ds.Tables[0].Rows[0];

                txtharga.Text = row["harga"].ToString();
                txtkapasitas.Text = row["kapasitas"].ToString();
            }
            else
            {
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

                db.crud($@"SELECT jadwal.idjadwal,
               kereta.nama_kereta,
               rute.id_rute,
               asal.nama_stasiun AS stasiun_asal,
               tujuan.nama_stasiun AS stasiun_tujuan,
               jadwal.tanggal,
               jadwal.jam_berangkat,
               jadwal.jam_tiba,
               jadwal.Harga
               FROM jadwal
               INNER JOIN kereta
               ON jadwal.id_kereta = kereta.idkereta
               INNER JOIN rute
               ON jadwal.id_rute = rute.id_rute
               INNER JOIN stasiun AS asal
               ON rute.id_stasiun_asal = asal.id_stasiun
               INNER JOIN stasiun AS tujuan
               ON rute.id_stasiun_tujuan = tujuan.id_stasiun
               WHERE jadwal.idjadwal = '{idjadwal}'");

                if (db.ds.Tables[0].Rows.Count > 0)
                {
                    DataRow brs = db.ds.Tables[0].Rows[0];

                    label9.Text = brs["idjadwal"].ToString();

                    cmbkereta.Items.Clear();

                    db.crud($@"SELECT nama_kereta FROM kereta");

                    foreach (DataRow row in db.ds.Tables[0].Rows)
                    {
                        cmbkereta.Items.Add(row["nama_kereta"].ToString());
                    }

                    cmbkereta.Text = brs["nama_kereta"].ToString();

                    cmbrute.Items.Clear();

                    db.crud($@"SELECT rute.id_rute,
                   asal.nama_stasiun AS stasiun_asal,
                   tujuan.nama_stasiun AS stasiun_tujuan
                   FROM rute
                   INNER JOIN stasiun AS asal
                   ON rute.id_stasiun_asal = asal.id_stasiun
                   INNER JOIN stasiun AS tujuan
                   ON rute.id_stasiun_tujuan = tujuan.id_stasiun");

                    foreach (DataRow row in db.ds.Tables[0].Rows)
                    {
                        cmbrute.Items.Add(
                            row["id_rute"].ToString() + " - " +
                            row["stasiun_asal"].ToString() + " - " +
                            row["stasiun_tujuan"].ToString()
                        );
                    }

                    string idrute = brs["id_rute"].ToString();
                    string asal = brs["stasiun_asal"].ToString();
                    string tujuan = brs["stasiun_tujuan"].ToString();

                    cmbrute.Text = idrute + " - " + asal + " - " + tujuan;

                    txtstaw.Text = asal;
                    txtsttuj.Text = tujuan;

                    dateTimePicker1.Value = Convert.ToDateTime(brs["tanggal"]);

                    txtberangkat.Text = brs["jam_berangkat"].ToString();
                    txttiba.Text = brs["jam_tiba"].ToString();
                    txtharga.Text = brs["Harga"].ToString();
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
            string rute = cmbrute.Text;

            string tgglbrngkt = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string brngkt = txtberangkat.Text + ":00";
            string jamtb = txttiba.Text + ":00";
            string hrg = txtharga.Text;

            db.crud($@"SELECT idkereta FROM kereta
               WHERE nama_kereta = '{krt}'");

            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Kereta tidak ditemukan!");
                return;
            }

            string idkereta = db.ds.Tables[0].Rows[0]["idkereta"].ToString();

            string idrute = rute.Split('-')[0].Trim();

            db.crud($@"SELECT id_rute FROM rute
               WHERE id_rute = '{idrute}'");

            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Rute tidak ditemukan!");
                return;
            }

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
            label9.Text = "";
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

            db.crud($@"SELECT idkereta FROM kereta 
               WHERE nama_kereta = '{krt}'");

            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Kereta tidak ditemukan!");
                return;
            }

            string idkereta = db.ds.Tables[0].Rows[0]["idkereta"].ToString();

            db.crud($@"SELECT rute.id_rute
               FROM rute
               INNER JOIN stasiun AS asal
               ON rute.id_stasiun_asal = asal.id_stasiun
               INNER JOIN stasiun AS tujuan
               ON rute.id_stasiun_tujuan = tujuan.id_stasiun
               WHERE rute.id_kereta = '{idkereta}'
               AND asal.nama_stasiun = '{staw}'
               AND tujuan.nama_stasiun = '{sttuj}'");

            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Rute tidak ditemukan!");
                return;
            }

            string idrute = db.ds.Tables[0].Rows[0]["id_rute"].ToString();

            db.crud($@"UPDATE jadwal SET
               id_kereta = '{idkereta}',
               id_rute = '{idrute}',
               tanggal = '{tgglbrngkt}',
               jam_berangkat = '{brngkt}',
               jam_tiba = '{jamtb}',
               Harga = '{hrg}'
               WHERE idjadwal = '{label9.Text}'");

            tampildata();
            bersih();
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbrute_DropDown(object sender, EventArgs e)
        {
            cmbrute.Items.Clear();

            db.crud($@"SELECT rute.id_rute,
               asal.nama_stasiun AS stasiun_asal,
               tujuan.nama_stasiun AS stasiun_tujuan
               FROM rute
               INNER JOIN stasiun AS asal
               ON rute.id_stasiun_asal = asal.id_stasiun
               INNER JOIN stasiun AS tujuan
               ON rute.id_stasiun_tujuan = tujuan.id_stasiun");

            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                cmbrute.Items.Add(
                    row["id_rute"].ToString() + " - " +
                    row["stasiun_asal"].ToString() + " - " +
                    row["stasiun_tujuan"].ToString()
                );
            }
        }

        private void cmbrute_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rute = cmbrute.Text;

            if (rute == "")
            {
                txtstaw.Clear();
                txtsttuj.Clear();
                return;
            }

            string idrute = rute.Split('-')[0].Trim();

            db.crud($@"SELECT asal.nama_stasiun AS stasiun_asal,
               tujuan.nama_stasiun AS stasiun_tujuan
               FROM rute
               INNER JOIN stasiun AS asal
               ON rute.id_stasiun_asal = asal.id_stasiun
               INNER JOIN stasiun AS tujuan
               ON rute.id_stasiun_tujuan = tujuan.id_stasiun
               WHERE rute.id_rute = '{idrute}'");

            if (db.ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = db.ds.Tables[0].Rows[0];

                txtstaw.Text = row["stasiun_asal"].ToString();
                txtsttuj.Text = row["stasiun_tujuan"].ToString();
            }
        }
    }
    
}
