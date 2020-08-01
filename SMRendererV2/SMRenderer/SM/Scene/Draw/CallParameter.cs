using OpenTK;
using SM.Core.Models;
using SM.Data.Types;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Draw
{
    public class CallParameter
    {
        private Position _position = new Position();
        private Size _size = new Size();
        private Rotation _rotation = new Rotation();

        private Position _textureOffset = new Position();
        private Size _textureSize = new Size(-1);

        public bool RequireUpdate { get; private set; } = true;

        public virtual Position Position
        {
            get => _position;
            set
            {
                UpdateEvents(_position, value);
                _position = value;
            }
        }
        public virtual Size Size {
            get => _size;
            set
            {
                UpdateEvents(_size, value);
                _size = value;
            }
        }

        public virtual Rotation Rotation
        {
            get => _rotation;
            set
            {
                UpdateEvents(_rotation, value);
                _rotation = value;
            }
        }

        public virtual Position TextureOffset
        {
            get => _textureOffset;
            set
            {
                UpdateEvents(_textureOffset, value);
                _textureOffset = value;
            }
        }
        public virtual Size TextureSize
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
            ModelMatrix = (Matrix4) _size * _rotation * _position;

            TextureSizeNormal = (_textureSize.X < 0 || _textureSize.Y < 0) ? new Vector2(1) : Vector2.Divide(_textureSize, textureSize);
            TextureOffsetNormal = Vector2.Divide(_textureOffset, textureSize);

            RequireUpdate = false;
        }

        private void UpdateEvents(VectorType old, VectorType newVal)
        {
            old.Change -= UpdateEvent;
            newVal.Change += UpdateEvent;

            RequireUpdate = true;
        }

        private void UpdateEvent(VectorType vector, int id, float old, float newVal)
        {
            RequireUpdate = true;
        }
    }
}