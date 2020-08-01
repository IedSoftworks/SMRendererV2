using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Models;

namespace SM.Data.Models
{
    [Serializable]
    public class Mesh : Model
    {
        public string Name;

        public override ModelData Vertices { get; } = new ModelData();
        public override ModelData Normals { get; } = new ModelData();
        public override ModelData UVs { get; } = new ModelData(pointerSize:2);
        public override ModelData VertexColors { get; } = new ModelData(pointerSize: 4);
        public virtual ModelData Tangents { get; } = new ModelData();

        public override int BufferSizeMultiplier { get; } = 4;
        public override PrimitiveType PrimitiveType { get; set; } = PrimitiveType.Triangles;

        public Mesh()
        {
            AttribDataIndex = new Dictionary<string, ModelData>
            {
                { "aPosition", Vertices },
                { "aTexture", UVs },
                { "aNormal", Normals },
                { "aColor", VertexColors },
                { "aTangent", Tangents}
            };
        }
    }
}