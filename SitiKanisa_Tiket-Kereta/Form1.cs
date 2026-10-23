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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            db.crud($"SELECT petugas.idp, petugas.username, petugas.status, role.nama_role " +
                    $"FROM petugas INNER JOIN role ON petugas.idrole = role.idRole " +
                    $"WHERE petugas.username = '{txtuser.Text}' AND petugas.Password = '{txtpass.Text}'");
            int cekbaris = db.ds.Tables[0].Rows.Count;
            if (cekbaris == 1)
            {
                DataRow baris = db.ds.Tables[0].Rows[0];
                string status = "" + baris["status"];

                if (status == "Tidak Aktif")
                {
                    MessageBox.Show("Akun Anda tidak aktif. Hubungi Super Admin.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string datamasuk = "" + baris["idp"];
                string namaRole  = "" + baris["nama_role"];
                string namaUser  = "" + baris["username"];

                MENU FMENU = new MENU();
                FMENU.login    = datamasuk;
                FMENU.namaRole = namaRole;
                FMENU.namaUser = namaUser;
                FMENU.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username atau Password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (txtuser.Text !="")
                {
                    txtpass.Focus();
                }
            }
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtpass.Text !="")
                {
                    e.Handled = true;
                    btnlogin.Focus();
                }
            }
        }
    }
}
