using OpenTK;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer.Core.Window;

namespace SMRenderer.Base.Scene.Cameras
{
    public abstract class Camera
    {
        public static Matrix4 World2D;
        public static Matrix4 World3D;

        public abstract bool Orth { get; }
        public virtual Position Position { get; set; } = new Position(0,0,0);
        public Matrix4 ViewMatrix { get; private set; }

        public void CalculateMatrix() => ViewMatrix = CalcMatrix();
        internal abstract Matrix4 CalcMatrix();
        public static Matrix4 CurrentWorld() => Scene.CurrentCam.Orth ? World2D : World3D;
    }
}