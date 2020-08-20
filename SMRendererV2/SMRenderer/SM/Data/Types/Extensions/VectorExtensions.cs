using OpenTK;

namespace SM.Data.Types.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 GetRadians(this Vector vector)
        {
            return new Vector3(MathHelper.DegreesToRadians(vector.X), MathHelper.DegreesToRadians(vector.Y), MathHelper.DegreesToRadians(vector.Z));
        }
    }
}