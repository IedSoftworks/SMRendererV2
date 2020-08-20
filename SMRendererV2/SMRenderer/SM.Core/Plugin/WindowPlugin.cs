using System;
using OpenTK;
using SM.Core.Enums;
using SM.Core.Renderer;
using SM.Core.Renderer.Framebuffers;
using SM.Core.Window;

namespace SM.Core.Plugin
{
    /// <summary>
    /// Allows access to the window.
    /// </summary>
    public abstract class WindowPlugin
    {
        /// <summary>
        /// The minimum required usage allowence.
        /// </summary>
        public abstract WindowUsage NeededUsage { get; }

        /// <summary>
        /// Allows the plugin to add own render programs.
        /// </summary>
        public virtual GenericRenderer[] Renderers { get; }

        /// <summary>
        /// Allows the plugin to add own framebuffers.
        /// </summary>
        public virtual Framebuffer[] Framebuffers { get; }

        /// <summary>
        /// This is called, when the plugin is connected to a window.
        /// </summary>
        /// <param name="window">The connected window</param>
        public virtual void Load(GLWindow window) { }

        /// <summary>
        /// This is called, when the connected window is loading.
        /// </summary>
        /// <param name="e">The default event arguments.</param>
        /// <param name="window">The connected window.</param>
        public virtual void Loading(EventArgs e, GLWindow window) { }
        /// <summary>
        /// This is called, before the window starts to render.
        /// </summary>
        /// <param name="e">Raw frame event arguments</param>
        /// <param name="window">The connected window</param>
        public virtual void BeforeRender(FrameEventArgs e, GLWindow window) { }
        /// <summary>
        /// This is called, when the window should render something.
        /// </summary>
        /// <param name="e">Raw frame event arguments</param>
        /// <param name="window">The connected window</param>
        public virtual void Render(FrameEventArgs e, GLWindow window) { }
        /// <summary>
        /// This is called, after the window is done rendering.
        /// <para>This usefully for post processing</para>
        /// </summary>
        /// <param name="e">Raw frame event arguments</param>
        /// <param name="window">The connected window</param>
        public virtual void AfterRender(FrameEventArgs e, GLWindow window) { }
        /// <summary>
        /// This is called, before the window is updating.
        /// </summary>
        /// <param name="e">Raw frame event arguments</param>
        /// <param name="window">The connected window</param>
        public virtual void BeforeUpdate(FrameEventArgs e, GLWindow window) { }
        /// <summary>
        /// This is called, when the window updates.
        /// </summary>
        /// <param name="e">Raw frame event arguments</param>
        /// <param name="window">The connected window</param>
        public virtual void Update(FrameEventArgs e, GLWindow window) { }
        /// <summary>
        /// This is called, after the window is done updating.
        /// </summary>
        /// <param name="e">Raw frame event arguments</param>
        /// <param name="window">The connected window</param>
        public virtual void AfterUpdate(FrameEventArgs e, GLWindow window) { }
        /// <summary>
        /// This is called, when the window is closing.
        /// </summary>
        /// <param name="e">Raw event arguments</param>
        /// <param name="window">The connected window</param>
        public virtual void Exit(EventArgs e, GLWindow window) { }
        /// <summary>
        /// This is called, if the mouse is moving.
        /// </summary>
        /// <param name="e">Raw event arguments</param>
        /// <param name="window">The connected window</param>
        public virtual void MouseMove(EventArgs e, GLWindow window) { }
        /// <summary>
        /// This is called, when the window has been resized.
        /// <para>Do not initialize framebuffers or render programs here! This happens inside the window itself.</para>
        /// </summary>
        /// <param name="e">Raw event arguments</param>
        /// <param name="window">The connected window</param>
        public virtual void Resize(EventArgs e, GLWindow window) { }
        /// <summary>
        /// This is called, when the window is needed to completely renew itself.
        /// </summary>
        public virtual void WindowUpdate() { }
    }
}
