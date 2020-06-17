using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base.Objects;
using SMRenderer.Core.Object;

namespace SMRenderer3D.Objects
{
    public class SMPlate : SMModelBase
    {
        public static SMPlate Object = new SMPlate();

        public override ModelData Vertices { get; } = new ModelData()
        {
            -.5f, 0, -.5f, 
            -.5f, 0, +.5f, 
            +.5f, 0, +.5f, 
            +.5f, 0, -.5f,

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
            0,1,0,
            0,1,0,
            0,1,0,
            0,1,0
        };

        public override PrimitiveType PrimitiveType { get; } = PrimitiveType.Quads;

        private SMPlate()
        {
            Compile();
        }
    }
}