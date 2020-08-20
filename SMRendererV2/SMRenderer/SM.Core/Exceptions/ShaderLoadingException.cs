using System;

namespace SM.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown, when something went wrong at loading or compiling a shader program.
    /// </summary>
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