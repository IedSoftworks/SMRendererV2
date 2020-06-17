using System;

namespace SMRenderer.Core.Exceptions
{
    /// <include file='exceptions.docu' path='Documentation/ShaderLoadingException/Class'/>
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