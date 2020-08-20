using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Enums;

namespace SM.Core
{
    /// <summary>
    /// Stores Debugging informations
    /// </summary>
    public class GLDebug
    {
        /// <summary>
        /// Checks for OpenGL errors.
        /// </summary>
        /// <returns></returns>
        public static bool CheckGLErrors()
        {
            bool hasError = false;
            ErrorCode c;
            while ((c = GL.GetError()) != ErrorCode.NoError)
            {
                hasError = true;
                Log.Write("GLError", c.ToString());
            }

            return hasError;
        }

        private static DebugProc _debugProc = DebugCallback;
        private static GCHandle _debuGcHandle;

        /// <summary>
        /// Tells you if the "proper" way of debugging OpenGL is running.
        /// </summary>
        public static bool Active { get; private set; }= false;
        
        /// <summary>
        /// Turning this to true, allows for more advanced debugging informations.
        /// </summary>
        public static bool AdvDebugging { get; set; } = false;
        /// <summary>
        /// This contains all extensions for OpenGL;
        /// </summary>
        public static string[] Extensions { get; } = GL.GetString(StringName.Extensions).Split(' ');

        internal static void Setup()
        {
            try
            {
                _debuGcHandle = GCHandle.Alloc(_debugProc);

                GL.DebugMessageCallback(_debugProc, IntPtr.Zero);
                GL.Enable(EnableCap.DebugOutput);
                GL.Enable(EnableCap.DebugOutputSynchronous);

                Active = true;
            }
            catch
            {
                Log.Write(LogWriteType.Info, "Setup for GLDebugger failed. GLDebugger disabled. This happends, if your GPU driver don't have support for OpenGL 4 or the extension 'GL_KHR_DEBUG'.");
            }
        }

        private static void DebugCallback(DebugSource source,
            DebugType type,
            int id,
            DebugSeverity severity,
            int length,
            IntPtr message,
            IntPtr userParam)
        {
            if (type == DebugType.DebugTypeOther) return;

            string messageString = Marshal.PtrToStringAnsi(message, length);

            Log.Write("GL_"+(type != DebugType.DontCare ? type.ToString().Substring(9) : "DontCare"), $"{severity} - {messageString}");

            if (type == DebugType.DebugTypeError)
            {
                throw new Exception(messageString);
            }
        }

        /// <summary>
        /// Names the object for the Debugger.
        /// </summary>
        /// <param name="obj">The object, that need to be named.</param>
        /// <param name="label">The name</param>
        public static void Name(IGLObject obj, string label)
        {
            if (!Active) return;

            GL.ObjectLabel(obj.Identifier, obj.ID, label.Length, label);
        }

    }
}