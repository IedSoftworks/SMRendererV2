using OpenTK.Graphics;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer.Core.Object;

namespace SMRenderer.Base.Objects
{
    public class Material
    {
        public static Texture DefaultTexture;

        public Color BaseColor = Color4.White;
        public Texture Texture = DefaultTexture;
    }
}