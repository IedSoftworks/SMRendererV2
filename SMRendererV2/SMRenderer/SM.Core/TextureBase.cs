using System;
using OpenTK.Graphics.OpenGL4;

namespace SM.Core
{
    /// <summary>
    /// Represents a texture basis
    /// </summary>
    [Serializable]
    public class TextureBase : IGLObject
    {
        [NonSerialized] private int _id = -1;

        /// <inheritdoc />
        public int ID
        {
            get
            {
                if (_id == -1) Compile();
                return _id;
            }
            set => _id = value;
        }

        /// <inheritdoc />
        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.Texture;

        /// <summary>
        /// Height of the texture
        /// </summary>
        public int Height;
        /// <summary>
        /// Width of the texture
        /// </summary>
        public int Width;

        /// <summary>
        /// The compile code.
        /// </summary>
        public virtual void Compile()
        {

        }
    }
}