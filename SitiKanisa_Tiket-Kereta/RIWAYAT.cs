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
    public partial class RIWAYAT : Form
    {
        public RIWAYAT()
        {
            InitializeComponent();
        }

        private void RIWAYAT_Load(object sender, EventArgs e)
        {
            tampildata();
        }

        public void tampildata()
        {
            dgvriwayat.Rows.Clear();

            db.crud($@"SELECT pemesanan.id_pemesanan,
               pemesanan.nama_pemesan,
               pemesanan.no_identitas,
               pemesanan.no_telp,
               asal.nama_stasiun AS stasiun_asal,
               tujuan.nama_stasiun AS stasiun_tujuan,
               jadwal.tanggal,
               pemesanan.harga,
               pemesanan.jumlah_tiket
               FROM pemesanan
               INNER JOIN jadwal
               ON pemesanan.id_jadwal = jadwal.idjadwal
               INNER JOIN rute
               ON jadwal.id_rute = rute.id_rute
               INNER JOIN stasiun AS asal
               ON rute.id_stasiun_asal = asal.id_stasiun
               INNER JOIN stasiun AS tujuan
               ON rute.id_stasiun_tujuan = tujuan.id_stasiun");

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                string idp = "" + baris["id_pemesanan"];
                string nm = "" + baris["nama_pemesan"];
                string noid = "" + baris["no_identitas"];
                string notlp = "" + baris["no_telp"];
                string sawal = "" + baris["stasiun_asal"];
                string stujuan = "" + baris["stasiun_tujuan"];
                string tgl = "" + baris["tanggal"];
                string hrg = "" + baris["harga"];
                string tkt = "" + baris["jumlah_tiket"];

                dgvriwayat.Rows.Add(idp, nm, noid, notlp, sawal, stujuan, tgl, hrg, tkt, "Print");
            }
        }
    }
}
