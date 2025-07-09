using Microsoft.Reporting.WinForms;
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
using System.Windows.Media;
using System.IO;

namespace BimbelBee
{
    public partial class Report: Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";

        public Report()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; // biar posisi ditengah
            strKonek = kn.connectionString();

        }

        private void Report_Load(object sender, EventArgs e)
        {
            SetupReportViewer();
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            //string connectionString = "Data Source=DESKTOP-7QP727C\\HUSNAKAMILA;Initial Catalog=BIMBELBEE;Integrated Security=True";
            string query = @"
                SELECT
                  s.nisn,
                  s.nama AS nama_siswa,
                  STRING_AGG(m.mapel, ', ') WITHIN GROUP (ORDER BY m.mapel) AS daftar_mapel,
                  SUM(p.total_pembayaran) AS total_harga,
                  p.tgl_daftar AS tanggal_pendaftaran
                FROM
                    siswa s
                JOIN
                    pendaftaran p ON s.nisn = p.nisn
                JOIN
                    mapel m ON p.id_mapel = m.id_mapel
                GROUP BY
                    s.nisn, s.nama, p.tgl_daftar
                ORDER BY
                    p.tgl_daftar";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(strKonek))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            ReportDataSource rds = new ReportDataSource("DataSet9", dt);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.LocalReport.ReportPath = @"C:\Users\T480s\Source\Repos\BimbelBee\BimbelBee\pendaftaranReport.rdlc"; 
            reportViewer1.RefreshReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            dashboard dashboardForm = new dashboard();
            dashboardForm.Show();
            this.Hide();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
