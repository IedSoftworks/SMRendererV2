using OpenTK;
using SMRenderer.Base.Types.Animations;

namespace SMRenderer.Base.Types
{
    public abstract class VectorType : BaseType
    {
        public abstract float X { get; set; }
        public abstract float Y { get; set; }
        public abstract float Z { get; set; }
        public abstract float W { get; set; }

        public static implicit operator Vector2(VectorType at) => new Vector2(at.X, at.Y);
        public static implicit operator Vector3(VectorType at) => new Vector3(at.X, at.Y, at.Z);
        public static implicit operator Vector4(VectorType at) => new Vector4(at.X, at.Y, at.Z, at.W);
        public static implicit operator AnimationVector(VectorType at) => new AnimationVector(at.X, at.Y, at.Z, at.W);

        public static implicit operator Matrix4(VectorType at) => at.ToMatrix4();

        public virtual Matrix4 ToMatrix4() => new Matrix4();
    }
}