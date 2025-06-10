namespace BimbelBee
{
    partial class Mapel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.dgvMapel = new System.Windows.Forms.DataGridView();
            this.txtIDmapel = new System.Windows.Forms.TextBox();
            this.txtWaktukursus = new System.Windows.Forms.TextBox();
            this.txtHarga = new System.Windows.Forms.TextBox();
            this.txtIDTutor = new System.Windows.Forms.TextBox();
            this.btnTambahMapel = new System.Windows.Forms.Button();
            this.btnHapusMapel = new System.Windows.Forms.Button();
            this.btnRefreshMapel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnEditMapel = new System.Windows.Forms.Button();
            this.cbMapel = new System.Windows.Forms.ComboBox();
            this.cbTingkat = new System.Windows.Forms.ComboBox();
            this.cbDurasi = new System.Windows.Forms.ComboBox();
            this.cbRuangan = new System.Windows.Forms.ComboBox();
            this.cbHariKursus = new System.Windows.Forms.ComboBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.lblMessageMapel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapel)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(79)))), ((int)(((byte)(76)))));
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(98, 600);
            this.panel1.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBack.Location = new System.Drawing.Point(21, 18);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(54, 50);
            this.btnBack.TabIndex = 53;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dgvMapel
            // 
            this.dgvMapel.BackgroundColor = System.Drawing.Color.White;
            this.dgvMapel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMapel.Location = new System.Drawing.Point(123, 338);
            this.dgvMapel.Name = "dgvMapel";
            this.dgvMapel.RowHeadersWidth = 62;
            this.dgvMapel.RowTemplate.Height = 28;
            this.dgvMapel.Size = new System.Drawing.Size(925, 198);
            this.dgvMapel.TabIndex = 1;
            this.dgvMapel.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMapel_CellContentClick);
            // 
            // txtIDmapel
            // 
            this.txtIDmapel.Location = new System.Drawing.Point(315, 22);
            this.txtIDmapel.Name = "txtIDmapel";
            this.txtIDmapel.Size = new System.Drawing.Size(415, 26);
            this.txtIDmapel.TabIndex = 2;
            // 
            // txtWaktukursus
            // 
            this.txtWaktukursus.Location = new System.Drawing.Point(315, 225);
            this.txtWaktukursus.Name = "txtWaktukursus";
            this.txtWaktukursus.Size = new System.Drawing.Size(415, 26);
            this.txtWaktukursus.TabIndex = 8;
            // 
            // txtHarga
            // 
            this.txtHarga.Location = new System.Drawing.Point(315, 257);
            this.txtHarga.Name = "txtHarga";
            this.txtHarga.Size = new System.Drawing.Size(415, 26);
            this.txtHarga.TabIndex = 9;
            // 
            // txtIDTutor
            // 
            this.txtIDTutor.Location = new System.Drawing.Point(315, 289);
            this.txtIDTutor.Name = "txtIDTutor";
            this.txtIDTutor.Size = new System.Drawing.Size(415, 26);
            this.txtIDTutor.TabIndex = 10;
            // 
            // btnTambahMapel
            // 
            this.btnTambahMapel.Location = new System.Drawing.Point(782, 25);
            this.btnTambahMapel.Name = "btnTambahMapel";
            this.btnTambahMapel.Size = new System.Drawing.Size(133, 36);
            this.btnTambahMapel.TabIndex = 11;
            this.btnTambahMapel.Text = "Tambah";
            this.btnTambahMapel.UseVisualStyleBackColor = true;
            this.btnTambahMapel.Click += new System.EventHandler(this.btnTambahMapel_Click);
            // 
            // btnHapusMapel
            // 
            this.btnHapusMapel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(57)))), ((int)(((byte)(42)))));
            this.btnHapusMapel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapusMapel.ForeColor = System.Drawing.Color.White;
            this.btnHapusMapel.Location = new System.Drawing.Point(782, 67);
            this.btnHapusMapel.Name = "btnHapusMapel";
            this.btnHapusMapel.Size = new System.Drawing.Size(133, 36);
            this.btnHapusMapel.TabIndex = 12;
            this.btnHapusMapel.Text = "Hapus";
            this.btnHapusMapel.UseVisualStyleBackColor = false;
            this.btnHapusMapel.Click += new System.EventHandler(this.btnHapusMapel_Click);
            // 
            // btnRefreshMapel
            // 
            this.btnRefreshMapel.Location = new System.Drawing.Point(782, 151);
            this.btnRefreshMapel.Name = "btnRefreshMapel";
            this.btnRefreshMapel.Size = new System.Drawing.Size(133, 36);
            this.btnRefreshMapel.TabIndex = 14;
            this.btnRefreshMapel.Text = "Refresh";
            this.btnRefreshMapel.UseVisualStyleBackColor = true;
            this.btnRefreshMapel.Click += new System.EventHandler(this.btnRefreshMapel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "ID Mapel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Mapel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Tingkat";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(207, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Ruangan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(207, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Durasi";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(207, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "Hari Kursus";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(207, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 20);
            this.label7.TabIndex = 21;
            this.label7.Text = "Waktu Kursus";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(207, 263);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 20);
            this.label8.TabIndex = 22;
            this.label8.Text = "Harga";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(207, 295);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 20);
            this.label9.TabIndex = 23;
            this.label9.Text = "ID Tutor";
            // 
            // btnEditMapel
            // 
            this.btnEditMapel.Location = new System.Drawing.Point(782, 109);
            this.btnEditMapel.Name = "btnEditMapel";
            this.btnEditMapel.Size = new System.Drawing.Size(133, 36);
            this.btnEditMapel.TabIndex = 25;
            this.btnEditMapel.Text = "Edit";
            this.btnEditMapel.UseVisualStyleBackColor = true;
            this.btnEditMapel.Click += new System.EventHandler(this.btnEditMapel_Click);
            // 
            // cbMapel
            // 
            this.cbMapel.FormattingEnabled = true;
            this.cbMapel.Location = new System.Drawing.Point(315, 52);
            this.cbMapel.Name = "cbMapel";
            this.cbMapel.Size = new System.Drawing.Size(415, 28);
            this.cbMapel.TabIndex = 26;
            // 
            // cbTingkat
            // 
            this.cbTingkat.FormattingEnabled = true;
            this.cbTingkat.Location = new System.Drawing.Point(315, 84);
            this.cbTingkat.Name = "cbTingkat";
            this.cbTingkat.Size = new System.Drawing.Size(415, 28);
            this.cbTingkat.TabIndex = 27;
            // 
            // cbDurasi
            // 
            this.cbDurasi.FormattingEnabled = true;
            this.cbDurasi.Location = new System.Drawing.Point(315, 151);
            this.cbDurasi.Name = "cbDurasi";
            this.cbDurasi.Size = new System.Drawing.Size(415, 28);
            this.cbDurasi.TabIndex = 29;
            // 
            // cbRuangan
            // 
            this.cbRuangan.FormattingEnabled = true;
            this.cbRuangan.Location = new System.Drawing.Point(315, 116);
            this.cbRuangan.Name = "cbRuangan";
            this.cbRuangan.Size = new System.Drawing.Size(415, 28);
            this.cbRuangan.TabIndex = 28;
            // 
            // cbHariKursus
            // 
            this.cbHariKursus.FormattingEnabled = true;
            this.cbHariKursus.Location = new System.Drawing.Point(315, 185);
            this.cbHariKursus.Name = "cbHariKursus";
            this.cbHariKursus.Size = new System.Drawing.Size(415, 28);
            this.cbHariKursus.TabIndex = 30;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(782, 193);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(133, 35);
            this.btnAnalyze.TabIndex = 31;
            this.btnAnalyze.Text = "Analisis";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // lblMessageMapel
            // 
            this.lblMessageMapel.AutoSize = true;
            this.lblMessageMapel.Location = new System.Drawing.Point(119, 555);
            this.lblMessageMapel.Name = "lblMessageMapel";
            this.lblMessageMapel.Size = new System.Drawing.Size(74, 20);
            this.lblMessageMapel.TabIndex = 32;
            this.lblMessageMapel.Text = "Message";
            // 
            // Mapel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(210)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.lblMessageMapel);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.cbHariKursus);
            this.Controls.Add(this.cbDurasi);
            this.Controls.Add(this.cbRuangan);
            this.Controls.Add(this.cbTingkat);
            this.Controls.Add(this.cbMapel);
            this.Controls.Add(this.btnEditMapel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefreshMapel);
            this.Controls.Add(this.btnHapusMapel);
            this.Controls.Add(this.btnTambahMapel);
            this.Controls.Add(this.txtIDTutor);
            this.Controls.Add(this.txtHarga);
            this.Controls.Add(this.txtWaktukursus);
            this.Controls.Add(this.txtIDmapel);
            this.Controls.Add(this.dgvMapel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Mapel";
            this.Text = "Mapel";
            this.Load += new System.EventHandler(this.Mapel_load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvMapel;
        private System.Windows.Forms.TextBox txtIDmapel;
        private System.Windows.Forms.TextBox txtWaktukursus;
        private System.Windows.Forms.TextBox txtHarga;
        private System.Windows.Forms.TextBox txtIDTutor;
        private System.Windows.Forms.Button btnTambahMapel;
        private System.Windows.Forms.Button btnHapusMapel;
        private System.Windows.Forms.Button btnRefreshMapel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnEditMapel;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ComboBox cbMapel;
        private System.Windows.Forms.ComboBox cbTingkat;
        private System.Windows.Forms.ComboBox cbDurasi;
        private System.Windows.Forms.ComboBox cbRuangan;
        private System.Windows.Forms.ComboBox cbHariKursus;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Label lblMessageMapel;
    }
}