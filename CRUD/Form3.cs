using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Retrieve username and password from textboxes
            string username = textBox1.Text;
            string password = textBox2.Text;

            try
            {
                // Establish connection to the database
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-8V1G5L8C\\MSSQLSERVER13;Initial Catalog=Product Details;Integrated Security=True;"))
                {
                    con.Open();

                    // SQL command to check if the username and password match any record
                    string query = "SELECT COUNT(*) FROM ut WHERE username = @username AND password = @password";

                    // Create SQL command and parameters
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        // Execute the SQL command and retrieve the result
                        int count = (int)cmd.ExecuteScalar();

                        // Check if a record with the entered username and password exists
                        if (count > 0)
                        {
                            MessageBox.Show("Login successful.");

                            // Navigate to Form1 and close the login form
                            Form1 form1 = new Form1();
                            form1.Show();
                            //this.Hide(); // or this.Close() if you want to close the login form completely
                        }
                        else
                        {
                            MessageBox.Show("Login failed. Please check your username and password.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
}
