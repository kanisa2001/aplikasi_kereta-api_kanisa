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
    public partial class FPETUGAS : Form
    {
        public FPETUGAS()
        {
            InitializeComponent();
        }

        public void bersih()
        {
            txtuser.Clear();
            txtpass.Clear();
            cbrole.Text = "";
            cbstatus.Text = "";
        }

        public void tampildata()
        {
            tablepetugas.Rows.Clear();
            db.crud("SELECT idp, username, role.nama_role, status FROM `petugas` INNER JOIN role ON petugas.idrole = role.idRole;");
            int no = 1;

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                string idp = "" + baris["idp"];
                string user = "" + baris["username"];
                string role = "" + baris["nama_role"];
                string status = "" + baris["status"];
                tablepetugas.Rows.Add(idp, no, user, role, status);
                no++;
            }
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {

            

        }

        private void cbrole_DropDown(object sender, EventArgs e)
        {

        }


        private void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbstatus_DropDown(object sender, EventArgs e)
        {
            
        }

        private void FPETUGAS_Load(object sender, EventArgs e)
        {
            tampildata();
            tablepetugas.Columns["Column6"].Visible = false;

            cbrole.Items.Clear();
            db.crud("SELECT nama_role FROM role");
            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                cbrole.Items.Add(row["nama_role"].ToString());
            }

            cbstatus.Items.Clear();
            cbstatus.Items.Add("Aktif");
            cbstatus.Items.Add("Tidak Aktif");


        }



        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            tablepetugas.Rows.Clear();
            db.crud($"SELECT username, role.nama_role, status FROM `petugas` INNER JOIN role ON petugas.idrole = role.idRole WHERE username LIKE '%{txtsearch.Text}%'");
            int no = 1;

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                string idp = "" + baris["idp"];
                string user = "" + baris["username"];
                string role = "" + baris["nama_role"];
                string status = "" + baris["status"];
                tablepetugas.Rows.Add(idp, no, user, role, status);
                no++;
            }
        }
        private void tablepetugas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            if (kolom == 5)
            {
                string idpetugas = tablepetugas.Rows[baris].Cells[0].Value.ToString();

                db.crud($"select * from petugas INNER JOIN role ON petugas.idRole = role.idRole WHERE idp = {idpetugas}");
                foreach (DataRow brs in db.ds.Tables[0].Rows)
                {
                    string us = "" + brs["username"];
                    string pw = "" + brs["password"];
                    string st = "" + brs["status"];
                    string rl = "" + brs["nama_role"];

                    LBLID.Text = idpetugas;
                    txtuser.Text = us;
                    txtpass.Text = pw;
                    cbstatus.Text = st;
                    cbrole.Text = rl;
                }
            }
            if (kolom == 6)
            {
                string idpetugas = tablepetugas.Rows[baris].Cells[0].Value.ToString(); // ambil dari kolom 0 yg dihide
                DialogResult setuju = MessageBox.Show("Hapus Data?", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (setuju == DialogResult.Yes)
                {
                    db.crud($"DELETE FROM petugas where idp = '{idpetugas}'");
                    tampildata();
                    bersih();
                }

            }

        }

        private void btnubah_Click(object sender, EventArgs e)
        {
            
        }

        private void btntampil_Click(object sender, EventArgs e)
        {

        }

        private void LBLID_Click(object sender, EventArgs e)
        {

        }

        private void txtuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtuser.Text != "")
                {
                    txtpass.Focus();
                }
            }
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtpass.Text != "")
                {
                    cbstatus.Focus();
                }
            }
        }

        private void cbstatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (cbstatus.Text != "")
                {
                    cbrole.Focus();
                }
            }
        }

        private void cbrole_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (cbrole.Text != "")
                {
                    btnsimpan.Focus();
                }
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            tablepetugas.Rows.Clear();
            db.crud("SELECT idp, username, role.nama_role, status FROM `petugas` INNER JOIN role ON petugas.idrole = role.idRole;");
            int no = 1;

            foreach (DataRow baris in db.ds.Tables[0].Rows)
            {
                string idp = "" + baris["idp"];
                string user = "" + baris["username"];
                string role = "" + baris["nama_role"];
                string status = "" + baris["status"];
                tablepetugas.Rows.Add(idp, no, user, role, status);
                no++;
            }
        }

        private void btnsimpan_Click_1(object sender, EventArgs e)
        {
            string user = txtuser.Text;
            string pass = txtpass.Text;
            string status = cbstatus.Text;
            string role = cbrole.Text;

            db.crud($"SELECT idRole FROM role WHERE nama_role = '{role}'");
            string idrole = db.ds.Tables[0].Rows[0]["idRole"].ToString();
            db.crud($"INSERT INTO petugas (`username`, `password`, `idrole`, `status`) VALUES ('{user}', '{pass}', '{idrole}', '{status}');");
            tampildata();
            bersih();
        }

        private void btnubah_Click_1(object sender, EventArgs e)
        {
            string us = txtuser.Text;
            string pw = txtpass.Text;
            string st = cbstatus.Text;
            string role = cbrole.Text;

            db.crud($"SELECT idRole FROM role WHERE nama_role = '{role}'");
            string idrole = db.ds.Tables[0].Rows[0]["idRole"].ToString();

            db.crud($"UPDATE petugas SET username='{us}', password='{pw}', idrole='{idrole}', status='{st}' WHERE idp='{LBLID.Text}'");

            tampildata();
            bersih();
            LBLID.Text = "";
        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
