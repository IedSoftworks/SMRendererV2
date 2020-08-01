using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using OpenTK.Graphics.OpenGL4;
using SM.Core;
using SM.DataManager;

namespace SM.Data.Fonts
{
    [Serializable]
    public class Font : TextureBase
    {
        private System.Drawing.FontFamily _font;
        public Dictionary<char, CharParameter> Positions = new Dictionary<char, CharParameter>();

        public FontStyle FontStyle = FontStyle.Regular;
        public float FontSize = 12;
        public ICollection<char> CharSet = FontCharStorage.SimpleUTF8;
        public int MaxWidth;

        public Bitmap Texture { get; }

        public Font(string path)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(path);
            _font = pfc.Families[0];


            Bitmap map = new Bitmap(1000, 20);
            MaxWidth = 0;
            using (System.Drawing.Font f = new System.Drawing.Font(_font, FontSize, FontStyle))
            {
                using (Graphics g = Graphics.FromImage(map))
                {
                    g.Clear(Color.Transparent);

                    foreach (char c in CharSet)
                    {
                        string s = c.ToString();
                        SizeF size = g.MeasureString(s, f);
                        try
                        {
                            Positions.Add(c, new CharParameter(Width, (int) size.Width, (int) size.Height));
                        }
                        catch
                        {
                            // ignored
                        }

                        if (Height < size.Height) Height = (int) size.Height;
                        if (MaxWidth < size.Width) MaxWidth = (int) size.Width;
                        Width += (int) size.Width;
                    }
                }

                map = new Bitmap(Width, Height);
                using (Graphics g = Graphics.FromImage(map))
                {

                    foreach (KeyValuePair<char, CharParameter> pair in Positions)
                    {
                        g.DrawString(pair.Key.ToString(), f, Brushes.White, pair.Value.X, 0);
                    }
                }
            }

            Texture = map;
        }

        public override void Compile()
        {
            ID = Models.Texture.GenerateTexture(Texture, TextureMinFilter.Nearest, TextureWrapMode.ClampToEdge, true);
        }
    }
}