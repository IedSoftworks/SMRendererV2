using System;

namespace SM.Core.Enums
{
    /// <include file='enums.docu' path='Documentation/WindowUsage/Class'/>
    [Flags]
    public enum WindowUsage
    {
        None = 0,
        Load = 1,
        Render = 2,
        Update = 4,
        Exit = 8,
        MouseMove = 16,
        Resize = 32,
        AddRenderer = 64,
        AddFramebuffers = 128,
        All = Load | Render | Update | Exit | MouseMove | Resize | AddRenderer | AddFramebuffers,
    }
}