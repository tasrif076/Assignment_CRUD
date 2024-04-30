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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
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

            // Check if username and password are not empty
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    // Establish connection to the database
                    using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-8V1G5L8C\\MSSQLSERVER13;Initial Catalog=Product Details;Integrated Security=True;"))
                    {
                        con.Open();

                        // SQL command to insert new user into the table
                        string query = "INSERT INTO ut (username, password) VALUES (@username, @password)";

                        // Create SQL command and parameters
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@password", password);

                            // Execute the SQL command
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if any rows were affected (if successful)
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Registration completed.");
                            }
                            else
                            {
                                MessageBox.Show("Registration failed. Please try again.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                // Show message if username or password is empty
                MessageBox.Show("Please input valid information.");
            }
        }
    }
}
