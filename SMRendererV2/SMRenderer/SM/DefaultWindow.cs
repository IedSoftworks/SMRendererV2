using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Controls;
using SM.Core;
using SM.Core.Enums;
using SM.Core.Models;
using SM.Core.Plugin;
using SM.Core.Renderer;
using SM.Core.Renderer.Framebuffers;
using SM.Core.Window;
using SM.Data.Models;
using SM.Data.Types.Extensions;
using SM.Keybinds;
using SM.PostProcessing;
using SM.Render.ShaderPrograms;
using SM.Scene.Cameras;
using SM.Utility;

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
        public PostProcessingRenderer PostProcessingRenderer;

        public DefaultWindow()
        {
            Framebuffers = new[] { MainFramebuffer = new Framebuffer(new ColorAttachmentCollection() { "color" }) };
            Renderers = new GenericRenderer[] {new GeneralRenderer(), new ParticleRenderer(), PostProcessingRenderer = new PostProcessingRenderer()};
        }

        public override void Load(GLWindow window)
        {
            this.window = window;

            Model.AttribIDs["aPosition"] = 0;
            Model.AttribIDs["aTexture"] = 1;
            Model.AttribIDs["aNormal"] = 2;
            Model.AttribIDs["aColor"] = 3;
            Model.AttribIDs["aTangent"] = 4;

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

            GL.ClearColor(window.BackgroundColor);
        }

        public override void Render(FrameEventArgs e, GLWindow window)
        {
            SMGlobals.CurrentFrame %= ulong.MaxValue;
            SMGlobals.CurrentFrame++;

            Framebuffer.ScreenFramebuffer.Activate();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Scene.Scene.Current.DrawAll(e.Time);
        }

        bool Screenshot = true;

        public override void AfterRender(FrameEventArgs e, GLWindow window)
        {
            if (Screenshot)
            {
                window.TakeScreenshot(Framebuffer.ScreenFramebuffer, ReadBufferMode.Back, 0, 0,
                    window.Width, window.Height).Save("TestScreenshot-01.png");

                Screenshot = false;
            }

            return;
            Framebuffer.ScreenFramebuffer.Activate(clear: true);
            for (var i = 0; i < PostProcessManager.PostProcesses.Count; i++)
                PostProcessManager.PostProcesses[i].Prepare();

            PostProcessingRenderer.Draw(MainFramebuffer.ColorAttachments["color"]);
        }

        public override void Update(FrameEventArgs e, GLWindow window)
        {
            Deltatime.UpdateDelta = (float)e.Time;

            KeybindCollection.ExecuteAutoCheck();
            Timer.RunTick(SMGlobals.MasterDeltatime.DeltaTime);
        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            OrthographicCamera.Calculate2DWorld(window.Aspect);
            PerspectiveCamera.Calculate3DWorld(window.Aspect);

            PostProcessingRenderer.MVP = Matrix4.CreateScale(window.Width, window.Height, 1) *
                                         Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0) *
                                         Matrix4.CreateOrthographic(window.Width, window.Height, .1f, 100f);
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