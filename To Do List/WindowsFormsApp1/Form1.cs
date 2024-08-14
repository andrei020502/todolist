using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp1.Main_Forms;
namespace YourNamespace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Optionally, test the DB connection here
            if (!DbConnection.DbConn())
            {
                MessageBox.Show("Failed to connect to the database.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // Close the application if the connection fails
            }
        }


        private void btn_exit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            {
                string username = txt_username.Text;
                string password = txt_password.Text;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    if (DbConnection.conn.State == ConnectionState.Closed)
                    {
                        DbConnection.conn.Open();
                    }

                    string query = "SELECT COUNT(*) FROM table_users WHERE username = @username AND password = @password";
                    DbConnection.cmd = new MySqlCommand(query, DbConnection.conn);
                    DbConnection.cmd.Parameters.AddWithValue("@username", username);
                    DbConnection.cmd.Parameters.AddWithValue("@password", password);

                    int result = Convert.ToInt32(DbConnection.cmd.ExecuteScalar());

                    if (result > 0)
                    {
                        MessageBox.Show("Login successful!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Open AdminForm after successful login
                        AdminForm adminForm = new AdminForm();
                        adminForm.Show();
                        this.Hide(); // Hide the login form
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    DbConnection.conn.Close();
                }
            }
        }
    }
}
