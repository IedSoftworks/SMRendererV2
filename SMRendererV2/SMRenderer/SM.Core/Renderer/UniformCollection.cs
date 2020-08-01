using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Enums;

namespace SM.Core.Renderer
{
    public class UniformCollection : Dictionary<string, Uniform>
    {
        public List<string> CheckedUniforms = new List<string>();
        internal GenericRenderer renderer;

        public int NextTexture = 0;
        public new Uniform? this[string key]
        {
            get
            {
                try
                {
                    if (ContainsKey(key))
                        return base[key];
                    return null;
                }
                catch (KeyNotFoundException)
                {
                    if (!CheckedUniforms.Contains(key))
                    {
                        CheckedUniforms.Add(key);
                        Log.Write(LogWriteType.Warning, "Uniform '" + key + "' doesn't exist.");
                    }

                    return null;
                }
            }
        }

        public new void Add(string key, Uniform uniform)
        {
            uniform.Parent = this;
            base.Add(key, uniform);
        }

        public void Add(string key, int location, ActiveUniformType type)
        {
            base.Add(key, new Uniform(location, this, type));
        }
    }
}