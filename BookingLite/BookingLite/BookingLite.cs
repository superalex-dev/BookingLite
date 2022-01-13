using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BookingLite
{
    public partial class BookingLite : Form
    {
        string connectionString = @"Data Source=DESKTOP-6KMDJUB;Initial Catalog=SchoolProjectsV2;Integrated Security=True";
        int ID = 0;
        //int Id = 0;
        public BookingLite()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GridFill();
            deleteButton.Enabled = false;
        }

        private void searchField_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("Search", sqlConnection);
                sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlData.SelectCommand.Parameters.AddWithValue("@Value", searchField.Text.Trim());
                DataTable datatable = new DataTable();
                sqlData.Fill(datatable);
                dataGridViewBookingLite.DataSource = datatable;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (firstNameField.Text.Trim() != "" && lastNameField.Text.Trim() != "" && phoneField.Text.Trim() != "" && emailField.Text.Trim() != "")
            {
                Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                Match match = regex.Match(emailField.Text.Trim());
                if (match.Success)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        SqlCommand sqlCommand = new SqlCommand("AddorEdit", sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Id", ID);
                        sqlCommand.Parameters.AddWithValue("@Firstname", firstNameField.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@LastName", lastNameField.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Phone", phoneField.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Email", emailField.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Address", addressField.Text.Trim());
                        sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("Submitted successfully");
                        Clear();
                        GridFill();
                    }
                }
                else
                {
                    MessageBox.Show("Email address is not valid.");
                }
            }
            else
            {
                MessageBox.Show("Please fill required fields.");
            }
        }
        void Clear()
        {
            firstNameField.Text = lastNameField.Text = phoneField.Text = emailField.Text = addressField.Text = searchField.Text = "";
            ID = 0;
            saveButton.Text = "Save";
            deleteButton.Enabled = false;
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void GridFill()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("ViewAll", sqlConnection);
                sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable datatable = new DataTable();
                sqlData.Fill(datatable);
                dataGridViewBookingLite.DataSource = datatable;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("DeleteById", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", ID);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully");
                Clear();
                GridFill();
            }
        }

        private void dataGridViewBookingLite_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewBookingLite.CurrentRow.Index != -1)
            {
                firstNameField.Text = dataGridViewBookingLite.CurrentRow.Cells[1].Value.ToString();
                lastNameField.Text = dataGridViewBookingLite.CurrentRow.Cells[2].Value.ToString();
                phoneField.Text = dataGridViewBookingLite.CurrentRow.Cells[3].Value.ToString();
                emailField.Text = dataGridViewBookingLite.CurrentRow.Cells[4].Value.ToString();
                addressField.Text = dataGridViewBookingLite.CurrentRow.Cells[5].Value.ToString();
                ID = Convert.ToInt32(dataGridViewBookingLite.CurrentRow.Cells[0].Value.ToString());
                saveButton.Text = "Update";
                deleteButton.Enabled = true;
            }
        }
        private void firstNameField_TextChanged(object sender, EventArgs e)
        {

        }
        private void lastName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
