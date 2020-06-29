using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Models;
using SMRenderer.Core.Renderer;
using SMRenderer.Renderer;
using SMRenderer.Scene.Cameras;
using SMRenderer.Scene.Lights;
using SMRenderer.Types.Extensions;
using SMRenderer.Utility;

namespace SMRenderer.Shaders
{
    public static class ShaderCatalog
    {
        public static ShaderFileCollection MainVertex { get; set; } = new ShaderFileCollection(ShaderType.VertexShader)
        {
            new ShaderFile(ShaderType.VertexShader, AssemblyUtility.ReadAssemblyFile("Shaders.MainShader.main.vert"), false) {
                SourceExt = new ShaderSourceExt() {{ "maximalCallParameters", SMRenderer.MAX_DRAW_PARAMETER.ToString() }}
            },
        };
        public static ShaderFileCollection MainFragment { get; set; } = new ShaderFileCollection(ShaderType.FragmentShader)
        {
            { AssemblyUtility.ReadAssemblyFile("Shaders.MainShader.main.frag"), false },
            new ShaderFile(ShaderType.FragmentShader, AssemblyUtility.ReadAssemblyFile("Shaders.MainShader.light.lighting.frag"), false) {
                SourceExt = new ShaderSourceExt() {{ "maxLights", SMRenderer.MAX_LIGHTS.ToString() }}
            },
        };

        public static void SetMainVertexUniforms(UniformCollection uniforms, ref Matrix4 modelMatrix, Model model)
        {
            Matrix4 world = Camera.CurrentWorld();
            Matrix4 view = Scene.Scene.CurrentCam.ViewMatrix;
            Matrix4 normal = Matrix4.Transpose(Matrix4.Invert(modelMatrix));

            uniforms["projection"]?.SetMatrix4(ref world);
            uniforms["view"]?.SetMatrix4(ref view);
            uniforms["model"]?.SetMatrix4(ref modelMatrix);
            uniforms["normal"]?.SetMatrix4(ref normal);

            uniforms["HasColors"]?.SetUniform1(model.VertexColors.HadContent);
        }

        public static void SetMainFragmentUniforms(UniformCollection u, Material material)
        {
            bool useTex = material.DiffuseTexture != null;
            if (useTex)
                u["DiffuseTexture"]?.SetTexture(material.DiffuseTexture, 0);
            u["material.UseTexture"]?.SetUniform1(useTex);
            u["material.ObjectColor"]?.SetUniform4(material.DiffuseColor);

            Lights lights = Scene.Scene.Current.Lights;
            u["AmbientLight"]?.SetUniform4((Vector4)lights.Ambient);
            u["UsedLights"]?.SetUniform1(lights.Count);

        }
    }
}