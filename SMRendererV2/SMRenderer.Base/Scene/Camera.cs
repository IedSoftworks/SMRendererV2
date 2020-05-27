using OpenTK;
using SMRenderer.Base.Types.VectorTypes;

namespace SMRenderer.Base.Scene
{
    public class Camera
    {
        public static Matrix4 WorldMatrix;
        public static Vector3 upParameter = Vector3.UnitY;

        public Position Position = new Position(0,0, 1);
        public Position Target = null;

        public Matrix4 ViewMatrix;

        public void CalculateView()
        {
            ViewMatrix = Matrix4.LookAt(Position,
                Target ?? new Vector3(Position.X, Position.Y, 0), 
                upParameter);
        }
    }
}