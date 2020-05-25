using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Object;

namespace SMRenderer2D.Objects.Models
{
    public class SMQuad : SMModelBase
    {
        public static SMQuad Object = new SMQuad();

        public override ModelData Vertices { get; } = new ModelData()
        {
            -.5f, -.5f, 0,
            -.5f, +.5f, 0,
            +.5f, +.5f, 0,
            +.5f, -.5f, 0
        };

        public override ModelData UVs { get; } = new ModelData(pointerSize: 2)
        {
            0,0,
            0,1,
            1,1,
            1,0
        };

        public override ModelData Normals { get; } = new ModelData()
        {
            0,0,1,
            0,0,1,
            0,0,1,
            0,0,1
        };

        public override PrimitiveType PrimitiveType { get; } = PrimitiveType.Quads;

        private SMQuad()
        {
            Compile();
        }
    }
}