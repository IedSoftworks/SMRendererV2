using OpenTK.Graphics.OpenGL4;

namespace SM.Core.Renderer.Framebuffers
{
    public class ColorAttachment : TextureBase
    {
        public string FragOutputVariable;
        public FramebufferAttachment FramebufferAttachment => (FramebufferAttachment) AttachmentID + 36064;
        public DrawBufferMode DrawBufferMode => (DrawBufferMode) AttachmentID + 36064;
        public ReadBufferMode ReadBufferMode => (ReadBufferMode) AttachmentID + 36064;
        public DrawBuffersEnum DrawBuffersEnum => (DrawBuffersEnum) AttachmentID + 36064;

        public float Scale = 1;
        public int AttachmentID;

        public ColorAttachment(string fragOutputVariable, int attachmentId, float scale = 1)
        {
            FragOutputVariable = fragOutputVariable;
            AttachmentID = attachmentId;
            Scale = scale;
        }

        public static implicit operator int(ColorAttachment ca) => ca.AttachmentID;
    }
}