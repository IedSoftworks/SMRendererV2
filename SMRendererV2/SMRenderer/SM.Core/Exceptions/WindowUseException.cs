using System;

namespace SM.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown, if a window plugin causes a error.
    /// </summary>
    public class WindowUseException : LogException
    {
        /// <inheritdoc />
        public WindowUseException() { }

        /// <inheritdoc />
        public WindowUseException(string message) : base(message) { }

        /// <inheritdoc />
        public WindowUseException(string message, Exception inner) : base(message, inner) { }
    }
}