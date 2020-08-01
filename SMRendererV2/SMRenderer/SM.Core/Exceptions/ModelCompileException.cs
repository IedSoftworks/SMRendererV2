using System;

namespace SM.Core.Exceptions
{
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