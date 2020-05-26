using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Exceptions;
using SMRenderer.Core.Window;

namespace SMRenderer.Core.Renderer.Framebuffers
{
    public class Framebuffer
    {
        public static Framebuffer ScreenFramebuffer { get; } = new Framebuffer { FramebufferID = 0 };
        public static Framebuffer MainFramebuffer = ScreenFramebuffer;
        public static Framebuffer ActiveFramebuffer { get; internal set; } = MainFramebuffer;

        public int FramebufferID { get; private set; }
        public ColorAttachmentCollection ColorAttachments { get; private set; } = new ColorAttachmentCollection();

        public GLWindow Window { get; private set; }

        public Framebuffer()
        { }

        public Framebuffer(ColorAttachmentCollection colorAttachments)
        {
            ColorAttachments = colorAttachments;
            Window = GLWindow.window;
        }
        public Framebuffer(ColorAttachmentCollection colorAttachments, GLWindow window)
        {
            ColorAttachments = colorAttachments;
            Window = window;
        }

        public virtual void Initilize()
        {
            if (Window == null)
                Window = GLWindow.window;

            FramebufferID = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferID);
            DrawBuffersEnum[] enums = new DrawBuffersEnum[ColorAttachments.Count];
            int c = 0;
            foreach (var attach in ColorAttachments)
            {
                attach.TexID = GL.GenTexture();

                GL.BindTexture(TextureTarget.Texture2D, attach.TexID);
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
                GL.FramebufferTexture(FramebufferTarget.Framebuffer, attach.FramebufferAttachment, attach.TexID, 0);
            }

            FramebufferErrorCode err = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (err != FramebufferErrorCode.FramebufferComplete)
                throw new LogException("Failed at loading framebuffer '" + GetType().Name + "'. Problem: " + err);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public virtual void Activate(bool GLBinding = true)
        {
            if (GLBinding) GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferID);
            ActiveFramebuffer = this;
        }

        public void AddAttachment(params string[] variableNames)
        {
            ColorAttachments.Add(variableNames);
        }


        public virtual void Dispose()
        {
            ColorAttachments.ForEach(a => GL.DeleteTexture(a));
            GL.DeleteFramebuffer(this);
        }

        public static implicit operator int(Framebuffer f) => f.FramebufferID;
    }
}