using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM.Core.Enums;
using SM.Core.Exceptions;
using SM.Core.Plugin;
using SM.Core.Renderer.Framebuffers;

namespace SM.Core.Window
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
            X = settings.X;
            Y = settings.Y;

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
            Log.Write("#", "--- Initilize OpenGL ---",
                "    VSync: " + Settings.VSync, 
                "    Required OGL Version: " + Settings.MinorVersion,
                "    OpenGL Version: "+GL.GetString(StringName.Version),
                "    GLSL Version: "+GL.GetString(StringName.ShadingLanguageVersion));
            if (GLDebug.AdvDebugging)
            {
                Log.Write("#Ext", GLDebug.Extensions);
            }

            GLDebug.Setup();

            Window = this;

            GLInformation.Renderers.ForEach(a => a.Compile() );

            _load?.Invoke(e, this);
            
            Log.Write("#", "--- OpenGL Initialization is done ---");

            base.OnLoad(e);
        }

        /// <inheritdoc />
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            if (WindowState == WindowState.Minimized) return;

            _beforeRender?.Invoke(e, this);

            (DisableAutoDrawing ? Framebuffer.MainFramebuffer : Framebuffer.ScreenFramebuffer).Activate();
            _render?.Invoke(e, this);

            _afterRender?.Invoke(e, this);
            SwapBuffers();
            base.OnRenderFrame(e);
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
            if (WindowState == WindowState.Minimized) return;
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

        public Bitmap TakeScreenshot()  => TakeScreenshot(Width, Height);

        public Bitmap TakeScreenshot(int width, int height) => TakeScreenshot(0, 0, width, height);

        public Bitmap TakeScreenshot(int x, int y, int width, int height)
        {
            // Merke dir, welche Framebuffer-IDs gerade gebunden waren...
            GL.GetInteger(GetPName.FramebufferBinding, out int prevFBId);
            GL.GetInteger(GetPName.DrawFramebufferBinding, out int prevFBDrawId);
            GL.GetInteger(GetPName.ReadFramebufferBinding, out int prevFBReadId);

            Bitmap b = new Bitmap(width, height);
            System.Drawing.Imaging.BitmapData bits = b.LockBits(new Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);
            GL.ReadBuffer(ReadBufferMode.Front);
            GL.ReadPixels(x, y, width, height, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte,
                bits.Scan0);

            b.UnlockBits(bits);
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);

            // ...stelle ursprüngliches Binding wieder her:
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, prevFBId);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, prevFBDrawId);
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, prevFBReadId);

            return b;
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