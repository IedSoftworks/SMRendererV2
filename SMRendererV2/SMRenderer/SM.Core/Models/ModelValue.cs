using System;

namespace SM.Core.Models
{
    [Serializable]
    public struct ModelValue
    {
        public float X; 
        public float Y; 
        public float Z; 
        public float W;

        public ModelValue(float x) : this(x, 0) { }

        public ModelValue(float x, float y) : this(x, y, 0) { }
        public ModelValue(float x, float y, float z): this(x, y, z,0) { }
        public ModelValue(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    default:
                        return W;
                }
            }
        }

        public override string ToString() => $"X:{X}, Y:{Y}, Z:{Z}, W:{W}";
    }
}