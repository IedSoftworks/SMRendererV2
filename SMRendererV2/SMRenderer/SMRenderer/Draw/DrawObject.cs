using SMRenderer.Types.VectorTypes;

namespace SMRenderer.Draw
{
    public class DrawObject : DrawCall
    {
        private CallParameter parameter = new CallParameter();

        public Position Position
        {
            get => parameter.Position;
            set => parameter.Position = value;
        }
        public Size Size
        {
            get => parameter.Size;
            set => parameter.Size = value;
        }
        public Rotation Rotation
        {
            get => parameter.Rotation;
            set => parameter.Rotation = value;
        }
        public Position TextureOffset
        {
            get => parameter.TextureOffset;
            set => parameter.TextureOffset = value;
        }
        public Size TextureSize
        {
            get => parameter.TextureSize;
            set => parameter.TextureSize = value;
        }
        public DrawObject(bool instantPrepare = false)
        {
            DrawCallParameters.Add(parameter);

            if (instantPrepare) Prepare(0);
        }
    }
}