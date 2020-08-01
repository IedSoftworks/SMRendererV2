using OpenTK;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Draw
{
    public class MoveableCollection : SMItemCollection
    {
        public Position Position;
        public Size Scale;
        public Rotation Rotation;

        public override void Prepare(double delta)
        {
            Matrix = (Matrix4) Scale * Rotation * Position;

            base.Prepare(delta);
        }
    }
}