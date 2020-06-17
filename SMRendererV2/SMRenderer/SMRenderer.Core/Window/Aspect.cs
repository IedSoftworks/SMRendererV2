using System.Diagnostics.Contracts;

namespace SMRenderer.Core.Window
{
    public class Aspect
    {
        public float Ratio = 0;
        public int OriginalWidth = 0;
        public int OriginalHeight = 0;

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
            OriginalHeight = height;
            OriginalWidth = width;
            Ratio = CalculateRatio(width, height);
        }

        public int GetWidth(int height) => CalculateWidth(Ratio, height);
        public int GetWidth(float scale) => (int)(OriginalWidth * scale);
        public int GetHeight(int width) => CalculateHeight(Ratio, width);
        public int GetHeight(float scale) => (int)(OriginalHeight * scale);

        public static float CalculateRatio(int width, int height) => (float)width / height;
        public static int CalculateWidth(float ratio, int height) => (int)(height / ratio);
        public static int CalculateHeight(float ratio, int width) => (int)(ratio * width);
        
    }
}