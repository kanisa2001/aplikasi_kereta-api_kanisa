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
    public partial class FKERETA : Form
    {
        public FKERETA()
        {
            InitializeComponent();
        }

        public void bersih()
        {
            txtnmkereta.Clear();
            cmbjenis.Text = "";
            txtkursi.Clear();
            cmbstatus.Text = "";
            txtharga.Clear();
            
        }

        public void tampildata()
        {
            tablekereta.Rows.Clear();

            db.crud("SELECT kereta.idkereta, kereta.nama_kereta, " +
                    "jenis_kereta.nama_jenis, kereta.kapasitas, " +
                    "kereta.status, kereta.harga " +
                    "FROM kereta " +
                    "INNER JOIN jenis_kereta " +
                    "ON kereta.id_jenis_kereta = jenis_kereta.id_jenis_kereta");

            int no = 1;

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                string id = baris["idkereta"].ToString();
                string nmk = baris["nama_kereta"].ToString();
                string jns = baris["nama_jenis"].ToString();
                string krs = baris["kapasitas"].ToString();
                string stts = baris["status"].ToString();
                string hrg = baris["harga"].ToString();

                tablekereta.Rows.Add(id, no, nmk, jns, krs, stts, hrg);

                no++;
            }
        }

        private void isiJenis()
        {
            cmbjenis.Items.Clear();

            db.crud("SELECT nama_jenis FROM jenis_kereta");

            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                cmbjenis.Items.Add(row["nama_jenis"].ToString());
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            string nmk = txtnmkereta.Text;
            string jns = cmbjenis.Text;
            string krs = txtkursi.Text;
            string stts = cmbstatus.Text;
            string hrg = txtharga.Text;
            db.crud($"SELECT id_jenis_kereta FROM jenis_kereta WHERE nama_jenis = '{jns}'");
            string id_jenis_kereta = db.ds.Tables[0].Rows[0]["id_jenis_kereta"].ToString();
            db.crud($"INSERT INTO kereta (idkereta, nama_kereta, id_jenis_kereta, kapasitas, status, harga) VALUES (null, '{nmk}', '{id_jenis_kereta}', '{krs}', '{stts}', '{hrg}');");
            tampildata();
            bersih();
        }

        private void tablekereta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            if (kolom == 7)
            {
                string idkereta = tablekereta.Rows[baris].Cells[0].Value.ToString();

                if (db.ds != null) db.ds.Clear();
                {
                    db.crud($"SELECT * FROM kereta INNER JOIN jenis_kereta ON kereta.id_jenis_kereta = jenis_kereta.id_jenis_kereta WHERE idkereta = '{idkereta}'");
                    foreach (DataRow brs in db.ds.Tables[0].Rows)
                    {
                        string id = "" + brs["idkereta"];
                        string nmk = "" + brs["nama_kereta"];
                        string jns = "" + brs["nama_jenis"];
                        string krs = "" + brs["kapasitas"];
                        string stts = "" + brs["status"];
                        string hrg = "" + brs["harga"];

                        LBLID.Text = id;
                        txtnmkereta.Text = nmk;
                        cmbjenis.SelectedItem = jns;
                        txtkursi.Text = krs;
                        cmbstatus.Text = stts;
                        txtharga.Text = hrg;

                    }
                }
            }

                if (kolom == 8)
                {
                    string id = tablekereta.Rows[baris].Cells[0].Value.ToString();
                    DialogResult hasil = MessageBox.Show("Yakin mau hapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (hasil == DialogResult.Yes)
                    {
                        db.crud($"DELETE FROM kereta WHERE idkereta = '{id}'");
                        tampildata();
                        bersih();
                    }
                }
            }
        
        private void cmbjenis_DropDown(object sender, EventArgs e)
        {
            cmbjenis.Items.Clear();
            db.crud("SELECT nama_jenis FROM jenis_kereta");
            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                cmbjenis.Items.Add(row["nama_jenis"].ToString());
            }
        }


        private void btntampil_Click(object sender, EventArgs e)
        {
            tampildata();
        }

        private void FKERETA_Load(object sender, EventArgs e)
        {
            isiJenis();
            cmbstatus.Items.Clear();

            cmbstatus.Items.Add("Sedang Beroperasi");
            cmbstatus.Items.Add("Tidak Sedang Beroperasi");
            cmbstatus.Items.Add("Tidak Beroperasi");

            tampildata();
            bersih();
        }

        private void btnubah_Click(object sender, EventArgs e)
        {
            string nmk = txtnmkereta.Text;
            string jns = cmbjenis.Text;
            string krs = txtkursi.Text;
            string stts = cmbstatus.Text;
            string hrg = txtharga.Text;

            db.crud($"SELECT id_jenis_kereta FROM jenis_kereta WHERE nama_jenis = '{jns}'");

            if (db.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Jenis kereta tidak ditemukan!");
                return;
            }

            string id_jenis_kereta =
                db.ds.Tables[0].Rows[0]["id_jenis_kereta"].ToString();

            db.crud($"UPDATE kereta SET " +
                    $"nama_kereta='{nmk}', " +
                    $"id_jenis_kereta='{id_jenis_kereta}', " +
                    $"kapasitas='{krs}', " +
                    $"status='{stts}', " +
                    $"harga='{hrg}' " +
                    $"WHERE idkereta='{LBLID.Text}'");

            tampildata();
            bersih();
            LBLID.Text = "";
        }

        private void tablekereta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtnmkereta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtnmkereta.Text != "")
                {
                    cmbjenis.Focus();
                }
            }
        }

        private void cmbjenis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (cmbjenis.Text !="")
                {
                    cmbstatus.Focus();
                }
            }
        }

        private void cmbstatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (cmbstatus.Text !="")
                {
                    txtkursi.Focus();
                }
            }
        }

        private void txtkursi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtkursi.Text !="")
                {
                    txtharga.Focus();
                }
            }
        }

        private void txtharga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtharga.Text !="")
                {
                    btnsimpan.Focus();
                }
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            tablekereta.Rows.Clear();

            string cari = txtsearch.Text;

            db.crud($@"SELECT kereta.idkereta, 
               kereta.nama_kereta, 
               jenis_kereta.nama_jenis, 
               kereta.kapasitas, 
               kereta.status, 
               kereta.harga
               FROM kereta
               INNER JOIN jenis_kereta 
               ON kereta.id_jenis_kereta = jenis_kereta.id_jenis_kereta
               WHERE kereta.nama_kereta LIKE '%{cari}%'
               OR jenis_kereta.nama_jenis LIKE '%{cari}%'
               OR kereta.kapasitas LIKE '%{cari}%'
               OR kereta.status LIKE '%{cari}%'
               OR kereta.harga LIKE '%{cari}%'");

            int no = 1;

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                string id = baris["idkereta"].ToString();
                string nmk = baris["nama_kereta"].ToString();
                string jns = baris["nama_jenis"].ToString();
                string krs = baris["kapasitas"].ToString();
                string stts = baris["status"].ToString();
                string hrg = baris["harga"].ToString();

                tablekereta.Rows.Add(id, no, nmk, jns, krs, stts, hrg);

                no++;
            }
        }
    }
}