using System;
using OpenTK.Graphics.OpenGL4;

namespace SM.Core
{
    [Serializable]
    public class TextureBase : IGLObject
    {
        [NonSerialized] private int _id = -1;

        public int ID
        {
            get
            {
                if (_id == -1) Compile();
                return _id;
            }
            set => _id = value;
        }
        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.Texture;

        public int Height;
        public int Width;

        public virtual void Compile()
        {

        }
    }
}