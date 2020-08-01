using OpenTK.Graphics.OpenGL4;
using SM.Core.Models;
using SM.Data.Models;
using SM.Scene.Cameras;

namespace SM.Scene.Draw.Base
{
    public class DrawingBase : IShowObject
    {
        private DepthFunction? _depthFunction = null;

        public bool Render { get; set; } = true;

        public float RenderPosition { get; set; }
        public bool Collection { get; set; } = false;
        public SMItemCollection Parent { get; set; }


        public DepthFunction DepthFunc
        {
            get => _depthFunction ?? (Parent?.DepthFunc ?? DepthFunction.Less);
            set => _depthFunction = value;
        }

        public string Name;

        public Model Mesh = SMGlobals.DefaultModel;
        public Material Material = new Material();

        public virtual void Update(double delta)
        {
            
        }

        public virtual void Prepare(double delta)
        {
        }

        public virtual void Draw(Camera camera)
        {
            if (!Render) return;

            GL.DepthFunc(DepthFunc);
        }

        public virtual bool AddTrigger(SMItemCollection collection) => true;
        public virtual bool RemoveTrigger(SMItemCollection collection) => true;
    }
}