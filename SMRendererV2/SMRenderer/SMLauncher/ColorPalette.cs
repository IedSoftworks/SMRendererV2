using System.Windows.Media;

namespace SMLauncher
{
    public struct ColorPalette
    {
        public static ColorPalette Dark = new ColorPalette(Color.FromRgb(0,0,255), Color.FromRgb(30, 30, 30));

        public Color Foreground;
        public Color Background;

        public ColorPalette(Color foreground, Color background)
        {
            Foreground = foreground;
            Background = background;
        }
    }
}