using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Models;
using SMRenderer.Core.Renderer;
using SMRenderer.Draw;
using SMRenderer.Scene.Cameras;
using SMRenderer.Shaders;

namespace SMRenderer.Renderer
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
        };

        public GeneralRenderer()
        {
            program = this;
        }

        public void Draw(Model model, Material material, List<CallParameter> callParameters)
        {
            GL.UseProgram(ID);
            GL.BindVertexArray(model.VAO);

            Matrix4 world = Camera.CurrentWorld();
            Matrix4 view = Scene.Scene.CurrentCam.ViewMatrix;

            U["projection"]?.SetMatrix4(ref world);
            U["view"]?.SetMatrix4(ref view);

            U["HasColors"]?.SetUniform1(model.VertexColors.HadContent);

            ShaderCatalog.SetMainFragmentUniforms(U, material);

            int modelLocation = U["model"];
            int texOffsetLocation = U["TexOffset"];
            int texSizeLocation = U["TexSize"];
            for (int i = 0; i < callParameters.Count; i++)
            {
                var currentLocationAdd = i % SMRenderer.MAX_DRAW_PARAMETER;
                if (currentLocationAdd == 0 && i != 0)
                    GL.DrawArrays(model.PrimitiveType, 0, model.VertexCount);

                CallParameter para = callParameters[i];
                GL.UniformMatrix4(modelLocation + currentLocationAdd, false, ref para.ModelMatrix);
                GL.Uniform2(texOffsetLocation + currentLocationAdd, para.TextureOffsetNormal);
                GL.Uniform2(texSizeLocation + currentLocationAdd, para.TextureSizeNormal);
            }

            GL.DrawArrays(model.PrimitiveType, 0, model.VertexCount);

            CleanUp();
            GL.UseProgram(0);
        }
    }
}