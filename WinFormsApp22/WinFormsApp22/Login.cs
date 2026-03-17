using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp22
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Get username and password from the textboxes
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            //Check if username or password is empty
            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter username and password");
                return;

            }
            // Create Database Connection
            DBConnect db = new DBConnect();

            try
            {
                //Open Database
                db.Open();
                string query = "SELECT COUNT (*) FROM users WHERE username =@username and password=@password";
                // Create MySQL query
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, db.Connection);
                //Add parameters to prevent SQL injections
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                //Execute query and get results number
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose(); //Release command resources

                if (count == 1)
                {
                    MessageBox.Show("Login Successful");
                    //Open Dashboard
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
