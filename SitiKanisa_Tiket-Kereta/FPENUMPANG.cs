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
    public partial class FPENUMPANG : Form
    {
        public FPENUMPANG()
        {
            InitializeComponent();
        }

        public void bersih()
        {
            txtnama.Clear();
            cmdkelamin.Text = "";
            txtindetitas.Clear();
            txttelp.Clear();
        }

        public void tampildata()
        {
            tablepenumpang.Rows.Clear();
            db.crud($"SELECT * FROM penumpang");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                string idp = "" + baris["idpenumpang"];
                string nama = "" + baris["nama"];
                string jnskelamin = "" + baris["jenis_kelamin"];
                string identitas = "" + baris["nik"];
                string telp = "" + baris["no_hp"];

                tablepenumpang.Rows.Add(idp, nama, identitas, jnskelamin, telp);
                no++;
            }
        }

        private void btnubah_Click(object sender, EventArgs e)
        {
            string nama = txtnama.Text;
            string jnskelamin = cmdkelamin.Text;
            string identitas = txtindetitas.Text;
            string telp = txttelp.Text;

            db.crud($"UPDATE penumpang SET nama = '{nama}', nik = '{identitas}', jenis_kelamin = '{jnskelamin}', no_hp = '{telp}' WHERE idpenumpang = '{LBLID.Text}'");

            tampildata();
            bersih();
            LBLID.Text = "";
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            string nama = txtnama.Text;
            string jnskelamin = cmdkelamin.Text;
            string identitas = txtindetitas.Text;
            string telp = txttelp.Text;

            db.crud($"INSERT INTO penumpang (idpenumpang, nama, nik, jenis_kelamin, no_hp) VALUES (null, '{nama}', '{identitas}', '{jnskelamin}', '{telp}')");

            tampildata();
            bersih();
        }

        private void tablepenumpang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            if (kolom == 5)
            {
                string idpenumpang = tablepenumpang.Rows[baris].Cells[0].Value.ToString();

                if (db.ds != null) db.ds.Clear();
                {
                    db.crud($"SELECT * FROM penumpang");
                    foreach (DataRow brs in db.ds.Tables[0].Rows)
                    {
                        string idp = "" + brs["idpenumpang"];
                        string nama = "" + brs["nama"];
                        string jnskelamin = "" + brs["jenis_kelamin"];
                        string identitas = "" + brs["nik"];
                        string telp = "" + brs["no_hp"];

                        LBLID.Text = idp;
                        txtnama.Text = nama;
                        cmdkelamin.Text = jnskelamin;
                        txtindetitas.Text = identitas;
                        txttelp.Text = telp;
                    }
                }
            }

            if (kolom == 6)
            {
                string idp = tablepenumpang.Rows[baris].Cells[0].Value.ToString();
                DialogResult hasil = MessageBox.Show("Yakin mau hapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (hasil == DialogResult.Yes)
                {
                    db.crud($"DELETE FROM penumpang WHERE idpenumpang = '{idp}'");
                    tampildata();
                    bersih();
                }
            }

        }

        private void FPENUMPANG_Load(object sender, EventArgs e)
        {
            cmdkelamin.Items.Clear();
            cmdkelamin.Items.Add("Perempuan");
            cmdkelamin.Items.Add("Laki-Laki");

            tampildata();
            bersih();
        }

        private void txtnama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtnama.Text != "")
                {
                    txtindetitas.Focus();
                }
            }
        }

        private void txtindetitas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtindetitas.Text != "")
                {
                    cmdkelamin.Focus();
                    cmdkelamin.DroppedDown = true;
                }

            }
        }

        private void cmdkelamin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (cmdkelamin.Text != "")
                {
                    txttelp.Focus();
                }
            }
        }

        private void txttelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txttelp.Text != "")
                {
                    btnsimpan.Focus();
                }
            }
        }
    }
}
