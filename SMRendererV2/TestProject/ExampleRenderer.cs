using System;
using OpenTK;
using SMRenderer.Core.Renderer;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Object;

namespace TestProject
{
    public class ExampleRenderer : GenericRenderer
    {
        public override ShaderFileCollection VertexFiles { get; } = new ShaderFileCollection(ShaderType.VertexShader);
        public override ShaderFileCollection FragmentFiles { get; } = new ShaderFileCollection(ShaderType.FragmentShader);

        public ExampleRenderer() : base()
        {
            Console.WriteLine("stop");
        }

        public override void Draw(Matrix4 MVP, Model model, Material material)
        {
            
        }
    }
}