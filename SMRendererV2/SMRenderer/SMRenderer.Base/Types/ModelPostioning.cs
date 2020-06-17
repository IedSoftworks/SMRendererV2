using OpenTK;
using SMRenderer.Base.Types.VectorTypes;

namespace SMRenderer.Base.Types
{
    public class ModelPostioning
    {
        public Matrix4 Matrix { get; private set; }

        public Position Position = new Position(0, 0);
        public Size Size = new Size(1, 1);
        public Rotation Rotation = new Rotation();

        public Matrix4 CalculateMatrix()
        {
            return Matrix = (Matrix4) Size * Rotation * Position;
        }
    }
}