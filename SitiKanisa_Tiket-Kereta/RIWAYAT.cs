using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace SitiKanisa_Tiket_Kereta
{
    public partial class RIWAYAT : Form
    {
        // Simpan data baris yang dipilih untuk cetak
        private DataGridViewRow _barisUntukCetak = null;

        public RIWAYAT()
        {
            InitializeComponent();
        }

        private void RIWAYAT_Load(object sender, EventArgs e)
        {
            dgvriwayat.DataError += (s, ex) => { ex.Cancel = true; };
            tampildata();
        }

        public void tampildata()
        {
            dgvriwayat.Rows.Clear();
            db.crud("SELECT pemesanan.id_pemesanan, pemesanan.nama_pemesan, " +
                    "pemesanan.no_identitas, pemesanan.no_telp, " +
                    "asal.nama_stasiun AS stasiun_asal, tujuan.nama_stasiun AS stasiun_tujuan, " +
                    "jadwal.tanggal, pemesanan.harga, pemesanan.jumlah_tiket " +
                    "FROM pemesanan " +
                    "INNER JOIN jadwal ON pemesanan.id_jadwal = jadwal.idjadwal " +
                    "INNER JOIN rute ON jadwal.id_rute = rute.id_rute " +
                    "INNER JOIN stasiun AS asal ON rute.id_stasiun_asal = asal.id_stasiun " +
                    "INNER JOIN stasiun AS tujuan ON rute.id_stasiun_tujuan = tujuan.id_stasiun " +
                    "ORDER BY pemesanan.id_pemesanan DESC");

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                dgvriwayat.Rows.Add(
                    baris["id_pemesanan"].ToString(),
                    baris["nama_pemesan"].ToString(),
                    baris["no_identitas"].ToString(),
                    baris["no_telp"].ToString(),
                    baris["stasiun_asal"].ToString(),
                    baris["stasiun_tujuan"].ToString(),
                    baris["tanggal"].ToString(),
                    baris["harga"].ToString(),
                    baris["jumlah_tiket"].ToString(),
                    "Print"
                );
            }
        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            dgvriwayat.Rows.Clear();
            string cari = txtcari.Text;
            db.crud($"SELECT pemesanan.id_pemesanan, pemesanan.nama_pemesan, " +
                    $"pemesanan.no_identitas, pemesanan.no_telp, " +
                    $"asal.nama_stasiun AS stasiun_asal, tujuan.nama_stasiun AS stasiun_tujuan, " +
                    $"jadwal.tanggal, pemesanan.harga, pemesanan.jumlah_tiket " +
                    $"FROM pemesanan " +
                    $"INNER JOIN jadwal ON pemesanan.id_jadwal = jadwal.idjadwal " +
                    $"INNER JOIN rute ON jadwal.id_rute = rute.id_rute " +
                    $"INNER JOIN stasiun AS asal ON rute.id_stasiun_asal = asal.id_stasiun " +
                    $"INNER JOIN stasiun AS tujuan ON rute.id_stasiun_tujuan = tujuan.id_stasiun " +
                    $"WHERE pemesanan.nama_pemesan LIKE '%{cari}%' " +
                    $"OR pemesanan.no_identitas LIKE '%{cari}%' " +
                    $"OR asal.nama_stasiun LIKE '%{cari}%' " +
                    $"OR tujuan.nama_stasiun LIKE '%{cari}%'");

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                dgvriwayat.Rows.Add(
                    baris["id_pemesanan"].ToString(),
                    baris["nama_pemesan"].ToString(),
                    baris["no_identitas"].ToString(),
                    baris["no_telp"].ToString(),
                    baris["stasiun_asal"].ToString(),
                    baris["stasiun_tujuan"].ToString(),
                    baris["tanggal"].ToString(),
                    baris["harga"].ToString(),
                    baris["jumlah_tiket"].ToString(),
                    "Print"
                );
            }
        }

        // Handle klik kolom Print
        private void dgvriwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            // Kolom 9 = Print
            if (e.ColumnIndex == 9)
            {
                _barisUntukCetak = dgvriwayat.Rows[e.RowIndex];
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_barisUntukCetak == null) return;

            Font judul  = new Font("Arial", 14, FontStyle.Bold);
            Font sub    = new Font("Arial", 10, FontStyle.Bold);
            Font isi    = new Font("Arial", 10);

            int x = 50, y = 40;

            e.Graphics.DrawString("BUKTI PEMESANAN TIKET KERETA API", judul, Brushes.Black, x, y); y += 35;
            e.Graphics.DrawString("==========================================", isi, Brushes.Black, x, y); y += 25;

            e.Graphics.DrawString("ID Pemesanan   : " + _barisUntukCetak.Cells[0].Value, isi, Brushes.Black, x, y); y += 22;
            e.Graphics.DrawString("Nama           : " + _barisUntukCetak.Cells[1].Value, isi, Brushes.Black, x, y); y += 22;
            e.Graphics.DrawString("No. Identitas  : " + _barisUntukCetak.Cells[2].Value, isi, Brushes.Black, x, y); y += 22;
            e.Graphics.DrawString("No. Telepon    : " + _barisUntukCetak.Cells[3].Value, isi, Brushes.Black, x, y); y += 30;

            e.Graphics.DrawString("DETAIL PERJALANAN", sub, Brushes.Black, x, y); y += 25;
            e.Graphics.DrawString("Stasiun Asal   : " + _barisUntukCetak.Cells[4].Value, isi, Brushes.Black, x, y); y += 22;
            e.Graphics.DrawString("Stasiun Tujuan : " + _barisUntukCetak.Cells[5].Value, isi, Brushes.Black, x, y); y += 22;
            e.Graphics.DrawString("Tanggal        : " + _barisUntukCetak.Cells[6].Value, isi, Brushes.Black, x, y); y += 30;

            e.Graphics.DrawString("PEMBAYARAN", sub, Brushes.Black, x, y); y += 25;
            e.Graphics.DrawString("Harga Tiket    : Rp " + _barisUntukCetak.Cells[7].Value, isi, Brushes.Black, x, y); y += 22;
            e.Graphics.DrawString("Jumlah Tiket   : " + _barisUntukCetak.Cells[8].Value, isi, Brushes.Black, x, y); y += 30;

            e.Graphics.DrawString("==========================================", isi, Brushes.Black, x, y); y += 25;
            e.Graphics.DrawString("Terima kasih telah menggunakan layanan kami.", isi, Brushes.Black, x, y);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }
    }
}
