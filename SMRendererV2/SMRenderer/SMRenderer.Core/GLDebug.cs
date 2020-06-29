using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Enums;

namespace SMRenderer.Core
{
    public class GLDebug
    {
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

        private static bool Active = false; 

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
            string messageString = Marshal.PtrToStringAnsi(message, length);

            Log.Write("GL_"+(type != DebugType.DontCare ? type.ToString().Substring(9) : "DontCare"), $"{severity} - {messageString}");

            if (type == DebugType.DebugTypeError)
            {
                throw new Exception(messageString);
            }
        }

        public static void Name(IGLObject obj, string label)
        {
            if (!Active) return;

            GL.ObjectLabel(obj.Identifier, obj.ID, label.Length, label);
        }

    }
}