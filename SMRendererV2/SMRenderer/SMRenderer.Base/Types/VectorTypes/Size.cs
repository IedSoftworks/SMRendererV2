using OpenTK;

namespace SMRenderer.Base.Types.VectorTypes
{
    public class Size : AnimatableType
    {
        public override float W { get; set; } = 0;

        public Size(float uniform)
        {
            X = uniform;
            Y = uniform;
            Z = uniform;
        }
        public Size(float width, float height, float depth = 1)
        {
            X = width;
            Y = height;
            Z = depth;
        }
        public static Size Uniform2D(float uniform)
        {
            return new Size(uniform, uniform);
        }

        public override Matrix4 ToMatrix4() => Matrix4.CreateScale(X, Y, Z);

        public override string ToString() => $"Width:{X}, Height:{Y}, Depth:{Z}";
    }
}