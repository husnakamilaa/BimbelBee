using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;


namespace BimbelBee
{
    public partial class dashboard: Form
    {
        private string connectionString = "Data Source=DESKTOP-7QP727C\\HUSNAKAMILA;Initial Catalog=BIMBELBEE;Integrated Security=True";

        public dashboard()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; // biar posisi ditengah

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }


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

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report ReportForm = new Report();
            ReportForm.Show();
            this.Hide();
        }
    }
}
