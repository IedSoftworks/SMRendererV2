using System;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Exceptions;
using SM.Core.Window;

namespace SM.Core.Renderer.Framebuffers
{
    /// <summary>
    /// Represents a OpenGL framebuffer
    /// </summary>
    public class Framebuffer : IGLObject
    {
        /// <summary>
        /// Contains the screen framebuffer.
        /// </summary>
        public static Framebuffer ScreenFramebuffer { get; } = new Framebuffer { ID = 0 };
        /// <summary>
        /// Contains the main framebuffer.
        /// </summary>
        public static Framebuffer MainFramebuffer = ScreenFramebuffer;
        /// <summary>
        /// Contains the currently active framebuffer.
        /// </summary>
        public static Framebuffer ActiveFramebuffer { get; internal set; } = MainFramebuffer;

        /// <inheritdoc />
        public int ID { get; set; }

        /// <inheritdoc />
        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.Framebuffer;

        /// <summary>
        /// Contains all color attachments.
        /// </summary>
        public ColorAttachmentCollection ColorAttachments { get; private set; } = new ColorAttachmentCollection();

        /// <summary>
        /// Represents the window the framebuffer is attached to.
        /// </summary>
        public GLWindow Window { get; private set; }

        /// <summary>
        /// Creates a empty framebuffer
        /// </summary>
        public Framebuffer()
        { }

        /// <summary>
        /// Creates a framebuffer with color attachments.
        /// <para>The window is selected over the 'GLWindow.Window'</para>
        /// </summary>
        /// <param name="colorAttachments">The color attachments</param>
        public Framebuffer(ColorAttachmentCollection colorAttachments)
        {
            ColorAttachments = colorAttachments;
            Window = GLWindow.Window;
        }
        /// <summary>
        /// Creates a framebuffer with color attachments and window.
        /// </summary>
        /// <param name="colorAttachments">The color attachments</param>
        /// <param name="window">The window</param>
        public Framebuffer(ColorAttachmentCollection colorAttachments, GLWindow window)
        {
            ColorAttachments = colorAttachments;
            Window = window;
        }

        /// <summary>
        /// Initialize the framebuffer
        /// </summary>
        public virtual void Initialize()
        {
            if (Window == null)
                Window = GLWindow.Window;

            ID = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ID);
            DrawBuffersEnum[] enums = new DrawBuffersEnum[ColorAttachments.Count];
            int c = 0;
            foreach (var attach in ColorAttachments)
            {
                attach.ID = GL.GenTexture();

                GL.BindTexture(TextureTarget.Texture2D, attach.ID);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, 
                    (int)(Window.Width * attach.Scale), (int)(Window.Height * attach.Scale),
                    0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureParameterName.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureParameterName.ClampToEdge);

                enums[c++] = attach.DrawBuffersEnum;
            }

            GL.DrawBuffers(enums.Length, enums);
            foreach (var attach in ColorAttachments)
            {
                GL.FramebufferTexture(FramebufferTarget.Framebuffer, attach.FramebufferAttachment, attach.ID, 0);
            }

            FramebufferErrorCode err = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (err != FramebufferErrorCode.FramebufferComplete)
                throw new LogException("Failed at loading framebuffer '" + GetType().Name + "'. Problem: " + err);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        /// <summary>
        /// Activates the framebuffer
        /// </summary>
        /// <param name="glBinding">Bind the framebuffer to OpenGL</param>
        /// <param name="clear">Clear the framebuffer</param>
        public virtual void Activate(bool glBinding = true, bool clear = false)
        {
            if (glBinding) GL.BindFramebuffer(FramebufferTarget.Framebuffer, ID);
            if (clear) GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            ActiveFramebuffer = this;
        }

        /// <summary>
        /// Adds attachments.
        /// </summary>
        /// <param name="variableNames">Names for the variables</param>
        public void AddAttachment(params string[] variableNames)
        {
            ColorAttachments.Add(variableNames);
        }

        /// <summary>
        /// Disposes the framebuffer.
        /// </summary>
        public virtual void Dispose()
        {
            ColorAttachments.ForEach(a => GL.DeleteTexture(a));
            GL.DeleteFramebuffer(this);
        }
        
        /// <summary>
        /// Returns the framebuffer ID
        /// </summary>
        /// <param name="f"></param>
        public static implicit operator int(Framebuffer f) => f.ID;
    }
}