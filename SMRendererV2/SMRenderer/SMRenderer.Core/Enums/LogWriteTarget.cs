using System;

namespace SMRenderer.Core.Enums
{
    [Flags]
    public enum LogWriteTarget
    {
        None = 0,
        Default = 1,
        Console = 2,
        VSDebugger = 4,
        LogFile = 8,
        All = Console | VSDebugger | LogFile
    }
}