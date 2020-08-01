using OpenTK.Graphics.OpenGL4;

namespace SM.Core
{
    public interface IGLObject
    {
        int ID { get; set; }
        ObjectLabelIdentifier Identifier { get; set; }
    }
}