using OpenTK;

namespace SM.Data.Types.Extensions
{
    public class MatrixCalc
    {
        public static Matrix4 CreateScale(Vector vec) => Matrix4.CreateScale(vec);
        public static Matrix4 CreateRotation(Vector vec) => Matrix4.CreateRotationX(MathHelper.DegreesToRadians(vec.X)) * 
                                                            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(vec.Y)) * 
                                                            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(vec.Z));
        public static Matrix4 CreateTranslation(Vector vec) => Matrix4.CreateTranslation(vec);

        public static Matrix4 CreateModelMatrix(Vector size, Vector rotation, Vector position)
        {
            return CreateScale(size) * 
                   CreateRotation(rotation) *
                   CreateTranslation(position);
        }
    }
}