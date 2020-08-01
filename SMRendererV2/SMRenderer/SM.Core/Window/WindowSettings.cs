using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;

namespace SM.Core.Window
{
    /// <include file='Window.docu' path='Documentation/WindowSettings/Class'/>
    public class WindowSettings
    {
        #region | Constructors |

        public WindowSettings(float sizePer, string title = "Default GLWindow Title")
        {
            Size screenS = Screen.PrimaryScreen.WorkingArea.Size;

            Width = (int)(screenS.Width * sizePer);
            Height = (int)(screenS.Height * sizePer);

            X = (screenS.Width - Width) / 2;
            Y = (screenS.Height - Height) / 2;
        }

        public WindowSettings(int width, int height, string title = "Default GLWindow Title")
        {
            Width = width;
            Height = height;
            Title = title;
        }
        #endregion

        #region | Fields |
        public int Width = 500;
        public int Height = 500;

        public int X = -1;
        public int Y = -1;

        public string Title = "Default GLWindow Title";

        public GraphicsMode GraphicsMode = GraphicsMode.Default;
        public GameWindowFlags Options = GameWindowFlags.Default;
        public DisplayDevice Device = DisplayDevice.Default;
        public int MajorVersion = 5;
        public int MinorVersion = 3;
        public GraphicsContextFlags Flags = GraphicsContextFlags.Default;
        public IGraphicsContext SharedContext = null;
        public bool SingleThread = true;
        public WindowState WindowState = WindowState.Normal;
        public VSyncMode VSync = VSyncMode.On;

        #endregion
    }
}