using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Renderer;
using SM.Data.Fonts;
using SM.Data.Models;
using SM.Data.Types;
using SM.Data.Types.VectorTypes;
using SM.Render.ShaderPrograms;
using SM.Scene.Cameras;
using SM.Scene.Draw.Base;

namespace SM.Scene.Draw
{
    public class DrawText : ModelPositioning
    {
        private ICollection<CallParameter> _parameters;
        private readonly Mesh _mesh = Meshes.Plane;
        private string _text;

        public Font Font
        {
            get => (Font)Material.Texture;
            set => Material.Texture = value;
        }

        public Color FontColor
        {
            get => Material.Color;
            set => Material.Color = value;
        }

        public float FontSize
        {
            get => Size.Y;
            set => Size.X = Size.Y = value;
        }

        public float Width;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                Update();
            }
        }

        public DrawText(Font font, string text)
        {
            Font = font;
            Text = text;
        }

        public override void Prepare(double delta)
        {
            Vector2 texSize = Material.Texture != null ? new Vector2(Material.Texture.Width, Material.Texture.Height) : new Vector2(1);
            foreach(CallParameter cp in _parameters) cp.CalcModelMatrix(texSize);

            ModelMatrix = Matrix4.CreateTranslation(-Width / 2, 0, 0);
            if (Parent.Camera.Orth) ModelMatrix *= Matrix4.CreateRotationX(180);
            ModelMatrix *= CalcMatrix();
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);
            RenderProgramCollection.General.Draw(camera, _mesh, Material, _parameters, ModelMatrix);
        }

        public void Update()
        {
            _parameters = new List<CallParameter>();
            _text = _text.Trim();

            float x = 0;
            for (int i = 0; i < _text.Length; i++)
            {
                char c = _text[i];

                if (c == ' ')
                {
                    x += 1;
                    continue;
                }

                if (!Font.Positions.ContainsKey(c)) continue;
                CharParameter charParameter = Font.Positions[c];

                float aspect = 1 / charParameter.Size.Y;
                float width = charParameter.Size.X * aspect;

                if (i == 0) x = width / 2;

                CallParameter cp = new CallParameter()
                {
                    Position = new Vector(x, 0),
                    Size = new Vector(width, 1),
                    TextureSize = new Vector(charParameter.Size.X, charParameter.Size.Y),
                    TextureOffset = new Vector(charParameter.X, 0)
                };

                _parameters.Add(cp);
                x += width;
            }

            Width = x;
        }

        private void ProcessChar(char c)
        {

        }
    }
}