using System;

namespace SM.Core.Exceptions
{
    public class ShaderLoadingException : LogException
    {
        /// <inheritdoc />
        public ShaderLoadingException() { }

        /// <inheritdoc />
        public ShaderLoadingException(string message) : base(message) { }

        /// <inheritdoc />
        public ShaderLoadingException(string message, Exception inner) : base(message, inner) { }
    }
}