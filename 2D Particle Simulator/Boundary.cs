using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Particle_Simulator
{
    internal class Boundary
    {
        public readonly float x1;
        public readonly float y1;
        public readonly float x2;
        public readonly float y2;
        public Boundary(float x1, float y1, float x2, float y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
        public void Draw(Graphics g, float scale)
        {
            g.DrawLine(new Pen(Color.White), new Point((int)(x1 * scale), (int)(y1 * scale)), new Point((int)(x2 * scale), (int)(y2 * scale)));
        }
        public float GetSlope()
        {
            return (y1 - y2) / (x1 - x2);
        }
        public float GetLength()
        {
            return MathF.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
    }
}
