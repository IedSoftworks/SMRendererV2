using OpenTK.Graphics;
using SMRenderer.Core.Renderer;
using SMRenderer.Types.VectorTypes;

namespace SMRenderer.Types.Extensions
{
    public static class UniformExtensions
    {
        public static void SetUniform4(this Uniform uniform, Color color)
        {
            uniform.SetUniform4((Color4) color);
        }
    }
}