using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;

namespace SM.Core.Window
{
    /// <summary>
    /// Contains all information for the window.
    /// <para>Represents pretty much all options from the GameWindow-constructor.</para>
    /// </summary>
    public class WindowSettings
    {
        #region | Constructors |

        /// <summary>
        /// Create new settings with a percentage of available desktop space and centered window. 
        /// </summary>
        /// <param name="sizePer">Percentage, how much the window should take.</param>
        /// <param name="title">The window title</param>
        public WindowSettings(float sizePer, string title = "Default GLWindow Title")
        {
            Size screenS = Screen.PrimaryScreen.WorkingArea.Size;

            Width = (int)(screenS.Width * sizePer);
            Height = (int)(screenS.Height * sizePer);

            X = (screenS.Width - Width) / 2;
            Y = (screenS.Height - Height) / 2;
        }
        /// <summary>
        /// Create new settings with width and height.
        /// </summary>
        /// <param name="width">Width of the window</param>
        /// <param name="height">Height of the window</param>
        /// <param name="title">The window title</param>
        public WindowSettings(int width, int height, string title = "Default GLWindow Title")
        {
            Width = width;
            Height = height;
            Title = title;
        }
        #endregion

        #region | Fields |

        /// <summary>
        /// Width of the window
        /// </summary>
        public int Width;
        /// <summary>
        /// Height of the window
        /// </summary>
        public int Height;

        /// <summary>
        /// X-position of the window.
        /// </summary>
        public int X = -1;
        /// <summary>
        /// Y-position of the window.
        /// </summary>
        public int Y = -1;

        /// <summary>
        /// The window title
        /// </summary>
        public string Title = "Default GLWindow Title";

        /// <summary>
        /// The OpenGL GraphicsMode
        /// </summary>
        public GraphicsMode GraphicsMode = GraphicsMode.Default;
        /// <summary>
        /// GLWindow options regarding window appearance and behavior.
        /// </summary>
        public GameWindowFlags Options = GameWindowFlags.Default;
        /// <summary>
        /// The DisplayDevice to construct the GLWindow in.
        /// </summary>
        public DisplayDevice Device = DisplayDevice.Default;
        /// <summary>
        /// The major version for the OpenGL GraphicsContext.
        /// </summary>
        public int MajorVersion = 3;
        /// <summary>
        /// The minor version for the OpenGL GraphicsContext.
        /// </summary>
        public int MinorVersion = 1;
        /// <summary>
        /// The GraphicsContextFlags version for the OpenGL GraphicsContext.
        /// </summary>
        public GraphicsContextFlags Flags = GraphicsContextFlags.Default;
        /// <summary>
        /// An IGraphicsContext to share resources with.
        /// </summary>
        public IGraphicsContext SharedContext = null;
        /// <summary>
        /// Should the update and render frames be fired on the same thread? If false, render and update events will be fired from separate threads.
        /// <para>Default: true</para>
        /// </summary>
        public bool SingleThread = true;
        /// <summary>
        /// WindowState contains the information if f.E. the window should be fullscreen.#
        /// <para>Default: Normal</para>
        /// </summary>
        public WindowState WindowState = WindowState.Normal;
        /// <summary>
        /// The option if you want to use VSync.
        /// <para>Default: On</para>
        /// </summary>
        public VSyncMode VSync = VSyncMode.On;

        #endregion
    }
}