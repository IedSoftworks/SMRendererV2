using OpenTK;

namespace SMRenderer.Types.VectorTypes
{
    public class Position : AnimatableType
    {
        public override float W { get; set; } = 0;

        public Position(float uniform) : this(uniform, uniform, uniform) {}
        public Position(float x, float y, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override Matrix4 ToMatrix4() => Matrix4.CreateTranslation(X, Y, Z);
    }
}