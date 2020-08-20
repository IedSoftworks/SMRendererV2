using SM.Data.Types;
using SM.Data.Types.VectorTypes;
using SM.Utility;

namespace SM.Scene.Draw.Particles
{
    public class CubeParticles : ParticleObject
    {
        public override Vector MotionAlgorithm(int index)
        {
            return new Vector(Randomize.GetFloat(1,2), Randomize.GetFloat(1, 2), Randomize.GetFloat(1, 2));
        }
    }
}