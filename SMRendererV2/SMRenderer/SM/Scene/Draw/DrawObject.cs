using System.Collections.Generic;
using OpenTK;
using SM.Data.Types.VectorTypes;
using SM.Render.ShaderPrograms;
using SM.Scene.Cameras;
using SM.Scene.Draw.Base;

namespace SM.Scene.Draw
{
    public class DrawObject : ModelPositioning
    {
        private CallParameter parameter = new CallParameter();
        private CallParameter[] Parameters;

        public new Position Position
        {
            get => parameter.Position;
            set => parameter.Position = value;
        }
        public new Size Size
        {
            get => parameter.Size;
            set => parameter.Size = value;
        }
        public new Rotation Rotation
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
            Parameters = new[] {parameter};

            if (instantPrepare) Prepare(0);
        }
        public override void Prepare(double delta)
        {
            base.Prepare(delta);

            Vector2 texSize = Material.DiffuseTexture != null ? new Vector2(Material.DiffuseTexture.Width, Material.DiffuseTexture.Height) : new Vector2(1);
            parameter.CalcModelMatrix(texSize);
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);
            RenderProgramCollection.General.Draw(camera, Mesh, Material, Parameters);
        }
    }
}