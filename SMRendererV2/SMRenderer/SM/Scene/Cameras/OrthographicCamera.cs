using OpenTK;
using SM.Core.Window;

namespace SM.Scene.Cameras
{
    public class OrthographicCamera : Camera
    {
        public static int WidthScale = -1;
        public static int HeightScale = -1;


        public override bool Orth { get; } = true;

        internal override Matrix4 CalcMatrix() => Matrix4.LookAt(Position.X, Position.Y, 1, Position.X, Position.Y, 0, 0, 1, 0);

        public static void Calculate2DWorld(Aspect aspect)
        {
            if (WidthScale == -1 && HeightScale == -1)
                aspect.ScaledResolution = aspect.OriginalResolution;
            else if (WidthScale > -1 && HeightScale > -1)
                aspect.ScaledResolution = new Vector2(WidthScale, HeightScale);
            else
                aspect.ScaledResolution = new Vector2((int) (WidthScale > -1 ? WidthScale : HeightScale * aspect.Ratio),
                    (int) (HeightScale > -1 ? HeightScale : WidthScale / aspect.Ratio));

            Matrix4.CreateOrthographicOffCenter(-aspect.ScaledResolution.X / 2, aspect.ScaledResolution.X / 2, aspect.ScaledResolution.Y / 2, -aspect.ScaledResolution.Y / 2, .1f, 100f, out OrthographicWorld);
        }
    }
}