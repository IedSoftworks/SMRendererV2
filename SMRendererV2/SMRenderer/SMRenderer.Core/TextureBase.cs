using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Core
{
    public class TextureBase : IGLObject
    {
        public int ID { get; set; } = -1;
        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.Texture;

        public int Height;
        public int Width;
    }
}