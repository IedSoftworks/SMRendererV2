using System;
using System.Runtime.CompilerServices;
using OpenTK;
using SMRenderer.Base.Types.Animations;
using SMRenderer.Base.Types.VectorTypes;

namespace SMRenderer.Base.Types
{
    public class VectorType : BaseType
    {
        public virtual float X { get; set; }
        public virtual float Y { get; set; }
        public virtual float Z { get; set; }
        public virtual float W { get; set; }

        public static implicit operator Vector2(VectorType at) => new Vector2(at.X, at.Y);
        public static implicit operator Vector3(VectorType at) => new Vector3(at.X, at.Y, at.Z);
        public static implicit operator Vector4(VectorType at) => new Vector4(at.X, at.Y, at.Z, at.W);
        public static implicit operator AnimationVector(VectorType at) => new AnimationVector(at.X, at.Y, at.Z, at.W);

        public static implicit operator Matrix4(VectorType at) => at.ToMatrix4();

        public virtual Matrix4 ToMatrix4() => new Matrix4();

        public override string ToString() => $"X:{X}, Y:{Y}, Z:{Z}, W:{W}";

        public static T Clone<T>(T obj)
        {
            VectorType source = Unsafe.As<T, VectorType>(ref obj);
            VectorType target = new VectorType {X = source.X, Y = source.Y, Z = source.Z, W = source.W};

            return Unsafe.As<VectorType, T>(ref target);
        }
    }
}