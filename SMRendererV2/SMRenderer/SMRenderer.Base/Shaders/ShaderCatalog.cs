using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base.Scene.Cameras;
using SMRenderer.Core;
using SMRenderer.Core.Models;
using SMRenderer.Core.Renderer;

namespace SMRenderer.Base.Shaders
{
    public static class ShaderCatalog
    {
        public static ShaderFile MainVertex = new ShaderFile(ShaderType.VertexShader, Utility.ReadAssemblyFile("Shaders.MainShader.main.vert"))
        {
            Individual = false,
            SetUniforms = (uniforms, args) =>
            {
                Matrix4 world = Camera.CurrentWorld();
                Matrix4 view = Scene.Scene.CurrentCam.ViewMatrix;
                Matrix4 model = (Matrix4) args[0];

                uniforms["projection"].SetMatrix4(ref world);
                uniforms["view"].SetMatrix4(ref view);
                uniforms["model"].SetMatrix4(ref model);
            }
        };
        public static ShaderFile MainFragment = new ShaderFile(ShaderType.FragmentShader, Utility.ReadAssemblyFile("Shaders.MainShader.main.frag"))
        {
            Individual = false,
            SetUniforms = (U, objects) =>
            {
                Model model = (Model)objects[0];
                Material material = (Material)objects[1];

                bool useTex = material.DiffuseTexture != null;
                if (useTex)
                    material.DiffuseTexture.ApplyTo(U["DiffuseTexture"], 0);
                U["material.UseTexture"].SetUniform1(useTex);
                U["material.ObjectColor"].SetUniform4(material.DiffuseColor);

                U["HasColors"].SetUniform1(model.VertexColors.HadContent);

            }
        };
        public static ShaderFileCollection Lights = new ShaderFileCollection(ShaderType.FragmentShader)
        {
            {  Utility.ReadAssemblyFile("Shaders.Light.main.frag"), false },
            {  Utility.ReadAssemblyFile("Shaders.Light.phong.frag"), false },

        };
    }
}