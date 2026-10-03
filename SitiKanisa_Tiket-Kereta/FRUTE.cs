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
    public partial class FRUTE : Form
    {
        public FRUTE()
        {
            InitializeComponent();
        }

        public void tampildata()
        {
            tablerute.Rows.Clear();

            db.crud($@"SELECT rute.id_rute,
               asal.nama_stasiun AS stasiun_asal,
               tujuan.nama_stasiun AS stasiun_tujuan
               FROM rute
               INNER JOIN stasiun AS asal ON rute.id_stasiun_asal = asal.id_stasiun
               INNER JOIN stasiun AS tujuan ON rute.id_stasiun_tujuan = tujuan.id_stasiun");

            int no = 1;

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                string idr = baris["id_rute"].ToString();
                string asal = baris["stasiun_asal"].ToString();
                string tujuan = baris["stasiun_tujuan"].ToString();

                tablerute.Rows.Add(
                    idr,
                    no,
                    asal,
                    tujuan
                );

                no++;
            }
        }

        public void bersih()
        {
            cbasal.Text = "";
            cbtujuan.Text = "";
        }
        

        private void tablerute_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            string asal = cbasal.Text;
            string tujuan = cbtujuan.Text;

            db.crud($@"SELECT id_stasiun FROM stasiun 
               WHERE nama_stasiun = '{asal}'");

            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Stasiun asal tidak ditemukan");
                return;
            }

            string idasal = db.ds.Tables[0].Rows[0]["id_stasiun"].ToString();

            db.crud($@"SELECT id_stasiun FROM stasiun 
               WHERE nama_stasiun = '{tujuan}'");

            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Stasiun tujuan tidak ditemukan");
                return;
            }

            string idtujuan = db.ds.Tables[0].Rows[0]["id_stasiun"].ToString();

            db.crud($@"INSERT INTO rute
               (id_rute, id_stasiun_asal, id_stasiun_tujuan)
               VALUES (null, '{idasal}', '{idtujuan}')");

            MessageBox.Show("Data rute berhasil disimpan!");

            tampildata();
            bersih();
        }


        private void FRUTE_Load(object sender, EventArgs e)
        {
            tampildata();

            cbasal.Items.Clear();
            db.crud("SELECT nama_stasiun FROM stasiun");
            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                cbasal.Items.Add(row["nama_stasiun"].ToString());
            }

            cbtujuan.Items.Clear();
            db.crud("SELECT nama_stasiun FROM stasiun");
            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                cbtujuan.Items.Add(row["nama_stasiun"].ToString());
            }
        }

        private void btntampil_Click(object sender, EventArgs e)
        {
            tampildata();
        }

        private void btnubah_Click(object sender, EventArgs e)
        {
            string asal = cbasal.Text;
            string tujuan = cbtujuan.Text;

            db.crud($@"SELECT id_stasiun FROM stasiun 
               WHERE nama_stasiun = '{asal}'");

            string idasal = db.ds.Tables[0].Rows[0]["id_stasiun"].ToString();

            db.crud($@"SELECT id_stasiun FROM stasiun 
               WHERE nama_stasiun = '{tujuan}'");

            string idtujuan = db.ds.Tables[0].Rows[0]["id_stasiun"].ToString();

            db.crud($@"UPDATE rute SET
               id_stasiun_asal = '{idasal}',
               id_stasiun_tujuan = '{idtujuan}'
               WHERE id_rute = '{LBLID.Text}'");

            tampildata();
            bersih();
            LBLID.Text = "";
        }

        private void tablerute_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            if (baris < 0)
                return;

            if (kolom == 4)
            {
                string idr = tablerute.Rows[baris].Cells[0].Value.ToString();

                if (db.ds != null) db.ds.Clear();

                db.crud($@"SELECT rute.id_rute, 
               asal.nama_stasiun AS stasiun_asal,
               tujuan.nama_stasiun AS stasiun_tujuan
               FROM rute
               INNER JOIN stasiun AS asal ON rute.id_stasiun_asal = asal.id_stasiun
               INNER JOIN stasiun AS tujuan ON rute.id_stasiun_tujuan = tujuan.id_stasiun
               WHERE rute.id_rute = '{idr}'");

                foreach (DataRow brs in db.ds.Tables[0].Rows)
                {
                    string id = "" + brs["id_rute"];
                    string asal = "" + brs["stasiun_asal"];
                    string tujuan = "" + brs["stasiun_tujuan"];

                    LBLID.Text = id;
                    cbasal.Text = asal;
                    cbtujuan.Text = tujuan;
                }
            }

            if (kolom == 5)
            {
                string id = tablerute.Rows[baris].Cells[0].Value.ToString();

                DialogResult hasil = MessageBox.Show("Yakin mau hapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (hasil == DialogResult.Yes)
                {
                    db.crud($"DELETE FROM rute WHERE id_rute = '{id}'");

                    tampildata();
                    bersih();
                }
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            tablerute.Rows.Clear();

            string cari = txtsearch.Text;

            db.crud($@"SELECT rute.id_rute, 
               asal.nama_stasiun AS stasiun_asal, 
               tujuan.nama_stasiun AS stasiun_tujuan
               FROM rute
               INNER JOIN stasiun AS asal 
               ON rute.id_stasiun_asal = asal.id_stasiun
               INNER JOIN stasiun AS tujuan 
               ON rute.id_stasiun_tujuan = tujuan.id_stasiun
               WHERE asal.nama_stasiun LIKE '%{cari}%'
               OR tujuan.nama_stasiun LIKE '%{cari}%'");

            int no = 1;

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                string idr = baris["id_rute"].ToString();
                string asal = baris["stasiun_asal"].ToString();
                string tujuan = baris["stasiun_tujuan"].ToString();

                tablerute.Rows.Add(idr, no, asal, tujuan);

                no++;
            }
        }
    }
}
