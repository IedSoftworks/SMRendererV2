using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Interfaces;
using SMRenderer.Models;
using SMRenderer.Models.Import;

namespace SMRenderer.Draw
{
    public class SMItemCollection : List<IShowObject>, IShowObject
    {
        public SMItemCollection Parent { get; set; }
        public float RenderPosition { get; set; } = 0;
        public virtual bool DepthTest { get; set; } = true;

        public Matrix4? Matrix = null;

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
            }
        }

        public virtual void Prepare(double delta)
        {
            foreach (IShowObject o in ToArray())
            {
                o.Prepare(delta);
            }
        }

        public virtual void Draw(double delta)
        {
            if (DepthTest) GL.Enable(EnableCap.DepthTest);
            else GL.Disable(EnableCap.DepthTest);

            ForEach(a => a.Draw(delta));
        }

        public bool AddTrigger(SMItemCollection collection) => this.All(a => a.AddTrigger(collection));

        public bool RemoveTrigger(SMItemCollection collection) => this.All(a => a.RemoveTrigger(collection));

        public static SMItemCollection Create(Mesh[] meshes) => Create<SMItemCollection>(meshes);


        /// <summary>
        /// Creates a new SMItemCollection by meshes.
        /// <para>WARNING! This method is highly unsafe.</para>
        /// <para>Please make sure, you use a Type that can cast to SMItemCollection.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="meshes"></param>
        /// <returns></returns>
        public static T Create<T>(Mesh[] meshes)
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

            return Unsafe.As<SMItemCollection, T>(ref collection);
        }
    }
}