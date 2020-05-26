using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base;
using SMRenderer.Base.Keybinds;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Plugin;
using SMRenderer.Core.Renderer;
using SMRenderer.Core.Renderer.Framebuffers;
using SMRenderer.Core.Window;
using SMRenderer3D.Renderer;

namespace SMRenderer3D
{
    public class Window3D : WindowPlugin
    {
        public override WindowUsage NeededUsage { get; } = WindowUsage.Load | WindowUsage.Render;

        public Framebuffer MainFramebuffer;

        public override Type[] Renderers { get; } = new[]
        {
            typeof(GeneralRenderer)
        };

        public override Framebuffer[] Framebuffers { get; }

        public int FOV;

        public Window3D(int FOV = 90)
        {
            this.FOV = FOV;

            Framebuffers = new[] { MainFramebuffer = new Framebuffer(new ColorAttachmentCollection() { "color" }) };

            Framebuffer.MainFramebuffer = MainFramebuffer;
            Framebuffer.MainFramebuffer.Activate(false);
        }

        public override void Load(GLWindow window)
        {
            GenericRenderer.AttribIDs["aPosition"] = 0;
            GenericRenderer.AttribIDs["aTexture"] = 1;
            GenericRenderer.AttribIDs["aNormal"] = 2;

            GenericRenderer.FragDataIDs["color"] = 0;
        }

        public override void Loading(EventArgs e, GLWindow window)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(window._glInformation.ClearColor);
        }

        public override void BeforeRender(FrameEventArgs e, GLWindow window)
        {

            //Scene.Current.Sort();
        }

        public override void Render(FrameEventArgs e, GLWindow window)
        {
            Scene.CurCam.CalculateView();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Scene.Current.DrawAll();
        }

        public override void Update(FrameEventArgs e, GLWindow window)
        {
            KeybindCollection.ExecuteAutoCheck();
        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            Camera.WorldMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), window.Width / window.Height, .1f, 100f);
        }
    }
}