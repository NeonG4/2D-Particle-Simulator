namespace _2D_Particle_Simulator
{
    public partial class FormParticleSim : Form
    {
        Particle[] particles = new Particle[1000];
        Boundary[] bounds = new Boundary[4];
        float scale = 1.5f;
        public FormParticleSim()
        {
            InitializeComponent();
            Random rand = new Random();
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i] = new Particle(rand.Next(1, 255), rand.Next(1, 255));
                particles[i].SetVelocity((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1));
            }
            bounds[0] = new Boundary(0, 0, 256, 0);
            bounds[1] = new Boundary(0, 0, 0, 256);
            bounds[2] = new Boundary(256, 256, 0, 256);
            bounds[3] = new Boundary(256, 256, 256, 0);
        }

        private void timerTick_Tick(object sender, EventArgs e)
        {
            this.Invalidate(); // retriggers paint event
        }

        private void FormParticleSim_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            foreach (Particle p in particles)
            {
                if (p != null)
                {
                    p.Tick(bounds);
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
    }
}
