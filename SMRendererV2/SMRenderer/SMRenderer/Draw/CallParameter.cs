using OpenTK;
using SMRenderer.Core.Models;
using SMRenderer.Types.VectorTypes;

namespace SMRenderer.Draw
{
    public class CallParameter
    {
        public virtual Position Position { get; set; } = new Position(0);
        public virtual Size Size { get; set; } = new Size(1);
        public virtual Rotation Rotation { get; set; } = new Rotation(0);

        public virtual Position TextureOffset { get; set; } = new Position(0);
        public virtual Size TextureSize { get; set; } = new Size(-1);


        public Matrix4 ModelMatrix;
        public Vector2 TextureOffsetNormal { get; private set; }
        public Vector2 TextureSizeNormal { get; private set; }

        public void CalcModelMatrix(Vector2 textureSize)
        {
            ModelMatrix = (Matrix4) Size * Rotation * Position;

            TextureSizeNormal = (TextureSize.X < 0 || TextureSize.Y < 0) ? new Vector2(1) : Vector2.Divide(TextureSize, textureSize);
            TextureOffsetNormal = Vector2.Divide(TextureOffset, textureSize);
        }
    }
}