using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2D_Particle_Simulator
{
    public partial class FormStartup : Form
    {
        public FormStartup()
        {
            InitializeComponent();
            UpdateDensity();
        }

        private void buttonCreateSimulator_Click(object sender, EventArgs e)
        {
            // start the new simulator form with the specified settings
            Form simulatorForm = new FormParticleSim((int)numericUpDownParticleTypeCount.Value, int.Parse(textBoxParticleCount.Text), int.Parse(textBoxBoundaryX.Text), int.Parse(textBoxBoundaryY.Text));
            simulatorForm.Show();
            this.Hide();
        }
        private void UpdateDensity()
        {
            this.textBoxAverageDensity.Text = ((int)(1000 * ((float)Convert.ToInt32(textBoxParticleCount.Text) / (Convert.ToInt32(textBoxBoundaryX.Text) * Convert.ToInt32(textBoxBoundaryY.Text)))) / 1000f) + " particles per pixel";
        }
        private void textBoxParticleCount_TextChanged(object sender, EventArgs e)
        {
            // update density
            UpdateDensity();
        }
        private void textBoxBoundaryX_TextChanged(object sender, EventArgs e)
        {
            // update density
            UpdateDensity();
        }

        private void textBoxBoundaryY_TextChanged_1(object sender, EventArgs e)
        {
            // update density
            UpdateDensity();
        }
    }
}
