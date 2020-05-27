using System.Collections.Generic;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base.Models;
using SMRenderer.Base.Models.CoreTypes;
using SMRenderer.Core;
using SMRenderer.Core.Renderer;

namespace SMRenderer.PostProcessing.Bloom
{
    public class BloomRenderer : GenericRenderer
    {
        public static BloomRenderer renderer;

        public override ShaderFileCollection VertexFiles { get; } = new ShaderFileCollection(ShaderType.VertexShader)
        {
            Utility.ReadAssemblyFile(Assembly.GetExecutingAssembly(), "Bloom.ShaderFiles.main.vert")
        };
        public override ShaderFileCollection FragmentFiles { get; } = new ShaderFileCollection(ShaderType.FragmentShader)
        {
            Utility.ReadAssemblyFile(Assembly.GetExecutingAssembly(), "Bloom.ShaderFiles.main.frag")
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
            U["MVP"].SetMatrix4(ref MVP);

            bloomTexture.ApplyTo(U["BloomTex"], 0);

            if (merge) scene.ApplyTo(U["Scene"], 1);
            U["Merge"].SetUniform1(merge);

            U["Horizontal"].SetUniform1(hoz);
            
            U["weight"].SetUniform1(BloomSettings.Weights.Length, BloomSettings.Weights);
            U["weightCount"].SetUniform1(BloomSettings.Weights.Length);
            U["tex_offset"].SetUniform2(BloomSettings.TextureOffset);
            U["bloomSizeFactor"].SetUniform1(BloomSettings.SizeFactor);
            U["multiplier"].SetUniform1(BloomSettings.Multiplier);

            Model obj = Meshes.Cube;

            GL.BindVertexArray(obj.VAO);
            GL.DrawArrays(obj.PrimitiveType, 0, obj.VertexCount);

            CleanUp();
        }
    }
}