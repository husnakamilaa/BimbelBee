using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Optional: Center the form on the screen
            this.CenterToScreen();
        }

        private void Masuk_Click(object sender, EventArgs e)
        {
            // Hardcoded credentials for simplicity
            string validUsername = "administrasi";
            string validPassword = "password123";

            // Assuming textBox1 is for Username and textBox2 is for Password
            string enteredUsername = "administrasi";
            string enteredPassword = "password123";

            if (string.IsNullOrWhiteSpace(enteredUsername) || string.IsNullOrWhiteSpace(enteredPassword))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error");
                return;
            }

            if (enteredUsername == validUsername && enteredPassword == validPassword)
            {
                MessageBox.Show("Login successful!", "Success");
                // Open the pendaftaran form
                dashboard dashboardForm = new dashboard();
                dashboardForm.Show();
                this.Hide(); // Hide the login form
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed");
                // Clear the password field for retry
                textBox2.Text = "";
            }
        }
    }

    static class Program
    {
        // Titik masuk utama aplikasi
        [STAThread]
        static void Main()
        {
            // Mengaktifkan visual styles untuk aplikasi Windows Forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Menampilkan form login saat aplikasi dimulai
            Application.Run(new login());
        }
    }
}
