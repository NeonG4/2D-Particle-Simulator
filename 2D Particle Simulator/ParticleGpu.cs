using ComputeSharp;
using ComputeSharp.Descriptors;
using System.Numerics;

namespace _2D_Particle_Simulator
{
    public struct ParticleGpu
    {
        public float X;
        public float Y;
        public float XVel;
        public float YVel;
        public int PartType;
    }

    [GeneratedComputeShaderDescriptor]
    [ThreadGroupSize(64, 1, 1)]
    public readonly partial struct ParticleUpdateShader : IComputeShader
    {
        public readonly ReadWriteBuffer<ParticleGpu> particles;
        public readonly ReadOnlyBuffer<float> attractionLevels;
        public readonly ReadOnlyBuffer<float> association;
        public readonly int particleTypes;
        public readonly int particleCount;
        public readonly float boundsWidth;
        public readonly float boundsHeight;

        public ParticleUpdateShader(
            ReadWriteBuffer<ParticleGpu> particles,
            ReadOnlyBuffer<float> attractionLevels,
            ReadOnlyBuffer<float> association,
            int particleTypes,
            int particleCount,
            float boundsWidth,
            float boundsHeight)
        {
            this.particles = particles;
            this.attractionLevels = attractionLevels;
            this.association = association;
            this.particleTypes = particleTypes;
            this.particleCount = particleCount;
            this.boundsWidth = boundsWidth;
            this.boundsHeight = boundsHeight;
        }

        public void Execute()
        {
            int i = ThreadIds.X;
            if (i >= particleCount) return;
            ParticleGpu self = particles[i];
            float xvel = self.XVel;
            float yvel = self.YVel;
            for (int j = 0; j < particleCount; j++)
            {
                if (i == j) continue;
                ParticleGpu p = particles[j];
                float dx = self.X - p.X;
                float dy = self.Y - p.Y;
                float distSq = dx * dx + dy * dy;
                if (distSq < 100 && distSq > 0.0001f)
                {
                    float dist = Hlsl.Sqrt(distSq);
                    int idx = self.PartType * (particleTypes + 1) + p.PartType;
                    float targetDistance = attractionLevels[idx];
                    float targetAssociation = association[idx];
                    float force = targetAssociation * (dist - targetDistance) / (1 + (dist - targetDistance) * (dist - targetDistance));
                    float fx = force * dx / dist;
                    float fy = force * dy / dist;
                    xvel -= fx;
                    yvel -= fy;
                }
            }
            xvel *= 0.99f;
            yvel *= 0.99f;

            float newX = self.X + xvel;
            float newY = self.Y + yvel;
            const float epsilon = 0.01f;

            if (newX < 0)
            {
                newX = -newX + epsilon;
                xvel = -xvel;
            }
            else if (newX > boundsWidth)
            {
                newX = boundsWidth - (newX - boundsWidth) - epsilon;
                xvel = -xvel;
            }

            if (newY < 0)
            {
                newY = -newY + epsilon;
                yvel = -yvel;
            }
            else if (newY > boundsHeight)
            {
                newY = boundsHeight - (newY - boundsHeight) - epsilon;
                yvel = -yvel;
            }

            self.XVel = xvel;
            self.YVel = yvel;
            self.X = newX;
            self.Y = newY;
            particles[i] = self;
        }
    }
}
