using OpenTK;

namespace SMRenderer.Base.Types.VectorTypes
{
    public class Rotation : AnimatableType
    {
        public override float X { get; set; }
        public override float Y { get; set; }
        public override float Z { get; set; }
        public override float W { get; set; } = 0;

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
        public static Rotation Uniform2D(float uniform) => new Rotation(z: uniform);

        public static implicit operator Vector2(Rotation p) => new Vector2(p.X, p.Y);
        public static implicit operator Vector3(Rotation p) => new Vector3(p.X, p.Y, p.Z);
        public static implicit operator Vector4(Rotation p) => new Vector4(p.X, p.Y, p.Z, 0);

        public static implicit operator Matrix4(Rotation p) => 
            Matrix4.CreateRotationX(p.X) * Matrix4.CreateRotationY(p.Y) * Matrix4.CreateRotationZ(p.Z);
    }
}