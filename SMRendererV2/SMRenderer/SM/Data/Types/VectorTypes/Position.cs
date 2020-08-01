using System;
using OpenTK;

namespace SM.Data.Types.VectorTypes
{
    [Serializable]
    public class Position : AnimatableType
    {
        public Position(float uniform) : this(uniform, uniform, uniform) {}
        public Position(Vector2 vector) : this(vector.X, vector.Y) {}
        public Position(Vector3 vector) : this(vector.X, vector.Y, vector.Z) {}
        public Position(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override OpenTK.Matrix4 CalcMatrix4() => OpenTK.Matrix4.CreateTranslation(X, Y, Z);



        public static Position operator -(Position v1, VectorType v2)
        {
            return new Position
            {
                X = v1.X - v2.X,
                Y = v1.Y - v2.Y,
                Z = v1.Z - v2.Z,
                W = v1.W - v2.W,
            };
        }
        public static Position operator +(Position v1, VectorType v2)
        {
            return new Position
            {
                X = v1.X + v2.X,
                Y = v1.Y + v2.Y,
                Z = v1.Z + v2.Z,
                W = v1.W + v2.W,
            };
        }
        public static Position operator *(Position v1, VectorType v2)
        {
            return new Position
            {
                X = v1.X * v2.X,
                Y = v1.Y * v2.Y,
                Z = v1.Z * v2.Z,
                W = v1.W * v2.W,
            };
        }
        public static Position operator /(Position v1, VectorType v2)
        {
            return new Position
            {
                X = v1.X / v2.X,
                Y = v1.Y / v2.Y,
                Z = v1.Z / v2.Z,
                W = v1.W / v2.W,
            };
        }
    }
}