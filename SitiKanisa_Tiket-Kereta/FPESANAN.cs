using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace SitiKanisa_Tiket_Kereta
{
    public partial class FPESANAN : Form
    {
        public FPESANAN()
        {
            InitializeComponent();

            cmbrute.SelectedIndexChanged += cmbrute_SelectedIndexChanged;
            txttiket.TextChanged += txttiket_TextChanged;
        }

        public void bersih()
        {
            txtnama.Clear();
            txtidentitas.Clear();
            txttelfon.Clear();
            txtharga.Clear();
            txttiket.Clear();
            txttotal.Clear();
            txttersedia.Clear();

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

        private void cmbrute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbrute.SelectedIndex >= 0)
            {
                carijadwal();
            }
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

            int jumlah;

            if (!int.TryParse(tkt, out jumlah))
            {
                MessageBox.Show("Jumlah tiket tidak valid!");
                return;
            }

            if (jumlah <= 0)
            {
                MessageBox.Show("Jumlah tiket harus lebih dari 0!");
                return;
            }

            db.crud($@"SELECT kereta.kapasitas,
                       COALESCE(SUM(pemesanan.jumlah_tiket), 0) AS terpakai
                       FROM jadwal
                       INNER JOIN kereta
                       ON jadwal.id_kereta = kereta.idkereta
                       LEFT JOIN pemesanan
                       ON jadwal.idjadwal = pemesanan.id_jadwal
                       WHERE jadwal.idjadwal = '{idjadwal}'
                       GROUP BY kereta.kapasitas");

            if (db.ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = db.ds.Tables[0].Rows[0];

                int kapasitas = Convert.ToInt32(row["kapasitas"]);
                int terpakai = Convert.ToInt32(row["terpakai"]);
                int tersedia = kapasitas - terpakai;

                if (jumlah > tersedia)
                {
                    MessageBox.Show("Tiket yang tersedia hanya " + tersedia + "!");
                    hitungTersedia();
                    return;
                }
            }

            db.crud($@"INSERT INTO pemesanan
                       (id_pemesanan, id_jadwal, nama_pemesan, no_identitas, no_telp,
                       jumlah_tiket, metode_bayar, tanggal_pemesanan, harga, total_harga)
                       VALUES
                       (NULL, '{idjadwal}', '{nm}', '{noid}', '{notelp}',
                       '{tkt}', '{byr}', NOW(), '{hrg}', '{ttl}')");

            MessageBox.Show(
                "Pemesanan berhasil disimpan!",
                "Informasi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            hitungTersedia();
        }

        private void txtnama_TextChanged(object sender, EventArgs e)
        {
            if (txtnama.Text == "")
            {
                txtidentitas.Clear();
                txttelfon.Clear();
                return;
            }

            db.crud($@"SELECT * FROM penumpang
                       WHERE nama = '{txtnama.Text}'");

            if (db.ds.Tables[0].Rows.Count > 0)
            {
                DataRow baris = db.ds.Tables[0].Rows[0];

                txtidentitas.Text = baris["idpenumpang"].ToString();
                txttelfon.Text = baris["no_hp"].ToString();
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

            db.crud($@"SELECT * FROM penumpang");

            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                nama.Add(row["nama"].ToString());
            }

            txtnama.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtnama.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtnama.AutoCompleteCustomSource = nama;

            cmbrute.Items.Clear();

            db.crud($@"SELECT rute.id_rute,
                       asal.nama_stasiun AS stasiun_asal,
                       tujuan.nama_stasiun AS stasiun_tujuan
                       FROM rute
                       INNER JOIN stasiun AS asal
                       ON rute.id_stasiun_asal = asal.id_stasiun
                       INNER JOIN stasiun AS tujuan
                       ON rute.id_stasiun_tujuan = tujuan.id_stasiun
                       ORDER BY rute.id_rute");

            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                string rute = $"{row["id_rute"]} - {row["stasiun_asal"]} - {row["stasiun_tujuan"]}";
                cmbrute.Items.Add(rute);
            }

            cmbrute.SelectedIndex = -1;
            txtkereta.Clear();
            txtkelas.Clear();
            txtharga.Clear();
            txttersedia.Clear();
            LBLIDJ.Text = "";
        }

        private void txttiket_TextChanged(object sender, EventArgs e)
        {
            if (txttiket.Text == "")
            {
                txttotal.Clear();
                return;
            }

            int harga;
            int tiket;
            int tersedia;

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

            if (!int.TryParse(txttersedia.Text, out tersedia))
            {
                txttotal.Clear();
                return;
            }

            if (tiket > tersedia)
            {
                MessageBox.Show("Jumlah tiket melebihi tiket yang tersedia!");

                txttiket.Clear();
                txttotal.Clear();
                return;
            }

            if (tiket <= 0)
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
            string rute = cmbrute.Text;

            if (rute == "")
            {
                LBLIDJ.Text = "";
                txttgl.Value = DateTime.Now;
                txtkereta.Clear();
                txtkelas.Clear();
                txtharga.Clear();
                txttersedia.Clear();
                return;
            }

            string idrute = rute.Split('-')[0].Trim();

            db.crud($@"SELECT jadwal.idjadwal,
                       jadwal.tanggal,
                       jadwal.Harga,
                       jadwal.jam_berangkat,
                       kereta.nama_kereta,
                       jenis_kereta.nama_jenis
                       FROM jadwal
                       INNER JOIN kereta
                       ON jadwal.id_kereta = kereta.idkereta
                       INNER JOIN jenis_kereta
                       ON kereta.id_jenis_kereta = jenis_kereta.id_jenis_kereta
                       WHERE jadwal.id_rute = '{idrute}'
                       AND jadwal.tanggal >= CURDATE()
                       ORDER BY jadwal.tanggal, jadwal.jam_berangkat
                       LIMIT 1");

            if (db.ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = db.ds.Tables[0].Rows[0];

                string idjadwal = row["idjadwal"].ToString();
                string tanggal = row["tanggal"].ToString();
                string kereta = row["nama_kereta"].ToString();
                string kelas = row["nama_jenis"].ToString();
                string harga = row["Harga"].ToString();

                LBLIDJ.Text = idjadwal;

                txttgl.Value = Convert.ToDateTime(tanggal);

                txtkereta.Text = kereta;

                txtkelas.Text = kelas;

                txtharga.Text = harga;

                hitungTersedia();
            }
            else
            {
                LBLIDJ.Text = "";
                txtkereta.Clear();
                txtkelas.Clear();
                txtharga.Clear();
                txttersedia.Clear();

                MessageBox.Show("Jadwal untuk rute tersebut tidak ditemukan!");
            }
        }

        private void txttgl_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void ptnprint_Click(object sender, EventArgs e)
        {
            if (LBLIDJ.Text == "")
            {
                MessageBox.Show("Belum ada tiket yang dapat dicetak!");
                return;
            }

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font judul = new Font("Arial", 18, FontStyle.Bold);
            Font subjudul = new Font("Arial", 12, FontStyle.Bold);
            Font isi = new Font("Arial", 10);
            Font kecil = new Font("Arial", 9);

            int x = 50;
            int y = 40;

            e.Graphics.DrawString("TIKET KERETA API", judul, Brushes.Black, x, y);
            y += 35;

            e.Graphics.DrawString("BUKTI PEMESANAN TIKET", subjudul, Brushes.Black, x, y);
            y += 35;

            e.Graphics.DrawString("==========================================", isi, Brushes.Black, x, y);
            y += 30;

            e.Graphics.DrawString($"ID Jadwal       : {LBLIDJ.Text}", isi, Brushes.Black, x, y);
            y += 25;

            e.Graphics.DrawString($"Nama Penumpang  : {txtnama.Text}", isi, Brushes.Black, x, y);
            y += 25;

            e.Graphics.DrawString($"No. Identitas   : {txtidentitas.Text}", isi, Brushes.Black, x, y);
            y += 25;

            e.Graphics.DrawString($"No. Telepon     : {txttelfon.Text}", isi, Brushes.Black, x, y);
            y += 30;

            e.Graphics.DrawString("DETAIL PERJALANAN", subjudul, Brushes.Black, x, y);
            y += 30;

            e.Graphics.DrawString($"Rute            : {cmbrute.Text}", isi, Brushes.Black, x, y);
            y += 25;

            e.Graphics.DrawString($"Kereta          : {txtkereta.Text}", isi, Brushes.Black, x, y);
            y += 25;

            e.Graphics.DrawString($"Kelas           : {txtkelas.Text}", isi, Brushes.Black, x, y);
            y += 25;

            e.Graphics.DrawString($"Tanggal Berangkat : {txttgl.Text}", isi, Brushes.Black, x, y);
            y += 30;

            e.Graphics.DrawString("PEMBAYARAN", subjudul, Brushes.Black, x, y);
            y += 30;

            e.Graphics.DrawString($"Harga Tiket     : Rp {txtharga.Text}", isi, Brushes.Black, x, y);
            y += 25;

            e.Graphics.DrawString($"Jumlah Tiket    : {txttiket.Text}", isi, Brushes.Black, x, y);
            y += 25;

            e.Graphics.DrawString($"Total Harga     : Rp {txttotal.Text}", isi, Brushes.Black, x, y);
            y += 25;

            e.Graphics.DrawString($"Metode Bayar    : {cmbbayar.Text}", isi, Brushes.Black, x, y);
            y += 35;

            e.Graphics.DrawString("==========================================", isi, Brushes.Black, x, y);
            y += 30;

            e.Graphics.DrawString(
                "Terima kasih telah melakukan pemesanan.",
                kecil,
                Brushes.Black,
                x,
                y);

            y += 20;

            e.Graphics.DrawString(
                "Harap datang sesuai tanggal keberangkatan.",
                kecil,
                Brushes.Black,
                x,
                y);
        }

        public void hitungTersedia()
        {
            if (LBLIDJ.Text == "")
            {
                txttersedia.Clear();
                return;
            }

            db.crud($@"SELECT kereta.kapasitas,
                       COALESCE(SUM(pemesanan.jumlah_tiket), 0) AS terpakai
                       FROM jadwal
                       INNER JOIN kereta
                       ON jadwal.id_kereta = kereta.idkereta
                       LEFT JOIN pemesanan
                       ON jadwal.idjadwal = pemesanan.id_jadwal
                       WHERE jadwal.idjadwal = '{LBLIDJ.Text}'
                       GROUP BY kereta.kapasitas");

            if (db.ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = db.ds.Tables[0].Rows[0];

                int kapasitas = Convert.ToInt32(row["kapasitas"]);
                int terpakai = Convert.ToInt32(row["terpakai"]);
                int tersedia = kapasitas - terpakai;

                if (tersedia < 0)
                {
                    tersedia = 0;
                }

                txttersedia.Text = tersedia.ToString();
            }
            else
            {
                txttersedia.Clear();
            }
        }
    }
}