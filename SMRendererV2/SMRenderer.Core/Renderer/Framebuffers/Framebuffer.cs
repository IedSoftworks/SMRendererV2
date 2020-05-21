using System;
using System.Collections.Generic;
using System.Net.Mime;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Exceptions;
using SMRenderer.Core.Window;

namespace SMRenderer.Core.Renderer.Framebuffers
{
    public class Framebuffer
    {
        public static Framebuffer ScreenFramebuffer { get; } = new Framebuffer {FramebufferID = 0, ColorAttachments = { new ColorAttachment("colo", 0) } };
        public static Framebuffer MainFramebuffer { get; internal set; } = ScreenFramebuffer;
        public static Framebuffer ActiveFramebuffer { get; internal set; } = MainFramebuffer;

        public int FramebufferID { get; private set; }
        public List<ColorAttachment> ColorAttachments { get; private set; }

        public GLWindow Window { get; }

        public Framebuffer()
        {
        }

        public Framebuffer(GLWindow window, List<ColorAttachment> colorAttachments)
        {
            ColorAttachments = colorAttachments;
            Window = window;

            FramebufferID = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferID);

            foreach (var attach in colorAttachments)
            {
                attach.TexID = GL.GenTexture();

                GL.BindTexture(TextureTarget.Texture2D, attach.TexID);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, window.Width, window.Height,
                    0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureParameterName.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureParameterName.ClampToEdge);

                GL.DrawBuffer(attach);
                GL.FramebufferTexture(FramebufferTarget.Framebuffer, attach, attach.TexID, 0);
            }

            FramebufferErrorCode err = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (err != FramebufferErrorCode.FramebufferComplete)
                throw new LogException("Failed at loading framebuffer '"+GetType().Name+"'. Problem: "+err);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Activate()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferID);
            ActiveFramebuffer = this;
        }
    }
}