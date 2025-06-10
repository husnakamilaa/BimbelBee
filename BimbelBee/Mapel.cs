    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Runtime.Caching;

    namespace BimbelBee
    {
        public partial class Mapel: Form
        {
            private string connectionString = "Data Source=DESKTOP-7QP727C\\HUSNAKAMILA;Initial Catalog=BIMBELBEE;Integrated Security=True";

            // caching 
            private MemoryCache cache = MemoryCache.Default;
            private CacheItemPolicy cachePolicy = new CacheItemPolicy 
            { 
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) 
            };
            private const string CacheKey = "MapelDataCache";

            public Mapel()
            {
                InitializeComponent();
                this.StartPosition = FormStartPosition.CenterScreen; // biar posisi ditengah
            }

            private void Mapel_load(object sender, EventArgs e)
            {
                cbMapel.Items.Clear();
                cbMapel.Items.Add("biologi");
                cbMapel.Items.Add("matematika");
                cbMapel.Items.Add("fisika");
                cbMapel.Items.Add("kimia");
                cbMapel.Items.Add("sejarah");
                cbMapel.Items.Add("geografi");
                cbMapel.Items.Add("ekonomi");
                cbMapel.Items.Add("sosiologi");

                cbTingkat.Items.Clear();
                cbTingkat.Items.Add("10");
                cbTingkat.Items.Add("11");
                cbTingkat.Items.Add("12");

                cbRuangan.Items.Clear();
                cbRuangan.Items.Add("a");
                cbRuangan.Items.Add("b");
                cbRuangan.Items.Add("c");
                cbRuangan.Items.Add("d");
                cbRuangan.Items.Add("e");

                cbDurasi.Items.Clear();
                cbDurasi.Items.Add("3 bulan");
                cbDurasi.Items.Add("6 bulan");
                cbDurasi.Items.Add("1 tahun");

                cbHariKursus.Items.Clear();
                cbHariKursus.Items.Add("senin");
                cbHariKursus.Items.Add("selasa");
                cbHariKursus.Items.Add("rabu");
                cbHariKursus.Items.Add("kamis");
                cbHariKursus.Items.Add("jumat");

                cbMapel.SelectedIndex = -1;
                cbTingkat.SelectedIndex = -1;
                cbRuangan.SelectedIndex = -1;
                cbDurasi.SelectedIndex = -1;
                cbHariKursus.SelectedIndex = -1;

                EnsureIndexes();
                LoadData();
            }

            private void ClearMapel()
            {
                txtIDmapel.Clear();
                cbMapel.SelectedIndex = -1;
                cbMapel.SelectedIndex = -1;
                cbRuangan.SelectedIndex = -1;
                cbDurasi.SelectedIndex = -1;
                cbHariKursus.SelectedIndex = -1;
                txtWaktukursus.Clear();
                txtHarga.Clear();
                txtIDTutor.Clear();

                txtIDmapel.Focus();
            }

            // ensure indexes disini yak
            private void EnsureIndexes()
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var indexScript = @"
                IF OBJECT_ID('dbo.mapel', 'U') IS NOT NULL
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM sys.indexes WHERE name = 'idx_mapelHarga'
                    )
                        CREATE NONCLUSTERED INDEX idx_mapelHarga
                        ON dbo.mapel (mapel, harga);
                END";

                    using (var cmd = new SqlCommand(indexScript, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            private void LoadData()
            {
                DataTable dt;

                if (cache.Contains(CacheKey))
                {
                    dt = cache.Get(CacheKey) as DataTable;
                }
                else
                {
                    dt = new DataTable();
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            using (SqlCommand cmd = new SqlCommand("getMapel", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                da.Fill(dt);

                                cache.Add(CacheKey, dt, cachePolicy); //nyimpen data ke cache
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error saat memuat data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; 
                        }
                    }
                }

                dgvMapel.AutoGenerateColumns = true;
                dgvMapel.DataSource = dt;

                ClearMapel();
            }

            // Method untuk memverifikasi apakah id_tutor valid (ada di tabel tutor)
            private bool IsValidTutor(string idTutor)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT COUNT(*) FROM tutor WHERE id_tutor = @id_tutor";  
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id_tutor", idTutor);
                            int count = (int)cmd.ExecuteScalar();  // ngitung jumlah tutor dengan id_tutor yang diberikan
                            return count > 0;  // klo count > 0, berarti id_tutor valid
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;  //klo ada error, kembalikan false
                    }
                }
            }


            private bool IsValidIdMapel(string idMapel, string mapel)
            {
                Dictionary<string, string> kodeMapel = new Dictionary<string, string>()
                {
                    { "biologi", "B" },
                    { "matematika", "M" },
                    { "fisika", "F" },
                    { "kimia", "K" },
                    { "sejarah", "S" },
                    { "geografi", "G"},
                    { "ekonomi", "E" },
                    { "sosiologi", "S" }
                };
                if (!kodeMapel.ContainsKey(mapel))
                    return false;
                string kode = kodeMapel[mapel];
                string pola = $"^{kode}(?!000)\\d{{3}}$";
                return Regex.IsMatch(idMapel, pola);
            }
            private string GetFormatId(string mapel)
            {
                //nentuin format ID berdasarkan mapel
                Dictionary<string, string> kodeMapel = new Dictionary<string, string>()
                {
                    { "biologi", "B" },
                    { "matematika", "M" },
                    { "fisika", "F" },
                    { "kimia", "K" },
                    { "sejarah", "S" },
                    { "geografi", "G" },
                    { "ekonomi", "E" },
                    { "sosiologi", "S" }
                };

                if (kodeMapel.ContainsKey(mapel))
                {
                    string kode = kodeMapel[mapel];
                    return $"{kode}001";
                }
                return "Format gak tersedia";
            }

            private bool IsScheduleConflict(string idTutor, string hariKursus, string waktuKursus, string idMapel = null)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = "SELECT COUNT(*) FROM mapel WHERE id_tutor = @id_tutor AND hari_kursus = @hari_kursus AND waktu_kursus = @waktu_kursus";
                        if (!string.IsNullOrEmpty(idMapel))
                        {
                            query += " AND id_mapel != @id_mapel";
                        }

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id_tutor", idTutor);
                            cmd.Parameters.AddWithValue("@hari_kursus", hariKursus);
                            cmd.Parameters.AddWithValue("@waktu_kursus", waktuKursus);

                            if (!string.IsNullOrEmpty(idMapel))
                            {
                                cmd.Parameters.AddWithValue("@id_mapel", idMapel);
                            }

                            int count = (int)cmd.ExecuteScalar();
                            return count > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saat cek jadwal tutor: " + ex.Message);
                        return true;
                    }
                }
            }

            private void btnTambahMapel_Click(object sender, EventArgs e)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlTransaction transaction = null;
                    try
                    {
                        conn.Open();
                        transaction = conn.BeginTransaction();

                        if (txtIDmapel.Text == "" || cbMapel.SelectedIndex == -1 || cbTingkat.SelectedIndex == -1 || cbRuangan.SelectedIndex == -1 || cbDurasi.SelectedIndex == -1 || cbHariKursus.SelectedIndex == -1 || txtWaktukursus.Text == "" || txtHarga.Text == "" || txtIDTutor.Text == "")
                        {
                            lblMessageMapel.Text = "Harap isi semua data! jangan cuman diisi spasi";
                            return;
                        }

                        // Validasi pilihan mapel
                        string mapel = cbMapel.SelectedItem?.ToString().Trim().ToLower();
                        if (string.IsNullOrEmpty(mapel))
                        {
                            lblMessageMapel.Text = "Pilih mata pelajaran dari daftar!";
                            return;
                        }

                        // Validasi apakah id_tutor ada di tabel tutor
                        if (!IsValidTutor(txtIDTutor.Text.Trim()))
                        {
                            lblMessageMapel.Text = "ID Tutor tidak valid! Pastikan ID Tutor sudah terdaftar.";
                            return;
                        }

                        // Ambil nilai mapel dari ComboBox dan validasi id yang sesuai
                        if (string.IsNullOrEmpty(mapel))
                        {
                            lblMessageMapel.Text = "Pilih mata pelajaran dari daftar!";
                            return;
                        }
                    
                        if (!IsValidIdMapel(txtIDmapel.Text.Trim(), mapel))
                        {
                            string formatID = GetFormatId(mapel);
                            lblMessageMapel.Text = $"Format ID Mapel tidak valid! Gunakan format seperti {formatID} dan mulai dari '001'.";
                            return;
                        } //bisa diapus???

                        //validasi tingkat
                        string tingkat = cbTingkat.SelectedItem?.ToString().Trim();
                        if (string.IsNullOrEmpty(tingkat))
                        {
                            lblMessageMapel.Text = "Tingkat harus dipilih dari ComboBox!";
                            return;
                        }

                        //val ruangan
                        string ruangan = cbRuangan.SelectedItem.ToString();
                        if (ruangan != "a" && ruangan != "b" && ruangan != "c" && ruangan != "d" && ruangan != "e")
                        {
                            lblMessageMapel.Text = "Hanya tersedia ruangan a, b, c, d, e untuk kursus BimbelBee ini!";
                            return;
                        }

                        //val durasi
                        string durasi = cbDurasi.SelectedItem.ToString().ToLower();
                        if (durasi != "3 bulan" && durasi != "6 bulan" && durasi != "1 tahun")
                        {
                            lblMessageMapel.Text = "Hanya tersedia durasi kursus selama 3 bulan, 6 bulan, dan 1 tahun";
                            return;
                        }

                        //val hari kursus
                        string hari = cbHariKursus.SelectedItem.ToString().ToLower();
                        if (hari != "senin" && hari != "selasa" && hari != "rabu" && hari != "kamis" && hari != "jumat")
                        {
                            lblMessageMapel.Text = "Kursus hanya tersedia hari senin-jumat";
                            return;
                        }

                        // Validasi waktu kursus (14:00 - 21:00 doangg)
                        if (!TimeSpan.TryParse(txtWaktukursus.Text.Trim(), out TimeSpan waktuKursus))
                        {
                            lblMessageMapel.Text = "Format waktu tidak valid! Gunakan format HH:mm (misalnya 14:30).";
                            return;
                        }

                        TimeSpan batasBawah = new TimeSpan(14, 0, 0); // jam 14:00
                        TimeSpan batasAtas = new TimeSpan(21, 0, 0);  // jam 21:00

                        if (waktuKursus < batasBawah || waktuKursus > batasAtas)
                        {
                            lblMessageMapel.Text = "Waktu kursus hanya boleh antara jam 14:00 sampai 21:00 dan gunakan format mm:yy!";
                            return;
                        }

                        // val harganya yak ini
                        string hargaStr = txtHarga.Text.Trim().Replace(".", "");

                        if (!long.TryParse(hargaStr, out long harga))
                        {
                            lblMessageMapel.Text = "Harga harus berupa angka tanpa simbol mata uang, gunakan titik sebagai pemisah ribuan!";
                            return;
                        }

                        if (harga < 2000000 || harga > 7000000)
                        {
                            lblMessageMapel.Text = "Harga harus berada di antara 2.000.000 dan 7.000.000!";
                            return;
                        }

                        // ini yg ngevalidasi misal tutor di jam segitu gk bisa ngajar lgi
                        if (IsScheduleConflict(txtIDTutor.Text.Trim(), cbHariKursus.SelectedItem.ToString(), txtWaktukursus.Text.Trim()))
                        {
                            lblMessageMapel.Text = "Tutor sudah memiliki jadwal di waktu tersebut!";
                            return;
                        }


                        using (SqlCommand cmd = new SqlCommand("addMapel", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id_mapel", txtIDmapel.Text.Trim());
                            cmd.Parameters.AddWithValue("@mapel", cbMapel.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@tingkat", cbTingkat.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@ruangan", cbRuangan.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@durasi", cbDurasi.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@hari_kursus", cbHariKursus.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@waktu_kursus", txtWaktukursus.Text.Trim());
                            cmd.Parameters.AddWithValue("@harga", txtHarga.Text.Trim());
                            cmd.Parameters.AddWithValue("@id_tutor", txtIDTutor.Text.Trim());

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                transaction.Commit();
                                cache.Remove(CacheKey);
                                lblMessageMapel.Text = "Data berhasil ditambahkan yeyy!";
                                LoadData();
                                ClearMapel(); // Auto Clear setelah tambah data
                            }
                            else
                            {
                                transaction.Rollback();
                                lblMessageMapel.Text = "Data tidak berhasil ditambahkan!";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction?.Rollback();
                        lblMessageMapel.Text = "Error: " + ex.Message;
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
                            SqlTransaction transaction = null;
                            try
                            {
                                string id_mapel = dgvMapel.SelectedRows[0].Cells["id_mapel"].Value.ToString();
                                conn.Open();
                                transaction = conn.BeginTransaction();

                                using (SqlCommand cmd = new SqlCommand("deleteMapel", conn, transaction))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@id_mapel", id_mapel);
                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        transaction.Commit();
                                        cache.Remove(CacheKey);
                                        lblMessageMapel.Text = "Data berhasil dihapus!";
                                        LoadData();
                                        ClearMapel(); // Auto clear setelah hapus data
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        lblMessageMapel.Text = "Data tidak ditemukan atau gagal dihapus!";
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                transaction?.Rollback();
                                lblMessageMapel.Text = "Error: " + ex.Message;
                            }
                        }
                    }
                }
                //ini cmn buat nanganin klo misal tabelnya itu gk ada isi datanya, jdi gk perlu rollback
                else
                { 
                    lblMessageMapel.Text = "Pilih data yang akan dihapus!";
                }
            }

            private void btnEditMapel_Click(object sender, EventArgs e)
            {
                if (txtIDmapel.Text == "")
                {
                    lblMessageMapel.Text = "Silakan pilih data yang ingin diedit dari tabel!";
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlTransaction transaction = null; 

                    try
                    {
                        conn.Open(); 
                        transaction = conn.BeginTransaction(); 

                        // validasi id tutor dulu, harus dh terdaftar
                        if (!IsValidTutor(txtIDTutor.Text.Trim()))
                        {
                            lblMessageMapel.Text = "ID Tutor tidak valid! Pastikan ID Tutor sudah terdaftar.";
                            return;
                        }
                    
                        // validasi mapel dan id
                        string mapel = cbMapel.SelectedItem?.ToString().Trim();
                        if (string.IsNullOrEmpty(mapel))
                        {
                            lblMessageMapel.Text = "Pilih mata pelajaran dari daftar!";
                            return;
                        }
                        if (!IsValidIdMapel(txtIDmapel.Text.Trim(), mapel))
                        {
                            string formatID = GetFormatId(mapel);
                            lblMessageMapel.Text = $"Format ID Mapel tidak valid! Gunakan format seperti {formatID} dan mulai dari '001'.";
                            return;
                        }

                        // val tingkat
                        string tingkat = cbTingkat.SelectedItem?.ToString().Trim();
                        if (string.IsNullOrEmpty(tingkat))
                        {
                            lblMessageMapel.Text = "Tingkat harus dipilih dari ComboBox!";
                            return;
                        }

                        // val ruangan
                        string ruangan = cbRuangan.SelectedItem.ToString().Trim();
                        if (ruangan != "a" && ruangan != "b" && ruangan != "c" && ruangan != "d" && ruangan != "e")
                        {
                            lblMessageMapel.Text = "Hanya tersedia ruangan a, b, c, d, e untuk kursus BimbelBee ini!";
                            return;
                        }

                        //val durasi
                        string durasi = cbDurasi.SelectedItem.ToString().Trim();
                        if (durasi != "3 bulan" && durasi != "6 bulan" && durasi != "1 tahun")
                        {
                            lblMessageMapel.Text = "Hanya tersedia durasi kursus selama 3 bulan, 6 bulan, dan 1 tahun";
                            return;
                        }

                        // val hari
                        string hari = cbHariKursus.SelectedItem.ToString().Trim();
                        if (hari != "senin" && hari != "selasa" && hari != "rabu" && hari != "kamis" && hari != "jumat")
                        {
                            lblMessageMapel.Text = "Kursus hanya tersedia hari senin-jumat";
                            return;
                        }

                        // val jam nya / waktu kursus
                        if (!TimeSpan.TryParse(txtWaktukursus.Text.Trim(), out TimeSpan waktuKursus))
                        {
                            lblMessageMapel.Text = "Format waktu tidak valid! Gunakan format HH:mm (misalnya 14:30).";
                            return;
                        }

                        TimeSpan batasBawah = new TimeSpan(14, 0, 0);
                        TimeSpan batasAtas = new TimeSpan(21, 0, 0);

                        if (waktuKursus < batasBawah || waktuKursus > batasAtas)
                        {
                            lblMessageMapel.Text = "Waktu kursus hanya boleh antara jam 14:00 sampai 21:00 dan gunakan format mm:yy!";
                            return;
                        }

                        // val harga
                        string hargaStr = txtHarga.Text.Trim().Replace(".", "");
                        if (!long.TryParse(hargaStr, out long harga))
                        {
                            lblMessageMapel.Text = "Harga harus berupa angka tanpa simbol mata uang, titik maupun koma!!";
                            return;
                        }

                        if (harga < 2000000 || harga > 7000000)
                        {
                            lblMessageMapel.Text = "Harga harus berada di antara 2.000.000 dan 7.000.000!";
                            return;
                        }

                        if (IsScheduleConflict(txtIDTutor.Text.Trim(), cbHariKursus.SelectedItem.ToString(), txtWaktukursus.Text.Trim(), txtIDmapel.Text.Trim()))
                        {
                            lblMessageMapel.Text = "Tutor sudah memiliki jadwal di waktu tersebut!";
                            return;
                        }

                        DialogResult confirm = MessageBox.Show("Yakin ingin menyimpan perubahan?", "Konfirmasi Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirm == DialogResult.Yes)
                        {
                            using (SqlCommand cmd = new SqlCommand("updateMapel", conn, transaction)) 
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@mapel", cbMapel.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@tingkat", cbTingkat.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@ruangan", cbRuangan.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@durasi", cbDurasi.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@hari_kursus", cbHariKursus.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@waktu_kursus", txtWaktukursus.Text.Trim());
                                cmd.Parameters.AddWithValue("@harga", txtHarga.Text.Trim());
                                cmd.Parameters.AddWithValue("@id_tutor", txtIDTutor.Text.Trim());
                                cmd.Parameters.AddWithValue("@id_mapel", txtIDmapel.Text.Trim());

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    transaction.Commit();
                                    cache.Remove(CacheKey);
                                    lblMessageMapel.Text = "Perubahan berhasil disimpan!";
                                    LoadData();
                                    ClearMapel();
                                }
                                else
                                {
                                    transaction.Rollback();
                                    lblMessageMapel.Text = "Gagal menyimpan perubahan!";
                                }
                            }
                        }
                        else 
                        {
                            transaction.Rollback(); //buat nanganin "yakin ingin menyimpan perubahan", sapa tau dia gk jdi mau nyimpen
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction?.Rollback(); 
                        lblMessageMapel.Text = "Error: " + ex.Message;
                    }
                }
            }

            private void btnRefreshMapel_Click(object sender, EventArgs e)
            {
                cache.Remove(CacheKey);
                LoadData();

                // ngecek jumlah kolom dan baris
                MessageBox.Show($"Jumlah Kolom: {dgvMapel.ColumnCount}\nJumlah Baris: {dgvMapel.RowCount}",
                    "Debugging DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            private void dgvMapel_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvMapel.Rows[e.RowIndex];

                    txtIDmapel.Text = row.Cells[0].Value.ToString();
                    cbMapel.SelectedItem = row.Cells[1].Value?.ToString();
                    cbTingkat.SelectedItem = row.Cells[2].Value?.ToString();
                    cbRuangan.SelectedItem = row.Cells[3].Value?.ToString();
                    cbDurasi.SelectedItem = row.Cells[4].Value?.ToString();
                    cbHariKursus.SelectedItem = row.Cells[5].Value?.ToString();
                    txtWaktukursus.Text = row.Cells[6].Value?.ToString();
                    txtHarga.Text = row.Cells[7].Value?.ToString();
                    txtIDTutor.Text = row.Cells[8].Value?.ToString();
                }
            }

            private void AnalyzeQuery(string sqlQuery)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.InfoMessage += (s, e) => MessageBox.Show(e.Message, "STATISTICS INFO"); //handle pesan dari SET STATISTICS

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

            private void btnBack_Click(object sender, EventArgs e)
            {
                dashboard dashboardForm = new dashboard();
                dashboardForm.Show();
                this.Hide();
            }

            private void btnAnalyze_Click(object sender, EventArgs e)
            {
                string mapelAnalysisQuery = "SELECT id_mapel, mapel, tingkat, harga FROM mapel WHERE mapel = 'matematika' AND harga < 3000000";
                AnalyzeQuery(mapelAnalysisQuery);
            }
        }
    }
