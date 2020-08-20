using System;
using System.Collections.Generic;
using OpenTK.Graphics;
using SM.Core;
using SM.Data.Types.VectorTypes;
using SM.DataManager;

namespace SM.Data.Models
{
    [Serializable]
    public class Material
    {
        public bool HasTexture => Texture != null;
        public bool HasSpecularTexture => SpecularTexture != null;
        public bool HasNormalMap => NormalMap != null;

        public Color Color = Color4.White;
        public Color SpecularColor = Color4.White;
        public float Shininess = 32;

        public TextureBase Texture;
        public TextureBase SpecularTexture;
        public TextureBase NormalMap;

        public bool AllowLight = true;

        public List<MaterialModifier> Modifiers = new List<MaterialModifier>();
    }
}