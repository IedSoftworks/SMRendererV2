using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Data.Models;
using SM.Data.Models.Import;
using SM.Scene.Cameras;

namespace SM.Scene.Draw
{
    public class SMItemCollection : List<IShowObject>, IShowObject
    {
        private Camera _camera;
        private DepthFunction? _depthFunction = null;

        public bool Collection { get; set; } = true;
        public SMItemCollection Parent { get; set; }
        public float RenderPosition { get; set; } = 0;

        public Matrix4? Matrix = null;

        public Camera Camera
        {
            get => _camera ?? Parent.Camera;
            set => _camera = value;
        }
        public DepthFunction DepthFunc
        {
            get => _depthFunction ?? Parent.DepthFunc;
            set => _depthFunction = value;
        }

        public new void Add(IShowObject item)
        {
            if (item.AddTrigger(this))
            {
                base.Add(item);
                item.Parent = this;
            }
        }

        public new void Remove(IShowObject item)
        {
            if (item.RemoveTrigger(this))
            {
                base.Remove(item);
                item.Parent = null;
            }
        }

        public void Update(double delta)
        {
            ForEach(a => a.Update(delta));
        }

        public virtual void Prepare(double delta)
        {
            Camera.CalculateMatrix();
            foreach (IShowObject o in ToArray()) o.Prepare(delta);
        }

        public virtual void Draw(Camera camera)
        {
            ForEach(a => a.Draw(_camera ?? camera));
        }

        public bool AddTrigger(SMItemCollection collection) => this.All(a => a.AddTrigger(collection));

        public bool RemoveTrigger(SMItemCollection collection) => this.All(a => a.RemoveTrigger(collection));

        public IShowObject[] GetAllObjects()
        {
            List<IShowObject> objects = new List<IShowObject>();
            foreach (IShowObject obj in this)
            {
                if (obj.Collection) objects.AddRange(((SMItemCollection)obj).GetAllObjects());
                else objects.Add(obj);
            }

            return objects.ToArray();
        }

        public static SMItemCollection Create(Mesh[] meshes) => Create<SMItemCollection>(meshes);


        /// <summary>
        /// Creates a new SMItemCollection by meshes.
        /// <para>WARNING! This method is highly unsafe.</para>
        /// <para>Please make sure, you use a Type that can cast to SMItemCollection.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="meshes"></param>
        /// <returns></returns>
        public static T Create<T>(Mesh[] meshes) where T : SMItemCollection
        {
            SMItemCollection collection = new SMItemCollection();

            foreach (Mesh mesh in meshes)
            {
                DrawObject obj = new DrawObject()
                {
                    Mesh = mesh
                };
                if (mesh.GetType() == typeof(ImportedMesh))
                {
                    ImportedMesh impMesh = (ImportedMesh) mesh;

                    obj.Material = impMesh.ImportedMaterial;
                }
                collection.Add(obj);
            }

            return (T)collection;
        }
    }
}