using System;
using SM.Core.Renderer;
using SM.Core.Window;

namespace SM.Data.Models
{
    [Serializable]
    public abstract class MaterialModifier
    {
        public abstract Type RequiredWindowPlugin { get; }

        public bool PluginLoaded => GLWindow.Window.LoadedPlugins.ContainsKey(RequiredWindowPlugin);

        public virtual void SetMaterialUniforms(UniformCollection uniforms)
        {
            if (!PluginLoaded) return;
        }

        public virtual void ClearUniforms(UniformCollection uniforms)
        {
            if (!PluginLoaded) return;
        }
    }
}