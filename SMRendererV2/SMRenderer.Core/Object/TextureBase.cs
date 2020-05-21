using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Core.Object
{
    public class TextureBase
    {
        public int TexID = -1;

        public void ApplyTo(int uniformID, int textureID)
        {
            GL.ActiveTexture((TextureUnit)(33984 + textureID));
            GL.BindTexture(TextureTarget.Texture2D, TexID);
            GL.Uniform1(uniformID, textureID);
        }
    }
}