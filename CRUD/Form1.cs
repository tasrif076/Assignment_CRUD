
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10); // Change the font to Arial with size 10

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve input values from textboxes
            string productId = textBox1.Text;
            string productName = textBox2.Text;
            string priceStr = textBox3.Text;

            // Check if any input field is empty
            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(priceStr))
            {
                MessageBox.Show("Please input all fields.");
                return;
            }

            // Validate Price input
            if (!decimal.TryParse(priceStr, out decimal price))
            {
                MessageBox.Show("Price must be a valid numeric value.");
                return;
            }

            try
            {
                // Establish connection to the database
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-8V1G5L8C\\MSSQLSERVER13;Initial Catalog=Product Details;Integrated Security=True;"))
                {
                    con.Open();

                    // SQL command to insert new product into the table
                    string query = "INSERT INTO pt (Product_ID, Product_Name, Price) VALUES (@productId, @productName, @price)";

                    // Create SQL command and parameters
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.Parameters.AddWithValue("@productName", productName);
                        cmd.Parameters.AddWithValue("@price", price);

                        // Execute the SQL command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if any rows were affected (if successful)
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product created successfully.");
                            // Clear input fields or perform any other actions upon successful creation
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Failed to create product. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish connection to the database
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-8V1G5L8C\\MSSQLSERVER13;Initial Catalog=Product Details;Integrated Security=True;"))
                {
                    con.Open();

                    // SQL command to select all product details
                    string query = "SELECT * FROM pt";

                    // Create a DataTable to hold the retrieved data
                    DataTable dt = new DataTable();

                    // Create a SqlDataAdapter to fill the DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                    {
                        // Fill the DataTable with the data from the database
                        adapter.Fill(dt);
                    }

                    // Bind the DataTable to the DataGridView to display the data
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Retrieve updated values from textboxes
            string productId = textBox1.Text;
            string productName = textBox2.Text;
            string priceStr = textBox3.Text;

            // Check if any value is provided for updating
            if (string.IsNullOrEmpty(productId) && string.IsNullOrEmpty(productName) && string.IsNullOrEmpty(priceStr))
            {
                MessageBox.Show("Please provide at least one value to update.");
                return;
            }

            try
            {
                // Establish connection to the database
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-8V1G5L8C\\MSSQLSERVER13;Initial Catalog=Product Details;Integrated Security=True;"))
                {
                    con.Open();

                    // Construct SQL UPDATE statement based on provided values
                    string query = "UPDATE pt SET";
                    bool needComma = false;

                    if (!string.IsNullOrEmpty(productName))
                    {
                        query += " Product_Name = @productName";
                        needComma = true;
                    }

                    if (!string.IsNullOrEmpty(priceStr))
                    {
                        if (needComma)
                            query += ",";
                        query += " Price = @price";
                        needComma = true;
                    }

                    query += " WHERE";

                    if (!string.IsNullOrEmpty(productId))
                    {
                        query += " Product_ID = @productId";
                    }

                    // Create SQL command and parameters
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (!string.IsNullOrEmpty(productName))
                        {
                            cmd.Parameters.AddWithValue("@productName", productName);
                        }

                        if (!string.IsNullOrEmpty(priceStr))
                        {
                            if (!decimal.TryParse(priceStr, out decimal price))
                            {
                                MessageBox.Show("Price must be a valid numeric value.");
                                return;
                            }
                            cmd.Parameters.AddWithValue("@price", price);
                        }

                        if (!string.IsNullOrEmpty(productId))
                        {
                            cmd.Parameters.AddWithValue("@productId", productId);
                        }

                        // Execute the SQL command only if at least one field is provided for updating
                        if (needComma)
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if any rows were affected (if successful)
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Product details updated successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Failed to update product details.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please provide at least one value to update.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Retrieve product ID from the input textbox
            string productId = textBox1.Text;

            // Check if product ID is provided
            if (string.IsNullOrEmpty(productId))
            {
                MessageBox.Show("Please provide the Product ID to delete.");
                return;
            }

            try
            {
                // Establish connection to the database
                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-8V1G5L8C\\MSSQLSERVER13;Initial Catalog=Product Details;Integrated Security=True;"))
                {
                    con.Open();

                    // Construct SQL DELETE statement
                    string query = "DELETE FROM pt WHERE Product_ID = @productId";

                    // Create SQL command and parameters
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@productId", productId);

                        // Execute the SQL command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if any rows were affected (if successful)
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete product. Product ID not found.");
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
