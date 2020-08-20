using OpenTK;
using SM.Data.Types;
using SM.Data.Types.Extensions;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Draw
{
    public class MoveableCollection : SMItemCollection
    {
        public Vector Position;
        public Vector Scale;
        public Vector Rotation;

        public override void Prepare(double delta)
        {
            Matrix = MatrixCalc.CreateModelMatrix(Scale, Rotation, Position);

            base.Prepare(delta);
        }
    }
}