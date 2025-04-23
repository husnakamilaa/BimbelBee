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
    public partial class Tutor: Form
    {
        private string connectionString = "Data Source=DESKTOP-7QP727C\\HUSNAKAMILA;Initial Catalog=BIMBELBEE;Integrated Security=True";

        public Tutor()
        {
            InitializeComponent();
        }

        private void Tutor_Load(object sender, EventArgs e)
        {
            LoadData();
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id_tutor AS [id_tutor], nama_tutor, notelp FROM BIMBELBEE.dbo.tutor";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvTutor.AutoGenerateColumns = true;
                    dgvTutor.DataSource = dt;

                    ClearTutor();//Autoclear stlh load data
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTambahTutor_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    if (txtIDTutor.Text == "" || txtNama.Text == "" || txtTelepon.Text == "")
                    {
                        MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    conn.Open();
                    string query = "INSERT INTO tutor (id_tutor, nama_tutor, notelp) VALUES (@id_tutor, @nama_tutor, @notelp)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_tutor", txtIDTutor.Text.Trim());
                        cmd.Parameters.AddWithValue("@nama_tutor", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@notelp", txtTelepon.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearTutor(); // Auto Clear setelah tambah data
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

        private void btnHapusTutor_Click(object sender, EventArgs e)
        {
            if (dgvTutor.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            string id_tutor = dgvTutor.SelectedRows[0].Cells["id_mapel"].Value.ToString();
                            conn.Open();
                            string query = "DELETE FROM tutor WHERE id_tutor = @id_tutor";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@id_tutor", id_tutor);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                    ClearTutor(); // Auto clear setelah hapus data
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

        private void btnEditTutor_Click(object sender, EventArgs e)
        {
            if (txtIDTutor.Text == "")
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
                        string query = "UPDATE tutor SET nama_tutor = @nama_tutor, notelp = @notelp WHERE id_tutor = @id_tutor";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@nama_tutor", txtNama.Text.Trim());
                            cmd.Parameters.AddWithValue("@notelp", txtTelepon.Text.Trim());
                            cmd.Parameters.AddWithValue("@id_tutor", txtIDTutor.Text.Trim());

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Perubahan berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                                ClearTutor();
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

        private void btnRefreshTutor_Click(object sender, EventArgs e)
        {
            LoadData();

            // Cek jumlah kolom dan baris
            MessageBox.Show($"Jumlah Kolom: {dgvTutor.ColumnCount}\nJumlah Baris: {dgvTutor.RowCount}",
                "Debugging DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvTutor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTutor.Rows[e.RowIndex];

                //Coba gunakan indeks jika "NISN" tidak ditemukan
                txtIDTutor.Text = row.Cells[0].Value.ToString();
                txtNama.Text = row.Cells[1].Value?.ToString();
                txtTelepon.Text = row.Cells[2].Value?.ToString();
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
