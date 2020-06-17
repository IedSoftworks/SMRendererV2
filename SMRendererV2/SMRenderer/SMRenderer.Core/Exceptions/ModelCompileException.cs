using System;

namespace SMRenderer.Core.Exceptions
{
    /// <include file='exceptions.docu' path='Documentation/ModelCompileException/Class'/>
    public class ModelCompileException : LogException
    {
        /// <inheritdoc />
        public ModelCompileException() { }

        /// <inheritdoc />
        public ModelCompileException(string message) : base(message) { }

        /// <inheritdoc />
        public ModelCompileException(string message, Exception inner) : base(message, inner) { }
    }
}