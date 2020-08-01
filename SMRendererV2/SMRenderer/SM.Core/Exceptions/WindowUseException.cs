using System;

namespace SM.Core.Exceptions
{
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