using System;
using System.Drawing;
using System.Drawing.Imaging;
using SM.Core;
using OpenTK.Graphics.OpenGL4;

namespace SM.Data.Models
{
    [Serializable]
    public class Texture : TextureBase
    {
        public Bitmap Map;
        public TextureMinFilter TextureFilter;
        public TextureWrapMode WrapMode;
        public bool AutoDispose = false;

        public Texture(string path) : this(path, TextureMinFilter.Linear, TextureWrapMode.ClampToEdge, true) {} 
        public Texture(string path, TextureMinFilter textureFilter, TextureWrapMode wrapMode, bool autoDispose = false) : this(new Bitmap(path), textureFilter, wrapMode, autoDispose) {}
        public Texture(Bitmap bitmap, TextureMinFilter textureFilter, TextureWrapMode wrapMode, bool autoDispose = false)
        {
            Map = bitmap;
            TextureFilter = textureFilter;
            WrapMode = wrapMode;
            AutoDispose = autoDispose;
        }

        public override void Compile()
        {
            Width = Map.Width;
            Height = Map.Height;
            ID = GenerateTexture(Map, TextureFilter, WrapMode, AutoDispose);
        }

        public static int GenerateTexture(Bitmap map, TextureMinFilter textureFilter, TextureWrapMode wrapMode, bool autoDispose = false)
        {
            int id = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, id);

            bool transparecy = map.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb;

            BitmapData data = map.LockBits(new Rectangle(0, 0, map.Width, map.Height),
                ImageLockMode.ReadOnly,
                map.PixelFormat);

            GL.TexImage2D(TextureTarget.Texture2D, 0,
                transparecy
                    ? PixelInternalFormat.Rgba
                    : PixelInternalFormat.Rgb,
                data.Width, data.Height, 0,
                transparecy ? OpenTK.Graphics.OpenGL4.PixelFormat.Bgra : OpenTK.Graphics.OpenGL4.PixelFormat.Bgr,
                PixelType.UnsignedByte,
                data.Scan0);


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)textureFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)textureFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)wrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)wrapMode);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            map.UnlockBits(data);
            if (autoDispose) map.Dispose();

            return id;
        }
    }
}