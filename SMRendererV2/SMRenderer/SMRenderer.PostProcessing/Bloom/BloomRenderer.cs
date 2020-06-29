using System.Collections.Generic;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core;
using SMRenderer.Core.Models;
using SMRenderer.Core.Renderer;
using SMRenderer.Models;
using SMRenderer.Utility;


namespace SMRenderer.PostProcessing.Bloom
{
    public class BloomRenderer : GenericRenderer
    {
        public static BloomRenderer renderer;

        public override ShaderFileCollection VertexFiles { get; } = new ShaderFileCollection(ShaderType.VertexShader)
        {
            AssemblyUtility.ReadAssemblyFile("Bloom.ShaderFiles.main.vert")
        };
        public override ShaderFileCollection FragmentFiles { get; } = new ShaderFileCollection(ShaderType.FragmentShader)
        {
            AssemblyUtility.ReadAssemblyFile("Bloom.ShaderFiles.main.frag")
        };

        public override Dictionary<string, int> CustomFragData { get; } = new Dictionary<string, int>
        {
            { "color", 0 }
        };

        public BloomRenderer()
        {
            renderer = this;
        }

        public void Draw(ref Matrix4 MVP,
            bool hoz, bool merge,
            int width, int height, 
            TextureBase scene, TextureBase bloomTexture)
        {
            U["MVP"]?.SetMatrix4(ref MVP);

            U["BloomTex"]?.SetTexture(bloomTexture, 0);

            if (merge) U["Scene"]?.SetTexture(scene, 1);
            U["Merge"]?.SetUniform1(merge);

            U["Horizontal"]?.SetUniform1(hoz);
            
            U["weight"]?.SetUniform1(BloomSettings.Weights.Length, BloomSettings.Weights);
            U["weightCount"]?.SetUniform1(BloomSettings.Weights.Length);
            U["tex_offset"]?.SetUniform2(BloomSettings.TextureOffset);
            U["bloomSizeFactor"]?.SetUniform1(BloomSettings.SizeFactor);
            U["multiplier"]?.SetUniform1(BloomSettings.Multiplier);

            Model obj = Meshes.Plane;

            GL.BindVertexArray(obj.VAO);
            GL.DrawArrays(obj.PrimitiveType, 0, obj.VertexCount);

            CleanUp();
        }
    }
}