using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Object;

namespace SMRenderer.Core.Renderer.Framebuffers
{
    public class ColorAttachment : TextureBase
    {
        public string FragOutputVariable;

        public int AttachmentID;

        public ColorAttachment(string fragOutputVariable, int attachmentId)
        {
            FragOutputVariable = fragOutputVariable;
            AttachmentID = attachmentId;
        }

        public static implicit operator FramebufferAttachment(ColorAttachment ca) => (FramebufferAttachment) ca.AttachmentID + 36064;
        public static implicit operator DrawBuffersEnum(ColorAttachment ca) => (DrawBuffersEnum) ca.AttachmentID + 36064;
        public static implicit operator DrawBufferMode(ColorAttachment ca) => (DrawBufferMode) ca.AttachmentID + 36064;

        public static implicit operator int(ColorAttachment ca) => ca.AttachmentID;
    }
}