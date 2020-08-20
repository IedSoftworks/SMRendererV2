using System;
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

            Matrix4 mat = cam.World * cam.ViewMatrix;
            U["projectView"]?.SetMatrix4(ref mat);

            U["HasColors"]?.SetUniform1(model.VertexColors.HadContent);

            ShaderCatalog.SetMainFragmentUniforms(U, material);
            Scene.Scene.Current.Lights.SetUniforms(U);

            int modelLocation = U["model"].Value;
            int texOffsetLocation = U["TexOffset"].Value;
            int texSizeLocation = U["TexSize"].Value;
            int i = 0;
            foreach (CallParameter para in callParameters)
            {
                var currentLocationAdd = i % SMGlobals.MAX_DRAW_PARAMETER;
                if (currentLocationAdd == 0 && i != 0)
                    DrawObject(model, SMGlobals.MAX_DRAW_PARAMETER);

                Matrix4 endModelMatrix = masterMatrix.GetValueOrDefault(Matrix4.Identity) * para.ModelMatrix;
                GL.UniformMatrix4(modelLocation + currentLocationAdd, false, ref endModelMatrix);
                GL.Uniform2(texOffsetLocation + currentLocationAdd, para.TextureOffsetNormal);
                GL.Uniform2(texSizeLocation + currentLocationAdd, para.TextureSizeNormal);

                i++;
            }


            DrawObject(model, i);

            material.Modifiers.ForEach(a => a.ClearUniforms(U));
            CleanUp();
            GL.UseProgram(0);
        }
    }
}