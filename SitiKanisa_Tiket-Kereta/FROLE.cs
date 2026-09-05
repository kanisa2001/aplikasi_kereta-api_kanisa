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
    public partial class FROLE : Form
    {
        public FROLE()
        {
            InitializeComponent();

        }

        private void FROLE_Load(object sender, EventArgs e)
        {
            tampildata();
            dgvrole.Columns["Column3"].Visible = false;
        }

        public void bersih()
        {
            txtket.Clear();
            txtrole.Clear();
        }

        public void tampildata()
        {
            dgvrole.Rows.Clear();
            db.crud("select * from role");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                string idr = "" + baris["idRole"];
                string role = "" + baris["nama_role"];
                string ket = "" + baris["keterangan"];
                dgvrole.Rows.Add(idr, no, role, ket);
                no++;
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            string role = txtrole.Text;
            string ket = txtket.Text;
            db.crud($"INSERT INTO role (idRole, nama_role, keterangan) VALUES(null, '{role}', '{ket}')");
            tampildata();
            bersih();
        }

       

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            dgvrole.Rows.Clear();
            db.crud($"select * from role WHERE nama_role LIKE '%{txtsearch.Text}%'");
            int no = 1;
            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {

                string role = "" + baris["nama_role"];
                string ket = "" + baris["keterangan"];
                dgvrole.Rows.Add(no, role, ket);
                no++;
            }
        }

        private void dgvrole_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            if (kolom == 4)
            {
                string idrole = dgvrole.Rows[baris].Cells[0].Value.ToString();

                if (db.ds != null) db.ds.Clear();

                db.crud($"SELECT * FROM role where idRole = '{idrole}'");
                foreach (DataRow brs in db.ds.Tables[0].Rows)
                {
                    string role = "" + brs["nama_role"];
                    string ket = "" + brs["keterangan"];

                    LBLID.Text = idrole;
                    txtrole.Text = role;
                    txtket.Text = ket;
                }
            }

           if (kolom == 5) 
            {
                string idRole = dgvrole.Rows[baris].Cells[0].Value.ToString();
                DialogResult hasil = MessageBox.Show("Yakin mau hapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
                if (hasil == DialogResult.Yes)
                {
                    db.crud($"DELETE FROM role where idRole = '{idRole}'");
                    tampildata();
                    bersih();
                }
            }

        }

       

        private void btnubah_Click_1(object sender, EventArgs e)
        {
            string role = txtrole.Text;
            string ket = txtket.Text;

            db.crud($"update role SET nama_role = '{role}', keterangan = '{ket}' where idrole = {LBLID.Text} ");
            tampildata();
        }

        private void btntampil_Click(object sender, EventArgs e)
        {

        }

        private void txtrole_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtrole.Text !="")
                {
                    txtket.Focus();
                }
            }
        }

        private void txtket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled=true;
                if (txtket.Text !="")
                {
                    btnsimpan.Focus();
                }
            }
        }
    }
}
