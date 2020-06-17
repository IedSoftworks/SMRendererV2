using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Exceptions;
using SMRenderer.Core.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using OpenTK.Input;
using SMRenderer.Core.Renderer.Framebuffers;

namespace SMRenderer.Core.Window
{
    /// <include file='Window.docu' path='Documentation/GLWindow/Class'/>
    public class GLWindow : GameWindow
    {
        #region Fields
        public static GLWindow Window;
        
        public WindowSettings Settings;
        public GLInformation GLInformation;
        public Dictionary<Type, WindowPlugin> LoadedPlugins = new Dictionary<Type, WindowPlugin>();

        public Aspect Aspect = new Aspect();

        public bool DisableAutoDrawing = false;

        public bool AllowUnfocusedUpdates = false;

        #endregion

        #region Events

        private event Action<EventArgs, GLWindow> _load;
        private event Action<FrameEventArgs, GLWindow> _beforeRender;
        private event Action<FrameEventArgs, GLWindow> _render;
        private event Action<FrameEventArgs, GLWindow> _afterRender;
        private event Action<FrameEventArgs, GLWindow> _beforeUpdate;
        private event Action<FrameEventArgs, GLWindow> _update;
        private event Action<FrameEventArgs, GLWindow> _afterUpdate;
        private event Action<CancelEventArgs, GLWindow> _exit;
        private event Action<MouseMoveEventArgs, GLWindow> _mouseMove;
        private event Action<EventArgs, GLWindow> _resize;
        #endregion

        #region Constructor

        public GLWindow(WindowSettings settings, GLInformation glInformation) : base(settings.Width, settings.Height, settings.GraphicsMode, settings.Title, settings.Options, settings.Device, settings.MajorVersion, settings.MinorVersion, settings.Flags, settings.SharedContext, settings.SingleThread)
        {
            WindowState = settings.WindowState;
            VSync = settings.VSync;

            Settings = settings;
            GLInformation = glInformation;
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        protected override void OnLoad(EventArgs e)
        {
            Window = this;

            GLInformation.Renderers.ForEach(a => a.Compile() );

            _load?.Invoke(e, this);

            base.OnLoad(e);
        }

        /// <inheritdoc />
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            _beforeRender?.Invoke(e, this);

            (DisableAutoDrawing ? Framebuffer.MainFramebuffer : Framebuffer.ScreenFramebuffer).Activate();
            _render?.Invoke(e, this);

            _afterRender?.Invoke(e, this);
            SwapBuffers();
        }

        /// <inheritdoc />
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused && !AllowUnfocusedUpdates) return;

            base.OnUpdateFrame(e);
            _beforeUpdate?.Invoke(e, this);

            Timer.RunTick(e.Time);
            _update?.Invoke(e, this);
            
            _afterUpdate?.Invoke(e, this);

        }

        /// <inheritdoc />
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Aspect.Recalulate(Width, Height);

            GL.Viewport(ClientRectangle);

            InitilizeFramebuffers();
            _resize?.Invoke(e, this);
        }

        /// <inheritdoc />
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _exit?.Invoke(e, this);
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            _mouseMove?.Invoke(e, this);
        }

        #endregion

        #region Instance Methods
        public GLWindow Use(WindowPlugin plugin, WindowUsage usage, bool ignoreRequired = false)
        {
            if (LoadedPlugins.ContainsKey(plugin.GetType()))
                throw new WindowUseException($"The plugin '{plugin.GetType().Name}' is already loaded.");

            if (!ignoreRequired && !usage.HasFlag(plugin.NeededUsage))
                throw new WindowUseException("The plugin require usage that doesn't match the granted usage.");

            foreach (WindowUsage u in Enum.GetValues(typeof(WindowUsage)))
            {
                if (!usage.HasFlag(u) && !ignoreRequired) continue;

                switch (u)
                {
                    case WindowUsage.Resize:
                        _resize += plugin.Resize;
                        break;

                    case WindowUsage.Exit:
                        _exit += plugin.Exit;
                        break;
                    case WindowUsage.Load:
                        _load += plugin.Loading;
                        break;
                    case WindowUsage.MouseMove:
                        _mouseMove += plugin.MouseMove;
                        break;
                    case WindowUsage.Render:
                        _beforeRender += plugin.BeforeRender;
                        _render += plugin.Render;
                        _afterRender += plugin.AfterRender;
                        break;
                    case WindowUsage.Update:
                        _beforeUpdate += plugin.BeforeUpdate;
                        _update += plugin.Update;
                        _afterUpdate += plugin.AfterUpdate;
                        break;

                    case WindowUsage.AddRenderer:
                        if (plugin.Renderers != null) GLInformation.Renderers.AddRange(plugin.Renderers);
                        break;
                    case WindowUsage.AddFramebuffers:
                        if (plugin.Framebuffers != null) GLInformation.Framebuffers.AddRange(plugin.Framebuffers);
                        break;
                }
            }

            if (ignoreRequired || usage.HasFlag(WindowUsage.Load))
                plugin.Load(this);

            LoadedPlugins.Add(plugin.GetType(), plugin);

            return this;
        }
        public GLWindow Use(WindowUsage usage, bool ignoreRequired, params WindowPlugin[] types)
        {
            foreach (WindowPlugin type in types) Use(type, usage, ignoreRequired);
            return this;
        }
        public GLWindow Use(WindowUsage usage, params WindowPlugin[] types)
        {
            return Use(usage, false, types);
        }
        public GLWindow Use(KeyValuePair<WindowPlugin, WindowUsage>[] types, bool ignoreRequired = false)
        {
            foreach (KeyValuePair<WindowPlugin, WindowUsage> type in types)
                Use(type.Key, type.Value, ignoreRequired);

            return this;
        }

        public virtual void Reload()
        {
            GLInformation.Renderers.ForEach(a => GL.DeleteProgram(a));

            OnLoad(new EventArgs());
            OnResize(new EventArgs());
        }
        #endregion

        #region Private Instance Methods

        private void InitilizeFramebuffers()
        {
            GLInformation.Framebuffers.ForEach(a => a.Dispose());

            // Sometimes, frame buffer initialization fails
            // if the Window gets resized too often.
            // I found no better way around this:
            Thread.Sleep(50);

            GLInformation.Framebuffers.ForEach(a => a.Initilize());
        }

        #endregion
    }
}