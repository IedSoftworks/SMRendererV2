using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Exceptions;
using SMRenderer.Core.Renderer;

namespace SMRenderer.Base.Models.CoreTypes
{
    public abstract class Model
    {
        public abstract ModelData Vertices { get; }
        public abstract ModelData UVs { get; }
        public abstract ModelData Normals { get; }
        public abstract PrimitiveType PrimitiveType { get; set; }
        public abstract int BufferSizeMultiplier { get; }

        public Dictionary<string, ModelData> AttribDataIndex;

        public int VAO { get; private set; } = -1;
        public int VertexCount { get; private set; }
        public void Compile()
        {
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            if (AttribDataIndex == null)
                throw new ModelCompileException("[General] The model requires a attribute data index.");

            foreach (KeyValuePair<string, ModelData> pair in AttribDataIndex)
            {
                if (!GenericRenderer.AttribIDs.ContainsKey(pair.Key)) 
                    throw new ModelCompileException("[General] The attribute '"+pair.Key+"' doesn't exist. \nIf you want to use it, please add it to GenericRenderer.AttribIDs.");

                CompileProcess(pair.Key, pair.Value, GenericRenderer.AttribIDs[pair.Key]);
            }

            GL.BindVertexArray(0);
            VertexCount = Vertices.Count / 3;
        }

        public virtual void CompileProcess(string attrib, ModelData data, int attribID)
        {
            int buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Count * BufferSizeMultiplier, data.ToArray(), data.BufferUsage);

            GL.VertexAttribPointer(attribID, data.PointerSize, data.AttribPointerType, data.Normalised, data.PointerStride, data.PointerOffset);
            GL.EnableVertexAttribArray(attribID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public virtual void Normalize()
        {

        }
    }
}