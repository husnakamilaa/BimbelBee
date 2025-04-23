using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BimbelBee
{
    public partial class dashboard: Form
    {
        public dashboard()
        {
            InitializeComponent();
        }

        private void btnTblSiswa_Click(object sender, EventArgs e)
        {
            formSiswa siswaForm = new formSiswa();
            siswaForm.Show();
            this.Hide();
        }

        private void btnTblMapel_Click(object sender, EventArgs e)
        {
            Mapel mapelForm = new Mapel();
            mapelForm.Show();
            this.Hide();
        }

        private void btnTblTutor_Click(object sender, EventArgs e)
        {
            Tutor tutorForm = new Tutor();
            tutorForm.Show();
            this.Hide();
        }

        private void btnTblPendaftaran_Click(object sender, EventArgs e)
        {
            pendaftaran pendaftaranForm = new pendaftaran();
            pendaftaranForm.Show();
            this.Hide();
        }

    }
}
