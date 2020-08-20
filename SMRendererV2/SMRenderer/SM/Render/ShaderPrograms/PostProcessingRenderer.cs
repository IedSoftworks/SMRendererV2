using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Renderer;
using SM.Core.Renderer.Framebuffers;
using SM.Core.Renderer.Shaders;
using SM.Data.Models;
using SM.PostProcessing;
using SM.Utility;

namespace SM.Render.ShaderPrograms
{
    public class PostProcessingRenderer : GenericRenderer
    {
        public static ShaderFile MergeVertex = new ShaderFile(AssemblyUtility.ReadAssemblyFile("Render.ShaderFiles.PostProcessing.postProcessing.vert"));
        public static ShaderFile MergeFragment = new ShaderFile(AssemblyUtility.ReadAssemblyFile("Render.ShaderFiles.PostProcessing.postProcessing.frag"));
        public static Matrix4 MVP;

        public override ShaderFile VertexFiles { get; } = MergeVertex;
        public override ShaderFile FragmentFiles { get; } = MergeFragment;

        public void Draw(ColorAttachment scene)
        {
            GL.UseProgram(this);

            Uniforms["MVP"]?.SetMatrix4(ref MVP);
            Uniforms["Scene"]?.SetTexture(scene);

            PostProcessManager.PostProcesses.ForEach(a => a.SetUniforms(U));

            GL.BindVertexArray(Meshes.Plane.ID);
            GL.DrawArrays(Meshes.Plane.PrimitiveType, 0, Meshes.Plane.Vertices.Count);

            CleanUp();
            GL.UseProgram(0);
        }
    }
}