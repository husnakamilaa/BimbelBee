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
            this.btnReport = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTblSiswa = new System.Windows.Forms.Button();
            this.btnTblTutor = new System.Windows.Forms.Button();
            this.btnTblPendaftaran = new System.Windows.Forms.Button();
            this.btnTblMapel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(79)))), ((int)(((byte)(76)))));
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnTblSiswa);
            this.panel1.Controls.Add(this.btnTblTutor);
            this.panel1.Controls.Add(this.btnTblPendaftaran);
            this.panel1.Controls.Add(this.btnTblMapel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(155, 600);
            this.panel1.TabIndex = 2;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(-3, 292);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(168, 54);
            this.btnReport.TabIndex = 11;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 470);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 46);
            this.button1.TabIndex = 7;
            this.button1.Text = "<";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTblSiswa
            // 
            this.btnTblSiswa.Location = new System.Drawing.Point(-3, 36);
            this.btnTblSiswa.Name = "btnTblSiswa";
            this.btnTblSiswa.Size = new System.Drawing.Size(168, 43);
            this.btnTblSiswa.TabIndex = 3;
            this.btnTblSiswa.Text = "Siswa";
            this.btnTblSiswa.UseVisualStyleBackColor = true;
            this.btnTblSiswa.Click += new System.EventHandler(this.btnTblSiswa_Click);
            // 
            // btnTblTutor
            // 
            this.btnTblTutor.Location = new System.Drawing.Point(0, 95);
            this.btnTblTutor.Name = "btnTblTutor";
            this.btnTblTutor.Size = new System.Drawing.Size(165, 43);
            this.btnTblTutor.TabIndex = 5;
            this.btnTblTutor.Text = "Tutor";
            this.btnTblTutor.UseVisualStyleBackColor = true;
            this.btnTblTutor.Click += new System.EventHandler(this.btnTblTutor_Click);
            // 
            // btnTblPendaftaran
            // 
            this.btnTblPendaftaran.Location = new System.Drawing.Point(0, 223);
            this.btnTblPendaftaran.Name = "btnTblPendaftaran";
            this.btnTblPendaftaran.Size = new System.Drawing.Size(168, 51);
            this.btnTblPendaftaran.TabIndex = 6;
            this.btnTblPendaftaran.Text = "Pendaftaran";
            this.btnTblPendaftaran.UseVisualStyleBackColor = true;
            this.btnTblPendaftaran.Click += new System.EventHandler(this.btnTblPendaftaran_Click);
            // 
            // btnTblMapel
            // 
            this.btnTblMapel.Location = new System.Drawing.Point(0, 158);
            this.btnTblMapel.Name = "btnTblMapel";
            this.btnTblMapel.Size = new System.Drawing.Size(168, 49);
            this.btnTblMapel.TabIndex = 4;
            this.btnTblMapel.Text = "Mapel";
            this.btnTblMapel.UseVisualStyleBackColor = true;
            this.btnTblMapel.Click += new System.EventHandler(this.btnTblMapel_Click);
            // 
            // dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(210)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "dashboard";
            this.Text = "dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTblSiswa;
        private System.Windows.Forms.Button btnTblMapel;
        private System.Windows.Forms.Button btnTblTutor;
        private System.Windows.Forms.Button btnTblPendaftaran;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnReport;
    }
}