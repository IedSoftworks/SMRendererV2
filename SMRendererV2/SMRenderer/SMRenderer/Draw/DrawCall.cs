using System.Collections.Generic;
using OpenTK;
using SMRenderer.Renderer;

namespace SMRenderer.Draw
{
    public class DrawCall : DrawBase
    {
        public List<CallParameter> DrawCallParameters = new List<CallParameter>();

        public override void Prepare(double delta)
        {
            Vector2 texSize = Material.DiffuseTexture != null ? new Vector2(Material.DiffuseTexture.Width, Material.DiffuseTexture.Height) : new Vector2(1);
            DrawCallParameters.ForEach(a => a.CalcModelMatrix(texSize));
        }

        public override void Draw(double delta)
        {
            GeneralRenderer.program.Draw(Mesh, Material, DrawCallParameters);
        }
    }
}