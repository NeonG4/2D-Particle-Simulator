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
        float xvel;
        float yvel;
        public Particle(float x, float y)
        {
            this.x = x;
            this.y = y;
            xvel = 0;
            yvel = 0;
        }
        public Particle(float x, float y, float xvel, float yvel)
        {
            this.x = x;
            this.y = y;
            this.xvel = xvel;
            this.yvel = yvel;
        }
        public void SetVelocity(float x, float y)
        {
            xvel = x;
            yvel = y;
        }
        public void Tick(Boundary[] bounds)
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
            g.FillEllipse(Brushes.White, x * scale, y * scale, 2 * scale, 2 * scale);
        }   
    }
}
