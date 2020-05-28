using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base;
using SMRenderer.Base.Keybinds;
using SMRenderer.Base.Renderer;
using SMRenderer.Base.Scene;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Plugin;
using SMRenderer.Core.Renderer;
using SMRenderer.Core.Renderer.Framebuffers;
using SMRenderer.Core.Window;

namespace SMRenderer2D
{
    public class Window2D : DefaultWindow
    {
        public override WindowUsage NeededUsage { get; } = WindowUsage.Load | WindowUsage.Render;

        public Framebuffer MainFramebuffer;

        public override Type[] Renderers { get; } = new[]
        {
            typeof(GeneralRenderer)
        };

        public override Framebuffer[] Framebuffers { get; }

        public override void Loading(EventArgs e, GLWindow window)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Disable(EnableCap.DepthTest);

            GL.ClearColor(window._glInformation.ClearColor);
        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            base.Resize(e, window);

            Camera.WorldMatrix = Matrix4.CreateOrthographicOffCenter(0, window.Width, window.Height, 0, .1f, 100f);
        }
    }
}