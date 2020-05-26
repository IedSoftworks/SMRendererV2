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

        private Framebuffer _bloomBuffer1, _bloomBuffer2;
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
            Framebuffer.MainFramebuffer.ColorAttachments.Add("bloom");
            
            window.DisableAutoDrawing = true;
        }

        /*
        private void TakeScreenshot(int fboId, int attachmentId, int width, int height, int counter)
        {
            // Merke dir, welche Framebuffer-IDs gerade gebunden waren...
            GL.GetInteger(GetPName.FramebufferBinding, out int prevFBId);
            GL.GetInteger(GetPName.DrawFramebufferBinding, out int prevFBDrawId);
            GL.GetInteger(GetPName.ReadFramebufferBinding, out int prevFBReadId);

            Bitmap b = new Bitmap(width, height);
            System.Drawing.Imaging.BitmapData bits = b.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, fboId);
            GL.ReadBuffer(ReadBufferMode.ColorAttachment0 + attachmentId);
            GL.ReadPixels(0, 0, width, height, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, bits.Scan0);

            b.UnlockBits(bits);
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);
            b.Save("c:\\temp\\fedde_" + counter.ToString().PadLeft(6, '0') + "_scene_fbo" + fboId + "_attachment" + attachmentId + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            // ...stelle ursprüngliches Binding wieder her:
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, prevFBId);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, prevFBDrawId);
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, prevFBReadId);

            b.Dispose();
        }
        */
        private int c = 0;

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
                    if (i == 0) // Sonst wird noch der Blur vom letzten Durchgang weiter verwendet. Dadurch wird irgendwann alles weiß.
                        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
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

        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            MVP = Matrix4.CreateScale(window.Width, window.Height, 1) *
                  Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0) *
                  Matrix4.CreateOrthographic(window.Width, window.Height, .1f, 100f);
        }
    }
}