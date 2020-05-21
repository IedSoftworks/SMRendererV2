using OpenTK.Graphics;

namespace SMRenderer.Core.Object
{
    public class Material
    {
        public static Texture DefaultTexture;

        public Color4 BaseColor = Color4.White;
        public Texture Texture = DefaultTexture;
    }
}