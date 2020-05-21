using OpenTK;

namespace SMRenderer.Types.VectorTypes
{
    public class Size : AnimatableType
    {
        public int Width;
        public int Height;
        public float Depth;

        #region SetAnimateValues
        public override float X
        {
            get => Width;
            set => Width = (int)value;
        }
        public override float Y
        {
            get => Height;
            set => Height = (int)value;
        }
        public override float Z
        {
            get => Depth;
            set => Depth = (int)value;
        }
        public override float W
        {
            get => 0;
            set { return; }
        }
        #endregion

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