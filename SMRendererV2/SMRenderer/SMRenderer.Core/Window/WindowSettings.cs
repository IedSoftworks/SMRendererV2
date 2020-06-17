using OpenTK;
using OpenTK.Graphics;

namespace SMRenderer.Core.Window
{
    /// <include file='Window.docu' path='Documentation/WindowSettings/Class'/>
    public class WindowSettings
    {
        #region | Constructors |
        /// <include file='Window.docu' path='Documentation/WindowSettings/Constructors/Constructor[@name="0"]'/>
        public WindowSettings(int width, int height) : this(width, height, "Default GLWindow Title")
        { }

        /// <include file='Window.docu' path='Documentation/WindowSettings/Constructors/Constructor[@name="1"]'/>
        public WindowSettings(int width, int height, string title)
        {
            Width = width;
            Height = height;
            Title = title;
        }
        #endregion

        #region | Fields |
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Width"]'/>
        public int Width = 500;
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Height"]'/>
        public int Height = 500;
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Title"]'/>
        public string Title = "Default GLWindow Title";

        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="GraphicsMode"]'/>
        public GraphicsMode GraphicsMode = GraphicsMode.Default;
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Options"]'/>
        public GameWindowFlags Options = GameWindowFlags.Default;
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Device"]'/>
        public DisplayDevice Device = DisplayDevice.Default;
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="MajorVersion"]'/>
        public int MajorVersion = 1;
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="MinorVersion"]'/>
        public int MinorVersion = 0;
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="Flags"]'/>
        public GraphicsContextFlags Flags = GraphicsContextFlags.Default;
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="SharedContext"]'/>
        public IGraphicsContext SharedContext = null;
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="SingleThread"]'/>
        public bool SingleThread = true;
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="WindowState"]'/>
        public WindowState WindowState = WindowState.Normal;
        /// <include file='Window.docu' path='Documentation/WindowSettings/Fields/Field[@name="VSync"]'/>
        public VSyncMode VSync = VSyncMode.On;

        #endregion
    }
}