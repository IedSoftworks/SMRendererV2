using OpenTK;
using SM.Data.Types;
using SM.Data.Types.Extensions;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Draw.Base
{
    public class ModelPositioning : MatrixObject
    {
        public override Matrix4 ModelMatrix { get; set; }

        public Vector Position = new Vector();
        public Vector Size = new Vector(1);
        public Vector Rotation = new Vector();

        public override void Prepare(double delta)
        {
            ModelMatrix = CalcMatrix();
        }

        public Matrix4 CalcMatrix() => MatrixCalc.CreateModelMatrix(Size, Rotation, Position);
    }
}