using System.Collections.Generic;
using OpenTK;
using SM.Core.Renderer;
using SM.Render.ShaderPrograms;
using SM.Scene.Cameras;
using SM.Scene.Draw.Base;

namespace SM.Scene.Draw
{
    public class DrawCall : DrawingBase
    {
        public ICollection<CallParameter> DrawCallParameters = new List<CallParameter>();

        public override void Prepare(double delta)
        {
            Vector2 texSize = Material.DiffuseTexture != null ? new Vector2(Material.DiffuseTexture.Width, Material.DiffuseTexture.Height) : new Vector2(1);
            foreach (CallParameter cp in DrawCallParameters) cp.CalcModelMatrix(texSize);
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);
            RenderProgramCollection.General.Draw(camera, Mesh, Material, DrawCallParameters);
        }
    }
}