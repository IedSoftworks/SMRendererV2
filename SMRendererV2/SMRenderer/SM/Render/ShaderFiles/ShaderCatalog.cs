using System.Collections.Generic;
using OpenTK;
using SM.Core.Models;
using SM.Core.Renderer;
using SM.Core.Renderer.Shaders;
using SM.Data.Models;
using SM.Data.Types.Extensions;
using SM.Scene.Cameras;
using SM.Scene.Lights;
using SM.Utility;

namespace SM.Render.ShaderFiles
{
    public static class ShaderCatalog
    {
        public static ShaderFile MainVertexFile = new ShaderFile(AssemblyUtility.ReadAssemblyFile("Render.ShaderFiles.MainShader.main.vert"), new Dictionary<string, string>()
        {
            {"maximalCallParameters", SMGlobals.MAX_DRAW_PARAMETER.ToString() }
        });
        public static ShaderFile MainFragmentFile = new ShaderFile(AssemblyUtility.ReadAssemblyFile("Render.ShaderFiles.MainShader.main.frag"))
        {
            Extensions = new List<ShaderFile>()
            {
                //new ShaderFile(AssemblyUtility.ReadAssemblyFile("Render.ShaderFiles.MainShader.lighting.frag"), new Dictionary<string, string>() {{ "maxLights", SMGlobals.MAX_LIGHTS.ToString() } })
            }
        };

        public static void SetMainVertexUniforms(UniformCollection uniforms, Camera cam,  ref Matrix4 modelMatrix, Model model)
        {
            Matrix4 world = cam.World;
            Matrix4 view = cam.ViewMatrix;
            Matrix4 normal = Matrix4.Transpose(Matrix4.Invert(modelMatrix));

            uniforms["projection"]?.SetMatrix4(ref world);
            uniforms["view"]?.SetMatrix4(ref view);
            uniforms["model"]?.SetMatrix4(ref modelMatrix);
            uniforms["normal"]?.SetMatrix4(ref normal);

            uniforms["HasColors"]?.SetUniform1(model.VertexColors.HadContent);
        }

        public static void SetMainFragmentUniforms(UniformCollection u, Material material)
        {
            bool useTex = material.Texture != null;
            bool useTex2 = material.SpecularTexture != null;
            bool useTex3 = material.NormalMap != null;
            if (useTex)
                u["material.DiffuseTexture"]?.SetTexture(material.Texture);
            if (useTex2)
                u["material.SpecularTexture"]?.SetTexture(material.SpecularTexture);
            if (useTex3)
                u["material.NormalMap"]?.SetTexture(material.NormalMap);

            u["material.UseDiffuseTexture"]?.SetUniform1(useTex);
            u["material.UseSpecularTexture"]?.SetUniform1(useTex2);
            u["material.UseNormalMap"]?.SetUniform1(useTex3);
            u["material.UseLight"]?.SetUniform1(material.AllowLight);
            u["material.Diffuse"]?.SetColor(material.Color);
            u["material.Specular"]?.SetUniform3(material.SpecularColor);
            u["material.Shininess"]?.SetUniform1(material.Shininess);

            material.Modifiers.ForEach(a => a.SetMaterialUniforms(u));
        }
    }
}