using OpenTK;

namespace SM.PostProcessing.Bloom
{
    public class BloomSettings
    {
        public static bool Enable = false;
        public static float Scale = .5f;

        public static float[] Weights = new float[] {.4f, .2f, .1f, .075f, .05f, .01f, .0075f, .005f, .0001f};
        public static Vector2 TextureOffset = Vector2.One;
        public static float SizeFactor = 0.0001f;
        public static float Multiplier = 2f;

        public static int LoopCount = 1;

        public static float BloomTexturePercentage = 1f;
    }
}