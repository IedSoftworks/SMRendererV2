using OpenTK;

namespace SM.Core.Window
{
    public class Aspect
    {
        public float Ratio = 0;
        public Vector2 OriginalResolution = Vector2.Zero;
        public Vector2 ScaledResolution = Vector2.Zero;
        public Aspect(float ratio = 0)
        {
            Ratio = ratio;
        }

        public Aspect(int width, int height)
        {
            Recalulate(width, height);
        }

        public void Recalulate(int width, int height)
        {
            OriginalResolution = new Vector2(width, height);
            Ratio = CalculateRatio(width, height);
        }

        public int GetWidth(int height) => CalculateWidth(Ratio, height);
        public int GetWidth(float scale) => (int)(OriginalResolution.X * scale);
        public int GetHeight(int width) => CalculateHeight(Ratio, width);
        public int GetHeight(float scale) => (int)(OriginalResolution.Y * scale);

        public static float CalculateRatio(int width, int height)
        {
            
            return (float) width / height;
        }

        public static int CalculateWidth(float ratio, int height) => (int)(height * ratio);
        public static int CalculateHeight(float ratio, int width) => (int)(ratio / width);
        
    }
}