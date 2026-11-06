using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vehicle_Rental_Management_System
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            // Hide password characters
            txtPassword.UseSystemPasswordChar = true;

            // Press Enter to login
            this.AcceptButton = btnLogin;

            // Press Escape to exit
            this.CancelButton = btnExit;

            // Connect Login button
            btnLogin.Click -= btnLogin_Click;
            btnLogin.Click += btnLogin_Click;

            // Connect Exit button
            btnExit.Click -= btnExit_Click;
            btnExit.Click += btnExit_Click;
        }

        // LOGIN BUTTON
        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Check if fields are empty
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Please enter your username and password.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Admin login (demo only)
            if (username == "admin" &&
                password == "admin123")
            {
                MessageBox.Show(
                    "Welcome, Admin!",
                    "Login Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Hide Login Form
                this.Hide();

                // Open Vehicle Rental Management System
                using (Form1 mainForm = new Form1())
                {
                    mainForm.ShowDialog();
                }

                // Close application after main form closes
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Invalid username or password.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        // EXIT BUTTON
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // LOGIN FORM LOAD
        private void frmLogin_Load(object sender, EventArgs e)
        {
            // No additional code needed.
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {

        }

        private void lblUsername_Click_1(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click_1(object sender, EventArgs e)
        {

        }
    }
}
