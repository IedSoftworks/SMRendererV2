using OpenTK;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Draw.Base
{
    public class ModelPositioning : MatrixObject
    {
        public override Matrix4 ModelMatrix { get; set; }

        public Position Position = new Position();
        public Size Size = new Size();
        public Rotation Rotation = new Rotation();

        public override void Prepare(double delta)
        {
            ModelMatrix = CalcMatrix();
        }

        public Matrix4 CalcMatrix() => (Matrix4) Size * Rotation * Position;
    }
}