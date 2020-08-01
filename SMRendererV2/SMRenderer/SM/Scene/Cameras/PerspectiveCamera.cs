using System;
using OpenTK;
using SM.Core.Window;
using SM.Data.Types;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Cameras
{
    public class PerspectiveCamera : Camera
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

        public static float NearClippingPlane = .1f;
        public static float FarClippingPlane = 100f;

        public override bool Orth { get; } = false;

        public Position Target = null;
        public Rotation Rotation = null;

        internal override OpenTK.Matrix4 CalcMatrix()
        {
            if (Target != null)
                Front = VectorType.Normalize<Position>(Target - Position);
            else if (Rotation != null)
            {
                Vector3 radians = Rotation.Radians;

                Front.X = (float) (Math.Cos(radians.X) * Math.Cos(radians.Y));
                Front.Y = (float) (Math.Sin(radians.Y));
                Front.Z = (float) (Math.Sin(radians.X) * Math.Cos(radians.Y));
                Front.Normalize();
            }


            return base.CalcMatrix();
        }

        public static void Calculate3DWorld(Aspect aspect)
        {
            OpenTK.Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(_fov), aspect.Ratio, NearClippingPlane, FarClippingPlane, out PerspectiveWorld);
        }

    }
}