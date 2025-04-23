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

namespace BimbelBee
{
    public partial class pendaftaran: Form
    {
        private string connectionString = "Data Source=DESKTOP-7QP727C\\HUSNAKAMILA;Initial Catalog=BIMBELBEE;Integrated Security=True";
        public pendaftaran()
        {
            InitializeComponent();
        }

        private void pendaftaran_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void ClearPendaftaran()
        {
            txtIDDaftar.Clear();
            txtNISN.Clear();
            txtIDMapel.Clear();
            txtTotalBayar.Clear();
            txtTglDaftar.Clear();

            txtIDDaftar.Focus();
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id_pendaftaran AS [id_pendaftaran], nisn, id_mapel, total_pembayaran, tgl_daftar FROM BIMBELBEE.dbo.pendaftaran";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvPendaftaran.AutoGenerateColumns = true;
                    dgvPendaftaran.DataSource = dt;

                    ClearPendaftaran();//Autoclear stlh load data
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void btnTambahDaftar_Click(object sender, EventArgs e)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    if (txtIDDaftar.Text == "" || txtNISN.Text == "" || txtIDMapel.Text == "" || txtTotalBayar.Text == "" || txtTglDaftar.Text == "")
                    {
                        MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Validasi apakah id_mapel dan nisn ada di tabel mapel dan siswa
                    if (!IsValidMapel(txtIDMapel.Text.Trim()))
                    {
                        MessageBox.Show("ID Mapel tidak valid! Pastikan ID Mapel sudah terdaftar.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!IsValidSiswa(txtNISN.Text.Trim()))
                    {
                        MessageBox.Show("NISN tidak valid! Pastikan NISN sudah terdaftar.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    conn.Open();
                    string query = "INSERT INTO pendaftaran (id_pendaftaran, nisn, id_mapel, total_pembayaran, tgl_daftar) VALUES (@id_pendaftaran, @nisn, @id_mapel, @total_pembayaran, @tgl_daftar)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_pendaftaran", txtIDDaftar.Text.Trim());
                        cmd.Parameters.AddWithValue("@nisn", txtNISN.Text.Trim());
                        cmd.Parameters.AddWithValue("@id_mapel", txtIDMapel.Text.Trim());
                        cmd.Parameters.AddWithValue("@total_pembayaran", txtTotalBayar.Text.Trim());
                        cmd.Parameters.AddWithValue("@tgl_daftar", txtTglDaftar.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearPendaftaran(); // Auto Clear setelah tambah data
                        }
                        else
                        {
                            MessageBox.Show("Data tidak berhasil ditambahkan!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapusDaftar_Click(object sender, EventArgs e)
        {
            if (dgvPendaftaran.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            string id_pendaftaran = dgvPendaftaran.SelectedRows[0].Cells["nisn"].Value.ToString();
                            conn.Open();
                            string query = "DELETE FROM pendaftaran WHERE id_pendaftaran = @id_pendaftaran";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@id_pendaftaran", id_pendaftaran);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                    ClearPendaftaran(); // Auto clear setelah hapus data
                                }
                                else
                                {
                                    MessageBox.Show("Data tidak ditemukan atau gagal dihapus!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEditDaftar_Click(object sender, EventArgs e)
        {
            if (txtIDDaftar.Text == "")
            {
                MessageBox.Show("Silakan pilih data yang ingin diedit dari tabel!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Yakin ingin menyimpan perubahan?", "Konfirmasi Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE pendaftaran SET nisn = @nisn, id_mapel = @id_mapel, total_pembayaran = @total_pembayaran, tgl_daftar = @tgl_daftar WHERE id_pendaftaran = @id_pendaftaran";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id_pandaftaran", txtIDDaftar.Text.Trim());
                            cmd.Parameters.AddWithValue("@nisn", txtNISN.Text.Trim());
                            cmd.Parameters.AddWithValue("@id_mapel", txtIDMapel.Text.Trim());
                            cmd.Parameters.AddWithValue("@total_pembayaran", txtTotalBayar.Text.Trim());
                            cmd.Parameters.AddWithValue("@tgl_daftar", txtTglDaftar.Text.Trim());

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Perubahan berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                                ClearPendaftaran();
                            }
                            else
                            {
                                MessageBox.Show("Gagal menyimpan perubahan!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRefreshDaftar_Click(object sender, EventArgs e)
        {
            LoadData();

            // Cek jumlah kolom dan baris
            MessageBox.Show($"Jumlah Kolom: {dgvPendaftaran.ColumnCount}\nJumlah Baris: {dgvPendaftaran.RowCount}",
                "Debugging DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvPendaftaran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPendaftaran.Rows[e.RowIndex];

                //Coba gunakan indeks jika "NISN" tidak ditemukan
                txtIDDaftar.Text = row.Cells[0].Value.ToString();
                txtNISN.Text = row.Cells[1].Value?.ToString();
                txtIDMapel.Text = row.Cells[2].Value?.ToString();
                txtTotalBayar.Text = row.Cells[3].Value?.ToString();
                txtTglDaftar.Text = row.Cells[4].Value?.ToString();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            dashboard dashboardForm = new dashboard();
            dashboardForm.Show();
            this.Hide();
        }
    }
}
