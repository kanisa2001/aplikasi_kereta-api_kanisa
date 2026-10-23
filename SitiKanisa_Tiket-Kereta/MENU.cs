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

        // Data user yang sedang login
        public string login;
        public string namaRole = "";
        public string namaUser = "";

        private void MENU_Load(object sender, EventArgs e)
        {
            TerapkanAksesRole();
            RapiButtonMenu();
        }

        /// <summary>
        /// Sembunyikan tombol menu sesuai role:
        /// - Admin        : Pesanan, Jadwal, Penumpang, Riwayat
        /// - Super Admin  : Petugas, Role, Kereta, Rute, Riwayat, Penumpang
        /// Nama role disesuaikan dengan data di tabel role database Anda.
        /// </summary>
        private void TerapkanAksesRole()
        {
            string role = namaRole.Trim().ToLower();

            // Tampilkan nama user dan role di label header (jika ada)
            if (lblrole != null)
                lblrole.Text = $"{namaUser}  |  {namaRole}";

            if (role == "admin")
            {
                // Admin HANYA boleh: Pesanan, Jadwal, Penumpang, Riwayat
                guna2Button1.Visible = false;  // PETUGAS
                guna2Button2.Visible = false;  // ROLE
                guna2Button7.Visible = false;  // KERETA
                guna2Button5.Visible = false;  // RUTE
                guna2Button3.Visible = true;   // PEMESANAN
                guna2Button8.Visible = true;   // JADWAL
                guna2Button4.Visible = true;   // PENUMPANG
                guna2Button6.Visible = true;   // RIWAYAT
            }
            else if (role == "super admin")
            {
                // Super Admin HANYA boleh: Petugas, Role, Kereta, Rute, Riwayat, Penumpang
                guna2Button1.Visible = true;   // PETUGAS
                guna2Button2.Visible = true;   // ROLE
                guna2Button7.Visible = true;   // KERETA
                guna2Button5.Visible = true;   // RUTE
                guna2Button3.Visible = false;  // PEMESANAN
                guna2Button8.Visible = false;  // JADWAL
                guna2Button4.Visible = true;   // PENUMPANG
                guna2Button6.Visible = true;   // RIWAYAT
            }
            // Jika role tidak dikenali, semua tombol tetap tampil (fallback)
        }

        /// <summary>
        /// Susun ulang posisi Y tombol-tombol yang masih Visible
        /// agar tidak ada celah kosong akibat tombol yang disembunyikan.
        /// </summary>
        private void RapiButtonMenu()
        {
            // Urutan tampil dari atas ke bawah (sesuai urutan menu yang diinginkan)
            var semuaTombol = new Guna.UI2.WinForms.Guna2Button[]
            {
                guna2Button1,  // PETUGAS
                guna2Button2,  // ROLE
                guna2Button7,  // KERETA
                guna2Button5,  // RUTE
                guna2Button4,  // PENUMPANG
                guna2Button3,  // PEMESANAN
                guna2Button8,  // JADWAL
                guna2Button6,  // RIWAYAT
            };

            int startY   = 92;   // jarak dari atas (di bawah logo)
            int gapAntar = 8;    // jarak antar tombol
            int tinggi   = 40;   // tinggi setiap tombol

            int posY = startY;
            foreach (var btn in semuaTombol)
            {
                if (btn.Visible)
                {
                    btn.Location = new System.Drawing.Point(btn.Location.X, posY);
                    posY += tinggi + gapAntar;
                }
            }
        }

        private bool _isLogout = false; // flag untuk bedakan logout vs tutup jendela

        private void MENU_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isLogout)
            {
                Application.Exit();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show(
                "Apakah Anda yakin ingin logout?",
                "Konfirmasi Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                _isLogout = true;
                Form1 formLogin = new Form1();
                formLogin.Show();
                this.Close();
            }
        }

        // Cek akses sebelum membuka form — mencegah akses langsung selain via tombol
        private bool CekAkses(string[] roleDiizinkan)
        {
            string role = namaRole.Trim().ToLower();
            if (Array.IndexOf(roleDiizinkan, role) >= 0) return true;

            MessageBox.Show("Anda tidak memiliki akses ke fitur ini.", "Akses Ditolak",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!CekAkses(new[] { "super admin" })) return;
            FPETUGAS Fp = new FPETUGAS() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (!CekAkses(new[] { "super admin" })) return;
            FROLE Fp = new FROLE() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (!CekAkses(new[] { "super admin" })) return;
            FKERETA Fp = new FKERETA() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (!CekAkses(new[] { "super admin" })) return;
            FRUTE Fp = new FRUTE() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // Tidak digunakan — guna2Button6 (RIWAYAT) terhubung ke guna2Button6_Click_1
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (!CekAkses(new[] { "admin", "super admin" })) return;
            FPENUMPANG Fp = new FPENUMPANG() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (!CekAkses(new[] { "admin" })) return;
            FPESANAN Fp = new FPESANAN() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (!CekAkses(new[] { "admin" })) return;
            FJADWAL Fp = new FJADWAL() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void guna2Button6_Click_1(object sender, EventArgs e)
        {
            if (!CekAkses(new[] { "admin", "super admin" })) return;
            RIWAYAT Fp = new RIWAYAT() { TopMost = true, TopLevel = false };
            KF.UntukForm(Fp, pnlkonten);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
