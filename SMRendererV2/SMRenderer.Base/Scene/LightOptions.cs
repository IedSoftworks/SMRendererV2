using System.Collections.Generic;
using OpenTK.Graphics;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer.Core.Renderer;

namespace SMRenderer.Base.Scene
{
    public class LightOptions : List<Light>
    {
        public Color Ambient = Color4.DarkSlateGray;

        public void SetUniforms(Dictionary<string, Uniform> RendererUniforms)
        {
            LightOptions lo = Scene.CurrentLight;

            RendererUniforms["light.ambientLight"].SetUniform4((Color4)lo.Ambient);

            if (Count <= 0) return;

            Light light = this[0];

            RendererUniforms["light.lights.Type"].SetUniform1(light.LightType);
            RendererUniforms["light.lights.Color"].SetUniform4((Color4)light.Color);
            RendererUniforms["light.lights.Position"].SetUniform3(light.Position);


        }
    }
}