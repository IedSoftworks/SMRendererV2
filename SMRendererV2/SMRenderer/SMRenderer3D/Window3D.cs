using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base;
using SMRenderer.Base.Draw;
using SMRenderer.Base.Keybinds;
using SMRenderer.Base.Models;
using SMRenderer.Base.Renderer;
using SMRenderer.Base.Scene;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Plugin;
using SMRenderer.Core.Renderer;
using SMRenderer.Core.Renderer.Framebuffers;
using SMRenderer.Core.Window;

namespace SMRenderer3D
{
    public class Window3D : DefaultWindow
    {
        public override WindowUsage NeededUsage { get; } = WindowUsage.Load | WindowUsage.Render;

        public override Type[] Renderers { get; } = new[]
        {
            typeof(GeneralRenderer)
        };

        public override Framebuffer[] Framebuffers { get; }

        public int FOV;

        public Window3D(int FOV = 90)
        {
            this.FOV = FOV;
        }

        public override void Loading(EventArgs e, GLWindow window)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(window._glInformation.ClearColor);
        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            base.Resize(e, window);
            Camera.WorldMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), window.Width / window.Height, .1f, 100f);
        }
    }
}