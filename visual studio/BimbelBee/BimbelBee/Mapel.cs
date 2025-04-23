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
    public partial class Mapel: Form
    {
        private string connectionString = "Data Source=DESKTOP-7QP727C\\HUSNAKAMILA;Initial Catalog=BIMBELBEE;Integrated Security=True";
        public Mapel()
        {
            InitializeComponent();
        }

        private void Mapel_load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearMapel()
        {
            txtIDmapel.Clear();
            txtMapel.Clear();
            txtTingkat.Clear();
            txtRuangan.Clear();
            txtDurasi.Clear();
            txtHarikursus.Clear();
            txtWaktukursus.Clear();
            txtHarga.Clear();
            txtIDTutor.Clear();

            txtIDmapel.Focus();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id_mapel AS [id_mapel], mapel, tingkat, ruangan, durasi, hari_kursus, waktu_kursus, harga, id_tutor FROM BIMBELBEE.dbo.mapel";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvMapel.AutoGenerateColumns = true;
                    dgvMapel.DataSource = dt;

                    ClearMapel();//Autoclear stlh load data
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Method untuk memverifikasi apakah id_tutor valid (ada di tabel tutor)
        private bool IsValidTutor(string idTutor)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM tutor WHERE id_tutor = @id_tutor";  // Pastikan id_tutor ada di tabel tutor
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_tutor", idTutor);
                        int count = (int)cmd.ExecuteScalar();  // Menghitung jumlah tutor dengan id_tutor yang diberikan
                        return count > 0;  // Jika count > 0, berarti id_tutor valid
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;  // Jika ada error, kembalikan false
                }
            }
        }


        private void btnTambahMapel_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    if (txtIDmapel.Text == "" || txtMapel.Text == "" || txtTingkat.Text == "" || txtRuangan.Text == "" || txtDurasi.Text == "" || txtHarikursus.Text == "" || txtWaktukursus.Text == "" || txtHarga.Text == "" || txtIDTutor.Text == "")
                    {
                        MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Validasi apakah id_tutor ada di tabel tutor
                    if (!IsValidTutor(txtIDTutor.Text.Trim()))
                    {
                        MessageBox.Show("ID Tutor tidak valid! Pastikan ID Tutor sudah terdaftar.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    conn.Open();
                    string query = "INSERT INTO mapel (id_mapel, mapel, tingkat, ruangan, durasi, hari_kursus, waktu_kursus, harga, id_tutor) VALUES (@id_mapel, @mapel, @tingkat, @ruangan, @durasi, @hari_kursus, @waktu_kursus, @harga, @id_tutor)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_mapel", txtIDmapel.Text.Trim());
                        cmd.Parameters.AddWithValue("@mapel", txtMapel.Text.Trim());
                        cmd.Parameters.AddWithValue("@tingkat", txtTingkat.Text.Trim());
                        cmd.Parameters.AddWithValue("@ruangan", txtRuangan.Text.Trim());
                        cmd.Parameters.AddWithValue("@durasi", txtDurasi.Text.Trim());
                        cmd.Parameters.AddWithValue("@hari_kursus", txtHarikursus.Text.Trim());
                        cmd.Parameters.AddWithValue("@waktu_kursus", txtWaktukursus.Text.Trim());
                        cmd.Parameters.AddWithValue("@harga", txtHarga.Text.Trim());
                        cmd.Parameters.AddWithValue("@id_tutor", txtIDTutor.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearMapel(); // Auto Clear setelah tambah data
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

        private void btnHapusMapel_Click(object sender, EventArgs e)
        {
            if (dgvMapel.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            string id_mapel = dgvMapel.SelectedRows[0].Cells["id_mapel"].Value.ToString();
                            conn.Open();
                            string query = "DELETE FROM mapel WHERE id_mapel = @id_mapel";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@id_mapel", id_mapel);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                    ClearMapel(); // Auto clear setelah hapus data
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

        private void btnEditMapel_Click(object sender, EventArgs e)
        {
            if (txtIDmapel.Text == "")
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
                        string query = "UPDATE mapel SET mapel = @mapel, tingkat = @tingkat, ruangan = @ruangan, durasi = @durasi, hari_kursus = @hari_kursus, waktu_kursus = @waktu_kursus, harga = @harga, id_tutor = @id_tutor WHERE id_mapel = @id_mapel";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@mapel", txtMapel.Text.Trim());
                            cmd.Parameters.AddWithValue("@tingkat", txtTingkat.Text.Trim());
                            cmd.Parameters.AddWithValue("@ruangan", txtRuangan.Text.Trim());
                            cmd.Parameters.AddWithValue("@durasi", txtDurasi.Text.Trim());
                            cmd.Parameters.AddWithValue("@hari_kursus", txtHarikursus.Text.Trim());
                            cmd.Parameters.AddWithValue("@waktu_kursus", txtWaktukursus.Text.Trim());
                            cmd.Parameters.AddWithValue("@harga", txtHarga.Text.Trim());
                            cmd.Parameters.AddWithValue("@id_tutor", txtIDTutor.Text.Trim());
                            cmd.Parameters.AddWithValue("@id_mapel", txtIDmapel.Text.Trim());

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Perubahan berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                                ClearMapel();
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

        private void btnRefreshMapel_Click(object sender, EventArgs e)
        {
            LoadData();

            // Cek jumlah kolom dan baris
            MessageBox.Show($"Jumlah Kolom: {dgvMapel.ColumnCount}\nJumlah Baris: {dgvMapel.RowCount}",
                "Debugging DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvMapel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMapel.Rows[e.RowIndex];

                //Coba gunakan indeks jika "NISN" tidak ditemukan
                txtIDmapel.Text = row.Cells[0].Value.ToString();
                txtMapel.Text = row.Cells[1].Value?.ToString();
                txtTingkat.Text = row.Cells[2].Value?.ToString();
                txtRuangan.Text = row.Cells[3].Value?.ToString();
                txtDurasi.Text = row.Cells[4].Value?.ToString();
                txtHarikursus.Text = row.Cells[5].Value?.ToString();
                txtWaktukursus.Text = row.Cells[6].Value?.ToString();
                txtHarga.Text = row.Cells[7].Value?.ToString();
                txtIDTutor.Text = row.Cells[8].Value?.ToString();
            }
        }

        private void Mapel_Load_1(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            dashboard dashboardForm = new dashboard();
            dashboardForm.Show();
            this.Hide();
        }
    }
}
