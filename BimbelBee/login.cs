using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BimbelBee
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            textBox2.PasswordChar = '●'; // buat nyembunyiin passwd
            chk1.CheckedChanged += chk1_CheckedChanged;
            this.StartPosition = FormStartPosition.CenterScreen; // biar posisi ditengah 
        }


        private void Masuk_Click(object sender, EventArgs e)
        {
            // Hardcoded credentials for simplicity
            string validUsername = "administrasi";
            string validPasswordHash = HashPassword("password123");

            // Assuming textBox1 is for Username and textBox2 is for Password
            string enteredUsername = textBox1.Text;
            string enteredPassword = textBox2.Text;
            string enteredPasswordHash = HashPassword(enteredPassword);

            if (string.IsNullOrWhiteSpace(enteredUsername) || string.IsNullOrWhiteSpace(enteredPassword))
            {
                MessageBox.Show("Silakan masukkan username dan password.", "Login Error");
                return;
            }

            if (enteredUsername == validUsername && enteredPasswordHash == validPasswordHash)
            {
                MessageBox.Show("Login berhasil!", "Success");
                // Open the pendaftaran form
                dashboard dashboardForm = new dashboard();
                dashboardForm.Show();
                this.Hide(); // Hide the login form
            }
            else
            {
                MessageBox.Show("Username atau password salah.", "Login Gagal");
                // Clear the password field for retry
                textBox2.Text = "";
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked)
            {
                textBox2.PasswordChar = '\0'; // Menampilkan password
                chk1.Text = "Sembunyikan Password";
            }
            else
            {
                textBox2.PasswordChar = '●'; // Menyembunyikan password
                chk1.Text = "Tampilkan Password";
            }
        }
    }


}
