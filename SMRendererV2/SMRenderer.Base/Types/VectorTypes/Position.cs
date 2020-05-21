using OpenTK;

namespace SMRenderer.Base.Types.VectorTypes
{
    public class Position : AnimatableType
    {
        public override float X { get; set; }
        public override float Y { get; set; }
        public override float Z { get; set; }
        public override float W { get; set; } = 0;

        public Position(float x, float y, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Position MoveAlongSizeAxis(Size size, float xPercent, float yPercent)
        {
            return MoveAlongSizeAxis(size.Width, size.Height, xPercent, yPercent);
        }

        public Position MoveAlongSizeAxis(int width, int height, float xPercent, float yPercent)
        {
            X += width * (xPercent / 100);
            Y += height * (yPercent / 100);

            return this;
        }

        public override Matrix4 ToMatrix4() => Matrix4.CreateTranslation(X, Y, Z);
    }
}