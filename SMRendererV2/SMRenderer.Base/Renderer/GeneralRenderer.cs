using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base.Models;
using SMRenderer.Base.Models.CoreTypes;
using SMRenderer.Base.Scene;
using SMRenderer.Base.Shaders;
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

        public override Dictionary<string, int> CustomFragData { get; } = new Dictionary<string, int>
        {
            {"color", 0 },
            {"bloom", 1 }
        };

        public GeneralRenderer()
        {
            program = this;
        }

        public void Draw(Matrix4 modelView, Model model, Material material)
        {
           GL.UseProgram(mProgramId);

           U["projection"].SetMatrix4(ref Camera.WorldMatrix);
           U["view"].SetMatrix4(ref Scene.Scene.CurrentCam.ViewMatrix);
           U["model"].SetMatrix4(ref modelView);

           bool useTex = material.Texture != null;
           if (useTex)
               material.Texture.ApplyTo(U["Texture"], 0);
           U["material.UseTexture"].SetUniform1(useTex);
           U["material.ObjectColor"].SetUniform4(material.BaseColor);
               
           Scene.Scene.CurrentLight.SetUniforms(U);

           GL.BindVertexArray(model.VAO);
           GL.DrawArrays(model.PrimitiveType, 0, model.VertexCount);

           CleanUp();
           GL.UseProgram(0);
        }
    }
}