using OpenTK.Graphics;
using SM.Core.Renderer;
using SM.Data.Types.VectorTypes;

namespace SM.Data.Types.Extensions
{
    public static class UniformExtensions
    {
        public static void SetColor(this Uniform uniform, Color color)
        {
            uniform.SetUniform4((Color4) color);
        }
    }
}