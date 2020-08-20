using System;

namespace SM.Core.Exceptions
{
    /// <summary>
    /// This exception only happens when something when wrong while a model was compiling.
    /// </summary>
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