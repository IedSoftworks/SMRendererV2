using OpenTK;
using SMRenderer.Core.Models;
using SMRenderer.Interfaces;
using SMRenderer.Models;
using SMRenderer.Types.VectorTypes;

namespace SMRenderer.Draw
{
    public class DrawBase : IShowObject
    {
        public float RenderPosition { get; set; }
        public SMItemCollection Parent { get; set; }

        public string Name;

        public Model Mesh = SMRenderer.DefaultModel;
        public Material Material = new Material();

        public virtual void Prepare(double delta)
        {
        }

        public virtual void Draw(double delta)
        {
            
        }

        public virtual bool AddTrigger(SMItemCollection collection) => true;
        public virtual bool RemoveTrigger(SMItemCollection collection) => true;
    }
}