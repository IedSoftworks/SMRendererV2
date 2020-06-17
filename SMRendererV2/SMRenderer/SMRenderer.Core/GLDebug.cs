using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Core
{
    internal class GLDebug
    {
        private static DebugProcKhr callBack = DebugCallback;
        private static GCHandle gcHandle;

        internal static void Load()
        {
            gcHandle = GCHandle.Alloc(callBack);

            GL.Khr.DebugMessageCallback(callBack, IntPtr.Zero);
            GL.Enable(EnableCap.DebugOutputSynchronous);
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
            Log.Write(type.ToString(), $"{severity} | {messageString}");

            if (type == DebugType.DebugTypeError)
            {
                throw new Exception(messageString);
            }
        }

    }
}