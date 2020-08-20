using OpenTK;
using SM.Data.Types;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Draw.Particles
{
    public struct Particle
    {
        public Vector Direction;
        
        public Vector Size;
        public Quaternion Rotation;
    }
}