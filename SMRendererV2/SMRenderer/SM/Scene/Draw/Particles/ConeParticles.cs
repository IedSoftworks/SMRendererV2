using OpenTK;
using SM.Data.Types;
using SM.Data.Types.VectorTypes;
using SM.Utility;

namespace SM.Scene.Draw.Particles
{
    public class ConeParticles : ParticleObject
    {
        public Vector2 Cone = Vector2.One;

        public override Vector MotionAlgorithm(int index)
        {
            return new Vector(Randomize.GetFloat(Cone.X, Cone.X * 2), 1f, Randomize.GetFloat(Cone.Y, Cone.Y * 2));
        }
    }
}