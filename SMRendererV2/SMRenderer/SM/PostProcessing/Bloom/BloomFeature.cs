using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Core;
using SM.Core.Enums;
using SM.Core.Plugin;
using SM.Core.Renderer;
using SM.Core.Renderer.Framebuffers;
using SM.Core.Renderer.Shaders;
using SM.Core.Window;
using SM.Render.ShaderFiles;
using SM.Utility;

namespace SM.PostProcessing.Bloom
{
    public class BloomFeature : WindowPlugin
    {
        public override WindowUsage NeededUsage { get; } = WindowUsage.All;

        private readonly Framebuffer _bloomBuffer1, _bloomBuffer2;
        private readonly ColorAttachment _bloomTexture1, _bloomTexture2;
        private Matrix4 MVPDown;
        private Matrix4 MVP;

        public override GenericRenderer[] Renderers { get; } = { new BloomRenderer(), new BloomMergeRenderer() };
        public override Framebuffer[] Framebuffers { get;  }

        public BloomFeature()
        {
            _bloomTexture1 = new ColorAttachment("color", 0, BloomSettings.Scale);
            _bloomTexture2 = new ColorAttachment("color", 0, BloomSettings.Scale);

            _bloomBuffer1 = new Framebuffer(new ColorAttachmentCollection() { _bloomTexture1 });
            _bloomBuffer2 = new Framebuffer(new ColorAttachmentCollection() { _bloomTexture2 });

            Framebuffers = new[] {_bloomBuffer1, _bloomBuffer2};
        }

        public override void Load(GLWindow window)
        {
            Framebuffer.MainFramebuffer.ColorAttachments.Add("bloom");
            ShaderCatalog.MainFragmentFile.Extentions.Add(new ShaderFile(AssemblyUtility.ReadAssemblyFile("PostProcessing.Bloom.ShaderFiles.FragModifier.frag")));
            
            window.DisableAutoDrawing = true;
        }

        
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
            b.Save("test.png");

            // ...stelle ursprüngliches Binding wieder her:
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, prevFBId);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, prevFBDrawId);
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, prevFBReadId);

            b.Dispose();
        }
        
        public override void AfterRender(FrameEventArgs e, GLWindow window)
        {
            ColorAttachment mainColors = Framebuffer.MainFramebuffer.ColorAttachments["color"];
            ColorAttachment bloomColors = Framebuffer.MainFramebuffer.ColorAttachments["bloom"];

            BloomRenderer b = RendererCollection.Get<BloomRenderer>();
            BloomMergeRenderer m = RendererCollection.Get<BloomMergeRenderer>();

            GL.UseProgram(b);
            if (BloomSettings.Scale != 1) GL.Viewport(0,0, (int)(window.Width * BloomSettings.Scale), (int)(window.Height * BloomSettings.Scale));
            int loopCount = BloomSettings.LoopCount * 2;
            for (int i = 0; i < loopCount; i++)
            {
                TextureBase source;
                if (i % 2 == 0)
                {
                    _bloomBuffer1.Activate();

                    if (i == 0)
                    {
                        source = bloomColors;
                        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    }
                    else source = _bloomTexture2;
                }
                else
                {
                    source = _bloomTexture1;
                    _bloomBuffer2.Activate();
                }

                b.Draw(ref MVPDown, i % 2 == 0, source);
            }

            Framebuffer.ScreenFramebuffer.Activate();
            if (BloomSettings.Scale != 1) GL.Viewport(0, 0, window.Width, window.Height);
            m.Draw(ref MVP, mainColors, _bloomTexture2);

            GL.UseProgram(0);
        }

        public override void Resize(EventArgs e, GLWindow window)
        {
            MVPDown = Matrix4.CreateScale(window.Width * BloomSettings.Scale, window.Height * BloomSettings.Scale, 1) *
                  Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0) *
                  Matrix4.CreateOrthographic(window.Width * BloomSettings.Scale, window.Height * BloomSettings.Scale, .1f, 100f);

            MVP = Matrix4.CreateScale(window.Width, window.Height, 1) *
                  Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0) *
                  Matrix4.CreateOrthographic(window.Width, window.Height, .1f, 100f);
        }
    }
}