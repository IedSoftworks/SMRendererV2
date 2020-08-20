using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Models;
using SM.Core.Renderer;
using SM.Core.Renderer.Shaders;
using SM.Data.Models;
using SM.Render.ShaderFiles;
using SM.Scene.Cameras;
using SM.Scene.Draw;

namespace SM.Render.ShaderPrograms
{
    public class GeneralRenderer : GenericRenderer
    {
        public override ShaderFile VertexFiles { get; } = ShaderCatalog.MainVertexFile;

        public override ShaderFile FragmentFiles { get; } = ShaderCatalog.MainFragmentFile;

        public override Dictionary<string, int> FragData { get; } = new Dictionary<string, int>()
        {
            {"color", 0}
        };

        public GeneralRenderer()
        {
            RenderProgramCollection.General = this;
        }

        public void Draw(Camera cam, Model model, Material material, ICollection<CallParameter> callParameters, Matrix4? masterMatrix = null)
        {
            GL.UseProgram(ID);
            GL.BindVertexArray(model.VAO);

            Matrix4 world = cam.World;
            Matrix4 view = cam.ViewMatrix;

            U["projection"]?.SetMatrix4(ref world);
            U["view"]?.SetMatrix4(ref view);

            U["HasColors"]?.SetUniform1(model.VertexColors.HadContent);
            U["HasMasterMatrix"]?.SetUniform1(masterMatrix.HasValue);
            if (masterMatrix.HasValue)
            {
                Matrix4 master = masterMatrix.Value;
                U["masterMatrix"]?.SetMatrix4(ref master);
            }

            ShaderCatalog.SetMainFragmentUniforms(U, material);
            Scene.Scene.Current.Lights.SetUniforms(U);

            Matrix4 mat = world * view;
            int modelLocation = U["model"].Value;
            int texOffsetLocation = U["TexOffset"].Value;
            int texSizeLocation = U["TexSize"].Value;
            int i = 0;
            foreach (CallParameter para in callParameters)
            {
                var currentLocationAdd = i % SMGlobals.MAX_DRAW_PARAMETER;
                if (currentLocationAdd == 0 && i != 0)
                    GL.DrawArraysInstanced(model.PrimitiveType, 0, model.Vertices.Count, SMGlobals.MAX_DRAW_PARAMETER);

                GL.UniformMatrix4(modelLocation + currentLocationAdd, false, ref para.ModelMatrix);
                GL.Uniform2(texOffsetLocation + currentLocationAdd, para.TextureOffsetNormal);
                GL.Uniform2(texSizeLocation + currentLocationAdd, para.TextureSizeNormal);

                i++;
            }


            GL.DrawArraysInstanced(model.PrimitiveType, 0, model.Vertices.Count, i);

            material.Modifiers.ForEach(a => a.ClearUniforms(U));
            CleanUp();
            GL.UseProgram(0);
        }
    }
}