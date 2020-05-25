using System;
using OpenTK;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Renderer.Framebuffers;
using SMRenderer.Core.Window;

namespace SMRenderer.Core.Plugin
{
    /// <include file='plugin.docu' path='Documentation/WindowPlugin/Class'/>
    public abstract class WindowPlugin
    {
        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Fields/Field[@name="NeededUsage"]'/>
        public abstract WindowUsage NeededUsage { get; }

        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Fields/Field[@name="Renderers"]'/>
        public virtual Type[] Renderers { get; }

        public virtual Framebuffer[] Framebuffers { get; }

        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Methods/VirtualMethod[@name="Load"]'/>
        public virtual void Load(GLWindow window) { }

        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Methods/VirtualMethod[@name="Loading"]'/>
        public virtual void Loading(EventArgs e, GLWindow window) { }
        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Methods/VirtualMethod[@name="BeforeRender"]'/>
        public virtual void BeforeRender(FrameEventArgs e, GLWindow window) { }
        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Methods/VirtualMethod[@name="Render"]'/>
        public virtual void Render(FrameEventArgs e, GLWindow window) { }
        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Methods/VirtualMethod[@name="AfterRender"]'/>
        public virtual void AfterRender(FrameEventArgs e, GLWindow window) { }
        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Methods/VirtualMethod[@name="BeforeUpdate"]'/>
        public virtual void BeforeUpdate(FrameEventArgs e, GLWindow window) { }
        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Methods/VirtualMethod[@name="Update"]'/>
        public virtual void Update(FrameEventArgs e, GLWindow window) { }
        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Methods/VirtualMethod[@name="AfterUpdate"]'/>
        public virtual void AfterUpdate(FrameEventArgs e, GLWindow window) { }
        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Methods/VirtualMethod[@name="Exit"]'/>
        public virtual void Exit(EventArgs e, GLWindow window) { }
        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Methods/VirtualMethod[@name="MouseMove"]'/>
        public virtual void MouseMove(EventArgs e, GLWindow window) { }
        /// <include file='plugin.docu' path='Documentation/WindowPlugin/Methods/VirtualMethod[@name="Resize"]'/>
        public virtual void Resize(EventArgs e, GLWindow window) { }

    }
}
