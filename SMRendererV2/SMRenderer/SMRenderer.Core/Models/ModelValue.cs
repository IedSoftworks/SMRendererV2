namespace SMRenderer.Core.Models
{
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
                if (i == 0) return X;
                if (i == 1) return Y;
                if (i == 2) return Z;
                else return W;
            }
        }

        public override string ToString() => $"X:{X}, Y:{Y}, Z:{Z}, W:{W}";
    }
}