using System;
using OpenTK;
using SM.Data.Types;
using SM.Data.Types.Extensions;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Draw.Particles
{
    public class ParticleMovements
    {
        public static Matrix4 Linear(ParticleObject obj, Particle particle, DrawParticles drawParticles)
        {
            Vector pos = particle.Direction * drawParticles.TotalTime;
            return MatrixCalc.CreateScale(particle.Size) * Matrix4.CreateFromQuaternion(particle.Rotation) * Matrix4.CreateTranslation(pos.X, pos.Y, pos.Z);
        }
    }
}