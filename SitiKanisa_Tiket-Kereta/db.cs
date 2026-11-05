using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace SitiKanisa_Tiket_Kereta
{
    class db
    {
        private static string connString = "server=127.0.0.1;user=root;password=;database=dbkereta_api;";
        public static DataSet ds = new DataSet();

        /// <summary>
        /// Jalankan query SELECT. Hasil tersimpan di db.ds.Tables[0].
        /// Untuk INSERT/UPDATE/DELETE gunakan eksekusi() agar lebih tepat.
        /// </summary>
        public static void crud(string query)
        {
            ds.Tables.Clear();
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    con.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan database:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Jalankan query INSERT / UPDATE / DELETE.
        /// Kembalikan true jika berhasil.
        /// </summary>
        public static bool eksekusi(string query)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan database:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
