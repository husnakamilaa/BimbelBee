using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BimbelBee
{
    public partial class dashboard : Form
    {
        // Variabel untuk fungsionalitas drag form
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        public dashboard()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            // Mengaktifkan Double Buffering untuk mengurangi flicker saat resize
            this.DoubleBuffered = true;
            SetupEventHandlers();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Event ini bisa Anda gunakan jika ada data yang perlu dimuat saat dashboard terbuka
        }

        private void SetupEventHandlers()
        {
            // Event untuk menggambar gradasi pada background
            pnlContent.Paint += new PaintEventHandler(pnlContent_Paint);
            // Event untuk menggambar ulang saat ukuran berubah
            pnlContent.Resize += new EventHandler(pnlContent_Resize);

            // Event handler untuk kontrol jendela kustom
            btnClose.Click += new EventHandler(btnClose_Click);
            btnMaximize.Click += new EventHandler(btnMaximize_Click);
            btnMinimize.Click += new EventHandler(btnMinimize_Click);

            // Event handler untuk fungsionalitas drag form
            pnlHeader.MouseDown += new MouseEventHandler(Form_MouseDown);
            pnlHeader.MouseMove += new MouseEventHandler(Form_MouseMove);
            pnlHeader.MouseUp += new MouseEventHandler(Form_MouseUp);
            lblTitle.MouseDown += new MouseEventHandler(Form_MouseDown);
            lblTitle.MouseMove += new MouseEventHandler(Form_MouseMove);
            lblTitle.MouseUp += new MouseEventHandler(Form_MouseUp);
        }

        #region Navigasi Menu (Sesuai Kode Asli Anda)

        private void btnTblSiswa_Click(object sender, EventArgs e)
        {
            formSiswa siswaForm = new formSiswa();
            siswaForm.Show();
            this.Hide();
        }

        private void btnTblMapel_Click(object sender, EventArgs e)
        {
            Mapel mapelForm = new Mapel();
            mapelForm.Show();
            this.Hide();
        }

        private void btnTblTutor_Click(object sender, EventArgs e)
        {
            Tutor tutorForm = new Tutor();
            tutorForm.Show();
            this.Hide();
        }

        private void btnTblPendaftaran_Click(object sender, EventArgs e)
        {
            pendaftaran pendaftaranForm = new pendaftaran();
            pendaftaranForm.Show();
            this.Hide();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report ReportForm = new Report();
            ReportForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Yakin ingin keluar dari aplikasi?",
                "Konfirmasi Keluar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        #endregion

        #region Kontrol Jendela Kustom

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnMaximize.Text = "❐"; // Karakter untuk restore
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                btnMaximize.Text = "☐"; // Karakter untuk maximize
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region Fungsionalitas UI (Gradasi & Drag)

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {
            // Gradasi warna kuning khas Bumblebee
            using (LinearGradientBrush brush = new LinearGradientBrush(
                pnlContent.ClientRectangle,
                Color.FromArgb(255, 209, 0),   // Warna utama Bumblebee Yellow (#FFD100)
                Color.FromArgb(255, 170, 0),     // Warna kuning lebih gelap untuk bayangan (#FFAA00)
                90F))
            {
                e.Graphics.FillRectangle(brush, pnlContent.ClientRectangle);
            }
        }

        private void pnlContent_Resize(object sender, EventArgs e)
        {
            // Memaksa panel untuk menggambar ulang dirinya sendiri saat ukurannya berubah
            pnlContent.Invalidate();
        }

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

        #endregion

        #region Kode untuk Resize Jendela Borderless

        // Override WndProc untuk menangani pesan window
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 0x1;
            const int HTCAPTION = 0x2;
            const int RESIZE_HANDLE_SIZE = 10;

            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);
                // Hanya proses jika kursor tidak berada di atas area klien (seperti tombol, dll)
                if ((int)m.Result == HTCLIENT)
                {
                    Point screenPoint = new Point(m.LParam.ToInt32());
                    Point clientPoint = this.PointToClient(screenPoint);

                    // Cek jika kursor berada di tepi untuk resize
                    if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                    {
                        if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                            m.Result = (IntPtr)13; // HTTOPLEFT
                        else if (clientPoint.X >= (this.ClientSize.Width - RESIZE_HANDLE_SIZE))
                            m.Result = (IntPtr)14; // HTTOPRIGHT
                        else
                            m.Result = (IntPtr)12; // HTTOP
                    }
                    else if (clientPoint.Y >= (this.ClientSize.Height - RESIZE_HANDLE_SIZE))
                    {
                        if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                            m.Result = (IntPtr)16; // HTBOTTOMLEFT
                        else if (clientPoint.X >= (this.ClientSize.Width - RESIZE_HANDLE_SIZE))
                            m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                        else
                            m.Result = (IntPtr)15; // HTBOTTOM
                    }
                    else if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                        m.Result = (IntPtr)10; // HTLEFT
                    else if (clientPoint.X >= (this.ClientSize.Width - RESIZE_HANDLE_SIZE))
                        m.Result = (IntPtr)11; // HTRIGHT
                }
                return;
            }
            base.WndProc(ref m);
        }

        #endregion
    }
}