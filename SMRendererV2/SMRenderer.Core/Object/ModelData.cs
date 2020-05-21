using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Core.Object
{
    public class ModelData : List<float>
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
    }
}