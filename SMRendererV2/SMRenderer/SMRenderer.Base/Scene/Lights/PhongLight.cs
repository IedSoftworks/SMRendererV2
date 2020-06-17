using System.Collections.Generic;
using OpenTK;
using SMRenderer.Core.Renderer;

namespace SMRenderer.Base.Scene.Lights
{
    public class PhongLight : Light
    {
        public override int LightType { get; } = 1;

        public Vector3 Direction = Vector3.One;

        public override void SetUniforms(Dictionary<string, Uniform> RendererUniforms)
        {
            RendererUniforms["light.lights.Phong.Direction"].SetUniform3(Direction);
        }
    }
}