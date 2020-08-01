using SM.Core.Models;
using OpenTK.Graphics.OpenGL4;

namespace SM.Data.Models.Objects
{
    public class AxisHelper : Mesh
    {
        public static AxisHelper Object = new AxisHelper();

        public override ModelData Vertices { get; } = new ModelData()
        {
            { -1, 0, 0 },
            { 1, 0, 0 },
            { 0, -1, 0 },
            { 0, 1, 0 },
            { 0, 0, -1 },
            { 0, 0, 1 },
        };

        public override ModelData VertexColors { get; } = new ModelData(pointerSize: 4)
        {
            { 1, 1, 1, 1 },
            { 1, 0, 0, 1 },
            { 1, 1, 1, 1 },
            { 0, 1, 0, 1 },
            { 1, 1, 1, 1 },
            { 0, 0, 1, 1 },
        };

        public override PrimitiveType PrimitiveType { get; set; } = PrimitiveType.Lines;

        public AxisHelper() => Compile();
    }
}