namespace BimbelBee
{
    partial class dashboard
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
            this.btnTblSiswa = new System.Windows.Forms.Button();
            this.btnTblMapel = new System.Windows.Forms.Button();
            this.btnTblTutor = new System.Windows.Forms.Button();
            this.btnTblPendaftaran = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(79)))), ((int)(((byte)(76)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(99, 544);
            this.panel1.TabIndex = 2;
            // 
            // btnTblSiswa
            // 
            this.btnTblSiswa.Location = new System.Drawing.Point(161, 39);
            this.btnTblSiswa.Name = "btnTblSiswa";
            this.btnTblSiswa.Size = new System.Drawing.Size(227, 205);
            this.btnTblSiswa.TabIndex = 3;
            this.btnTblSiswa.Text = "Siswa";
            this.btnTblSiswa.UseVisualStyleBackColor = true;
            this.btnTblSiswa.Click += new System.EventHandler(this.btnTblSiswa_Click);
            // 
            // btnTblMapel
            // 
            this.btnTblMapel.Location = new System.Drawing.Point(441, 39);
            this.btnTblMapel.Name = "btnTblMapel";
            this.btnTblMapel.Size = new System.Drawing.Size(227, 205);
            this.btnTblMapel.TabIndex = 4;
            this.btnTblMapel.Text = "Mapel";
            this.btnTblMapel.UseVisualStyleBackColor = true;
            this.btnTblMapel.Click += new System.EventHandler(this.btnTblMapel_Click);
            // 
            // btnTblTutor
            // 
            this.btnTblTutor.Location = new System.Drawing.Point(161, 276);
            this.btnTblTutor.Name = "btnTblTutor";
            this.btnTblTutor.Size = new System.Drawing.Size(227, 205);
            this.btnTblTutor.TabIndex = 5;
            this.btnTblTutor.Text = "Tutor";
            this.btnTblTutor.UseVisualStyleBackColor = true;
            this.btnTblTutor.Click += new System.EventHandler(this.btnTblTutor_Click);
            // 
            // btnTblPendaftaran
            // 
            this.btnTblPendaftaran.Location = new System.Drawing.Point(441, 276);
            this.btnTblPendaftaran.Name = "btnTblPendaftaran";
            this.btnTblPendaftaran.Size = new System.Drawing.Size(227, 205);
            this.btnTblPendaftaran.TabIndex = 6;
            this.btnTblPendaftaran.Text = "Pendaftaran";
            this.btnTblPendaftaran.UseVisualStyleBackColor = true;
            this.btnTblPendaftaran.Click += new System.EventHandler(this.btnTblPendaftaran_Click);
            // 
            // dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(210)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(1078, 544);
            this.Controls.Add(this.btnTblPendaftaran);
            this.Controls.Add(this.btnTblTutor);
            this.Controls.Add(this.btnTblMapel);
            this.Controls.Add(this.btnTblSiswa);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "dashboard";
            this.Text = "dashboard";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTblSiswa;
        private System.Windows.Forms.Button btnTblMapel;
        private System.Windows.Forms.Button btnTblTutor;
        private System.Windows.Forms.Button btnTblPendaftaran;
    }
}