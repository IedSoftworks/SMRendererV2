using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SM.Core.Exceptions;

namespace SM.Core.Renderer
{
    /// <summary>
    /// This collects all shader programs.
    /// </summary>
    public class RendererCollection
    {
        /// <summary>
        /// All active render programs are stored here.
        /// </summary>
        private static Dictionary<Type, GenericRenderer> _activeRenderer = new Dictionary<Type, GenericRenderer>();

        /// <summary>
        /// This gets the wanted renderer.
        /// </summary>
        /// <typeparam name="T">Type of the wanted renderer</typeparam>
        /// <returns></returns>
        public static T Get<T>() where T : GenericRenderer
        {
            if (_activeRenderer.TryGetValue(typeof(T), out GenericRenderer renderer))
            {
                return (T)renderer;
            }
            throw new LogException("The searched renderer isn't loaded.\nSearched for: " + typeof(T).Name);
        }

        /// <summary>
        /// This adds a renderer to the active list.
        /// </summary>
        /// <param name="renderer">The renderer that need to be added.</param>
        internal static void Add(GenericRenderer renderer)
        {
            _activeRenderer.Add(renderer.GetType(), renderer);
        }
    }
}