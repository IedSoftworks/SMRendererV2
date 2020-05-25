using OpenTK;

namespace SMRenderer.Base.Types.VectorTypes
{
    public class Size : AnimatableType
    {
        public int Width
        {
            get => (int)X;
            set => X = value;
        }
        public int Height
        {
            get => (int)Y;
            set => Y = value;
        }
        public float Depth
        {
            get => (int)Z;
            set => Z = value;
        }
        public override float W
        {
            get => 0;
            set { return; }
        }

        public Size(int uniform)
        {
            Width = uniform;
            Height = uniform;
            Depth = uniform;
        }
        public Size(int width, int height, int depth = 1)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }
        public static Size Uniform2D(int uniform)
        {
            return new Size(uniform, uniform);
        }

        public override Matrix4 ToMatrix4() => Matrix4.CreateScale(Width, Height, Depth);

        
    }
}