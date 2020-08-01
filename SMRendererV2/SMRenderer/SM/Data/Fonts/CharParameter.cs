using System;
using OpenTK;

namespace SM.Data.Fonts
{
    [Serializable]
    public struct CharParameter
    {
        public int X;
        public Vector2 Size;

        public CharParameter(int x, int width, int height)
        {
            X = x;
            Size = new Vector2(width, height);
        }
    }
}