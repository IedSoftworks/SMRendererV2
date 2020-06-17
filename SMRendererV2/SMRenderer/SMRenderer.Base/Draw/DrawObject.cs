using OpenTK;
using SMRenderer.Base.Interfaces;
using SMRenderer.Base.Models;
using SMRenderer.Base.Renderer;
using SMRenderer.Base.Types;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer.Core.Models;

namespace SMRenderer.Base.Draw
{
    public class DrawObject : ModelPostioning, IShowObject
    {
        public string Name;

        public SMItemCollection Parent { get; set; }
        public float RenderPosition { get; set; }

        private Matrix4 _modelMatrix;
        public Matrix4? ParentMatrix = null;

        public Model Mesh = Meshes.DefaultModel;
        public Material Material = new Material();

        public DrawObject(bool instantPrepare = false)
        {
            if (instantPrepare) Prepare(0);
        }

        public void Prepare(double delta)
        {
            _modelMatrix = CalculateMatrix();
            if (ParentMatrix.HasValue) _modelMatrix *= ParentMatrix.Value;
        }

        public void Draw(double delta)
        {
            GeneralRenderer.program.Draw(_modelMatrix, Mesh, Material);
        }

        public bool AddTrigger(SMItemCollection collection) => true;

        public bool RemoveTrigger(SMItemCollection collection) => true;
    }
}