using OpenTK;
using SMRenderer.Base.Types.VectorTypes;

namespace SMRenderer.Base
{
    public class Camera
    {
        public static Matrix4 WorldMatrix;

        public Position Position = new Position(0,0, 1);
        public Position Target = null;

        public Matrix4 ViewMatrix;

        public void CalculateView()
        {
            ViewMatrix = Matrix4.LookAt(Position,
                Target ?? new Vector3(Position.X, Position.Y, 0), 
                Vector3.UnitY) * WorldMatrix;
        }
    }
}