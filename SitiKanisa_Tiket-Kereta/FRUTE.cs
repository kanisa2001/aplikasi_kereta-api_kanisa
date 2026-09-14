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

        private void isiKereta()
        {
            cmdkereta.Items.Clear();

            db.crud("SELECT nama_kereta FROM kereta");

            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                cmdkereta.Items.Add(row["nama_kereta"].ToString());
            }
        }

        public void tampildata()
        {
            tablerute.Rows.Clear();

            db.crud("SELECT rute.id_rute, kereta.nama_kereta, " +
                    "rute.stasiun_asal, rute.stasiun_tujuan " +
                    "FROM rute " +
                    "INNER JOIN kereta " +
                    "ON rute.id_kereta = kereta.idkereta");

            int no = 1;

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                string idr = baris["id_rute"].ToString();
                string nmk = baris["nama_kereta"].ToString();
                string asal = baris["stasiun_asal"].ToString();
                string tujuan = baris["stasiun_tujuan"].ToString();

                tablerute.Rows.Add(
                    idr,
                    no,
                    nmk,
                    asal,
                    tujuan
                );

                no++;
            }
        }

        public void bersih()
        {
            cmdkereta.Text = "";
            txtasal.Clear();
            txtujuan.Clear();
        }
        

        private void tablerute_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            string namaKereta = cmdkereta.Text;
            string asal = txtasal.Text;
            string tujuan = txtujuan.Text;

            db.crud($"SELECT idkereta FROM kereta WHERE nama_kereta = '{namaKereta}'");

            string idkereta = db.ds.Tables[0].Rows[0]["idkereta"].ToString();

            db.crud($"INSERT INTO rute " +
                    $"(id_rute, id_kereta, stasiun_asal, stasiun_tujuan) " +
                    $"VALUES (null, '{idkereta}', '{asal}', '{tujuan}')");

            tampildata();
            bersih();
        }

        private void tablerute_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void cmdkereta_DropDown(object sender, EventArgs e)
        {
            isiKereta();
        }

        private void FRUTE_Load(object sender, EventArgs e)
        {
            isiKereta();
            tampildata();
        }

        private void btntampil_Click(object sender, EventArgs e)
        {
            tampildata();
        }

        private void btnubah_Click(object sender, EventArgs e)
        {
            string namaKereta = cmdkereta.Text;
            string asal = txtasal.Text;
            string tujuan = txtujuan.Text;

            db.crud($"SELECT idkereta FROM kereta WHERE nama_kereta = '{namaKereta}'");

            string idkereta = db.ds.Tables[0].Rows[0]["idkereta"].ToString();

            db.crud($"UPDATE rute SET " +
                    $"id_kereta = '{idkereta}', " +
                    $"stasiun_asal = '{asal}', " +
                    $"stasiun_tujuan = '{tujuan}' " +
                    $"WHERE id_rute = '{LBLID.Text}'");

            tampildata();
            bersih();
            LBLID.Text = "";
        }



        private void cmdkereta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (cmdkereta.Text !="")
                {
                    txtasal.Focus();
                }
            }
        }

        private void txtasal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtasal.Text !="")
                {
                    txttujuan.Focus();
                }
            }
        }

        private void txtujuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txttujuan.Text !="")
                {
                    btnsimpan.Focus();
                }
            }
        }

        private void tablerute_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            if (baris < 0)
                return;

            if (kolom == 5)
            {
                string idr = tablerute.Rows[baris].Cells[0].Value.ToString();

                if (db.ds != null) db.ds.Clear();
                {
                    db.crud($"SELECT rute.*, kereta.nama_kereta " +
                            $"FROM rute " +
                            $"INNER JOIN kereta " +
                            $"ON rute.id_kereta = kereta.idkereta " +
                            $"WHERE rute.id_rute = '{idr}'");

                    foreach (DataRow brs in db.ds.Tables[0].Rows)
                    {
                        string id = "" + brs["id_rute"];
                        string nmk = "" + brs["nama_kereta"];
                        string asal = "" + brs["stasiun_asal"];
                        string tujuan = "" + brs["stasiun_tujuan"];

                        LBLID.Text = id;
                        cmdkereta.Text = nmk;
                        txtasal.Text = asal;
                        txtujuan.Text = tujuan;
                    }
                }
            }

            if (kolom == 6)
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
    }
}
