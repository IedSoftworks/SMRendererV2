using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Plugin;
using SMRenderer.Core.Renderer;
using SMRenderer.Core.Renderer.Framebuffers;
using SMRenderer.Core.Window;
using SMRenderer.Keybinds;
using SMRenderer.Models;
using SMRenderer.Renderer;
using SMRenderer.Scene.Cameras;

namespace SMRenderer.Window
{
    public class DefaultWindow : WindowPlugin
    {
        #region WindowInformations - Private

        private int _fov = 75;

        #endregion

        #region WindowInformations - Public

        public int FOV
        {
            get => _fov;
            set { _fov = value;
                CheckAndReload();
            }
        }
        #endregion


        public override WindowUsage NeededUsage { get; } = WindowUsage.All;
        public override Framebuffer[] Framebuffers { get; }
        public override GenericRenderer[] Renderers { get; }

        public Framebuffer MainFramebuffer;

        public GLWindow window;

        public DefaultWindow()
        {
            Framebuffers = new[] { MainFramebuffer = new Framebuffer(new ColorAttachmentCollection() { "color" }) };
            Renderers = new GenericRenderer[] {new GeneralRenderer(), new ParticleRenderer(), };
        }

        public override void Load(GLWindow window)
        {
            this.window = window;

            GenericRenderer.AttribIDs["aPosition"] = 0;
            GenericRenderer.AttribIDs["aTexture"] = 1;
            GenericRenderer.AttribIDs["aNormal"] = 2;
            GenericRenderer.AttribIDs["aColor"] = 3;

            GenericRenderer.DefaultFragData = new Dictionary<string, int>
            {
                {"color", 0 },
                {"bloom", 1 }
            };

            Meshes.Load();

            Framebuffer.MainFramebuffer = MainFramebuffer;
            Framebuffer.MainFramebuffer.Activate(false);
        }

        public override void Loading(EventArgs e, GLWindow window)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(window.GLInformation.ClearColor);
        }

        public override void Render(FrameEventArgs e, GLWindow window)
        {
            Scene.Scene.CurrentCam.CalculateMatrix();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Scene.Scene.Current.DrawAll(e.Time);
        }

        public override void Update(FrameEventArgs e, GLWindow window)
        {
            KeybindCollection.ExecuteAutoCheck();
        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            Camera2D.Calculate2DWorld(window.Aspect);
            Camera3D.Calculate3DWorld(window.Aspect);
        }
        private void CheckAndReload()
        {
            if (window != null && window.Exists)
            {
                window.Reload();
            }
        }
    }
}