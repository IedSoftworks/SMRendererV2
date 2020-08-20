using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM.Core.Enums;
using SM.Core.Exceptions;
using SM.Core.Plugin;
using SM.Core.Renderer;
using SM.Core.Renderer.Framebuffers;

namespace SM.Core.Window
{
    public class GLWindow : GameWindow
    {
        #region Fields

        /// <summary>
        /// This contains all renderers that needs to load.
        /// </summary>
        private List<GenericRenderer> _renderers = new List<GenericRenderer>();
        /// <summary>
        /// This contains all framebuffer that needs to load.
        /// </summary>
        private List<Framebuffer> _framebuffers = new List<Framebuffer>();

        /// <summary>
        /// This contains the currently running window.
        /// </summary>
        public static GLWindow Window;
        
        /// <summary>
        /// This contains all settings for the window.
        /// </summary>
        public WindowSettings Settings;
        /// <summary>
        /// This contains all plugins loaded.
        /// </summary>
        public Dictionary<Type, WindowPlugin> LoadedPlugins = new Dictionary<Type, WindowPlugin>();

        /// <summary>
        /// This contains the currently active aspect ratio.
        /// </summary>
        public Aspect Aspect = new Aspect();
        /// <summary>
        /// Unfocused updates means, that the window keeps updating and rendering, even if the window isn't in focus.
        /// <para>Default: false</para>
        /// </summary>
        public bool AllowUnfocusedUpdates = false;
        /// <summary>
        /// This sets the background color of the window
        /// <para>Default: Black</para>
        /// </summary>
        public Color4 BackgroundColor = Color4.Black;

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

        /// <summary>
        /// This creates a new window with specific settings.
        /// </summary>
        /// <param name="settings">The settings</param>
        public GLWindow(WindowSettings settings) : base(settings.Width, settings.Height, settings.GraphicsMode, settings.Title, settings.Options, settings.Device, settings.MajorVersion, settings.MinorVersion, settings.Flags, settings.SharedContext, settings.SingleThread)
        {
            X = settings.X;
            Y = settings.Y;

            WindowState = settings.WindowState;
            VSync = settings.VSync;

            Settings = settings;
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        protected override void OnLoad(EventArgs e)
        {
            Log.Write("#", "--- Initilize OpenGL ---",
                "    VSync: " + Settings.VSync, 
                $"    Required OGL Version: {Settings.MajorVersion}.{Settings.MinorVersion}",
                "    OpenGL Version: "+GL.GetString(StringName.Version),
                "    GLSL Version: "+GL.GetString(StringName.ShadingLanguageVersion));
            if (GLDebug.AdvDebugging)
            {
                Log.Write("#Ext", GLDebug.Extensions);
            }

            GLDebug.Setup();

            Window = this;

            _renderers.ForEach(a => a.Compile() );

            _load?.Invoke(e, this);
            
            Log.Write("#", "--- OpenGL Initialization is done ---");

            base.OnLoad(e);
        }

        /// <inheritdoc />
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            if (!Focused && !AllowUnfocusedUpdates || WindowState == WindowState.Minimized) return;

            _beforeRender?.Invoke(e, this);

            _render?.Invoke(e, this);
            base.OnRenderFrame(e);

            _afterRender?.Invoke(e, this);
            SwapBuffers();
        }

        /// <inheritdoc />
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused && !AllowUnfocusedUpdates) return;

            base.OnUpdateFrame(e);
            _beforeUpdate?.Invoke(e, this);

            _update?.Invoke(e, this);
            
            _afterUpdate?.Invoke(e, this);

        }

        /// <inheritdoc />
        protected override void OnResize(EventArgs e)
        {
            if (!Focused || WindowState == WindowState.Minimized) return;
            base.OnResize(e);

            Aspect.Recalculate(Width, Height);

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
        /// <summary>
        /// Applies window plugins, by selecting one Usage, and selecting multiple plugins.
        /// </summary>
        /// <param name="usage">Usage allowence</param>
        /// <param name="types">Plugins</param>
        /// <returns>The GLWindow</returns>
        public GLWindow Use(WindowUsage usage, params WindowPlugin[] types)
        {
            return Use(usage, false, types);
        }
        /// <summary>
        /// Applies window plugins, by setting for each plugin the usage allowence.
        /// </summary>
        /// <param name="types">The list of plugins</param>
        /// <param name="ignoreRequired">This overrides every requirements set by the plugins.</param>
        /// <returns></returns>
        public GLWindow Use(KeyValuePair<WindowPlugin, WindowUsage>[] types, bool ignoreRequired = false)
        {
            foreach (KeyValuePair<WindowPlugin, WindowUsage> type in types)
                Use(type.Key, type.Value, ignoreRequired);

            return this;
        }
        /// <summary>
        /// Applies window plugins, by selecting one Usage, and selecting multiple plugins.
        /// </summary>
        /// <param name="usage">Usage allowence</param>
        /// <param name="ignoreRequired">This overrides every requirements set by the plugins.</param>
        /// <param name="types">Plugins</param>
        /// <returns>The GLWindow</returns>
        public GLWindow Use(WindowUsage usage, bool ignoreRequired, params WindowPlugin[] types)
        {
            foreach (WindowPlugin type in types) Use(type, usage, ignoreRequired);
            return this;
        }

        /// <summary>
        /// Applies a plugin.
        /// </summary>
        /// <param name="plugin">The plugin</param>
        /// <param name="usage"></param>
        /// <param name="ignoreRequired">This overrides every requirements set by the plugins.</param>
        /// <returns></returns>
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
                        if (plugin.Renderers != null) _renderers.AddRange(plugin.Renderers);
                        break;
                    case WindowUsage.AddFramebuffers:
                        if (plugin.Framebuffers != null) _framebuffers.AddRange(plugin.Framebuffers);
                        break;
                }
            }

            if (ignoreRequired || usage.HasFlag(WindowUsage.Load))
                plugin.Load(this);

            LoadedPlugins.Add(plugin.GetType(), plugin);

            return this;
        }

        /// <summary>
        /// This completely reloads the window.
        /// </summary>
        public virtual void Reload()
        {
            _renderers.ForEach(a => GL.DeleteProgram(a));

            OnLoad(new EventArgs());
            OnResize(new EventArgs());
        }

        /// <summary>
        /// This takes a screenshot of the current visible area of the window.
        /// </summary>
        public Bitmap TakeScreenshot()  => TakeScreenshot(Width, Height);

        /// <summary>
        /// This takes a screenshot of a specific width and height.
        /// </summary>
        public Bitmap TakeScreenshot(int width, int height) => TakeScreenshot(0, 0, width, height);

        /// <summary>
        /// This takes a screenshot of a specifc area inside the screen.
        /// </summary>
        public Bitmap TakeScreenshot(int x, int y, int width, int height) =>
            TakeScreenshot(Framebuffer.ScreenFramebuffer, ReadBufferMode.Front, x, y, width, height);

        /// <summary>
        /// This takes a screenshot of a specifc area inside the selected framebuffer.
        /// </summary>
        public Bitmap TakeScreenshot(Framebuffer framebuffer, ReadBufferMode readBuffer,  int x, int y, int width, int height)
        {
            GL.GetInteger(GetPName.FramebufferBinding, out int prevFBId);
            GL.GetInteger(GetPName.DrawFramebufferBinding, out int prevFBDrawId);
            GL.GetInteger(GetPName.ReadFramebufferBinding, out int prevFBReadId);

            Bitmap b = new Bitmap(width, height);
            System.Drawing.Imaging.BitmapData bits = b.LockBits(new Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, framebuffer);
            GL.ReadBuffer(readBuffer);
            GL.ReadPixels(x, y, width, height, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte,
                bits.Scan0);

            b.UnlockBits(bits);
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, prevFBId);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, prevFBDrawId);
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, prevFBReadId);

            return b;
        }

        #endregion

        #region Private Instance Methods

        private void InitilizeFramebuffers()
        {
            _framebuffers.ForEach(a => a.Dispose());

            // Sometimes, frame buffer initialization fails
            // if the Window gets resized too often.
            // I found no better way around this:
            Thread.Sleep(50);

            _framebuffers.ForEach(a => a.Initialize());
        }

        #endregion
    }
}