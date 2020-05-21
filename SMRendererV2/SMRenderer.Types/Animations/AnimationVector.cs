using OpenTK;

namespace SMRenderer2D.Types.ValueStructs
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

        public static AnimationVector Add(AnimationVector a, AnimationVector b) => 
            new AnimationVector(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + a.Z,
                a.W + a.W);
        public static AnimationVector Sub(AnimationVector a, AnimationVector b)
        {
            return new AnimationVector(
                a.X - b.X,
                a.Y - b.Y,
                a.Z - a.Z,
                a.W - a.W);
        }
        public static AnimationVector Mul(AnimationVector a, AnimationVector b)
        {
            return new AnimationVector(
                a.X * b.X,
                a.Y * b.Y,
                a.Z * a.Z,
                a.W * a.W);
        }
        public static AnimationVector Div(AnimationVector a, AnimationVector b)
        {
            return new AnimationVector(
                a.X / b.X,
                a.Y / b.Y,
                a.Z / a.Z,
                a.W / a.W);
        }
        public static AnimationVector Mod(AnimationVector a, AnimationVector b)
        {
            return new AnimationVector(
                a.X % b.X,
                a.Y % b.Y,
                a.Z % a.Z,
                a.W % a.W);
        }
    }
}