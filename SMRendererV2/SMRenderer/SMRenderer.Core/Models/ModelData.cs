using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Core.Models
{
    public class ModelData : List<ModelValue>
    {
        public ModelData(BufferUsageHint bufferUsage = BufferUsageHint.StaticDraw, int pointerSize = 3, VertexAttribPointerType attribPointerType = VertexAttribPointerType.Float, bool normalised = false, int pointerStride = 0, int pointerOffset = 0)
        {
            BufferUsage = bufferUsage;
            PointerSize = pointerSize;
            AttribPointerType = attribPointerType;
            Normalised = normalised;
            PointerStride = pointerStride;
            PointerOffset = pointerOffset;
        }

        public BufferUsageHint BufferUsage;
        public int PointerSize;
        public VertexAttribPointerType AttribPointerType;
        public bool Normalised;
        public int PointerStride;
        public int PointerOffset;

        public bool HadContent = false;

        public void Add(float x = 0, float y = 0, float z = 0, float w = 0)
        {
            Add(new ModelValue(x,y,z,w));
        }

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