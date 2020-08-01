using OpenTK;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Draw.Particles
{
    public class ConeParticles : ParticleObject
    {
        public Vector2 Cone = Vector2.One;

        public override Direction MotionAlgorithm(int index)
        {
            return new Direction((float)SMGlobals.Randomizer.NextDouble() * (Cone.X * 2) - Cone.X, 1f, (float)SMGlobals.Randomizer.NextDouble() * (Cone.Y * 2) - Cone.Y);
        }
    }
}