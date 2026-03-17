using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinFormsApp22
{
    public partial class RegisterStudent : Form
    {
        public RegisterStudent()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Declare inputs
            string firstname = textBox1.Text;
            string lastname = textBox2.Text;
            string course = comboBox1.SelectedItem.ToString();
            string section = comboBox2.SelectedItem.ToString();
            int age;
            //Validate inputs if empty
            if (string.IsNullOrEmpty(firstname)
                || string.IsNullOrEmpty(lastname)
                ||  string.IsNullOrEmpty(course)
                ||  string.IsNullOrEmpty(section))
            {
                MessageBox.Show("Please fill all fields");
                return; //Stop process
            }
            // Validate input for age 
            if (int.TryParse(textBox3.Text.Trim(), out age))
            {
                MessageBox.Show("Please enter a valid age");
                return; //Stop process
            }
            //Call Database Connection
            DBConnect db = new DBConnect();
            try
            {
                db.Open();
                string query = "INSERT INTO students(firstname, lastname, course, section, age) " +
                    "VALUES(@firstname, @lastname, @course, @section, @age)";
                // Create MySQL query
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, db.Connection);
                // Add parameters
                cmd.Parameters.AddWithValue("@firstname", firstname);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@section", section);
                cmd.Parameters.AddWithValue("@age", age);
                //Execute cmd
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student added successfully");
                
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
    }
}
