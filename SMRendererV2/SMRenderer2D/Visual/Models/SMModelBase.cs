using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Object;

namespace SMRenderer2D.Visual.Models
{
    public class SMModelBase : Model
    {
        public override ModelData Vertices { get; }
        public override ModelData UVs { get; }
        public override ModelData Normals { get; }
        public override PrimitiveType PrimitiveType { get; }
        public override int BufferSizeMultiplier { get; } = 4;

        public SMModelBase()
        {
            AttribDataIndex = new Dictionary<string, ModelData>()
            {
                { "aPosition", Vertices },
                { "aTexture", UVs },
                { "aNormal", Normals }
            };
        }
    }
}