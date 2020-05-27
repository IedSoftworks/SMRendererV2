using OpenTK;
using SMRenderer.Base.Interfaces;
using SMRenderer.Base.Models;
using SMRenderer.Base.Models.CoreTypes;
using SMRenderer.Base.Renderer;
using SMRenderer.Base.Types.VectorTypes;

namespace SMRenderer.Base.Draw
{
    public class DrawObject : IShowObject
    {
        public static Model DefaultModel = Meshes.Cube;

        public float RenderPosition { get; set; }

        private Matrix4 _modelMatrix;

        public Position Position = new Position(0,0);
        public Size Size = new Size(1,1);
        public Rotation Rotation = new Rotation();

        public Model Mesh = DefaultModel;
        public Material Material = new Material();

        public void Prepare()
        {
            _modelMatrix = (Matrix4)Size * Rotation * Position;
        }

        public void Draw()
        {
            GeneralRenderer.program.Draw(_modelMatrix, Mesh, Material);
        }

        public void Add(SMItemCollection collection)
        { }

        public void Remove(SMItemCollection collection)
        { }
    }
}