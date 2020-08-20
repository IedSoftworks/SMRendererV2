using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Enums;

namespace SM.Core.Renderer
{
    /// <summary>
    /// This controls and collected all uniforms.
    /// </summary>
    public class UniformCollection : Dictionary<string, Uniform>
    {
        /// <summary>
        /// The renderer this collection is connected to.
        /// </summary>
        internal GenericRenderer renderer;

        /// <summary>
        /// The next texture id
        /// </summary>
        public int NextTexture = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The uniform. If not existing, null.</returns>
        public new Uniform? this[string key]
        {
            get
            {
                if (ContainsKey(key))
                    return base[key];
                return null;
            }
        }

        /// <summary>
        /// Adds a uniform to the collection
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="uniform">The uniform that is needed to add.</param>
        public new void Add(string key, Uniform uniform)
        {
            uniform.Parent = this;
            base.Add(key, uniform);
        }

        /// <summary>
        /// Adds a new uniform to the collection
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="location">The uniform location</param>
        /// <param name="type">The uniform type</param>
        public void Add(string key, int location, ActiveUniformType type)
        {
            base.Add(key, new Uniform(location, this, type));
        }
    }
}