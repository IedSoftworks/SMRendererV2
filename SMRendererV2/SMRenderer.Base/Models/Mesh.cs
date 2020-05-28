using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base.Models.CoreTypes;

namespace SMRenderer.Base.Models
{
    public class Mesh : Model
    {
        public string Name;

        public override ModelData Vertices { get; } = new ModelData();
        public override ModelData Normals { get; } = new ModelData();
        public override ModelData UVs { get; } = new ModelData(pointerSize:2);
        public virtual ModelData VertexColors { get; } = new ModelData(pointerSize: 4);

        public override int BufferSizeMultiplier { get; } = 3;
        public override PrimitiveType PrimitiveType { get; set; } = PrimitiveType.Triangles;

        public Mesh()
        {
            AttribDataIndex = new Dictionary<string, ModelData>()
            {
                { "aPosition", Vertices },
                { "aTexture", UVs },
                { "aNormal", Normals },
                { "aColor", VertexColors }
            };
        }
    }
}