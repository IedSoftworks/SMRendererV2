using OpenTK;

namespace SMRenderer.PostProcessing.Bloom
{
    public class BloomSettings
    {
        public static float[] Weights = new float[] {.4f, .2f, .1f, .075f, .05f, .01f, .0075f};
        public static Vector2 TextureOffset = Vector2.One;
        public static float SizeFactor = 0.0005f;
        public static float Multiplier = 1f;
    }
}