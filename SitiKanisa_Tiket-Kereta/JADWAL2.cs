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
    public partial class JADWAL2 : Form
    {
        public JADWAL2()
        {
            InitializeComponent();
        }

        private void JADWAL2_Load(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

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
            }
        }

    }
}
