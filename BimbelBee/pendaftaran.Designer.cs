namespace BimbelBee
{
    partial class pendaftaran
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
            this.btnEditDaftar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefreshDaftar = new System.Windows.Forms.Button();
            this.btnHapusDaftar = new System.Windows.Forms.Button();
            this.btnTambahDaftar = new System.Windows.Forms.Button();
            this.txtTglDaftar = new System.Windows.Forms.TextBox();
            this.txtTotalBayar = new System.Windows.Forms.TextBox();
            this.txtIDMapel = new System.Windows.Forms.TextBox();
            this.txtNISN = new System.Windows.Forms.TextBox();
            this.txtIDDaftar = new System.Windows.Forms.TextBox();
            this.dgvPendaftaran = new System.Windows.Forms.DataGridView();
            this.btnAnalyzeDaftar = new System.Windows.Forms.Button();
            this.lblMessageDaftar = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendaftaran)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(79)))), ((int)(((byte)(76)))));
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(99, 600);
            this.panel1.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBack.Location = new System.Drawing.Point(22, 27);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(54, 50);
            this.btnBack.TabIndex = 41;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnEditDaftar
            // 
            this.btnEditDaftar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEditDaftar.Location = new System.Drawing.Point(796, 118);
            this.btnEditDaftar.Name = "btnEditDaftar";
            this.btnEditDaftar.Size = new System.Drawing.Size(133, 36);
            this.btnEditDaftar.TabIndex = 40;
            this.btnEditDaftar.Text = "Edit";
            this.btnEditDaftar.UseVisualStyleBackColor = true;
            this.btnEditDaftar.Click += new System.EventHandler(this.btnEditDaftar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(156, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 20);
            this.label5.TabIndex = 39;
            this.label5.Text = "Tanggal Pendaftaran";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(177, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 20);
            this.label4.TabIndex = 38;
            this.label4.Text = "Total Pembayaran";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(196, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 37;
            this.label3.Text = "ID Mapel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(196, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 36;
            this.label2.Text = "NISN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(196, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "ID Pendaftaran";
            // 
            // btnRefreshDaftar
            // 
            this.btnRefreshDaftar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefreshDaftar.Location = new System.Drawing.Point(796, 160);
            this.btnRefreshDaftar.Name = "btnRefreshDaftar";
            this.btnRefreshDaftar.Size = new System.Drawing.Size(133, 36);
            this.btnRefreshDaftar.TabIndex = 34;
            this.btnRefreshDaftar.Text = "Refresh";
            this.btnRefreshDaftar.UseVisualStyleBackColor = true;
            this.btnRefreshDaftar.Click += new System.EventHandler(this.btnRefreshDaftar_Click);
            // 
            // btnHapusDaftar
            // 
            this.btnHapusDaftar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(57)))), ((int)(((byte)(42)))));
            this.btnHapusDaftar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapusDaftar.ForeColor = System.Drawing.Color.White;
            this.btnHapusDaftar.Location = new System.Drawing.Point(796, 76);
            this.btnHapusDaftar.Name = "btnHapusDaftar";
            this.btnHapusDaftar.Size = new System.Drawing.Size(133, 36);
            this.btnHapusDaftar.TabIndex = 33;
            this.btnHapusDaftar.Text = "Hapus";
            this.btnHapusDaftar.UseVisualStyleBackColor = false;
            this.btnHapusDaftar.Click += new System.EventHandler(this.btnHapusDaftar_Click);
            // 
            // btnTambahDaftar
            // 
            this.btnTambahDaftar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTambahDaftar.Location = new System.Drawing.Point(796, 34);
            this.btnTambahDaftar.Name = "btnTambahDaftar";
            this.btnTambahDaftar.Size = new System.Drawing.Size(133, 36);
            this.btnTambahDaftar.TabIndex = 32;
            this.btnTambahDaftar.Text = "Tambah";
            this.btnTambahDaftar.UseVisualStyleBackColor = true;
            this.btnTambahDaftar.Click += new System.EventHandler(this.btnTambahDaftar_Click);
            // 
            // txtTglDaftar
            // 
            this.txtTglDaftar.Location = new System.Drawing.Point(329, 159);
            this.txtTglDaftar.Name = "txtTglDaftar";
            this.txtTglDaftar.Size = new System.Drawing.Size(415, 26);
            this.txtTglDaftar.TabIndex = 31;
            // 
            // txtTotalBayar
            // 
            this.txtTotalBayar.Location = new System.Drawing.Point(329, 127);
            this.txtTotalBayar.Name = "txtTotalBayar";
            this.txtTotalBayar.Size = new System.Drawing.Size(415, 26);
            this.txtTotalBayar.TabIndex = 30;
            this.txtTotalBayar.TextChanged += new System.EventHandler(this.txtTotalBayar_TextChanged);
            // 
            // txtIDMapel
            // 
            this.txtIDMapel.Location = new System.Drawing.Point(329, 95);
            this.txtIDMapel.Name = "txtIDMapel";
            this.txtIDMapel.Size = new System.Drawing.Size(415, 26);
            this.txtIDMapel.TabIndex = 29;
            // 
            // txtNISN
            // 
            this.txtNISN.Location = new System.Drawing.Point(329, 63);
            this.txtNISN.Name = "txtNISN";
            this.txtNISN.Size = new System.Drawing.Size(415, 26);
            this.txtNISN.TabIndex = 28;
            // 
            // txtIDDaftar
            // 
            this.txtIDDaftar.Location = new System.Drawing.Point(329, 31);
            this.txtIDDaftar.Name = "txtIDDaftar";
            this.txtIDDaftar.Size = new System.Drawing.Size(415, 26);
            this.txtIDDaftar.TabIndex = 27;
            // 
            // dgvPendaftaran
            // 
            this.dgvPendaftaran.BackgroundColor = System.Drawing.Color.White;
            this.dgvPendaftaran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPendaftaran.Location = new System.Drawing.Point(134, 285);
            this.dgvPendaftaran.Name = "dgvPendaftaran";
            this.dgvPendaftaran.RowHeadersWidth = 62;
            this.dgvPendaftaran.RowTemplate.Height = 28;
            this.dgvPendaftaran.Size = new System.Drawing.Size(925, 236);
            this.dgvPendaftaran.TabIndex = 26;
            this.dgvPendaftaran.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPendaftaran_CellContentClick);
            this.dgvPendaftaran.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPendaftaran_CellContentClick_1);
            // 
            // btnAnalyzeDaftar
            // 
            this.btnAnalyzeDaftar.ForeColor = System.Drawing.Color.Black;
            this.btnAnalyzeDaftar.Location = new System.Drawing.Point(796, 202);
            this.btnAnalyzeDaftar.Name = "btnAnalyzeDaftar";
            this.btnAnalyzeDaftar.Size = new System.Drawing.Size(133, 42);
            this.btnAnalyzeDaftar.TabIndex = 41;
            this.btnAnalyzeDaftar.Text = "Analisis";
            this.btnAnalyzeDaftar.UseVisualStyleBackColor = true;
            this.btnAnalyzeDaftar.Click += new System.EventHandler(this.btnAnalyzeDaftar_Click);
            // 
            // lblMessageDaftar
            // 
            this.lblMessageDaftar.AutoSize = true;
            this.lblMessageDaftar.ForeColor = System.Drawing.Color.Black;
            this.lblMessageDaftar.Location = new System.Drawing.Point(142, 535);
            this.lblMessageDaftar.Name = "lblMessageDaftar";
            this.lblMessageDaftar.Size = new System.Drawing.Size(74, 20);
            this.lblMessageDaftar.TabIndex = 42;
            this.lblMessageDaftar.Text = "Message";
            // 
            // pendaftaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(210)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.lblMessageDaftar);
            this.Controls.Add(this.btnAnalyzeDaftar);
            this.Controls.Add(this.btnEditDaftar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefreshDaftar);
            this.Controls.Add(this.btnHapusDaftar);
            this.Controls.Add(this.btnTambahDaftar);
            this.Controls.Add(this.txtTglDaftar);
            this.Controls.Add(this.txtTotalBayar);
            this.Controls.Add(this.txtIDMapel);
            this.Controls.Add(this.txtNISN);
            this.Controls.Add(this.txtIDDaftar);
            this.Controls.Add(this.dgvPendaftaran);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "pendaftaran";
            this.Text = "pendaftaran";
            this.Load += new System.EventHandler(this.pendaftaran_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendaftaran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEditDaftar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefreshDaftar;
        private System.Windows.Forms.Button btnHapusDaftar;
        private System.Windows.Forms.Button btnTambahDaftar;
        private System.Windows.Forms.TextBox txtTglDaftar;
        private System.Windows.Forms.TextBox txtTotalBayar;
        private System.Windows.Forms.TextBox txtIDMapel;
        private System.Windows.Forms.TextBox txtNISN;
        private System.Windows.Forms.TextBox txtIDDaftar;
        private System.Windows.Forms.DataGridView dgvPendaftaran;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAnalyzeDaftar;
        private System.Windows.Forms.Label lblMessageDaftar;
    }
}