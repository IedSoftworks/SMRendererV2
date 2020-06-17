using System;
using SMRenderer.Core.Enums;

namespace SMRenderer.Core.Exceptions
{
    public class LogException : Exception
    {
        public LogException() : base()
        {
            Log.Write(LogWriteType.Error, GetType().Name + "\n\n" + StackTrace);
        }

        /// <inheritdoc />
        public LogException(string message) : base(message)
        {
            Log.Write(LogWriteType.Error, GetType().Name+ ": " + message + "\n\n" + StackTrace);
        }

        /// <inheritdoc />
        public LogException(string message, Exception inner) : base(message, inner) { }
    }
}