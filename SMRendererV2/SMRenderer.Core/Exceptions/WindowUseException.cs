using System;

namespace SMRenderer.Core.Exceptions
{
    /// <include file='exceptions.docu' path='Documentation/WindowUseException/Class'/>
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