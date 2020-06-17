using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base.Models;
using SMRenderer.Base.Scene;
using SMRenderer.Base.Scene.Cameras;
using SMRenderer.Base.Shaders;
using SMRenderer.Core.Models;
using SMRenderer.Core.Renderer;

namespace SMRenderer.Base.Renderer
{
    public class GeneralRenderer : GenericRenderer
    {
        public static GeneralRenderer program;
        public override ShaderFileCollection VertexFiles { get; } = new ShaderFileCollection(ShaderType.VertexShader)
        {
            ShaderCatalog.MainVertex
        };

        public override ShaderFileCollection FragmentFiles { get; } = new ShaderFileCollection(ShaderType.FragmentShader)
        {
            ShaderCatalog.MainFragment,
            ShaderCatalog.Lights
        };

        public GeneralRenderer()
        {
            program = this;
        }

        public void Draw(Matrix4 modelView, Model model, Material material)
        {
            GL.UseProgram(mProgramId);

            ShaderCatalog.MainVertex.SetUniforms(U, new[] {(object)modelView});
            ShaderCatalog.MainFragment.SetUniforms(U, new[] {(object) model, material});
            Scene.Scene.CurrentLight.SetUniforms(U);

            GL.BindVertexArray(model.VAO);
            GL.DrawArrays(model.PrimitiveType, 0, model.VertexCount);

            CleanUp();
            GL.UseProgram(0);
        }
    }
}