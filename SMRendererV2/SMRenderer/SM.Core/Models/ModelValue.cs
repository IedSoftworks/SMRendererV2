using System;

namespace SM.Core.Models
{
    /// <summary>
    /// Stores the data that is required for a model
    /// <para>Example: Positions, Colors, etc.</para>
    /// </summary>
    [Serializable]
    public struct ModelValue
    {
        /// <summary>
        /// The X-component
        /// </summary>
        public float X;
        /// <summary>
        /// The Y-component
        /// </summary>
        public float Y;
        /// <summary>
        /// The Z-component
        /// </summary>
        public float Z;
        /// <summary>
        /// The W-component
        /// </summary>
        public float W;

        /// <summary>
        /// Create a one value-data.
        /// <para>Y, Z, W is set to 0</para>
        /// </summary>
        /// <param name="x">The X-component</param>
        public ModelValue(float x) : this(x, 0) { }

        /// <summary>
        /// Create a two value-data.
        /// <para>Z, W is set to 0</para>
        /// </summary>
        /// <param name="x">The X-component</param>
        /// <param name="y">The Y-component</param>
        public ModelValue(float x, float y) : this(x, y, 0) { }

        /// <summary>
        /// Create a three value-data.
        /// <para>W is set to 0</para>
        /// </summary>
        /// <param name="x">The X-component</param>
        /// <param name="y">The Y-component</param>
        /// <param name="z">The Z-component</param>
        public ModelValue(float x, float y, float z): this(x, y, z,0) { }
        /// <summary>
        /// Create a four value-data.
        /// </summary>
        /// <param name="x">The X-component</param>
        /// <param name="y">The Y-component</param>
        /// <param name="z">The Z-component</param>
        /// <param name="w">The W-component</param>
        public ModelValue(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Returns the value based on a index.
        /// </summary>
        /// <param name="i">Index between 0-3</param>
        /// <returns>The value that is represented by the index</returns>
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

        /// <inheritdoc />
        public override string ToString() => $"X:{X}, Y:{Y}, Z:{Z}, W:{W}";
    }
}