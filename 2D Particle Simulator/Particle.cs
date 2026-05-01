using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _2D_Particle_Simulator
{
    internal class Particle
    {
        float x;
        float y;
        internal float xvel;
        internal float yvel;
        public float attraction; // Positive for attraction, negative for repulsion
        public Particle(float x, float y, float attraction = -10)
        {
            this.x = x;
            this.y = y;
            xvel = 0;
            yvel = 0;
            this.attraction = attraction;
        }
        public Particle(float x, float y, float xvel, float yvel, float attraction = -10)
        {
            this.x = x;
            this.y = y;
            this.xvel = xvel;
            this.yvel = yvel;
            this.attraction = attraction;
        }
        public void SetVelocity(float x, float y)
        {
            xvel = x;
            yvel = y;
        }
        public void Tick(Boundary[] bounds, Particle[] particles)
        {
            UpdateVelocities(particles);
            UpdatePosition(bounds);
        }
        private void UpdateVelocities(Particle[] particles)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                Particle p = particles[i];
                if (p == this) continue;
                // Only apply force once per unique pair
                if (this.GetHashCode() < p.GetHashCode())
                {
                    float dx = p.x - x;
                    float dy = p.y - y;
                    float distSq = dx * dx + dy * dy;
                    if (distSq < 100)
                    {
                        float dist = MathF.Sqrt(distSq);
                        if (dist > 0)
                        {
                            // Average the attraction values for the pair
                            float avgAttraction = (this.attraction + p.attraction) / 2f;
                            float force = avgAttraction / distSq;
                            float fx = force * dx / dist;
                            float fy = force * dy / dist;
                            // Apply equal and opposite forces
                            xvel -= fx;
                            yvel -= fy;
                            p.xvel += fx;
                            p.yvel += fy;
                        }
                    }
                }
            }
        }
        private void UpdatePosition(Boundary[] bounds)
        {
            float newx = x + xvel;
            float newy = y + yvel;
            float slope = (newy - y) / (newx - x);
            bool intersect = false;
            float storet = 0;
            float distance = -1;
            foreach (Boundary b in bounds)
            {
                // Line 1: particle's path from (x, y) to (newx, newy)
                // Line 2: boundary from (b.x1, b.y1) to (b.x2, b.y2)
                float denom = (x - newx) * (b.y1 - b.y2) - (y - newy) * (b.x1 - b.x2);
                float intx = 0, inty = 0;
                bool foundIntersection = false;
                if (denom != 0)
                {
                    float t = ((x - b.x1) * (b.y1 - b.y2) - (y - b.y1) * (b.x1 - b.x2)) / denom;
                    float u = -((x - newx) * (y - b.y1) - (y - newy) * (x - b.x1)) / denom;
                    // t is the parameter for the particle's path, u is the parameter for the boundary
                    if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
                    {
                        intx = x + t * (newx - x);
                        inty = y + t * (newy - y);
                        foundIntersection = true;
                    }
                    if (foundIntersection)
                    {
                        // Use intx and inty as the intersection point
                        intersect = true;
                        float d = MathF.Sqrt((intx - newx) * (intx - newx) + (inty - newy) * (inty - newy));
                        if (d < distance || distance == -1)
                        {
                            distance = d;
                            storet = t;
                        }
                    }
                }
            }
            storet -= 0.01f; // Move slightly back to prevent sticking to the boundary
            if (intersect)
            {
                x = x + storet * (newx - x);
                y = y + storet * (newy - y);
                // Reflect velocity based on the boundary's normal vector
                // Find the boundary that caused the intersection
                Boundary hitBoundary = null;
                float minDist = float.MaxValue;
                foreach (Boundary b in bounds)
                {
                    // Check if this boundary is close to the intersection point
                    float bx = b.x2 - b.x1;
                    float by = b.y2 - b.y1;
                    float px = x - b.x1;
                    float py = y - b.y1;
                    float proj = (px * bx + py * by) / (bx * bx + by * by);
                    float closestX = b.x1 + proj * bx;
                    float closestY = b.y1 + proj * by;
                    float dist = MathF.Sqrt((closestX - x) * (closestX - x) + (closestY - y) * (closestY - y));
                    if (dist < minDist)
                    {
                        minDist = dist;
                        hitBoundary = b;
                    }
                }
                if (hitBoundary != null)
                {
                    float bx = hitBoundary.x2 - hitBoundary.x1;
                    float by = hitBoundary.y2 - hitBoundary.y1;
                    // Normal vector (perpendicular to boundary)
                    float nx = -by;
                    float ny = bx;
                    // Normalize normal
                    float nlen = MathF.Sqrt(nx * nx + ny * ny);
                    if (nlen != 0)
                    {
                        nx /= nlen;
                        ny /= nlen;
                        // Velocity vector
                        float vdotn = xvel * nx + yvel * ny;
                        // Reflect velocity
                        xvel = xvel - 2 * vdotn * nx;
                        yvel = yvel - 2 * vdotn * ny;
                    }
                }
            }

            else
            {
                x = newx;
                y = newy;
            }
        }
        public void Draw(Graphics g, float scale)
        {
            // Map attraction value to color between red (repulsion) and blue (attraction)
            // Clamp attraction between -20 (red) and +20 (blue)
            float minAttr = -20f, maxAttr = 20f;
            float t = (attraction - minAttr) / (maxAttr - minAttr);
            t = MathF.Max(0, MathF.Min(1, t));
            // Red: (255,0,0), Blue: (0,0,255)
            int r = (int)(255 * (1 - t));
            int gcol = 0;
            int b = (int)(255 * t);
            using (var brush = new SolidBrush(System.Drawing.Color.FromArgb(r, gcol, b)))
            {
                g.FillEllipse(brush, x * scale, y * scale, 2 * scale, 2 * scale);
            }
        }   
    }
}
