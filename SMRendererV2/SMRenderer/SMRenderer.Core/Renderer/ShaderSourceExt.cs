using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SMRenderer.Core.Renderer
{
    public class ShaderSourceExt : Dictionary<string, List<string>>
    {
        public new List<string> this[string key]
        {
            get
            {
                if (!ContainsKey(key)) base.Add(key, new List<string>());
                return base[key];
;           }
        }

        public void Add(string key, string value)
        {
            base.Add(key, new List<string>() {value});
        }
    }
}