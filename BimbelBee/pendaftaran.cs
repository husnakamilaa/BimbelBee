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
    public partial class pendaftaran : Form
    {
        private string connectionString = "Data Source=DESKTOP-7QP727C\\HUSNAKAMILA;Initial Catalog=BIMBELBEE;Integrated Security=True";
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) };
        private const string CacheKey = "PendaftaranData";

        public pendaftaran()
        {
            InitializeComponent();
            txtTotalBayar.ReadOnly = true;
            txtIDMapel.TextChanged += txtIDMapel_TextChanged;
            this.StartPosition = FormStartPosition.CenterScreen; // biar posisi ditengah
        }

        private void pendaftaran_Load(object sender, EventArgs e)
        {
            try
            {
                dgvPendaftaran.DefaultCellStyle.BackColor = Color.White;
                dgvPendaftaran.DefaultCellStyle.ForeColor = Color.Black;
                dgvPendaftaran.DefaultCellStyle.SelectionForeColor = Color.White;
                EnsureIndexes();
                LoadData();

                txtTglDaftar.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtTglDaftar.ReadOnly = true;      
                txtTglDaftar.TabStop = false;      

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat memuat data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        private void EnsureIndexes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string indexScript = @"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.indexes WHERE name = 'idx_pendaftaran_tgl_daftar' AND object_id = OBJECT_ID('pendaftaran')
                )
                BEGIN
                    CREATE NONCLUSTERED INDEX idx_pendaftaran_tgl_daftar ON pendaftaran(tgl_daftar);
                END";

                using (SqlCommand cmd = new SqlCommand(indexScript, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AnalyzeQueryPerformance(string sqlQuery)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.InfoMessage += (s, e) => MessageBox.Show(e.Message, "STATISTICS INFO");
                conn.Open();
                string wrapped = $@"
                    SET STATISTICS IO ON;
                    SET STATISTICS TIME ON;
                    {sqlQuery}
                    SET STATISTICS TIME OFF;
                    SET STATISTICS IO OFF;";

                using (SqlCommand cmd = new SqlCommand(wrapped, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void ClearPendaftaran()
        {
            txtIDDaftar.Clear();
            txtNISN.Clear();
            txtIDMapel.Clear();
            txtTotalBayar.Clear();
            txtTglDaftar.Text = DateTime.Today.ToString("yyyy-MM-dd");

            txtIDDaftar.Focus();
        }
        private void LoadData()
        {
            if (_cache.Contains(CacheKey))
            {
                dgvPendaftaran.DataSource = _cache.Get(CacheKey);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_lihat_pendaftaran", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dgvPendaftaran.DataSource = dt;
                            _cache.Add(CacheKey, dt, _policy);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal memuat data: {ex.Message}", "Error");
                }
            }


        }

        private bool IsValidMapel(string idMapel)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM mapel WHERE id_mapel = @id_mapel";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_mapel", idMapel);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private bool IsValidSiswa(string nisn)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM siswa WHERE nisn = @nisn";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nisn", nisn);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private bool IsValidIDPendaftaran(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            if (!id.StartsWith("R") || id.Length != 6) return false;
            string angka = id.Substring(1);
            return int.TryParse(angka, out int numericValue) && numericValue > 0;
        }

        private bool IsTodayDate(string tglInput)
        {
            return DateTime.TryParse(tglInput, out DateTime inputDate) && inputDate.Date == DateTime.Today;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtIDDaftar.Text) ||
                string.IsNullOrWhiteSpace(txtNISN.Text) ||
                string.IsNullOrWhiteSpace(txtIDMapel.Text) ||
                string.IsNullOrWhiteSpace(txtTglDaftar.Text))
            {
                lblMessageDaftar.Text = "Semua kolom wajib diisi dong!!";
                return false;
            }

            if (!DateTime.TryParse(txtTglDaftar.Text, out DateTime _))
            {
                lblMessageDaftar.Text = "Tanggal tidak valid.";
                return false;
            }

            if (!IsValidIDPendaftaran(txtIDDaftar.Text.Trim())) // <--- PANGGILAN PERTAMA
            {
                lblMessageDaftar.Text = "ID Pendaftaran tidak valid. Harus diawali 'R' dan diikuti 5 digit angka dan dimulai dari '00001'.";
                return false;
            }

            if (!IsValidSiswa(txtNISN.Text.Trim()))
            {
                lblMessageDaftar.Text = "NISN tidak terdaftar.";
                return false;
            }

            if (!IsValidMapel(txtIDMapel.Text.Trim()))
            {   
                lblMessageDaftar.Text = "ID Mapel tidak ditemukan.";
                return false;
            }


            return true;
        }

        private void btnTambahDaftar_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            GetHargaMapel(txtIDMapel.Text.Trim());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_tambah_pendaftaran", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_pendaftaran", txtIDDaftar.Text.Trim());
                        cmd.Parameters.AddWithValue("@nisn", txtNISN.Text.Trim());
                        cmd.Parameters.AddWithValue("@id_mapel", txtIDMapel.Text.Trim());
                        cmd.Parameters.AddWithValue("@total_pembayaran", 0);
                        cmd.Parameters.AddWithValue("@tgl_daftar", DateTime.Parse(txtTglDaftar.Text.Trim()));

                        cmd.ExecuteNonQuery();
                        
                        transaction.Commit();
                        lblMessageDaftar.Text = "Data berhasil ditambahkan!";
                        _cache.Remove(CacheKey); 
                        LoadData();
                        ClearPendaftaran();
                    }
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();

                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        lblMessageDaftar.Text = "ID Pendaftaran sudah terdaftar! Gunakan ID yang berbeda.";
                    }
                    else
                    {
                        lblMessageDaftar.Text = "Terjadi error SQL: " + ex.Message;
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    lblMessageDaftar.Text = "Error tak terduga: " + ex.Message;
                }
            }

        }

        private void btnEditDaftar_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            GetHargaMapel(txtIDMapel.Text.Trim());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_update_pendaftaran", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_pendaftaran", txtIDDaftar.Text.Trim());
                        cmd.Parameters.AddWithValue("@nisn", txtNISN.Text.Trim());
                        cmd.Parameters.AddWithValue("@id_mapel", txtIDMapel.Text.Trim());
                        cmd.Parameters.AddWithValue("@total_pembayaran", int.Parse(txtTotalBayar.Text.Trim()));
                        cmd.Parameters.AddWithValue("@tgl_daftar", DateTime.Parse(txtTglDaftar.Text.Trim()));

                        cmd.ExecuteNonQuery();
                        transaction.Commit();

                        lblMessageDaftar.Text= "Data berhasil diperbarui!";
                        _cache.Remove(CacheKey);
                        LoadData();
                        ClearPendaftaran();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblMessageDaftar.Text = "Error: " + ex.Message;
                }
            }


        }
        private void txtIDMapel_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT harga FROM mapel WHERE id_mapel = @id_mapel";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_mapel", txtIDMapel.Text.Trim());
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            txtTotalBayar.Text = result.ToString();
                        else
                            txtTotalBayar.Clear();
                    }
                }
                catch
                {
                    txtTotalBayar.Clear();
                }
            }

        }

        private void GetHargaMapel(string idMapel)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT harga FROM mapel WHERE id_mapel = @id_mapel";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_mapel", idMapel);
                        object result = cmd.ExecuteScalar();

                        if (result != null && decimal.TryParse(result.ToString(), out decimal harga))
                        {
                            txtTotalBayar.Text = harga.ToString("0.##"); // format 2 desimal opsional
                        }
                        else
                        {
                            txtTotalBayar.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat mengambil harga mapel: " + ex.Message);
                }
            }
        }


        private void btnHapusDaftar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDDaftar.Text))
            {
                lblMessageDaftar.Text = "Pilih data terlebih dahulu!";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_hapus_pendaftaran", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_pendaftaran", txtIDDaftar.Text.Trim());

                        cmd.ExecuteNonQuery();
                        transaction.Commit();

                        lblMessageDaftar.Text = "Data berhasil dihapus!";
                        _cache.Remove(CacheKey);
                        LoadData();
                        ClearPendaftaran();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblMessageDaftar.Text = "Error: " + ex.Message;
                }
            }

        }

        private void btnRefreshDaftar_Click(object sender, EventArgs e)
        {
            _cache.Remove(CacheKey);
            LoadData();
            lblMessageDaftar.Text = "Data berhasil diperbarui!";
        }

        private void dgvPendaftaran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPendaftaran.Rows[e.RowIndex];
                txtIDDaftar.Text = row.Cells["id_pendaftaran"].Value.ToString();
                txtNISN.Text = row.Cells["nisn"].Value.ToString();
                txtIDMapel.Text = row.Cells["id_mapel"].Value.ToString();
                txtTotalBayar.Text = row.Cells["total_pembayaran"].Value.ToString();
                txtTglDaftar.Text = Convert.ToDateTime(row.Cells["tgl_daftar"].Value).ToString("yyyy-MM-dd");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            dashboard dashboardForm = new dashboard();
            dashboardForm.Show();
            this.Hide();
        }



        private void dgvPendaftaran_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAnalyzeDaftar_Click(object sender, EventArgs e)
        {
            var heavyQuery = "SELECT nisn, total_pembayaran FROM dbo.pendaftaran WHERE tgl_daftar < '2025-07-01'";

            AnalyzeQueryPerformance(heavyQuery);
        }

        private void txtTotalBayar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}