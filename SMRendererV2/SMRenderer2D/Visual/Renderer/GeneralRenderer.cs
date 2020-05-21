using System.Reflection;
using OpenTK;
using SMRenderer.Core.Renderer;
using SMRenderer.Core;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Object;

namespace SMRenderer2D.Visual.Renderer
{
    public class GeneralRenderer : GenericRenderer
    {
        public static GeneralRenderer program;
        public override ShaderFileCollection VertexFiles { get; } = new ShaderFileCollection(ShaderType.VertexShader)
        {
            Utility.ReadAssemblyFile(Assembly.GetExecutingAssembly(), "Visual.ShaderFiles.general.main.vert")
        };

        public override ShaderFileCollection FragmentFiles { get; } = new ShaderFileCollection(ShaderType.FragmentShader)
        {
            Utility.ReadAssemblyFile(Assembly.GetExecutingAssembly(), "Visual.ShaderFiles.general.main.frag")
        };

        public GeneralRenderer() : base()
        {
            program = this;
        }

        public override void Draw(Matrix4 MVP, Model model, Material material)
        {
           GL.UseProgram(mProgramId);

           GL.UniformMatrix4(U["MVP"], false, ref MVP);
           
           material.Texture.ApplyTo(U["Texture"], 0);
           GL.Uniform4(U["ObjectColor"], material.BaseColor);

           GL.BindVertexArray(model.VAO);
           GL.DrawArrays(model.PrimitiveType, 0, model.VertexCount);

           CleanUp();
           GL.UseProgram(0);
        }
    }
}