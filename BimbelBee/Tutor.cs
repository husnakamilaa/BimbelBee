using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.Caching; // Untuk MemoryCache

namespace BimbelBee
{
    public partial class Tutor : Form
    {
        private readonly string connectionString = "Data Source=DESKTOP-7QP727C\\HUSNAKAMILA;Initial Catalog=BIMBELBEE;Integrated Security=True";
        private MemoryCache cache = MemoryCache.Default;

        public Tutor()
        {
            InitializeComponent();
        }

        private void Tutor_Load(object sender, EventArgs e)
        {
            EnsureIndex();
            LoadData();
        }

        private void EnsureIndex()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string checkIndexQuery = @"
                    IF NOT EXISTS (
                        SELECT 1 FROM sys.indexes 
                        WHERE name = 'IX_Tutor_Nama' AND object_id = OBJECT_ID('tutor')
                    )
                    BEGIN
                        CREATE NONCLUSTERED INDEX IX_Tutor_Nama ON tutor(nama_tutor);
                    END";

                    using (SqlCommand cmd = new SqlCommand(checkIndexQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memastikan index: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ClearTutor()
        {
            txtIDTutor.Clear();
            txtNama.Clear();
            txtTelepon.Clear();
            txtIDTutor.Focus();
        }

        private void LoadData()
        {
            DataTable dt;

            if (cache.Contains("TutorData"))
            {
                dt = cache["TutorData"] as DataTable;
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_GetAllTutors", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);

                        // Simpan hasil ke cache selama 5 menit
                        var policy = new CacheItemPolicy
                        {
                            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
                        };
                        cache.Set("TutorData", dt, policy);
                    }
                    catch (Exception ex)
                    {
                        lblMessageTutor.Text = "Gagal memuat data: " + ex.Message;
                        return;
                    }
                }
            }

            dgvTutor.DataSource = dt;
            ClearTutor();
        }

        private bool ValidasiInput()
        {
            string id = txtIDTutor.Text.Trim();
            string nama = txtNama.Text.Trim();
            string telp = txtTelepon.Text.Trim();

            if (string.IsNullOrWhiteSpace(id) || !Regex.IsMatch(id, @"^T\d+$") || id.Equals("T000"))
            {
                lblMessageTutor.Text = "ID Tutor harus diawali 'T' dan tidak boleh T000";
                return false;
            }

            if (string.IsNullOrWhiteSpace(nama) || !Regex.IsMatch(nama, @"^[a-zA-Z\s]+$"))
            {
                lblMessageTutor.Text = "Nama hanya boleh huruf dan tidak boleh kosong";
                return false;
            }

            if (string.IsNullOrWhiteSpace(telp) || !Regex.IsMatch(telp, @"^08\d{8,11}$"))
            {
                lblMessageTutor.Text = "Nomor telepon harus diawali 08 dan memiliki 10–13 digit";
                return false;
            }

            return true;
        }

        private void btnTambahTutor_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertTutor", conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_tutor", txtIDTutor.Text.Trim());
                    cmd.Parameters.AddWithValue("@nama_tutor", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@notelp", txtTelepon.Text.Trim());

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    cache.Remove("TutorData"); // Refresh cache
                    lblMessageTutor.Text = "Tutor berhasil ditambahkan.";
                    LoadData();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblMessageTutor.Text = "Gagal menambahkan data: " + ex.Message;
                }
            }
        }

        private void btnEditTutor_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            if (string.IsNullOrWhiteSpace(txtIDTutor.Text))
            {
                lblMessageTutor.Text = "Silakan pilih data yang akan diedit dari tabel.";
                return;
            }

            DialogResult confirm = MessageBox.Show("Yakin ingin mengubah data?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateTutor", conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_tutor", txtIDTutor.Text.Trim());
                    cmd.Parameters.AddWithValue("@nama_tutor", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@notelp", txtTelepon.Text.Trim());

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        transaction.Commit();
                        cache.Remove("TutorData"); // Refresh cache
                        lblMessageTutor.Text = "Data berhasil diperbarui.";
                        LoadData();
                    }
                    else
                    {
                        transaction.Rollback();
                        lblMessageTutor.Text = "Data tidak ditemukan.";
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblMessageTutor.Text = "Gagal mengubah data: " + ex.Message;
                }
            }
        }

        private void btnHapusTutor_Click(object sender, EventArgs e)
        {
            if (dgvTutor.SelectedRows.Count == 0)
            {
                lblMessageTutor.Text = "Pilih data yang akan dihapus.";
                return;
            }

            string id = dgvTutor.SelectedRows[0].Cells["id_tutor"].Value.ToString();
            DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteTutor", conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_tutor", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        transaction.Commit();
                        cache.Remove("TutorData"); // Refresh cache
                        lblMessageTutor.Text = "Data berhasil dihapus.";
                        LoadData();
                    }
                    else
                    {
                        transaction.Rollback();
                        lblMessageTutor.Text = "Data tidak ditemukan.";
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblMessageTutor.Text = "Gagal menghapus data: " + ex.Message;
                }
            }
        }

        private void btnRefreshTutor_Click(object sender, EventArgs e)
        {
            cache.Remove("TutorData"); // Pastikan refresh tidak ambil dari cache lama
            LoadData();
        }

        private void dgvTutor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTutor.Rows[e.RowIndex];
                txtIDTutor.Text = row.Cells[0].Value?.ToString();
                txtNama.Text = row.Cells[1].Value?.ToString();
                txtTelepon.Text = row.Cells[2].Value?.ToString();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAnalyzeTutor_Click(object sender, EventArgs e)
        {
            var statsBuilder = new StringBuilder();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.InfoMessage += (s, args) =>
                {
                    if (!string.IsNullOrWhiteSpace(args.Message))
                    {
                        statsBuilder.AppendLine(args.Message);
                    }
                };

                string queryToAnalyze = @"
                    SET STATISTICS TIME ON;
                    SET STATISTICS IO ON;
                    
                    EXEC sp_GetAllTutors;
                    
                    SET STATISTICS TIME OFF;
                    SET STATISTICS IO OFF;";

                using (var cmd = new SqlCommand(queryToAnalyze, conn))
                {
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.NextResult()) { }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saat menganalisis query: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            string finalOutput = FormatStatsOutput(statsBuilder.ToString());
            MessageBox.Show(finalOutput, "STATISTICS INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string FormatStatsOutput(string rawStats)
        {
            var lines = rawStats.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var formattedBuilder = new StringBuilder();

            string parseAndCompileTime = "";
            string ioStats = "";
            string executionTime = "";

            foreach (var line in lines)
            {
                if (line.StartsWith("SQL Server parse and compile time:"))
                {
                    parseAndCompileTime = line;
                }
                else if (line.Trim().StartsWith("CPU time ="))
                {
                    if (string.IsNullOrEmpty(parseAndCompileTime))
                    {
                        parseAndCompileTime += "\n" + line.Trim();
                    }
                    else
                    {
                        executionTime = line.Trim();
                    }
                }
                else if (line.StartsWith("Table"))
                {
                    ioStats = line;
                }
            }

            formattedBuilder.AppendLine(parseAndCompileTime);
            formattedBuilder.AppendLine(ioStats);
            formattedBuilder.AppendLine();
            formattedBuilder.AppendLine("SQL Server Execution Times:");
            formattedBuilder.AppendLine(executionTime);

            return formattedBuilder.ToString();
        }
    }
}
