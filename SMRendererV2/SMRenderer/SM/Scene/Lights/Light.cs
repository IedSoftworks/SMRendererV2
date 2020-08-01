using OpenTK.Graphics;
using SM.Core.Renderer;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Lights
{
    public abstract class Light
    {
        public abstract LightType Type { get; set; }

        public virtual Color DiffuseColor { get; set; } = Color4.White;
        public virtual Color SpecularColor { get; set; } = Color4.White;

        public virtual void SetUniforms(UniformCollection u, int index)
        {
            u[$"Lights[{index}].Type"]?.SetUniform1((int)Type);
            u[$"Lights[{index}].Diffuse"]?.SetUniform3(DiffuseColor);
            u[$"Lights[{index}].Specular"]?.SetUniform3(SpecularColor);
        }
    }
}