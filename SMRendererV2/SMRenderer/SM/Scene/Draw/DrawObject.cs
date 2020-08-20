using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using SM.Data.Types;
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

        public new Vector Position
        {
            get => parameter.Position;
            set => parameter.Position = value;
        }
        public new Vector Size
        {
            get => parameter.Size;
            set => parameter.Size = value;
        }
        public new Vector Rotation
        {
            get => parameter.Rotation;
            set => parameter.Rotation = value;
        }
        public Vector TextureOffset
        {
            get => parameter.TextureOffset;
            set => parameter.TextureOffset = value;
        }
        public Vector TextureSize
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

            Vector2 texSize = Material.Texture != null ? new Vector2(Material.Texture.Width, Material.Texture.Height) : new Vector2(1);
            parameter.CalcModelMatrix(texSize);
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);
            RenderProgramCollection.General.Draw(camera, Mesh, Material, Parameters);
        }
    }
}