using SM.Data.Types.VectorTypes;

namespace SM.Scene.Draw.Particles
{
    public class CubeParticles : ParticleObject
    {
        public override Direction MotionAlgorithm(int index)
        {
            return new Direction((float) (SMGlobals.Randomizer.NextDouble() * 2 - 1), (float)(SMGlobals.Randomizer.NextDouble() * 2 - 1), (float)(SMGlobals.Randomizer.NextDouble() * 2 - 1));
        }
    }
}