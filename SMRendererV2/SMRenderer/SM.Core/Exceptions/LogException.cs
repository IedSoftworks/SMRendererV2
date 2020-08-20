using System;
using SM.Core.Enums;

namespace SM.Core.Exceptions
{
    /// <summary>
    /// Logs automaticly the exception
    /// </summary>
    public class LogException : Exception
    {
        /// <inheritdoc />
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