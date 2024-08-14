using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace YourNamespace
{
    public static class DbConnection
    {
        public static MySqlConnection conn = new MySqlConnection();
        public static MySqlDataAdapter da = new MySqlDataAdapter();
        public static DataTable dt = new DataTable();
        public static MySqlDataReader dr;
        public static MySqlCommand cmd;
        public static bool result;

        public static bool DbConn()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.ConnectionString = "Server=localhost;Database=todo_list;Uid=root;Pwd=";
                    conn.Open();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show("Server is Not Connected", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return result;
        }
    }
}
