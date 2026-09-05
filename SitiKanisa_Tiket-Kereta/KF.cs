using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SitiKanisa_Tiket_Kereta
{
    class KF
    {
        public static void UntukForm(Form FormApa, Panel PanelApa)
        {
            PanelApa.Controls.Clear();
            PanelApa.Controls.Add(FormApa);
            FormApa.FormBorderStyle = FormBorderStyle.None;
            FormApa.Dock = DockStyle.Fill;
            FormApa.Show();
        }
    }
}
