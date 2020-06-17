using System;
using OpenTK;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Renderer;
using SMRenderer.Core.Renderer.Framebuffers;
using SMRenderer.Core.Window;

namespace SMRenderer.Core.Plugin
{
    public abstract class WindowPlugin
    {
        public abstract WindowUsage NeededUsage { get; }

        public virtual GenericRenderer[] Renderers { get; }

        public virtual Framebuffer[] Framebuffers { get; }

        public virtual void Load(GLWindow window) { }

        public virtual void Loading(EventArgs e, GLWindow window) { }
        public virtual void BeforeRender(FrameEventArgs e, GLWindow window) { }
        public virtual void Render(FrameEventArgs e, GLWindow window) { }
        public virtual void AfterRender(FrameEventArgs e, GLWindow window) { }
        public virtual void BeforeUpdate(FrameEventArgs e, GLWindow window) { }
        public virtual void Update(FrameEventArgs e, GLWindow window) { }
        public virtual void AfterUpdate(FrameEventArgs e, GLWindow window) { }
        public virtual void Exit(EventArgs e, GLWindow window) { }
        public virtual void MouseMove(EventArgs e, GLWindow window) { }
        public virtual void Resize(EventArgs e, GLWindow window) { }
        public virtual void WindowUpdate() { }
    }
}
