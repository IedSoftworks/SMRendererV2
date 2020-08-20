using System;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Renderer;
using SM.Data.Types.VectorTypes;

namespace SM.Data.Types.Extensions
{
    public static class UniformExtensions
    {
        public static void SetColor(this Uniform uniform, Color color)
        {
            uniform.SetUniform4((Color4) color);
        }

        public static void SetVector(this Uniform uniform, Vector vector, int size)
        {
            switch (size)
            {
                case 1:
                    GL.Uniform1(uniform, vector.X);
                    break;

                case 2:
                    GL.Uniform2(uniform, vector);
                    break;

                case 3:
                    GL.Uniform3(uniform, vector);
                    break;

                case 4:
                    GL.Uniform4(uniform, vector);
                    break;
            }
        }
    }
}