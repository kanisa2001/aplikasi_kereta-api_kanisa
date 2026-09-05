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
    public partial class MENU : Form
    {
        public MENU()
        {
            InitializeComponent();
        }

        private void MENU_Load(object sender, EventArgs e)
        {

        }
        public string login;

        private void MENU_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FPETUGAS Fp = new FPETUGAS() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            FROLE Fp = new FROLE() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            FKERETA Fp = new FKERETA() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            FRUTE Fp = new FRUTE() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            FGERBONG Fp = new FGERBONG() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            FPENUMPANG Fp = new FPENUMPANG() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            FPESANAN Fp = new FPESANAN() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            FJADWAL Fp = new FJADWAL() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }
    }
}
