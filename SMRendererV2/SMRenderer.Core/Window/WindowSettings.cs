using Microsoft.SqlServer.Server;
using OpenTK;
using OpenTK.Graphics;

namespace SMRenderer.Core.Window
{
    /// <include file='window.docu' path='Documentation/WindowSettings/Class'/>
    public struct WindowSettings
    {
        #region | Constructors |
        /// <include file='window.docu' path='Documentation/WindowSettings/Constructors/Constructor[@name="0"]'/>
        public WindowSettings(int width, int height) : this(width, height, "Default GLWindow Title")
        { }

        /// <include file='window.docu' path='Documentation/WindowSettings/Constructors/Constructor[@name="1"]'/>
        public WindowSettings(int width, int height, string title) : this(width, height, title, GraphicsMode.Default)
        { }

        /// <include file='window.docu' path='Documentation/WindowSettings/Constructors/Constructor[@name="2"]'/>
        public WindowSettings(int width, int height, string title, GraphicsMode graphicsMode) : this(width, height, title, graphicsMode, GameWindowFlags.Default)
        { }

        /// <include file='window.docu' path='Documentation/WindowSettings/Constructors/Constructor[@name="3"]'/>
        public WindowSettings(int width, int height, string title, GraphicsMode graphicsMode, GameWindowFlags options) : this(width, height, title, graphicsMode, options, DisplayDevice.Default)
        { }

        /// <include file='window.docu' path='Documentation/WindowSettings/Constructors/Constructor[@name="4"]'/>
        public WindowSettings(int width, int height, string title, GraphicsMode graphicsMode, GameWindowFlags options, DisplayDevice device) : this(width, height, title, graphicsMode, options, device, 1, 0)
        { }

        /// <include file='window.docu' path='Documentation/WindowSettings/Constructors/Constructor[@name="5"]'/>
        public WindowSettings(int width, int height, string title, GraphicsMode graphicsMode, GameWindowFlags options, DisplayDevice device, int majorVersion, int minorVersion) : this(width, height, title, graphicsMode, options, device, majorVersion, minorVersion, GraphicsContextFlags.Default)
        { }

        /// <include file='window.docu' path='Documentation/WindowSettings/Constructors/Constructor[@name="6"]'/>
        public WindowSettings(int width, int height, string title, GraphicsMode graphicsMode, GameWindowFlags options, DisplayDevice device, int majorVersion, int minorVersion, GraphicsContextFlags flags) : this(width, height, title, graphicsMode, options, device, majorVersion, minorVersion, flags, null)
        { }

        /// <include file='window.docu' path='Documentation/WindowSettings/Constructors/Constructor[@name="7"]'/>
        public WindowSettings(int width, int height, string title, GraphicsMode graphicsMode, GameWindowFlags options, DisplayDevice device, int majorVersion, int minorVersion, GraphicsContextFlags flags, IGraphicsContext sharedContext) : this(width, height, title, graphicsMode, options, device, majorVersion, minorVersion, flags, sharedContext, false)
        { }

        /// <include file='window.docu' path='Documentation/WindowSettings/Constructors/Constructor[@name="8"]'/>
        public WindowSettings(int width, 
            int height, 
            string title, 
            GraphicsMode graphicsMode, 
            GameWindowFlags options, 
            DisplayDevice device, 
            int majorVersion, 
            int minorVersion,
            GraphicsContextFlags flags, 
            IGraphicsContext sharedContext, 
            bool singleThread)
        {
            Width = width;
            Height = height;
            Title = title;
            GraphicsMode = graphicsMode;
            Options = options;
            Device = device;
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            Flags = flags;
            SharedContext = sharedContext;
            SingleThread = singleThread;
        }
        #endregion

        #region | Fields |
        /// <include file='window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Width"]'/>
        public int Width;
        /// <include file='window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Height"]'/>
        public int Height;
        /// <include file='window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Title"]'/>
        public string Title;

        /// <include file='window.docu' path='Documentation/WindowSettings/Fields/Field[@name="GraphicsMode"]'/>
        public GraphicsMode GraphicsMode;
        /// <include file='window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Options"]'/>
        public GameWindowFlags Options;
        /// <include file='window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Device"]'/>
        public DisplayDevice Device;
        /// <include file='window.docu' path='Documentation/WindowSettings/Fields/Field[@name="MajorVersion"]'/>
        public int MajorVersion;
        /// <include file='window.docu' path='Documentation/WindowSettings/Fields/Field[@name="MinorVersion"]'/>
        public int MinorVersion;
        /// <include file='window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Flags"]'/>
        public GraphicsContextFlags Flags;
        /// <include file='window.docu' path='Documentation/WindowSettings/Fields/Field[@name="SharedContext"]'/>
        public IGraphicsContext SharedContext;
        /// <include file='window.docu' path='Documentation/WindowSettings/Field[@name="SingleThread"]'/>
        public bool SingleThread;
        #endregion
    }
}