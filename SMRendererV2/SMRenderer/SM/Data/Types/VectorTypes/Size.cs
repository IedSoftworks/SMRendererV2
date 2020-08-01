using System;
using OpenTK;

namespace SM.Data.Types.VectorTypes
{
    [Serializable]
    public class Size : AnimatableType
    {
        public Size(float uniform)
        {
            X = uniform;
            Y = uniform;
            Z = uniform;
        }
        public Size(float width = 1, float height = 1, float depth = 1)
        {
            X = width;
            Y = height;
            Z = depth;
        }
        public static Size Uniform2D(float uniform)
        {
            return new Size(uniform, uniform);
        }

        public override OpenTK.Matrix4 CalcMatrix4() => OpenTK.Matrix4.CreateScale(X, Y, Z);

        public override string ToString() => $"Width:{X}, Height:{Y}, Depth:{Z}";
    }
}