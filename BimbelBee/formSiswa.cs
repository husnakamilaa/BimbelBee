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
using System.Runtime.Caching;

namespace BimbelBee
{
    public partial class formSiswa : Form
    {
        private string connectionString = "Data Source=DESKTOP-7QP727C\\HUSNAKAMILA;Initial Catalog=BIMBELBEE;Integrated Security=True";

        private MemoryCache cache = MemoryCache.Default;
        private CacheItemPolicy cachePolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        private const string CacheKey = "SiswaDataCache";

        public formSiswa()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; // biar posisi ditengah
        }

        private void Siswa_load(object sender, EventArgs e)
        {
            LoadData();
            EnsureIndex();

        }

        // Fungsi untuk mengosongkan semua input pada textbox
        private void ClearSiswa()
        {
            txtNISN.Clear();
            txtNama.Clear();
            txtNotelp.Clear();
            txtAlamat.Clear();
            txtEmail.Clear();

            txtNISN.Focus(); // Fokus ke NISN untuk input data baru
        }

        // Fungsi untuk menampilkan data di DataGridView
        private void LoadData()
        {
            DataTable dt = cache.Get(CacheKey) as DataTable;

            if (dt == null)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    string query = "SELECT nisn, nama, notelp, alamat, email FROM siswa ORDER BY nama";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        dt = new DataTable();
                        da.Fill(dt);

                        // Cache 5 menit
                        CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) };
                        cache.Set(CacheKey, dt, policy);
                    }
                }
            }

            dgvSiswa.DataSource = dt;
            ClearSiswa();
        }

        private void EnsureIndex()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string checkAndCreateIndex = @"
            IF NOT EXISTS (
                SELECT 1 
                FROM sys.indexes
                WHERE name = 'idx_namaSiswa'
                AND object_id = OBJECT_ID('dbo.siswa')
            )
            BEGIN
                CREATE NONCLUSTERED INDEX idx_namaSiswa ON dbo.siswa (nama)
            END";


                using (SqlCommand cmd = new SqlCommand(checkAndCreateIndex, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AnalyzeQuery(string sqlQuery)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.InfoMessage += (s, e) => MessageBox.Show(e.Message, "STATISTICS INFO"); // Handle pesan dari SET STATISTICS

                try
                {
                    conn.Open();

                    string wrapped = $@"
                            SET STATISTICS IO ON;
                            SET STATISTICS TIME ON;
                            {sqlQuery};
                            SET STATISTICS IO OFF;
                            SET STATISTICS TIME OFF;
                        ";

                    using (SqlCommand cmd = new SqlCommand(wrapped, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat menganalisis query: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // Tombol Tambah dengan transaksi dan error handling
        private void btnTambah_Click(object sender, EventArgs e)
        {
            string nisn = txtNISN.Text.Trim();
            string nama = txtNama.Text.Trim();
            string noTelp = txtNotelp.Text.Trim();
            string alamat = txtAlamat.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(nisn) || string.IsNullOrEmpty(nama) ||
                string.IsNullOrEmpty(noTelp) || string.IsNullOrEmpty(alamat) ||
                string.IsNullOrEmpty(email))
            {
                lblMessageSiswa.Text = "Semua kolom harus diisi!";
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(nisn, @"^\d{10}$"))
            {
                lblMessageSiswa.Text = "NISN harus berupa 10 digit angka!";
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(nama, @"^[a-zA-Z\s]+$"))
            {
                lblMessageSiswa.Text = "Nama hanya boleh berisi huruf dan tanpa spasi!";
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(noTelp, @"^\d{10,13}$"))
            {
                lblMessageSiswa.Text = "No Telepon harus berupa angka dan terdiri dari 10-13 digit!";
                return;
            }

            if (!noTelp.StartsWith("08"))
            {
                lblMessageSiswa.Text = "No Telepon harus diawali dengan '08'!";
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@gmail\.com$"))
            {
                lblMessageSiswa.Text = "Email harus berformat @gmail.com!";
                return;
            }


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertSiswa", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nisn", nisn);
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@notelp", noTelp);
                        cmd.Parameters.AddWithValue("@alamat", alamat);
                        cmd.Parameters.AddWithValue("@email", email);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            cache.Remove(CacheKey);
                            lblMessageSiswa.Text = "Data berhasil ditambahkan!";
                            LoadData();
                            ClearSiswa();
                        }
                        else
                        {
                            transaction.Rollback();
                            lblMessageSiswa.Text = "Data gagal ditambahkan!";
                        }
                    }
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        lblMessageSiswa.Text = "ID Mapel sudah terdaftar! Gunakan ID yang berbeda.";
                    }
                    else
                    {
                        lblMessageSiswa.Text = "Terjadi error SQL: " + ex.Message;
                    }
                }
            }
        }



        // Tombol Hapus dengan transaksi dan error handling
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvSiswa.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            string nisn = dgvSiswa.SelectedRows[0].Cells["nisn"].Value.ToString();
                            using (SqlCommand cmd = new SqlCommand("sp_DeleteSiswa", conn, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@nisn", nisn);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    transaction.Commit();
                                    cache.Remove(CacheKey);
                                    lblMessageSiswa.Text = "Data berhasil dihapus!";
                                    LoadData();
                                    ClearSiswa();
                                }
                                else
                                {
                                    transaction.Rollback();
                                    lblMessageSiswa.Text = "Data tidak ditemukan atau gagal dihapus!";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            lblMessageSiswa.Text = "Error: " + ex.Message;
                        }
                    }
                }
            }
            else
            {
                lblMessageSiswa.Text = "Pilih data yang akan dihapus!";
            }
        }


        // Tombol Edit dengan transaksi dan error handling

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtNISN.Text == "")
            {
                lblMessageSiswa.Text = "Silakan pilih data yang ingin diedit dari tabel!";
                return;
            }

            string nisn = txtNISN.Text.Trim();
            string nama = txtNama.Text.Trim();
            string noTelp = txtNotelp.Text.Trim();
            string alamat = txtAlamat.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(nisn) || string.IsNullOrEmpty(nama) ||
                string.IsNullOrEmpty(noTelp) || string.IsNullOrEmpty(alamat) ||
                string.IsNullOrEmpty(email))
            {
                lblMessageSiswa.Text = "Semua kolom harus diisi!";
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(nisn, @"^\d{10}$") ||
                !System.Text.RegularExpressions.Regex.IsMatch(nama, @"^[a-zA-Z\s]+$") ||
                !System.Text.RegularExpressions.Regex.IsMatch(noTelp, @"^\d{10,13}$") ||
                !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@gmail\.com$"))
            {
                lblMessageSiswa.Text = "Validasi input gagal!";
                return;
            }

            DialogResult confirm = MessageBox.Show("Yakin ingin menyimpan perubahan?", "Konfirmasi Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_UpdateSiswa", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@nisn", nisn);
                            cmd.Parameters.AddWithValue("@nama", nama);
                            cmd.Parameters.AddWithValue("@notelp", noTelp);
                            cmd.Parameters.AddWithValue("@alamat", alamat);
                            cmd.Parameters.AddWithValue("@email", email);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                transaction.Commit();
                                cache.Remove(CacheKey);
                                lblMessageSiswa.Text = "Perubahan berhasil disimpan!";
                                LoadData();
                                ClearSiswa();
                            }
                            else
                            {
                                transaction.Rollback();
                                lblMessageSiswa.Text = "NISN tidak ditemukan";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        lblMessageSiswa.Text = "Terjadi error SQL: " + ex.Message;
                    }
                }
            }
        }

        // Tombol Refresh untuk memuat ulang data dan menampilkan debug info
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cache.Remove(CacheKey);
            LoadData();

            // Debugging: Tampilkan jumlah kolom dan baris pada DataGridView
            MessageBox.Show($"Jumlah Kolom: {dgvSiswa.ColumnCount}\nJumlah Baris: {dgvSiswa.RowCount}",
                "Debugging DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Mengisi TextBox ketika baris DataGridView diklik
        private void dgvSiswa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSiswa.Rows[e.RowIndex];

                // Gunakan indeks untuk memastikan kolom diambil dengan benar
                txtNISN.Text = row.Cells[0].Value?.ToString();
                txtNama.Text = row.Cells[1].Value?.ToString();
                txtNotelp.Text = row.Cells[2].Value?.ToString();
                txtAlamat.Text = row.Cells[3].Value?.ToString();
                txtEmail.Text = row.Cells[4].Value?.ToString();
            }
        }

        // Tombol Back untuk kembali ke dashboard
        private void btnBack_Click(object sender, EventArgs e)
        {
            dashboard dashboardForm = new dashboard();
            dashboardForm.Show();
            this.Hide();
        }

        private void btnAnalyzeSiswa_Click(object sender, EventArgs e)
        {
            string siswaAnalysisQuery = "SELECT nisn, nama, notelp FROM siswa WHERE nama LIKE 'R%'";
            AnalyzeQuery(siswaAnalysisQuery);
        } 
    }
}
