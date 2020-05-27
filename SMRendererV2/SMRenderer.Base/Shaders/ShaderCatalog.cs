using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core;
using SMRenderer.Core.Renderer;

namespace SMRenderer.Base.Shaders
{
    public static class ShaderCatalog
    {
        public static ShaderFile MainVertex = new ShaderFile(ShaderType.VertexShader, Utility.ReadAssemblyFile("Shaders.MainShader.main.vert"));
        public static ShaderFile MainFragment = new ShaderFile(ShaderType.FragmentShader, Utility.ReadAssemblyFile("Shaders.MainShader.main.frag"));

        public static ShaderFileCollection Lights = new ShaderFileCollection(ShaderType.FragmentShader)
        {
            {  Utility.ReadAssemblyFile("Shaders.Light.main.frag"), false },
            {  Utility.ReadAssemblyFile("Shaders.Light.phong.frag"), false },

        };
    }
}