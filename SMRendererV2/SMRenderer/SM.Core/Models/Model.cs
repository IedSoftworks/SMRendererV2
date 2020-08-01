using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Exceptions;
using SM.Core.Renderer;

namespace SM.Core.Models
{
    [Serializable]
    public abstract class Model : IGLObject
    {
        [NonSerialized] private int? _vao;

        [NonSerialized] private Vector3 _obbMin;
        [NonSerialized] private Vector3 _obbMax;
        public int ID
        {
            get => _vao.GetValueOrDefault(-1);
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

        public Vector3 OBB_Min => _obbMin;
        public Vector3 OBB_Max => _obbMax;

        public Dictionary<string, ModelData> AttribDataIndex;

        public int VAO
        {
            get
            {
                if (!_vao.HasValue) Compile();
                return _vao.Value;
            }
        }
        public void Compile()
        {
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao.Value);

            if (AttribDataIndex == null)
                throw new ModelCompileException("[General] The model requires a attribute data index.");

            foreach (KeyValuePair<string, ModelData> pair in AttribDataIndex)
            {
                ModelData data = pair.Value;

                data.HadContent = data.Count > 0;
                if (data.HadContent) data.CompileProcess(this, pair.Key, GenericRenderer.AttribIDs[pair.Key]);
            }

            _obbMin = new Vector3(0);
            _obbMax = new Vector3(0);
            foreach (ModelValue vert in Vertices)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (vert[i] < _obbMin[i]) _obbMin[i] = vert[i];
                    if (_obbMax[i] < vert[i]) _obbMax[i] = vert[i];
                }
            }

            GL.BindVertexArray(0);
        }
    }
}