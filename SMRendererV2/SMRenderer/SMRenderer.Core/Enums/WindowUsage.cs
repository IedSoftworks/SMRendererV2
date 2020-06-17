using System;

namespace SMRenderer.Core.Enums
{
    /// <include file='enums.docu' path='Documentation/WindowUsage/Class'/>
    [Flags]
    public enum WindowUsage
    {
        /// <include file='enums.docu' path='Documentation/WindowUsage/Enum[@name="None"]'/>
        None = 0,
        /// <include file='enums.docu' path='Documentation/WindowUsage/Enum[@name="Load"]'/>
        Load = 1,
        /// <include file='enums.docu' path='Documentation/WindowUsage/Enum[@name="Render"]'/>
        Render = 2,
        /// <include file='enums.docu' path='Documentation/WindowUsage/Enum[@name="Update"]'/>
        Update = 4,
        /// <include file='enums.docu' path='Documentation/WindowUsage/Enum[@name="Exit"]'/>
        Exit = 8,
        /// <include file='enums.docu' path='Documentation/WindowUsage/Enum[@name="MouseMove"]'/>
        MouseMove = 16,
        /// <include file='enums.docu' path='Documentation/WindowUsage/Enum[@name="Resize"]'/>
        Resize = 32,
        /// <include file='enums.docu' path='Documentation/WindowUsage/Enum[@name="AddRenderer"]'/>
        AddRenderer = 64,
        AddFramebuffers = 128,
        /// <include file='enums.docu' path='Documentation/WindowUsage/Enum[@name="All"]'/>
        All = Load | Render | Update | Exit | MouseMove | Resize | AddRenderer | AddFramebuffers,
    }
}