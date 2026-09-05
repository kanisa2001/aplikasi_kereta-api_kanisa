using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace SitiKanisa_Tiket_Kereta
{
    class db
    {
        public static MySqlConnection koneksi = new MySqlConnection("server=127.0.0.1; username=root; password=''; database='dbkereta_api'");
        public static DataSet ds = new DataSet();
        public static MySqlDataAdapter da;
        public static MySqlCommand perintah;

        public static void crud(string queri)
        {
            Console.WriteLine(queri);
            ds.Tables.Clear();
            perintah = new MySqlCommand(queri, koneksi);
            da = new MySqlDataAdapter(perintah);
            da.Fill(ds);
        }

    }
}
