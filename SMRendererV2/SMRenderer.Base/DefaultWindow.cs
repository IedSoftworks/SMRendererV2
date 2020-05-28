using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base.Keybinds;
using SMRenderer.Base.Models;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Plugin;
using SMRenderer.Core.Renderer;
using SMRenderer.Core.Renderer.Framebuffers;
using SMRenderer.Core.Window;

namespace SMRenderer.Base
{
    public class DefaultWindow : WindowPlugin
    {
        public override WindowUsage NeededUsage { get; } = WindowUsage.All;
        public override Framebuffer[] Framebuffers { get; }
        public override Type[] Renderers { get; }

        public Framebuffer MainFramebuffer;

        public DefaultWindow()
        {
            Framebuffers = new[] { MainFramebuffer = new Framebuffer(new ColorAttachmentCollection() { "color" }) };

            Framebuffer.MainFramebuffer = MainFramebuffer;
            Framebuffer.MainFramebuffer.Activate(false);
        }

        public override void Load(GLWindow window)
        {
            GenericRenderer.AttribIDs["aPosition"] = 0;
            GenericRenderer.AttribIDs["aTexture"] = 1;
            GenericRenderer.AttribIDs["aNormal"] = 2;
            GenericRenderer.AttribIDs["aColor"] = 3;

            Meshes.Load();
        }

        public override void Render(FrameEventArgs e, GLWindow window)
        {
            Scene.Scene.CurrentCam.CalculateView();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Scene.Scene.Current.DrawAll();
        }

        public override void Update(FrameEventArgs e, GLWindow window)
        {
            KeybindCollection.ExecuteAutoCheck();
        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            GL.Viewport(window.ClientRectangle);
        }
    }
}