using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Core;
using SM.Core.Models;
using SM.Core.Renderer;
using SM.Core.Renderer.Shaders;
using SM.Data.Models;
using SM.Utility;

namespace SM.PostProcessing.Bloom
{
    internal class BloomMergeRenderer : GenericRenderer
    {
        public override ShaderFile VertexFiles { get; } = new ShaderFile(AssemblyUtility.ReadAssemblyFile("PostProcessing.Bloom.ShaderFiles.main.vert"));
        public override ShaderFile FragmentFiles { get; } = new ShaderFile(AssemblyUtility.ReadAssemblyFile("PostProcessing.Bloom.ShaderFiles.merge.frag"));

        public void Draw(ref Matrix4 mvp, TextureBase scene, TextureBase bloom)
        {
            GL.UseProgram(ID);

            Uniforms["MVP"]?.SetMatrix4(ref mvp);

            U["SceneTexture"]?.SetTexture(scene, 0);
            U["BloomTexture"]?.SetTexture(bloom, 1);

            U["Percentage"]?.SetUniform1(BloomSettings.BloomTexturePercentage);

            Model obj = Meshes.Plane;

            GL.BindVertexArray(obj.VAO);
            GL.DrawArrays(obj.PrimitiveType, 0, obj.Vertices.Count);

            CleanUp();
        }
    }
}