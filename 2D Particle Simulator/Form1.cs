using System.Net.NetworkInformation;

using ComputeSharp;
using _2D_Particle_Simulator;


namespace _2D_Particle_Simulator
{
    public partial class FormParticleSim : Form
    {
        Particle[] particles;
        Boundary[] bounds = new Boundary[4];
        Point corner;
        float scale = 2f;
        int cycles = 1;
        bool paused = false;
        readonly Random rand = new Random();
        public FormParticleSim(int particleTypes, int particleCount, int boundX, int boundY)
        {
            InitializeComponent();

            // constructor arguments
            particles = new Particle[particleCount];
            int size = particleTypes;
            corner = new Point(boundX, boundY);

            float[,] al = new float[size, size];
            float[,] assoc = new float[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    al[i, j] = (float)(rand.NextDouble() * 10);
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

            // helper function for selecting particle types based on weights, gives uneven distribution favoring lower indexed types
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

            numericUpDownCycles.Location = new Point(labelXPos + numericUpDownCycles.Location.X, numericUpDownCycles.Location.Y);
            labelCyclesPerFrame.Location = new Point(labelXPos, labelCyclesPerFrame.Location.Y);

            buttonPausePlay.Location = new Point(labelXPos, buttonPausePlay.Location.Y);
            buttonRandomize.Location = new Point(labelXPos, buttonRandomize.Location.Y);
            labelAttractionKey.Location = new Point(0, buttonRandomize.Location.Y + buttonRandomize.Height + 10);

            InitializeAttractionTable();
        }
        private void InitializeAttractionTable()
        {
            int size = Particle.attractionLevels.GetLength(0);
            dataGridAttraction.ColumnCount = size;
            dataGridAttraction.RowCount = size;
            dataGridAttraction.ReadOnly = false;
            dataGridAttraction.DefaultCellStyle.Format = "0.###";
            dataGridAttraction.BackgroundColor = Color.Black;
            dataGridAttraction.DefaultCellStyle.BackColor = Color.Black;
            dataGridAttraction.DefaultCellStyle.ForeColor = Color.White;
            dataGridAttraction.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridAttraction.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridAttraction.RowHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridAttraction.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridAttraction.EnableHeadersVisualStyles = false;
            dataGridAttraction.RowHeadersWidth = dataGridAttraction.RowHeadersWidth + 35;
            dataGridAttraction.CellValidating += dataGridAttraction_CellValidating;
            dataGridAttraction.CellValueChanged += dataGridAttraction_CellValueChanged;

            for (int i = 0; i < size; i++)
            {
                string colorName = Particle.colors[i].IsKnownColor
                    ? Particle.colors[i].Name
                    : $"Color {i}";
                dataGridAttraction.Columns[i].Name = colorName;
                dataGridAttraction.Rows[i].HeaderCell.Value = colorName;
                for (int j = 0; j < size; j++)
                {
                    dataGridAttraction.Rows[i].Cells[j].Value = Particle.attractionLevels[i, j];
                }
            }

            int gridTop = (int)(corner.Y * scale) + 20;
            labelAttractionKey.Text = "Rows: affected particle type | Columns: influencing particle type"; 
            labelAttractionKey.Location = new Point(0, gridTop - labelAttractionKey.Height - 5);
            dataGridAttraction.Location = new Point(0, gridTop);
            int tableSpacing = 10;
            dataGridAttraction.Width = ClientSize.Width - buttonRandomizeAttraction.Width - tableSpacing;
            int desiredHeight = 200;
            dataGridAttraction.Height = desiredHeight;
            buttonRandomizeAttraction.Location = new Point(dataGridAttraction.Right + tableSpacing, gridTop);
            if (ClientSize.Height < gridTop + desiredHeight + 10)
            {
                ClientSize = new Size(ClientSize.Width, gridTop + desiredHeight + 10);
            }
        }

        private void dataGridAttraction_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (!float.TryParse(e.FormattedValue?.ToString(), out _))
            {
                e.Cancel = true;
            }
        }

        private void dataGridAttraction_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            var cellValue = dataGridAttraction.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
            if (float.TryParse(cellValue, out float value))
            {
                Particle.attractionLevels[e.RowIndex, e.ColumnIndex] = value;
            }
        }
        private void timerTick_Tick(object sender, EventArgs e)
        {
            this.Invalidate(); // retriggers paint event
            if (paused)
            {
                return;
            }
            cycles = (int)numericUpDownCycles.Value;
            for (int j = 0; j < cycles; j++)
            {
                GpuUpdateParticles();
            }
            labelAverageSpeed.Text = "Average Speed: " + GetAverageSpeed() + " pixels per tick";
        }

        private void GpuUpdateParticles()
        {
            // Prepare data for GPU
            var particleData = new ParticleGpu[particles.Length];
            for (int i = 0; i < particles.Length; i++)
            {
                var p = particles[i];
                particleData[i] = new ParticleGpu
                {
                    X = p.GetX(),
                    Y = p.GetY(),
                    XVel = p.xvel,
                    YVel = p.yvel,
                    PartType = p.partType
                };
            }
            int types = Particle.particleTypes;

            // Flatten attractionLevels and association for GPU
            var attractionFlat = new float[(types + 1) * (types + 1)];
            var assocFlat = new float[(types + 1) * (types + 1)];
            for (int i = 0; i <= types; i++)
                for (int j = 0; j <= types; j++)
                {
                    int idx = i * (types + 1) + j;
                    attractionFlat[idx] = Particle.attractionLevels[i, j];
                    assocFlat[idx] = Particle.assosiation[i, j];
                }

            var device = GraphicsDevice.GetDefault();
            using (var gpuParticles = device.AllocateReadWriteBuffer(particleData))
            using (var gpuAttraction = device.AllocateReadOnlyBuffer(attractionFlat))
            using (var gpuAssoc = device.AllocateReadOnlyBuffer(assocFlat))
            {
                var shader = new ParticleUpdateShader(gpuParticles, gpuAttraction, gpuAssoc, types, particles.Length, corner.X, corner.Y);
                device.For(particles.Length, in shader);
                var collisionShader = new ParticleCollisionShader(gpuParticles, particles.Length);
                device.For(particles.Length, in collisionShader);
                gpuParticles.CopyTo(particleData);
            }

            // Copy back to CPU particles
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].x = particleData[i].X;
                particles[i].y = particleData[i].Y;
                particles[i].xvel = particleData[i].XVel;
                particles[i].yvel = particleData[i].YVel;
            }

        }

        private void ResolveParticleCollisions()
        {
            const float minDistance = 2f;
            const float minDistanceSq = minDistance * minDistance;
            const float epsilon = 0.0001f;

            for (int i = 0; i < particles.Length; i++)
            {
                for (int j = i + 1; j < particles.Length; j++)
                {
                    var a = particles[i];
                    var b = particles[j];
                    float dx = a.x - b.x;
                    float dy = a.y - b.y;
                    float distSq = dx * dx + dy * dy;
                    if (distSq >= minDistanceSq || distSq <= epsilon)
                    {
                        continue;
                    }

                    float dist = MathF.Sqrt(distSq);
                    float nx = dx / dist;
                    float ny = dy / dist;

                    float overlap = (minDistance - dist) / 2f;
                    a.x += nx * overlap;
                    a.y += ny * overlap;
                    b.x -= nx * overlap;
                    b.y -= ny * overlap;

                    float relVel = (a.xvel - b.xvel) * nx + (a.yvel - b.yvel) * ny;
                    if (relVel < 0f)
                    {
                        float impulse = -relVel;
                        a.xvel += nx * impulse;
                        a.yvel += ny * impulse;
                        b.xvel -= nx * impulse;
                        b.yvel -= ny * impulse;
                    }
                }
            }
        }

        private void FormParticleSim_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
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

        private void FormParticleSim_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // closes all forms
        }

        private void buttonPausePlay_Click(object sender, EventArgs e)
        {
            TickPauseButton();
        }
        private void buttonRandomize_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].x = rand.Next(1, corner.X - 1);
                particles[i].y = rand.Next(1, corner.Y - 1);
                particles[i].xvel = 0;
                particles[i].yvel = 0;
            }

            labelAverageSpeed.Text = "Average Speed: " + GetAverageSpeed() + " pixels per tick";
            Invalidate();
        }
        private void buttonRandomizeAttraction_Click(object sender, EventArgs e)
        {
            const float minValue = 0.01f;
            const float maxValue = 10.00f;
            int size = Particle.attractionLevels.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    float value = (float)(rand.NextDouble() * (maxValue - minValue) + minValue);
                    Particle.attractionLevels[i, j] = value;
                    Particle.attractionLevels[j, i] = value;
                    dataGridAttraction.Rows[i].Cells[j].Value = value;
                    if (i != j)
                    {
                        dataGridAttraction.Rows[j].Cells[i].Value = value;
                    }
                }
            }
        }
        private void TickPauseButton()
        {
            paused = !paused;
            if (paused)
            {
                buttonPausePlay.Text = "Play";
            }
            else
            {
                buttonPausePlay.Text = "Pause";
            }
        }
        private void FormParticleSim_KeyDown(object sender, KeyEventArgs e)
        {
            // spacebar to pause/play
            if (e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void FormParticleSim_KeyUp(object sender, KeyEventArgs e)
        {
            // spacebar to pause/play
            if (e.KeyCode == Keys.Space)
            {
                TickPauseButton();
            }
        }

        private void FormParticleSim_Enter(object sender, EventArgs e)
        {
            TickPauseButton();
        }
    }
}
