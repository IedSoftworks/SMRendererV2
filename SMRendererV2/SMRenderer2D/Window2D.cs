using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base;
using SMRenderer.Base.Keybinds;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Object;
using SMRenderer.Core.Plugin;
using SMRenderer.Core.Renderer;
using SMRenderer.Core.Renderer.Framebuffers;
using SMRenderer.Core.Window;
using SMRenderer2D.Renderer;
using Material = SMRenderer2D.Objects.Material;

namespace SMRenderer2D
{
    public class Window2D : WindowPlugin
    {
        public override WindowUsage NeededUsage { get; } = WindowUsage.Load | WindowUsage.Render;

        public Framebuffer MainFramebuffer;

        public override Type[] Renderers { get; } = new[]
        {
            typeof(GeneralRenderer)
        };

        public override Framebuffer[] Framebuffers { get; }

        public Window2D()
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

            GenericRenderer.FragDataIDs["color"] = 0;
        }

        public override void Loading(EventArgs e, GLWindow window)
        {
            Bitmap empty = new Bitmap(1,1);
            empty.SetPixel(0,0, Color.White);
            Material.DefaultTexture = new Texture(empty, TextureMinFilter.Nearest, TextureWrapMode.ClampToBorder, true);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Disable(EnableCap.DepthTest);

            GL.ClearColor(window._glInformation.ClearColor);
        }

        public override void BeforeRender(FrameEventArgs e, GLWindow window)
        {

            //Scene.Current.Sort();
            Scene.CurCam.CalculateView();
        }

        public override void Render(FrameEventArgs e, GLWindow window)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Scene.Current.DrawAll();
        }

        public override void Update(FrameEventArgs e, GLWindow window)
        {
            KeybindCollection.ExecuteAutoCheck();
        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            GL.Viewport(window.ClientRectangle);

            Camera.WorldMatrix = Matrix4.CreateOrthographicOffCenter(0, window.Width, window.Height, 0, .1f, 100f);
        }
    }
}