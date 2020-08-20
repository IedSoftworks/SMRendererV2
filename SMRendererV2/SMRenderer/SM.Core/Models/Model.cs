using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Exceptions;
using SM.Core.Renderer;

namespace SM.Core.Models
{
    /// <summary>
    /// Represents a 3D models in OpenGL
    /// </summary>
    [Serializable]
    public abstract class Model : IGLObject
    {
        /// <summary>
        /// This contains the attribute ids for the models.
        /// </summary>
        public static Dictionary<string, int> AttribIDs = new Dictionary<string, int>();

        /// <summary>
        /// Contains the OpenGL id for the models
        /// </summary>
        [NonSerialized] private int? _vao;

        /// <summary>
        /// Contains the most lowest point at the object bounding box.
        /// </summary>
        [NonSerialized] private Vector3 _obbMin;
        /// <summary>
        /// Contains the most highest point at the object bounding box.
        /// </summary>
        [NonSerialized] private Vector3 _obbMax;
        /// <summary>
        /// Contains the OpenGL id for the models
        /// </summary>
        public int ID
        {
            get => _vao.GetValueOrDefault(-1);
            set
            { }
        }

        /// <summary>
        /// Stores the identifier, what exactly the OpenGL object is.
        /// </summary>
        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.VertexArray;

        /// <summary>
        /// Stores the Vertices of the object
        /// </summary>
        public abstract ModelData Vertices { get; }
        /// <summary>
        /// Stores the texture coordinates of the object
        /// </summary>
        public abstract ModelData UVs { get; }
        /// <summary>
        /// Stores the normal values of the object
        /// </summary>
        public abstract ModelData Normals { get; }
        /// <summary>
        /// Stores the Vertex colors of the object
        /// </summary>
        public abstract ModelData VertexColors { get; }
        /// <summary>
        /// Sets the primitive type of the object
        /// </summary>
        public abstract PrimitiveType PrimitiveType { get; set; }
        /// <summary>
        /// Stores a buffer size multiplier
        /// </summary>
        public abstract int BufferSizeMultiplier { get; }

        /// <summary>
        /// Contains the most lowest point at the object bounding box.
        /// </summary>
        public Vector3 OBB_Min => _obbMin;
        /// <summary>
        /// Contains the most highest point at the object bounding box.
        /// </summary>
        public Vector3 OBB_Max => _obbMax;

        /// <summary>
        /// Connects the data with the attributes 
        /// </summary>
        public Dictionary<string, ModelData> AttribDataIndex;

        /// <summary>
        /// Contains the id for the Vertex Array Object.
        /// <para>It automaticly compiles the object, if the id doesn't exist.</para>
        /// </summary>
        public int VAO
        {
            get
            {
                if (!_vao.HasValue) Compile();
                return _vao.Value;
            }
        }
        /// <summary>
        /// Compiles the object to the VAO.
        /// </summary>
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
                if (data.HadContent) data.CompileProcess(this, pair.Key, AttribIDs[pair.Key]);
            }

            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Calculates the bounding box
        /// </summary>
        public void CalculateBoundingBox()
        {
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
        }
    }
}