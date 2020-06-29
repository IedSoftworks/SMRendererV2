using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Exceptions;
using SMRenderer.Core.Renderer;

namespace SMRenderer.Core.Models
{
    public abstract class Model : IGLObject
    {
        private int _vao = -1;
        public int ID
        {
            get => _vao;
            set
            { }
        }

        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.VertexArray;

        public abstract ModelData Vertices { get; }
        public abstract ModelData UVs { get; }
        public abstract ModelData Normals { get; }
        public abstract ModelData VertexColors { get; }
        public abstract PrimitiveType PrimitiveType { get; set; }
        public abstract int BufferSizeMultiplier { get; }

        public Dictionary<string, ModelData> AttribDataIndex;

        public int VAO
        {
            get
            {
                if (_vao == -1) Compile();
                return _vao;
            }
        }
        public int VertexCount { get; private set; }
        public void Compile()
        {
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);

            if (AttribDataIndex == null)
                throw new ModelCompileException("[General] The model requires a attribute data index.");

            foreach (KeyValuePair<string, ModelData> pair in AttribDataIndex)
            {
                if (!GenericRenderer.AttribIDs.ContainsKey(pair.Key)) 
                    throw new ModelCompileException("[General] The attribute '"+pair.Key+"' doesn't exist. \nIf you want to use it, please add it to GenericRenderer.AttribIDs.");

                ModelData data = pair.Value;

                data.HadContent = data.Count > 0;
                if (data.HadContent) data.CompileProcess(this, pair.Key, GenericRenderer.AttribIDs[pair.Key]);
            }

            GL.BindVertexArray(0);
            VertexCount = Vertices.Count;
        }


        public virtual void Normalize()
        {

        }
    }
}