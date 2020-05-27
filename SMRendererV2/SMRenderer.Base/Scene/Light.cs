using System.Collections.Generic;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer.Core.Renderer;

namespace SMRenderer.Base.Scene
{
    public abstract class Light
    {
        public abstract int LightType { get; }

        public Position Position;
        public Color Color;

        public abstract void SetUniforms(Dictionary<string, Uniform> RendererUniforms);
    }
}