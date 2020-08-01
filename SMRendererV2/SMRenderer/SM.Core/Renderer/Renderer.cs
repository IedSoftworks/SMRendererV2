using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SM.Core.Exceptions;

namespace SM.Core.Renderer
{
    public class RendererCollection
    {
        private static Dictionary<Type, GenericRenderer> _activeRenderer = new Dictionary<Type, GenericRenderer>();

        public static T Get<T>()
        {
            if (_activeRenderer.TryGetValue(typeof(T), out GenericRenderer renderer))
            {
                return Unsafe.As<GenericRenderer, T>(ref renderer);
            }
            throw new LogException("The searched renderer isn't loaded.\nSearched for: " + typeof(T).Name);
            return default;
        }

        internal static void Add(GenericRenderer renderer)
        {
            _activeRenderer.Add(renderer.GetType(), renderer);
        }
    }
}