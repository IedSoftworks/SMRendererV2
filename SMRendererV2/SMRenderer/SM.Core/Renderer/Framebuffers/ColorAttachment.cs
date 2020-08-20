using OpenTK.Graphics.OpenGL4;

namespace SM.Core.Renderer.Framebuffers
{
    /// <summary>
    /// This represents a color attachment inside a framebuffer.
    /// <para>Can be used like a texture, too.</para>
    /// </summary>
    public class ColorAttachment : TextureBase
    {
        /// <summary>
        /// This contains the variable the input from the shaders.
        /// </summary>
        public string FragOutputVariable;
        /// <summary>
        /// This returns the 'FramebufferAttachment'-id for this attachment.
        /// </summary>
        public FramebufferAttachment FramebufferAttachment => (FramebufferAttachment) AttachmentID + 36064;
        /// <summary>
        /// This returns the 'DrawBufferMode' for this attachment.
        /// </summary>
        public DrawBufferMode DrawBufferMode => (DrawBufferMode) AttachmentID + 36064;
        /// <summary>
        /// This returns the 'ReadBufferMode' for this attachment.
        /// </summary>
        public ReadBufferMode ReadBufferMode => (ReadBufferMode) AttachmentID + 36064;
        /// <summary>
        /// This returns the 'DrawBuffer'-id for this attachment.
        /// </summary>
        public DrawBuffersEnum DrawBuffersEnum => (DrawBuffersEnum) AttachmentID + 36064;

        /// <summary>
        /// This set the scale for the attachment
        /// </summary>
        public float Scale = 1;
        /// <summary>
        /// This contains the attachment id.
        /// </summary>
        public int AttachmentID;

        /// <summary>
        /// This creates a color attachment.
        /// </summary>
        /// <param name="fragOutputVariable">The variable for the input from shaders.</param>
        /// <param name="attachmentId">The id for the attachment.</param>
        /// <param name="scale">The scale for the attachment.</param>
        public ColorAttachment(string fragOutputVariable, int attachmentId, float scale = 1)
        {
            FragOutputVariable = fragOutputVariable;
            AttachmentID = attachmentId;
            Scale = scale;
        }
        /// <summary>
        /// Returns the pure id for the attachment.
        /// </summary>
        /// <param name="ca">The color attachment</param>
        public static implicit operator int(ColorAttachment ca) => ca.AttachmentID;
    }
}