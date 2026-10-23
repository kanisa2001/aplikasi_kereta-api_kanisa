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
    public partial class FGERBONG : Form
    {
        public FGERBONG()
        {
            InitializeComponent();
        }

        private void FGERBONG_Load(object sender, EventArgs e)
        {
            dgvrole.DataError       += (s, ex) => { ex.Cancel = true; };
            guna2DataGridView1.DataError += (s, ex) => { ex.Cancel = true; };
            guna2DataGridView2.DataError += (s, ex) => { ex.Cancel = true; };
            guna2DataGridView3.DataError += (s, ex) => { ex.Cancel = true; };

        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
