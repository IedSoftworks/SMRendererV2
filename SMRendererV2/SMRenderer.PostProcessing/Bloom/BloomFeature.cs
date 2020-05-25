using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Object;
using SMRenderer.Core.Plugin;
using SMRenderer.Core.Renderer.Framebuffers;
using SMRenderer.Core.Window;

namespace SMRenderer.PostProcessing.Bloom
{
    public class BloomFeature : WindowPlugin
    {
        public override WindowUsage NeededUsage { get; } = WindowUsage.All;

        public int LoopCount = 2;

        private Framebuffer _bloomBuffer1, _bloomBuffer2 = new Framebuffer();
        private ColorAttachment _bloomTexture1, _bloomTexture2;
        private Matrix4 MVP;

        public override Type[] Renderers { get; } = new[] {typeof(BloomRenderer)};
        public override Framebuffer[] Framebuffers { get; }

        public BloomFeature()
        {
            _bloomTexture1 = new ColorAttachment("color", 0);
            _bloomTexture2 = new ColorAttachment("color", 0);

            _bloomBuffer1 = new Framebuffer(new ColorAttachmentCollection() { _bloomTexture1 });
            _bloomBuffer2 = new Framebuffer(new ColorAttachmentCollection() { _bloomTexture2 });

            Framebuffers = new[] {_bloomBuffer1, _bloomBuffer2};
        }

        public override void Load(GLWindow window)
        {
            Framebuffer.MainFramebuffer.AddAttachment("bloom");
            
            window.DisableAutoDrawing = !true;
        }


        public override void AfterRender(FrameEventArgs e, GLWindow window)
        {
            ColorAttachment mainColors = Framebuffer.MainFramebuffer.ColorAttachments["color"];
            ColorAttachment bloomColors = Framebuffer.MainFramebuffer.ColorAttachments["bloom"];

            
            GL.UseProgram(BloomRenderer.renderer);

            int loopCount = LoopCount * 2;
            for (int i = 0; i < loopCount; i++)
            {
                TextureBase source;
                if (i % 2 == 0)
                {
                    source = i == 0
                        ? bloomColors
                        : _bloomTexture2;
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, _bloomBuffer1);
                }
                else
                {
                    source = _bloomTexture1;
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, i == loopCount - 1 ? Framebuffer.ScreenFramebuffer : _bloomBuffer2);
                }

                BloomRenderer.renderer.Draw(ref MVP, i % 2 == 0, i == loopCount - 1, window.Width, window.Height,
                    mainColors, source);
            }

            GL.UseProgram(0);
            return;
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, Framebuffer.MainFramebuffer);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, Framebuffer.ScreenFramebuffer);

            GL.ReadBuffer(bloomColors);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            GL.BlitFramebuffer(0, 0, window.Width, window.Height, 0, 0, window.Width, window.Height, ClearBufferMask.ColorBufferBit,
                BlitFramebufferFilter.Linear);

        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            MVP = Matrix4.CreateScale(window.Width, window.Height, 1) *
                  Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0) *
                  Matrix4.CreateOrthographic(window.Width, window.Height, .1f, 100f);
        }
    }
}