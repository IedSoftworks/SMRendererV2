using System;

namespace SM.Core.Enums
{
    /// <summary>
    /// This contains options how a plugin can interact with the GLWindow
    /// </summary>
    [Flags]
    public enum WindowUsage
    {
        /// <summary>
        /// No access to everything
        /// </summary>
        None = 0,
        /// <summary>
        /// Allows to load itself and access the loading function of the window
        /// </summary>
        Load = 1,
        /// <summary>
        /// Allows access to the render functions
        /// </summary>
        Render = 2,
        /// <summary>
        /// Allows access to the update functions
        /// </summary>
        Update = 4,
        /// <summary>
        /// Allows access to the exit function
        /// </summary>
        Exit = 8,
        /// <summary>
        /// Allows access to the mouse move function
        /// </summary>
        MouseMove = 16,
        /// <summary>
        /// Allows acces to the resize functions
        /// </summary>
        Resize = 32,
        /// <summary>
        /// Allows to import plugin render programs
        /// </summary>
        AddRenderer = 64,
        /// <summary>
        /// Allows to import plugin framebuffers
        /// </summary>
        AddFramebuffers = 128,
        /// <summary>
        /// Allows everything
        /// </summary>
        All = Load | Render | Update | Exit | MouseMove | Resize | AddRenderer | AddFramebuffers,
    }
}