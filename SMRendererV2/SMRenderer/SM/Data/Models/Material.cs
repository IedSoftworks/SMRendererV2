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
        public bool HasDiffuseTexture => DiffuseTexture != null;
        public bool HasSpecularTexture => SpecularTexture != null;
        public bool HasNormalMap => NormalMap != null;

        public Color DiffuseColor = Color4.White;
        public Color SpecularColor = Color4.White;
        public float Shininess = 32;

        public TextureBase DiffuseTexture;
        public TextureBase SpecularTexture;
        public TextureBase NormalMap;

        public bool AllowLight = true;

        public List<MaterialModifier> Modifiers = new List<MaterialModifier>();
    }
}