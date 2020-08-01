using System;
using OpenTK.Graphics;
using SM.Core;
using SM.Core.Renderer;
using SM.Data.Models;
using SM.Data.Types.Extensions;
using SM.Data.Types.VectorTypes;

namespace SM.PostProcessing.Bloom
{
    [Serializable]
    public class BloomModifier : MaterialModifier
    {
        public TextureBase _texture;
        public Color _color;
        public BloomMaterialType _type;

        public override Type RequiredWindowPlugin { get; } = typeof(BloomFeature);

        public BloomModifier(BloomMaterialType type)
        {
            _texture = default;
            _color = default;
            _type = type;
        }

        public BloomModifier(Color color)
        {
            _type = BloomMaterialType.Color;
            _color = color;
            _texture = default;
        }

        public BloomModifier(TextureBase texture)
        {
            _type = BloomMaterialType.Texture;
            _texture = texture;
            _color = Color4.White;
        }

        public BloomModifier(TextureBase texture, Color colorModifier)
        {
            _type = BloomMaterialType.Texture;
            _texture = texture;
            _color = colorModifier;
        }

        public override void SetMaterialUniforms(UniformCollection uniforms)
        {
            base.SetMaterialUniforms(uniforms);
            uniforms["BloomOptions.Type"]?.SetUniform1((int)_type);
            if (_color != default) uniforms["BloomOptions.Color"]?.SetColor(_color);
            if (_texture != default) 
                uniforms["BloomOptions.Texture"]?.SetTexture(_texture);
        }

        public override void ClearUniforms(UniformCollection uniforms)
        {
            base.ClearUniforms(uniforms);
            uniforms["BloomOptions.Type"]?.SetUniform1(0);
        }
    }
}