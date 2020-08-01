using OpenTK;

namespace SM.Scene.Draw.Base
{
    public abstract class MatrixObject : DrawingBase
    {
        public abstract Matrix4 ModelMatrix { get; set; }
        public virtual bool Clickable { get; set; }
    }
}