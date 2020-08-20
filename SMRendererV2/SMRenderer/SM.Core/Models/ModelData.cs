using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SM.Core.Models
{

    /// <summary>
    /// Controls all model data that is needed for a model
    /// </summary>
    [Serializable]
    public class ModelData : List<ModelValue>
    {
        /// <summary>
        /// Creates new model data
        /// </summary>
        public ModelData(BufferUsageHint bufferUsage = BufferUsageHint.StaticDraw, int pointerSize = 3, VertexAttribPointerType attribPointerType = VertexAttribPointerType.Float, bool normalised = false, int pointerStride = 0, int pointerOffset = 0)
        {
            BufferUsage = bufferUsage;
            PointerSize = pointerSize;
            AttribPointerType = attribPointerType;
            Normalised = normalised;
            PointerStride = pointerStride;
            PointerOffset = pointerOffset;
        }
        /// <summary>
        /// This gives the GPU a hint how the data is used.
        /// </summary>
        public BufferUsageHint BufferUsage;
        /// <summary>
        /// This sets the pointer size.
        /// <para>Normally it represents how many float are in one value.</para>
        /// </summary>
        public int PointerSize;
        /// <summary>
        /// Specifys the type of the pointer.
        /// </summary>
        public VertexAttribPointerType AttribPointerType;
        /// <summary>
        /// Specifies, if the compiler should normalize those values first.
        /// </summary>
        public bool Normalised;
        /// <summary>
        /// Specifies the byte offset between consecutive generic vertex attributes.
        /// <para>If stride is 0, the generic vertex attributes are understood to be tightly packed in the array.</para>
        /// </summary>
        public int PointerStride;
        /// <summary>
        /// Specifies a offset of the first component.
        /// </summary>
        public int PointerOffset;

        /// <summary>
        /// This is true, if the model had this kind of data.
        /// </summary>
        public bool HadContent = false;

        /// <summary>
        /// This add a new value.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        /// <param name="w">W</param>
        public void Add(float x = 0, float y = 0, float z = 0, float w = 0)
        {
            Add(new ModelValue(x,y,z,w));
        }

        /// <summary>
        /// This transforms the values to a float array.
        /// </summary>
        /// <returns>The created float array</returns>
        public float[] ToFloats()
        {
            float[] values = new float[PointerSize * Count];
            int c = 0;
            for (int i = 0; i < Count; i++)
            {
                for (int i2 = 0; i2 < PointerSize; i2++, c++) values[c] = this[i][i2];
            }

            return values;
        }

        /// <summary>
        /// This compiles the data.
        /// </summary>
        /// <param name="model">The model, that executed the compile process</param>
        /// <param name="attrib">The attribute name</param>
        /// <param name="attribID">The attribute id</param>
        public virtual void CompileProcess(Model model, string attrib, int attribID)
        {
            float[] data = ToFloats();

            int buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * model.BufferSizeMultiplier, data, BufferUsage);

            GL.VertexAttribPointer(attribID, PointerSize, AttribPointerType, Normalised, PointerStride, PointerOffset);
            GL.EnableVertexAttribArray(attribID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}