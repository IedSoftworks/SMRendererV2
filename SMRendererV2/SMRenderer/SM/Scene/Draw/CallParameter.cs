using System.Drawing.Drawing2D;
using OpenTK;
using SM.Core.Models;
using SM.Data.Types;
using SM.Data.Types.Extensions;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Draw
{
    public class CallParameter
    {
        private Vector _position = new Vector();
        private Vector _size = new Vector();
        private Vector _rotation = new Vector();

        private Vector _textureOffset = new Vector();
        private Vector _textureSize = new Vector(-1);

        public bool RequireUpdate { get; private set; } = true;

        public virtual Vector Position
        {
            get => _position;
            set
            {
                UpdateEvents(_position, value);
                _position = value;
            }
        }
        public virtual Vector Size {
            get => _size;
            set
            {
                UpdateEvents(_size, value);
                _size = value;
            }
        }

        public virtual Vector Rotation
        {
            get => _rotation;
            set
            {
                UpdateEvents(_rotation, value);
                _rotation = value;
            }
        }

        public virtual Vector TextureOffset
        {
            get => _textureOffset;
            set
            {
                UpdateEvents(_textureOffset, value);
                _textureOffset = value;
            }
        }
        public virtual Vector TextureSize
        {
            get => _textureSize;
            set
            {
                UpdateEvents(_textureSize, value);
                _textureSize = value;
            }
        }

        public Matrix4 ModelMatrix;
        public Vector2 TextureOffsetNormal { get; private set; }
        public Vector2 TextureSizeNormal { get; private set; }

        public CallParameter()
        {
            _position.Change += UpdateEvent;
            _size.Change += UpdateEvent;
            _rotation.Change += UpdateEvent;
            
            _textureOffset.Change += UpdateEvent;
            _textureSize.Change += UpdateEvent;
        }

        internal void CalcModelMatrix(Vector2 textureSize)
        {
            if (!RequireUpdate) return;
            ModelMatrix = MatrixCalc.CreateModelMatrix(_size, _rotation, _position);

            TextureSizeNormal = (_textureSize.X < 0 || _textureSize.Y < 0) ? new Vector2(1) : Vector2.Divide(_textureSize, textureSize);
            TextureOffsetNormal = Vector2.Divide(_textureOffset, textureSize);

            RequireUpdate = false;
        }

        private void UpdateEvents(Vector old, Vector newVal)
        {
            old.Change -= UpdateEvent;
            newVal.Change += UpdateEvent;

            RequireUpdate = true;
        }

        private void UpdateEvent(Vector vector, int id, float old, float newVal)
        {
            RequireUpdate = true;
        }
    }
}