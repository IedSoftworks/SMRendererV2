using OpenTK;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Cameras
{
    public abstract class Camera
    {
        public static Matrix4 OrthographicWorld;
        public static Matrix4 PerspectiveWorld;

        public abstract bool Orth { get; }
        public virtual Position Position { get; set; } = new Position(0,0,0);
        public virtual Position Front { get; set; } = new Position(z: 1);
        public Matrix4 ViewMatrix { get; private set; }

        public Matrix4 WorldViewProjection => (Orth ? OrthographicWorld : PerspectiveWorld) * ViewMatrix;

        public Vector3 UpVector = Vector3.UnitY;

        public void CalculateMatrix() => ViewMatrix = CalcMatrix();

        internal virtual Matrix4 CalcMatrix()
        {
            return Matrix4.LookAt(Position, Position + Front, UpVector);
        }

        public Matrix4 World => Orth ? OrthographicWorld : PerspectiveWorld;
    }
}