using OpenTK;
using SMRenderer.Base;
using SMRenderer.Base.Interfaces;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer.Core.Object;
using SMRenderer2D.Objects.Models;
using SMRenderer2D.Renderer;
using Material = SMRenderer2D.Objects.Material;

namespace SMRenderer2D.Draw
{
    public class DrawObject : IShowObject
    {
        public float RenderPosition { get; set; }

        private Matrix4 _modelMatrix;

        public Position Position = new Position(0,0);
        public Size Size = new Size(1,1);
        public Rotation Rotation = new Rotation();

        public Model Mesh = SMQuad.Object;
        public Material Material = new Material();

        public void Prepare()
        {
            _modelMatrix = (Matrix4)Size * Rotation * Position;
        }

        public void Draw()
        {
            GeneralRenderer.program.Draw(_modelMatrix * Scene.CurCam.ViewMatrix, Mesh, Material);
        }

        public void Add(SMItemCollection collection)
        { }

        public void Remove(SMItemCollection collection)
        { }
    }
}