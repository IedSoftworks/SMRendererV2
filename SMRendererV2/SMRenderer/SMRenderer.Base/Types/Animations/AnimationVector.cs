

using OpenTK;

namespace SMRenderer.Base.Types.Animations
{
    public struct AnimationVector
    {
        public int ValueCount => 1;

        public float X;
        public float Y;
        public float Z;
        public float W;

        public AnimationVector(float x = default, float y = default, float z = default, float w = default)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public AnimationVector(Vector2 vec2, float z = default, float w = default)
        {
            X = vec2.X;
            Y = vec2.Y;
            Z = z;
            W = w;
        }
        public AnimationVector(Vector3 vec3, float w = default)
        {
            X = vec3.X;
            Y = vec3.Y;
            Z = vec3.Z;
            W = w;
        }
        public AnimationVector(Vector4 vec4)
        {
            X = vec4.X;
            Y = vec4.Y;
            Z = vec4.Z;
            W = vec4.W;
        }

        public static AnimationVector Add(AnimationVector a, AnimationVector b)
        {
            return new AnimationVector(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z,
                a.W + b.W);
        }
        public static AnimationVector Add(AnimationVector a, float b)
        {
            return new AnimationVector(
                a.X + b,
                a.Y + b,
                a.Z + b,
                a.W + b);
        }

        public static AnimationVector Sub(AnimationVector a, AnimationVector b)
        {
            return new AnimationVector(
                a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z,
                a.W - b.W);
        }
        public static AnimationVector Sub(AnimationVector a, float b)
        {
            return new AnimationVector(
                a.X - b,
                a.Y - b,
                a.Z - b,
                a.W - b);
        }
        public static AnimationVector Mul(AnimationVector a, AnimationVector b)
        {
            return new AnimationVector(
                a.X * b.X,
                a.Y * b.Y,
                a.Z * b.Z,
                a.W * b.W);
        }
        public static AnimationVector Mul(AnimationVector a, float b)
        {
            return new AnimationVector(
                a.X * b,
                a.Y * b,
                a.Z * b,
                a.W * b);
        }
        public static AnimationVector Div(AnimationVector a, AnimationVector b)
        {
            return new AnimationVector(
                a.X / b.X,
                a.Y / b.Y,
                a.Z / b.Z,
                a.W / b.W);
        }
        public static AnimationVector Div(AnimationVector a, float b)
        {
            return new AnimationVector(
                a.X / b,
                a.Y / b,
                a.Z / b,
                a.W / b);
        }
        public static AnimationVector Mod(AnimationVector a, AnimationVector b)
        {
            return new AnimationVector(
                a.X % b.X,
                a.Y % b.Y,
                a.Z % b.Z,
                a.W % b.W);
        }
        public static AnimationVector Mod(AnimationVector a, float b)
        {
            return new AnimationVector(
                a.X % b,
                a.Y % b,
                a.Z % b,
                a.W % b);
        }

        public static AnimationVector operator +(AnimationVector a, AnimationVector b) => Add(a, b);
        public static AnimationVector operator -(AnimationVector a, AnimationVector b) => Sub(a, b);
        public static AnimationVector operator *(AnimationVector a, AnimationVector b) => Mul(a, b);
        public static AnimationVector operator /(AnimationVector a, AnimationVector b) => Div(a, b);
        public static AnimationVector operator %(AnimationVector a, AnimationVector b) => Mod(a, b);
        public static AnimationVector operator +(AnimationVector a, float b) => Add(a, b);
        public static AnimationVector operator -(AnimationVector a, float b) => Sub(a, b);
        public static AnimationVector operator *(AnimationVector a, float b) => Mul(a, b);
        public static AnimationVector operator /(AnimationVector a, float b) => Div(a, b);
        public static AnimationVector operator %(AnimationVector a, float b) => Mod(a, b);
    }
}