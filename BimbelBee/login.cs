using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BimbelBee
{
    public partial class login : Form
    {
        // Variabel untuk fungsionalitas drag form
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        public login()
        {
            InitializeComponent();
            SetupEventHandlers();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void SetupEventHandlers()
        {
            // --- Event Handlers untuk UI ---
            btnMasuk.Click += new System.EventHandler(this.Masuk_Click);
            chk1.CheckedChanged += new System.EventHandler(this.chk1_CheckedChanged);
            lblClose.Click += new System.EventHandler(this.lblClose_Click);

            // --- Placeholder untuk Username (textBox1) ---
            textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            textBox1.Leave += new System.EventHandler(this.textBox1_Leave);

            // --- Placeholder untuk Password (textBox2) ---
            textBox2.Enter += new System.EventHandler(this.textBox2_Enter);
            textBox2.Leave += new System.EventHandler(this.textBox2_Leave);

            // --- Event untuk menggambar gradasi ---
            pnlMain.Paint += new PaintEventHandler(this.pnlMain_Paint);

            // --- Fungsionalitas Drag Form (karena borderless) ---
            pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            pnlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            pnlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);

            pnlLogin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            pnlLogin.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            pnlLogin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);

            // Inisialisasi awal tampilan placeholder
            textBox1_Leave(null, null);
            textBox2_Leave(null, null);
        }

        // --- Logika Inti Aplikasi (Sama seperti kode Anda) ---

        private void Masuk_Click(object sender, EventArgs e)
        {
            string validUsername = "administrasi";
            string validPasswordHash = HashPassword("password123");

            string enteredUsername = (textBox1.Text == "Username") ? "" : textBox1.Text;
            string enteredPassword = (textBox2.Text == "Password") ? "" : textBox2.Text;
            string enteredPasswordHash = HashPassword(enteredPassword);

            if (string.IsNullOrWhiteSpace(enteredUsername) || string.IsNullOrWhiteSpace(enteredPassword))
            {
                MessageBox.Show("Silakan masukkan username dan password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (enteredUsername == validUsername && enteredPasswordHash == validPasswordHash)
            {
                MessageBox.Show("Login berhasil!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dashboard dashboardForm = new dashboard();
                dashboardForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username atau password salah.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "";
                textBox2_Leave(sender, e);
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
            if (textBox2.Text != "Password")
            {
                textBox2.PasswordChar = chk1.Checked ? '\0' : '●';
            }
        }

        // --- Event Handlers untuk Fungsionalitas UI Baru ---

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {
            // Gradasi warna kuning khas Bumblebee
            using (LinearGradientBrush brush = new LinearGradientBrush(
                pnlMain.ClientRectangle,
                Color.FromArgb(255, 209, 0),   // Warna utama Bumblebee Yellow (#FFD100)
                Color.FromArgb(255, 170, 0),     // Warna kuning lebih gelap untuk bayangan (#FFAA00)
                90F))
            {
                e.Graphics.FillRectangle(brush, pnlMain.ClientRectangle);
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // --- Event Handlers untuk Placeholder Teks ---

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Username")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Username";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Password")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
                textBox2.PasswordChar = '●';
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Password";
                textBox2.ForeColor = Color.Gray;
                textBox2.PasswordChar = '\0';
            }
        }

        // --- Event Handlers untuk Drag Form ---

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = this.Location;
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = new Point(lastForm.X + (Cursor.Position.X - lastCursor.X), lastForm.Y + (Cursor.Position.Y - lastCursor.Y));
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void pnlMain_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}