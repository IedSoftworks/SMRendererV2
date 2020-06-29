using OpenTK.Graphics;
using SMRenderer.Types.VectorTypes;

namespace SMRenderer.Scene.Lights
{
    public struct Light
    {
        public LightType Type;
        public Position Position;
        public Rotation Rotation;
        public Size Size;
        public Color Color;

        public Light(LightType type, Position position = default, Rotation rotation = default, Size size = default, Color color = default)
        {
            Type = type;
            Position = position ?? new Position(0);
            Rotation = rotation ?? new Rotation(0);
            Size = size ?? new Size(1);
            Color = color ?? Color4.White;
        }
    }
}