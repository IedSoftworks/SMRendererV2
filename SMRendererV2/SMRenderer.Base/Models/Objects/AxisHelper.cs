using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base.Models.CoreTypes;

namespace SMRenderer.Base.Models.Objects
{
    public class AxisHelper : Mesh
    {
        public static AxisHelper Object = new AxisHelper();

        public override ModelData Vertices { get; } = new ModelData()
        {
            0, 0, 0,
            1, 0, 0,
            0, 0, 0,
            0, 1, 0,
            0, 0, 0,
            0, 0, 1,
            0, 0, 0,
            0, 0, 0,
        };

        public override ModelData VertexColors { get; } = new ModelData(pointerSize: 4)
        {
            1, 0, 0, 1,
            1, 0, 0, 1,
            0, 1, 0, 1,
            0, 1, 0, 1,
            0, 0, 1, 1,
            0, 0, 1, 1,
            0, 0, 0, 0,
            0, 0, 0, 0,
        };

        public override PrimitiveType PrimitiveType { get; set; } = PrimitiveType.LineStripAdjacency;

        private AxisHelper() => Compile();
    }
}