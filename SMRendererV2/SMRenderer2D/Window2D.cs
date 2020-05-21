using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Object;
using SMRenderer.Core.Plugin;
using SMRenderer.Core.Renderer;
using SMRenderer.Core.Window;
using SMRenderer2D.Visual.Renderer;

namespace SMRenderer2D
{
    public class Window2D : WindowPlugin
    {
        public override WindowUsage NeededUsage { get; } = WindowUsage.Load | WindowUsage.Render;
        private Matrix4 mvp;
        private Model model;
        private Material material;

        public Window2D()
        {
            Renderers = new []
            {
                typeof(GeneralRenderer)
            };
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

            ApplySize(window);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Disable(EnableCap.DepthTest);

            GL.ClearColor(window._glInformation.ClearColor);
        }

        public override void BeforeRender(FrameEventArgs e, GLWindow window)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Scene.Current.Sort();
            Scene.CurCam.CalculateView();
        }

        public override void Render(FrameEventArgs e, GLWindow window)
        {
            Scene.Current.DrawAll();
        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            GL.Viewport(window.ClientRectangle);
            ApplySize(window);
        }

        private void ApplySize(GLWindow window)
        {
            Camera.WorldMatrix = Matrix4.CreateOrthographicOffCenter(0, window.Width, window.Height, 0, .1f, 100f);
        }
    }
}