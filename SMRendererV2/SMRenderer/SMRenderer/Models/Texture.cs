using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core;

namespace SMRenderer.Models
{
    public class Texture : TextureBase
    {
        public Texture(Bitmap bitmap, TextureMinFilter textureFilter, TextureWrapMode wrapMode, bool autoDispose = false)
        {
            ID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, ID);

            bool transparecy = bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb;

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                bitmap.PixelFormat);
            GL.TexImage2D(TextureTarget.Texture2D, 0,
                transparecy
                    ? PixelInternalFormat.Rgba
                    : PixelInternalFormat.Rgb,
                data.Width, data.Height, 0,
                transparecy ? OpenTK.Graphics.OpenGL4.PixelFormat.Bgra : OpenTK.Graphics.OpenGL4.PixelFormat.Bgr,
                PixelType.UnsignedByte,
                data.Scan0);
            

            Width = bitmap.Width;
            Height = bitmap.Height;

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) textureFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) textureFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) wrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) wrapMode);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            bitmap.UnlockBits(data);
            if(autoDispose) bitmap.Dispose();
        }
    }
}