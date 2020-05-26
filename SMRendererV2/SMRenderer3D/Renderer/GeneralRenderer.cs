using System.Collections.Generic;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core;
using SMRenderer.Core.Object;
using SMRenderer.Core.Renderer;
using Material = SMRenderer.Base.Objects.Material;

namespace SMRenderer3D.Renderer
{
    public class GeneralRenderer : GenericRenderer
    {
        public static GeneralRenderer program;
        public override ShaderFileCollection VertexFiles { get; } = new ShaderFileCollection(ShaderType.VertexShader)
        {
            Utility.ReadAssemblyFile(Assembly.GetExecutingAssembly(), "ShaderFiles.general.main.vert")
        };

        public override ShaderFileCollection FragmentFiles { get; } = new ShaderFileCollection(ShaderType.FragmentShader)
        {
            Utility.ReadAssemblyFile(Assembly.GetExecutingAssembly(), "ShaderFiles.general.main.frag")
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

        public void Draw(Matrix4 MVP, Model model, Material material)
        {
           GL.UseProgram(mProgramId);

           U["MVP"].SetMatrix4(ref MVP);

           bool useTex = material.Texture != null;
           if (useTex)
               material.Texture.ApplyTo(U["Texture"], 0);
           U["material.UseTexture"].SetUniform1(useTex);
           U["material.ObjectColor"].SetUniform4((Color4)material.BaseColor);

           GL.BindVertexArray(model.VAO);
           GL.DrawArrays(model.PrimitiveType, 0, model.VertexCount);

           CleanUp();
           GL.UseProgram(0);
        }
    }
}