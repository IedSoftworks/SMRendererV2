using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Controls;
using SM.Core.Enums;
using SM.Core.Plugin;
using SM.Core.Renderer;
using SM.Core.Renderer.Framebuffers;
using SM.Core.Window;
using SM.Data.Models;
using SM.Data.Types.Extensions;
using SM.Keybinds;
using SM.Render.ShaderPrograms;
using SM.Scene.Cameras;

namespace SM
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
            GenericRenderer.AttribIDs["aTangent"] = 4;

            GenericRenderer.DefaultFragData = new Dictionary<string, int>
            {
                {"color", 0 },
                {"bloom", 1 }
            };

            DataManage.Decompile();

            Framebuffer.MainFramebuffer = MainFramebuffer;
            Framebuffer.MainFramebuffer.Activate(false);
        }

        public override void Loading(EventArgs e, GLWindow window)
        {
            window.MouseMove += Mouse.Window_MouseMove;

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.FramebufferSrgb);

            GL.ClearColor(window.GLInformation.ClearColor);
        }

        public override void Render(FrameEventArgs e, GLWindow window)
        {
            SMGlobals.CurrentFrame %= ulong.MaxValue;
            SMGlobals.CurrentFrame++;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Scene.Scene.Current.DrawAll(e.Time);
        }

        public override void Update(FrameEventArgs e, GLWindow window)
        {
            SMGlobals.UpdateDeltatime = e.Time;

            KeybindCollection.ExecuteAutoCheck();
        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            OrthographicCamera.Calculate2DWorld(window.Aspect);
            PerspectiveCamera.Calculate3DWorld(window.Aspect);
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