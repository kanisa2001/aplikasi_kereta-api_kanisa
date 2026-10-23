using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace SitiKanisa_Tiket_Kereta
{
    public partial class Form2 : Form
    {
        private ReportDocument DokumenLaporan;
        public Form2()
        {
            InitializeComponent();
        }

        private void crystalReportViewer3_Load(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string reportPath = Path.Combine(Application.StartupPath, "pemesanan.rpt");
            try
            {
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show(
                        "File report tidak ditemukan: \n" + reportPath, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DokumenLaporan = new ReportDocument();
                DokumenLaporan.Load(reportPath);

                DokumenLaporan.SetDatabaseLogon( "root", "");

                crystalReportViewer1.ReportSource = DokumenLaporan;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gagal menampilkan report:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (DokumenLaporan != null)
                {
                    DokumenLaporan.Close();
                    DokumenLaporan.Dispose();
                    DokumenLaporan = null;
                }
            }
        }
    }
}
