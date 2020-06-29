using SMRenderer.Core.Models;
using SMRenderer.Models;

namespace SMRenderer
{
    public class SMRenderer
    {
        public const int MAX_PARTICLES = 512;
        public const int MAX_DRAW_PARAMETER = 32;
        public const int MAX_LIGHTS = 4;

        public static Model DefaultModel = Meshes.Cube1;

        internal static void Setup() {}
    }
}