using System.Net.NetworkInformation;

namespace _2D_Particle_Simulator
{
    public partial class FormParticleSim : Form
    {
        Particle[] particles = new Particle[2000];
        Boundary[] bounds = new Boundary[4];
        Point corner;
        float scale = 2f;
        bool overlayDensity = false;
        int cycles = 1;
        public FormParticleSim()
        {
            InitializeComponent();
            Random rand = new Random();

            int size = 6; // the number of particle types
            corner = new Point(1028, 512); // the size of the simulation area

            float[,] al = new float[size, size];
            float[,] assoc = new float[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    al[i, j] = (float)(rand.NextDouble() * 10);
                    // assoc[i, j] = (float)(rand.NextDouble());
                    assoc[i, j] = 0.08f;
                }
            }
            Particle.SetAttractionLevels(al, assoc);
            float vdamper = 0.01f;
            int[] weights = new int[size];
            int totalWeight = 0;
            for (int i = 0; i < size; i++)
            {
                weights[i] = size - i;
                totalWeight += weights[i];
            }

            int SelectWeightedType()
            {
                int roll = rand.Next(totalWeight);
                int cumulative = 0;
                for (int i = 0; i < size; i++)
                {
                    cumulative += weights[i];
                    if (roll < cumulative)
                    {
                        return i;
                    }
                }

                return size - 1;
            }

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i] = new Particle(rand.Next(1, corner.X - 1), rand.Next(1, corner.Y - 1), SelectWeightedType());
            }
            bounds[0] = new Boundary(0, 0, corner.X, 0);
            bounds[1] = new Boundary(0, 0, 0, corner.Y);
            bounds[2] = new Boundary(corner.X, corner.Y, 0, corner.Y);
            bounds[3] = new Boundary(corner.X, corner.Y, corner.X, 0);

            // setup labels
            int labelXPos = (int)(corner.X * scale + 10);
            labelParticleCount.Text = "Particle Count: " + particles.Length;
            labelParticleCount.Location = new Point(labelXPos, labelParticleCount.Location.Y);

            labelDensity.Text = "Density: " + ((int)(1000 * ((float)particles.Length / (corner.X * corner.Y)))) / 1000f + " particles per pixel";
            labelDensity.Location = new Point(labelXPos, labelDensity.Location.Y);

            labelAverageSpeed.Text = "Average Speed: " + GetAverageSpeed() + " pixels per tick";
            labelAverageSpeed.Location = new Point(labelXPos, labelAverageSpeed.Location.Y);

            checkBoxOverylayDensity.Location = new Point(labelXPos, checkBoxOverylayDensity.Location.Y);

            numericUpDownCycles.Location = new Point(labelXPos + numericUpDownCycles.Location.X, numericUpDownCycles.Location.Y);
            labelCyclesPerFrame.Location = new Point(labelXPos, labelCyclesPerFrame.Location.Y);
        }
        private Particle[] CreateCopy(Particle[] particles)
        {
            Particle[] copy = new Particle[particles.Length];
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i] != null)
                {
                    copy[i] = Particle.CopyParticle(particles[i]); 
                }
            }
            return copy;
        }
        private void timerTick_Tick(object sender, EventArgs e)
        {
            this.Invalidate(); // retriggers paint event
            cycles = (int)numericUpDownCycles.Value;
            for (int j = 0; j < cycles; j++)
            {
                Particle[] lastTickParticles = CreateCopy(particles);
                for (int i = 0; i < particles.Length; i++)
                {
                    if (particles[i] != null)
                    {
                        particles[i].Tick(bounds, lastTickParticles);
                    }
                }
            }

            labelAverageSpeed.Text = "Average Speed: " + GetAverageSpeed() + " pixels per tick";
        }

        private void FormParticleSim_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            

            if (overlayDensity)
            {
                int perSector = 16;
                int[,] counts = new int[corner.X / perSector, corner.Y / perSector];
                foreach (Particle p in particles)
                {
                    counts[(int)p.GetX() / perSector, (int)p.GetY() / perSector]++;
                }
                for (int i = 0; i < corner.X / perSector; i++)
                {
                    for (int j = 0; j < corner.Y / perSector; j++)
                    {
                        int count = counts[i, j];
                        if (count > 0)
                        {
                            int alpha = Math.Min(255, count * 32);
                            float t = Math.Min(1f, alpha / 255f);
                            int red = (int)(255 * t);
                            int blue = (int)(255 * (1f - t));
                            using (Brush b = new SolidBrush(Color.FromArgb(alpha, red, 0, blue)))
                            {
                                e.Graphics.FillRectangle(b, scale * (i * perSector), scale * (j * perSector), scale * perSector, scale * perSector);
                            }
                        }
                    }
                }
            }    
            foreach (Particle p in particles)
            {
                if (p != null)
                {
                    p.Draw(e.Graphics, scale);
                }
            }
            foreach (Boundary b in bounds)
            {
                if (b != null)
                {
                    b.Draw(e.Graphics, scale);
                }
            }
        }
        private float GetAverageSpeed()
        {
            float totalSpeed = 0;
            foreach (Particle p in particles)
            {
                if (p != null)
                {
                    totalSpeed += p.GetVelocity();
                }
            }
            return (int)(totalSpeed / particles.Length * 1000) / 1000f;
        }

        private void checkBoxOverylayDensity_CheckedChanged(object sender, EventArgs e)
        {
            overlayDensity = checkBoxOverylayDensity.Checked;
        }
    }
}
