namespace BimbelBee
{
    partial class Tutor 
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
            this.btnEditTutor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefreshTutor = new System.Windows.Forms.Button();
            this.btnHapusTutor = new System.Windows.Forms.Button();
            this.btnTambahTutor = new System.Windows.Forms.Button();
            this.txtTelepon = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtIDTutor = new System.Windows.Forms.TextBox();
            this.dgvTutor = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTutor)).BeginInit();
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
            this.panel1.TabIndex = 1;
            // 
            // btnEditTutor
            // 
            this.btnEditTutor.Location = new System.Drawing.Point(799, 120);
            this.btnEditTutor.Name = "btnEditTutor";
            this.btnEditTutor.Size = new System.Drawing.Size(133, 36);
            this.btnEditTutor.TabIndex = 51;
            this.btnEditTutor.Text = "Edit";
            this.btnEditTutor.UseVisualStyleBackColor = true;
            this.btnEditTutor.Click += new System.EventHandler(this.btnEditTutor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 50;
            this.label3.Text = "Telepon";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 49;
            this.label2.Text = "Nama";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 48;
            this.label1.Text = "ID Tutor";
            // 
            // btnRefreshTutor
            // 
            this.btnRefreshTutor.Location = new System.Drawing.Point(799, 162);
            this.btnRefreshTutor.Name = "btnRefreshTutor";
            this.btnRefreshTutor.Size = new System.Drawing.Size(133, 36);
            this.btnRefreshTutor.TabIndex = 47;
            this.btnRefreshTutor.Text = "Refresh";
            this.btnRefreshTutor.UseVisualStyleBackColor = true;
            this.btnRefreshTutor.Click += new System.EventHandler(this.btnRefreshTutor_Click);
            // 
            // btnHapusTutor
            // 
            this.btnHapusTutor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(57)))), ((int)(((byte)(42)))));
            this.btnHapusTutor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapusTutor.ForeColor = System.Drawing.Color.White;
            this.btnHapusTutor.Location = new System.Drawing.Point(799, 78);
            this.btnHapusTutor.Name = "btnHapusTutor";
            this.btnHapusTutor.Size = new System.Drawing.Size(133, 36);
            this.btnHapusTutor.TabIndex = 46;
            this.btnHapusTutor.Text = "Hapus";
            this.btnHapusTutor.UseVisualStyleBackColor = false;
            this.btnHapusTutor.Click += new System.EventHandler(this.btnHapusTutor_Click);
            // 
            // btnTambahTutor
            // 
            this.btnTambahTutor.Location = new System.Drawing.Point(799, 36);
            this.btnTambahTutor.Name = "btnTambahTutor";
            this.btnTambahTutor.Size = new System.Drawing.Size(133, 36);
            this.btnTambahTutor.TabIndex = 45;
            this.btnTambahTutor.Text = "Tambah";
            this.btnTambahTutor.UseVisualStyleBackColor = true;
            this.btnTambahTutor.Click += new System.EventHandler(this.btnTambahTutor_Click);
            // 
            // txtTelepon
            // 
            this.txtTelepon.Location = new System.Drawing.Point(351, 130);
            this.txtTelepon.Name = "txtTelepon";
            this.txtTelepon.Size = new System.Drawing.Size(415, 26);
            this.txtTelepon.TabIndex = 44;
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(351, 98);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(415, 26);
            this.txtNama.TabIndex = 43;
            // 
            // txtIDTutor
            // 
            this.txtIDTutor.Location = new System.Drawing.Point(351, 66);
            this.txtIDTutor.Name = "txtIDTutor";
            this.txtIDTutor.Size = new System.Drawing.Size(415, 26);
            this.txtIDTutor.TabIndex = 42;
            // 
            // dgvTutor
            // 
            this.dgvTutor.BackgroundColor = System.Drawing.Color.White;
            this.dgvTutor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTutor.Location = new System.Drawing.Point(140, 317);
            this.dgvTutor.Name = "dgvTutor";
            this.dgvTutor.RowHeadersWidth = 62;
            this.dgvTutor.RowTemplate.Height = 28;
            this.dgvTutor.Size = new System.Drawing.Size(925, 236);
            this.dgvTutor.TabIndex = 41;
            this.dgvTutor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTutor_CellContentClick);
            // 
            // btnBack
            // 
            this.btnBack.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBack.Location = new System.Drawing.Point(22, 22);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(54, 50);
            this.btnBack.TabIndex = 52;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Tutor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(210)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.btnEditTutor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefreshTutor);
            this.Controls.Add(this.btnHapusTutor);
            this.Controls.Add(this.btnTambahTutor);
            this.Controls.Add(this.txtTelepon);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtIDTutor);
            this.Controls.Add(this.dgvTutor);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tutor";
            this.Text = "Tutor";
            this.Load += new System.EventHandler(this.Tutor_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTutor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEditTutor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefreshTutor;
        private System.Windows.Forms.Button btnHapusTutor;
        private System.Windows.Forms.Button btnTambahTutor;
        private System.Windows.Forms.TextBox txtTelepon;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtIDTutor;
        private System.Windows.Forms.DataGridView dgvTutor;
        private System.Windows.Forms.Button btnBack;
    }
}