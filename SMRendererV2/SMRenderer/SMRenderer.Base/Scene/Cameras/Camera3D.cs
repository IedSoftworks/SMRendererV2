using System.Reflection;
using System.Runtime.InteropServices;
using OpenTK;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer.Core.Window;

namespace SMRenderer.Base.Scene.Cameras
{
    public class Camera3D : Camera
    {
        private static int _fov = 90;

        public static int FOV
        {
            get => _fov;
            set
            {
                _fov = value;
                Calculate3DWorld(GLWindow.Window.Aspect);
            }
        } 

        public override bool Orth { get; } = false;

        public Position Target = null;
        public Rotation Rotation = new Rotation();
        public bool UseTarget = true;

        internal override Matrix4 CalcMatrix()
        {
            return Matrix4.LookAt(Position, Target ?? new Vector3(0), Vector3.UnitY);
        }

        public static void Calculate3DWorld(Aspect aspect)
        {
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(_fov), aspect.Ratio, .1f, 100f, out World3D);
        }

    }
}