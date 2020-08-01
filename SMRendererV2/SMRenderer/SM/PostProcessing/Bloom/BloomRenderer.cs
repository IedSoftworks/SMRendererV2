using System.Collections.Generic;
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
    internal class BloomRenderer : GenericRenderer
    {
        internal static ShaderFile VertexFile = new ShaderFile(AssemblyUtility.ReadAssemblyFile("PostProcessing.Bloom.ShaderFiles.main.vert"));

        public override ShaderFile VertexFiles { get; } = VertexFile;
            
        public override ShaderFile FragmentFiles { get; } = new ShaderFile(AssemblyUtility.ReadAssemblyFile("PostProcessing.Bloom.ShaderFiles.main.frag"));

        public override Dictionary<string, int> CustomFragData { get; } = new Dictionary<string, int>
        {
            { "color", 0 }
        };

        public void Draw(ref Matrix4 MVP, bool hoz, TextureBase bloomTexture)
        {
            U["MVP"]?.SetMatrix4(ref MVP);

            U["BloomTex"]?.SetTexture(bloomTexture, 0);

            U["Horizontal"]?.SetUniform1(hoz);
            
            U["weight"]?.SetUniform1(BloomSettings.Weights.Length, BloomSettings.Weights);
            U["weightCount"]?.SetUniform1(BloomSettings.Weights.Length);
            U["tex_offset"]?.SetUniform2(BloomSettings.TextureOffset);
            U["bloomSizeFactor"]?.SetUniform1(BloomSettings.SizeFactor);
            U["multiplier"]?.SetUniform1(BloomSettings.Multiplier);

            Model obj = Meshes.Plane;

            GL.BindVertexArray(obj.VAO);
            GL.DrawArrays(obj.PrimitiveType, 0, obj.Vertices.Count);

            CleanUp();
        }
    }
}