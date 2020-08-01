using System;

namespace SM.Data.Types.VectorTypes
{
    [Serializable]
    public class Direction : AnimatableType
    {
        public Direction(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}