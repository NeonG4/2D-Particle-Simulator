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
        internal float x;
        internal float y;
        internal float xvel;
        internal float yvel;
        public int partType; // Positive for attraction, negative for repulsion
        public static float[,] attractionLevels;
        public static float[,] assosiation;
        public static int particleTypes;
        public static Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Magenta, Color.Yellow, Color.Cyan };
        public static void SetAttractionLevels(float[,] al, float[,] assoc)
        {
            if (al.GetLength(1) != al.GetLength(0))
            {
                throw new ArgumentException("Invalid attraction level dimensions");
            }
            if (al.GetLength(0) > colors.Length)
            {
                throw new ArgumentException("Too many attraction levels");
            }
            if (assoc.GetLength(0) != al.GetLength(0) || assoc.GetLength(1) != al.GetLength(0))
            {
                throw new ArgumentException("Association matrix must be same size as attraction levels");
            }
            particleTypes = al.GetLength(0) - 1;
            attractionLevels = al;
            assosiation = assoc;
        }
        public static Particle CopyParticle(Particle p)
        {
            return new Particle(p.x, p.y, p.xvel, p.yvel, p.partType);
        }
        public Particle(float x, float y, int particleType)
        {
            if (attractionLevels == null)
            {
                throw new Exception("Please call static SetAttractionLevels method before any constructor");
            }
            if (particleType < 0 || particleType > particleTypes)
            {
                throw new Exception("Illegal particleType value, out of bounds.");
            }
            this.x = x;
            this.y = y;
            xvel = 0;
            yvel = 0;

            
            this.partType = particleType;
        }
        public Particle(float x, float y, float xvel, float yvel, int particleType)
        {
            if (attractionLevels == null)
            {
                throw new Exception("Please call static SetAttractionLevels method before any constructor");
            }
            if (particleType < 0 || particleType > particleTypes)
            {
                throw new Exception("Illegal particleType value, out of bounds.");
            }
            this.x = x;
            this.y = y;
            this.xvel = xvel;
            this.yvel = yvel;
            this.partType= particleType;
        }
        public void SetVelocity(float x, float y)
        {
            xvel = x;
            yvel = y;
        }
        public float GetVelocity()
        {
            return MathF.Sqrt(xvel * xvel + yvel * yvel);
        }
        public float GetX()
        {
            return x;
        }
        public float GetY()
        {
            return y;
        }
        public bool IsInArea(float x, float y, float width, float height)
        {
            return this.x >= x && this.x <= x + width && this.y >= y && this.y <= y + height;
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
                    float dx = x - p.x;
                    float dy = y - p.y;
                    float distSq = dx * dx + dy * dy;
                    if (distSq < 100)
                    {
                        float dist = MathF.Sqrt(distSq);
                        if (dist > 0)
                        {
                            // Targeted distance for the pair; farther than target attracts, closer repels
                            float targetDistance = attractionLevels[this.partType, p.partType];
                            float targetAssociation = assosiation[this.partType, p.partType];
                            float force;
                            
                            force = targetAssociation * (dist - targetDistance) / (1 + (dist - targetDistance) * (dist - targetDistance));
                            
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
            xvel *= 0.99f;
            yvel *= 0.99f;
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

            using (var brush = new SolidBrush(colors[partType]))
            {
                g.FillEllipse(brush, (x - 1) * scale, (y - 1) * scale, 2 * scale, 2 * scale);
            }
        }   
    }
}
