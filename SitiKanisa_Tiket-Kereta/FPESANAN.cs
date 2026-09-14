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
    public partial class FPESANAN : Form
    {
        public FPESANAN()
        {
            InitializeComponent();
        }

        public void bersih()
        {
            txtnama.Clear();
            txtidentitas.Clear();
            txttelfon.Clear();
            txtharga.Clear();
            txttiket.Clear();
            txttotal.Clear();
            cmbbayar.Text = "";
            cmbrute.Text = "";
            txtkereta.Text = "";
            txtkelas.Text = "";
            LBLIDJ.Text = "";
        }

        public void tampildata()
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            carijadwal();
        }

        private void btnpesan_Click(object sender, EventArgs e)
        {
            string nm = txtnama.Text;
            string noid = txtidentitas.Text;
            string notelp = txttelfon.Text;
            string tujuan = cmbrute.Text;
            string krt = txtkereta.Text;
            string byr = cmbbayar.Text;
            string hrg = txtharga.Text;
            string tkt = txttiket.Text;
            string ttl = txttotal.Text;

            if (nm == "" || noid == "" || notelp == "" || tujuan == "" ||
                krt == "" || byr == "" || hrg == "" || tkt == "" || ttl == "")
            {
                MessageBox.Show("Data masih belum lengkap!");
                return;
            }

            string idjadwal = LBLIDJ.Text;

            if (idjadwal == "")
            {
                MessageBox.Show("Jadwal tidak ditemukan!");
                return;
            }

            db.crud($@"INSERT INTO pemesanan
            (id_pemesanan, id_jadwal, nama_pemesan, no_identitas, no_telp, no_kursi, jumlah_tiket, metode_bayar, tanggal_pemesanan, harga, total_harga)
            VALUES
            (NULL, '{idjadwal}', '{nm}', '{noid}', '{notelp}', '-', '{tkt}', '{byr}', NOW(), '{hrg}', '{ttl}')");

            MessageBox.Show("Pemesanan berhasil disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            bersih();
        }

        private void txtnama_TextChanged(object sender, EventArgs e)
        {
            if (txtnama.Text == "")
            {
                txtidentitas.Clear();
                txttelfon.Clear();
                return;
            }

            db.crud($"SELECT * FROM penumpang WHERE nama = '{txtnama.Text}'");

            if (db.ds.Tables[0].Rows.Count > 0)
            {
                DataRow baris = db.ds.Tables[0].Rows[0];

                string idi = baris["idpenumpang"].ToString();
                string telp = baris["no_hp"].ToString();

                txtidentitas.Text = idi;
                txttelfon.Text = telp;
            }
            else
            {
                txtidentitas.Clear();
                txttelfon.Clear();
            }
        }

        private void FPESANAN_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection nama = new AutoCompleteStringCollection();

            db.crud("SELECT * FROM penumpang");

            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                nama.Add(row["nama"].ToString());
            }

            txtnama.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtnama.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtnama.AutoCompleteCustomSource = nama;

            cmbrute.Items.Clear();

            db.crud("SELECT DISTINCT stasiun_tujuan FROM rute");

            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                cmbrute.Items.Add(row["stasiun_tujuan"].ToString());
            }

            carijadwal();
        }

        private void txttiket_TextChanged(object sender, EventArgs e)
        {
            if (txttiket.Text == "")
            {
                txttotal.Clear();
                return;
            }

            if (txtharga.Text == "")
            {
                txttotal.Clear();
                return;
            }

            int harga;
            int tiket;

            if (!int.TryParse(txtharga.Text, out harga))
            {
                txttotal.Clear();
                return;
            }

            if (!int.TryParse(txttiket.Text, out tiket))
            {
                txttotal.Clear();
                return;
            }

            int total = harga * tiket;

            txttotal.Text = total.ToString();
        }

        private void cmbbayar_DropDown(object sender, EventArgs e)
        {
            cmbbayar.Items.Clear();

            cmbbayar.Items.Add("Transfer");
            cmbbayar.Items.Add("Gopay");
            cmbbayar.Items.Add("Dana");
        }

        public void carijadwal()
        {
            string tujuan = cmbrute.Text;

            if (tujuan == "")
            {
                LBLIDJ.Text = "";
                txtkereta.Text = "";
                txtkelas.Text = "";
                txtharga.Clear();
                return;
            }

            db.crud($@"SELECT jadwal.idjadwal,
           kereta.nama_kereta,
           jenis_kereta.nama_jenis,
           jadwal.tanggal,
           kereta.harga
           FROM jadwal
           INNER JOIN kereta ON jadwal.id_kereta = kereta.idkereta
           INNER JOIN jenis_kereta ON kereta.id_jenis_kereta = jenis_kereta.id_jenis_kereta
           INNER JOIN rute ON jadwal.id_rute = rute.id_rute
           WHERE rute.stasiun_tujuan = '{tujuan}';");

            if (db.ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = db.ds.Tables[0].Rows[0];

                LBLIDJ.Text = row["idjadwal"].ToString();
                txtkereta.Text = row["nama_kereta"].ToString();
                txttgl.Text = row["tanggal"].ToString();
                txtkelas.Text = row["nama_jenis"].ToString();
                txtharga.Text = row["harga"].ToString();
            }
            else
            {
                LBLIDJ.Text = "";
                txtkereta.Text = "";
                txtkelas.Text = "";
                txtharga.Clear();

                MessageBox.Show("Data rute tidak ditemukan!");
            }
        }

        private void txttgl_ValueChanged(object sender, EventArgs e)
        {
            carijadwal();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}