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
            db.crud($"SELECT * FROM petugas WHERE username = '{txtuser.Text}' AND Password = '{txtpass.Text}'");
            int cekbaris = db.ds.Tables[0].Rows.Count;
            if (cekbaris == 1)
            {
                DataRow baris = db.ds.Tables[0].Rows[0];
                string datamasuk = "" + baris["idp"];
                MENU FMENU = new MENU();
                FMENU.login = datamasuk;
                FMENU.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username atau Pass salah!!!");
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
