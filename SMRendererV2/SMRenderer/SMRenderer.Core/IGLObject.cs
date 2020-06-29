using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Core
{
    public interface IGLObject
    {
        int ID { get; set; }
        ObjectLabelIdentifier Identifier { get; set; }
    }
}