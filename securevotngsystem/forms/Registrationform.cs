using securevotngsystem.forms;
using securevotngsystem.Singleton;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace securevotngsystem
{
    public partial class Registrationform : Form
    {
        public Registrationform()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Email validation
            if (string.IsNullOrWhiteSpace(email) || email == "Enter your email")
            {
                errorProvider.SetError(txtEmail, "Email is required");
                return;
            }
            else if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider.SetError(txtEmail, "Invalid email format");
                return;
            }

            // Password validation
            if (string.IsNullOrWhiteSpace(password))
            {
                errorProvider.SetError(txtPassword, "Password is required");
                return;
            }

            // Confirm password validation
            if (password != confirmPassword)
            {
                errorProvider.SetError(txtConfirmPassword, "Passwords do not match");
                return;
            }

            try
            {
                SqlConnection conn = DbManager.Instance.Connection;

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                // Check if email already exists
                using (var checkEmailCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", conn))
                {
                    checkEmailCmd.Parameters.AddWithValue("@Email", email);
                    int count = (int)checkEmailCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        errorProvider.SetError(txtEmail, "Email already registered");
                        MessageBox.Show("Email is already registered. Redirecting to login...");

                        this.Hide();
                        new loginform().Show();
                        return;  // Stop registration here
                    }
                }

                // Insert new user if email is unique
                using (var cmd = new SqlCommand("INSERT INTO Users (Email, PasswordHash) VALUES (@Email, @PasswordHash)", conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    string hashedPassword = PasswordHelper.HashPassword(password);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Registration successful!");

                this.Hide(); // Hide registration form
                new loginform().Show(); // Show login form
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique constraint violation (if DB constraint exists)
                {
                    errorProvider.SetError(txtEmail, "Email already registered");
                    MessageBox.Show("Email is already registered. Redirecting to login...");

                    this.Hide();
                    new loginform().Show();
                }
                else
                {
                    MessageBox.Show($"Database Error: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error: {ex.Message}");
            }
        }

        private void btnLoginRedirect_Click(object sender, EventArgs e)
        {
            this.Hide();
            new loginform().Show();
        }

        private void Registrationform_Load(object sender, EventArgs e)
        {
            // Optional: Any initialization code here
        }
    }
}