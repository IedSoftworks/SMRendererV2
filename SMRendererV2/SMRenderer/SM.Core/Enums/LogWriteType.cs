namespace SM.Core.Enums
{
    /// <summary>
    /// Access the default log write types
    /// </summary>
    public enum LogWriteType
    {
        /// <summary>
        /// Creates an info log
        /// </summary>
        Info,
        /// <summary>
        /// Creates a warning log
        /// </summary>
        Warning,
        /// <summary>
        /// Creates a error log
        /// </summary>
        Error,
        /// <summary>
        /// Creates a unexpected error.
        /// <para>Used in the default crash handler</para>
        /// </summary>
        UnexpectedError,

        /// <summary>
        /// Creates a unexpected critical error.
        /// <para>Used in the default crash handler, when the program can't continue</para>
        /// </summary>
        UnexpectedCriticalError
    }
}