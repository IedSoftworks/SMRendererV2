using System;
using OpenTK;

namespace SM.Data.Types.VectorTypes
{
    [Serializable]
    public class Rotation : AnimatableType
    {
        public Rotation(float uniform)
        {
            X = uniform;
            Y = uniform;
            Z = uniform;
        }

        public Rotation(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override OpenTK.Matrix4 CalcMatrix4() => 
            OpenTK.Matrix4.CreateRotationX(MathHelper.DegreesToRadians(X)) * OpenTK.Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Y)) * OpenTK.Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Z));

        public Vector3 Radians => new Vector3(MathHelper.DegreesToRadians(X), MathHelper.DegreesToRadians(Y), MathHelper.DegreesToRadians(Z));

        public static Rotation Uniform2D(float uniform) => new Rotation(z: uniform);

        public static Rotation RadiansRotation(float x = 0, float y = 0, float z = 0) => new Rotation(MathHelper.RadiansToDegrees(x), MathHelper.RadiansToDegrees(y), MathHelper.RadiansToDegrees(z));

        public static implicit operator Vector2(Rotation p) => new Vector2(p.X, p.Y);
        public static implicit operator Vector3(Rotation p) => new Vector3(p.X, p.Y, p.Z);
        public static implicit operator Vector4(Rotation p) => new Vector4(p.X, p.Y, p.Z, 0);
    }
}