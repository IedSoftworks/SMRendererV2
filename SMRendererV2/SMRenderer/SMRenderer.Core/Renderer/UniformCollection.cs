using System.Collections.Generic;
using SMRenderer.Core.Enums;

namespace SMRenderer.Core.Renderer
{
    public class UniformCollection : Dictionary<string, Uniform>
    {
        public List<string> CheckedUniforms = new List<string>();

        public new Uniform this[string key]
        {
            get
            {
                try
                {
                    return base[key];
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
    }
}