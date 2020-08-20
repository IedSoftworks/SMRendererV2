using System;

namespace SM.Core.Enums
{
    /// <summary>
    /// Contains possible write targets for the log system
    /// </summary>
    [Flags]
    public enum LogWriteTarget
    {
        /// <summary>
        /// Writes nowhere
        /// </summary>
        None = 0,
        /// <summary>
        /// Uses the default settings
        /// </summary>
        Default = 1,
        /// <summary>
        /// Writes the log message to the console
        /// </summary>
        Console = 2,
        /// <summary>
        /// Writes the log message to the Visual Studio Debugger
        /// </summary>
        VSDebugger = 4,
        /// <summary>
        /// Writes the log message to the active log file
        /// </summary>
        LogFile = 8,
        /// <summary>
        /// Writes the log message to all targets
        /// </summary>
        All = Console | VSDebugger | LogFile
    }
}