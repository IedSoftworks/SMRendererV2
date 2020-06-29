using OpenTK.Graphics;

namespace SMRenderer.Types.VectorTypes
{
    public class Color : AnimatableType
    {
        public float R
        {
            get => X;
            set => X = value;
        }
        public float G
        {
            get => Y;
            set => Y = value;
        }
        public float B
        {
            get => Z;
            set => Z = value;
        }
        public float A
        {
            get => W;
            set => W = value;
        }

        /// <summary>
        ///     Creates a color, with values between 0 - 1
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public Color(float r, float g, float b, float a = 1)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static Color From255Basis(byte r, byte g, byte b, byte a = 255) => new Color(r / 255, g / 255, b / 255, a / 255);
        public static Color From255Basis(int r, int g, int b, int a = 255) => new Color(r / 255, g / 255, b / 255, a / 255);

        public static implicit operator Color4(Color c) => new Color4(c.R, c.G, c.B, c.A);
        public static implicit operator Color(Color4 c4) => new Color(c4.R, c4.G, c4.B, c4.A);

        public static implicit operator System.Drawing.Color(Color c) => System.Drawing.Color.FromArgb(
            (int) c.A * byte.MaxValue,
            (int) c.R * byte.MaxValue,
            (int) c.G * byte.MaxValue,
            (int) c.B * byte.MaxValue
        );
        public static implicit operator Color(System.Drawing.Color c) => Color.From255Basis(c.R, c.G, c.B, c.A);


        public override string ToString() => $"R:{X}, G:{Y}, B:{Z}, A:{W}";
    }
}