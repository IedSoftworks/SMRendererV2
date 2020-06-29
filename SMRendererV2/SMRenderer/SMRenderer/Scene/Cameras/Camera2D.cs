using OpenTK;
using SMRenderer.Core.Window;

namespace SMRenderer.Scene.Cameras
{
    public class Camera2D : Camera
    {
        public float Distance = 5;

        public override bool Orth { get; } = true;

        internal override Matrix4 CalcMatrix() => Matrix4.LookAt(Position.X, Position.Y, Distance, Position.X, Position.Y, 0, 0, 1, 0);

        public static void Calculate2DWorld(Aspect aspect) => Matrix4.CreateOrthographic(aspect.OriginalWidth, aspect.OriginalHeight, .1f, 100f, out World2D);
    }
}